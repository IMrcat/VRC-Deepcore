using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using VRC.UI.Elements.Controls;

namespace DeepCore.Client.UI.QM
{
	// Token: 0x0200000D RID: 13
	internal class MenuMusic
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003C3C File Offset: 0x00001E3C
		public static void OnUpdate()
		{
			if (MenuMusic.IsLoaded)
			{
				if (ConfManager.ShouldMenuMusic.Value && ((MenuMusic.menuStateController != null && MenuMusic.menuStateController.enabled) || (MenuMusic.menuStateController != null && MenuMusic.menuStateControllerMain.enabled)))
				{
					if (GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>() != null && !GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().isPlaying)
					{
						GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Play();
						return;
					}
				}
				else if (GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>() != null && GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().isPlaying)
				{
					GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Stop();
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003D28 File Offset: 0x00001F28
		public static void State(bool s)
		{
			try
			{
				if (MenuMusic.IsLoaded)
				{
					if (s)
					{
						GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Play();
						MenuMusic.IsMenuOpen = true;
					}
					else
					{
						GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Stop();
						MenuMusic.IsMenuOpen = false;
					}
				}
			}
			catch (Exception ex)
			{
				DeepConsole.LogConsole("MenuMusic", "Exception:" + ex.Message);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public static IEnumerator MenuMusicInit()
		{
			if (!File.Exists("DeepClient/LoadingMusic/MenuMusic.ogg"))
			{
				MenuMusic.DownloadFiles("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/MenuMusic.ogg", "DeepClient/LoadingMusic/MenuMusic.ogg");
			}
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return null;
			}
			new GameObject("MenuMusic").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/").transform;
			GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").AddComponent<AudioSource>();
			GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().loop = true;
			string path = Path.Combine(Directory.CreateDirectory("DeepClient/LoadingMusic").FullName, "MenuMusic.ogg");
			if (!File.Exists(path))
			{
				yield return new UnityWebRequest("https://github.com/BiscuiTheHobkin/XEngine-files/raw/main/FirstLoading.ogg", UnityWebRequest.kHttpVerbGET)
				{
					downloadHandler = new DownloadHandlerFile(path)
				}.SendWebRequest();
			}
			UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
			yield return localfile.SendWebRequest();
			MenuMusic.clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
			AudioSource musicObj = GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>();
			while (GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic") == null)
			{
				yield return new WaitForEndOfFrame();
			}
			musicObj.clip = MenuMusic.clip;
			musicObj.volume = 100f;
			musicObj.Stop();
			MenuMusic.IsLoaded = true;
			yield break;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003DAC File Offset: 0x00001FAC
		public static byte[] DownloadFiles(string downloadUrl, string savePath)
		{
			byte[] array = null;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					array = webClient.DownloadData(downloadUrl);
					File.WriteAllBytes(savePath, array);
					DeepConsole.Log("AudioManager", "Downloaded: " + savePath);
				}
				catch (Exception ex)
				{
					DeepConsole.Log("AudioManager", string.Format("An error occurred while downloading: {0}", ex));
				}
			}
			return array;
		}

		// Token: 0x0400003C RID: 60
		public static AudioClip clip;

		// Token: 0x0400003D RID: 61
		public static bool isPlaying = false;

		// Token: 0x0400003E RID: 62
		public static bool IsLoaded = false;

		// Token: 0x0400003F RID: 63
		public static bool IsMenuOpen = false;

		// Token: 0x04000040 RID: 64
		public static MenuStateController menuStateController = GameObject.Find("Canvas_QuickMenu(Clone)").GetComponent<MenuStateController>();

		// Token: 0x04000041 RID: 65
		public static MenuStateController menuStateControllerMain = GameObject.Find("Canvas_MainMenu(Clone)").GetComponent<MenuStateController>();
	}
}
