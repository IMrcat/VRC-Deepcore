using System;
using System.Net;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x0200009F RID: 159
	internal class LovenseFunctions
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00015884 File Offset: 0x00013A84
		public static void LovenseFunctionsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Lovense Functions", "Open the Lovense Functions menu", null, "#fc03e8");
			reCategoryPage.AddCategory("Functions");
			reCategoryPage.GetCategory("Functions").AddButton("Try Connect", "Try Connect.", async delegate
			{
				string text = "http://127.0.0.1:30010";
				Console.WriteLine("Checking Lovense connection...");
				if (!LovenseFunctions.CheckConnection(text))
				{
					Console.WriteLine("Lovense Connect API is not available. Please ensure Lovense Connect is running.");
				}
				else
				{
					Console.WriteLine("Fetching connected devices...");
					string deviceId = LovenseFunctions.GetDeviceId(text);
					if (string.IsNullOrEmpty(deviceId))
					{
						Console.WriteLine("No Lovense devices found.");
					}
					else
					{
						Console.WriteLine("Device found: " + deviceId);
						Console.WriteLine("Sending vibration command...");
						LovenseFunctions.SendVibrationCommand(text, deviceId, 5);
					}
				}
			}, null, "#ffffff");
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000158F8 File Offset: 0x00013AF8
		private static bool CheckConnection(string baseUrl)
		{
			bool flag;
			try
			{
				new WebClient().DownloadString(baseUrl + "/GetToys");
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00015938 File Offset: 0x00013B38
		private static string GetDeviceId(string baseUrl)
		{
			string text;
			try
			{
				new WebClient().DownloadString(baseUrl + "/GetToys");
				text = "a";
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001597C File Offset: 0x00013B7C
		private static void SendVibrationCommand(string baseUrl, string deviceId, int level)
		{
			try
			{
				WebClient webClient = new WebClient();
				string text = string.Format("{0}/Vibrate?v={1}&t={2}", baseUrl, level, deviceId);
				webClient.DownloadString(text);
				Console.WriteLine("Vibration command sent successfully!");
			}
			catch
			{
				Console.WriteLine("Failed to send vibration command.");
			}
		}
	}
}
