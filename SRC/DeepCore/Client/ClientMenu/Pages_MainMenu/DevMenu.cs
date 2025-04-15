using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.ApplicationBot;
using DeepCore.Client.Module.Exploits;
using DeepCore.Client.UI.QM;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.Udon;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A4 RID: 164
	internal class DevMenu
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x00016008 File Offset: 0x00014208
		public static void DevFunctionsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Dev Functions", null, null, "#ffffff");
			reCategoryPage.AddCategory("Functions");
			ReMenuCategory category = reCategoryPage.GetCategory("Functions");
			category.AddButton("Test\nAlertPopup", "", delegate
			{
				PopupHelper.AlertPopup("Disconnected", "You have been permenently banned from VRChat. Please\ncontect the Trust and Safety team at vrch.at/moderation", 100);
			}, null, "#ffffff");
			category.AddButton("Test\nStandardPopup", "", delegate
			{
				PopupHelper.StandardPopup("Dev Test", "Dev Test MSG", "nigga", null);
			}, null, "#ffffff");
			category.AddButton("Test\nStandardPopupV2", "", delegate
			{
				PopupHelper.StandardPopupV2("Dev Test", "Dev Test MSG", "nigga", null);
			}, null, "#ffffff");
			category.AddButton("Test\nThings", "", delegate
			{
				PopupHelper.testthings();
			}, null, "#ffffff");
			category.AddButton("Become the bot", "", delegate
			{
				SocketConnection.Client();
			}, null, "#ffffff");
			category.AddButton("Test LogConsole", "", delegate
			{
				QMConsole.LogMessage("Test", "Shit");
			}, null, "#ffffff");
			category.AddButton("Test E1", "", delegate
			{
				EarRape.Teste1();
			}, null, "#ffffff");
			category.AddButton("Test Spoofer", "", delegate
			{
				DeepConsole.LogConsole("Spoofer", "PC Name New: " + SystemInfo.deviceName);
				DeepConsole.LogConsole("Spoofer", "Model New: " + SystemInfo.deviceModel);
				DeepConsole.LogConsole("Spoofer", "PBU New: " + SystemInfo.graphicsDeviceName);
				DeepConsole.LogConsole("Spoofer", "CPU New: " + SystemInfo.processorType);
				DeepConsole.LogConsole("Spoofer", "PBU ID New: " + SystemInfo.graphicsDeviceID.ToString());
				DeepConsole.LogConsole("Spoofer", "OS New:" + SystemInfo.operatingSystem);
			}, null, "#ffffff");
			category.AddButton("Make gobj", "", delegate
			{
				GameObject gameObject = new GameObject("NewGobj");
				gameObject.AddComponent<UdonBehaviour>();
				DeepConsole.Log("Yes", "New game object created: " + gameObject.name);
			}, null, "#ffffff");
		}
	}
}
