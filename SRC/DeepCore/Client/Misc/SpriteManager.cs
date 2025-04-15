using System;
using System.IO;
using System.Net;
using ReMod.Core.Managers;
using UnityEngine;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000080 RID: 128
	internal class SpriteManager
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public static void LoadSprite()
		{
			if (File.Exists("DeepClient/ClientIcon.png"))
			{
				SpriteManager.clientIcon = ResourceManager.LoadSpriteFromDisk("DeepClient\\ClientIcon.png", 512, 512);
			}
			else
			{
				DeepConsole.Log("SpriteManager", "File not found: DeepClient/ClientIcon.png, Downloading...");
				SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/ClientIcon.png?raw=true", "DeepClient/ClientIcon.png");
				SpriteManager.clientIcon = ResourceManager.LoadSpriteFromDisk("DeepClient/ClientIcon.png", 512, 512);
			}
			if (!File.Exists("DeepClient/LoadingBackgrund.png"))
			{
				DeepConsole.Log("SpriteManager", "File not found: DeepClient/LoadingBackgrund.png, Downloading...");
				SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/LoadingBackgrund.png?raw=true", "DeepClient/LoadingBackgrund.png");
			}
			if (!File.Exists("DeepClient/MMBG.png"))
			{
				DeepConsole.Log("SpriteManager", "File not found: DeepClient/MMBG.png, Downloading...");
				SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/MMBG.png?raw=true", "DeepClient/MMBG.png");
			}
			if (!File.Exists("DeepClient/QMBG.png"))
			{
				DeepConsole.Log("SpriteManager", "File not found: DeepClient/QMBG.png, Downloading...");
				SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/QMBG.png?raw=true", "DeepClient/QMBG.png");
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public static byte[] DownloadFiles(string downloadUrl, string savePath)
		{
			byte[] array = null;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					array = webClient.DownloadData(downloadUrl);
					File.WriteAllBytes(savePath, array);
					DeepConsole.Log("SpriteManager", "Downloaded: " + savePath);
				}
				catch (Exception ex)
				{
					DeepConsole.Log("SpriteManager", string.Format("An error occurred while downloading: {0}", ex));
				}
			}
			return array;
		}

		// Token: 0x0400016A RID: 362
		public static Sprite clientIcon;
	}
}
