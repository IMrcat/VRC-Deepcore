using System;
using System.Collections;
using System.IO;
using System.Net;
using MelonLoader;
using UnityEngine;
using UnityEngine.Networking;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000095 RID: 149
	internal class CustomLoadingAudios
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000136AF File Offset: 0x000118AF
		public static IEnumerator Init()
		{
			if (!File.Exists("DeepClient/LoadingMusic/LoginScreenMusic.ogg"))
			{
				CustomLoadingAudios.DownloadFiles("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/LoginScreenMusic.ogg", "DeepClient/LoadingMusic/LoginScreenMusic.ogg");
			}
			while (GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound") == null)
			{
				yield return null;
			}
			string path = Path.Combine(Directory.CreateDirectory("DeepClient/LoadingMusic").FullName, "LoginScreenMusic.ogg");
			if (!File.Exists(path))
			{
				yield return new UnityWebRequest("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/LoginScreenMusic.ogg", UnityWebRequest.kHttpVerbGET)
				{
					downloadHandler = new DownloadHandlerFile(path)
				}.SendWebRequest();
			}
			UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
			yield return localfile.SendWebRequest();
			CustomLoadingAudios.clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
			AudioSource musicObj = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound").GetComponent<AudioSource>();
			while (GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound") == null)
			{
				yield return new WaitForEndOfFrame();
			}
			musicObj.clip = CustomLoadingAudios.clip;
			musicObj.volume = 100f;
			musicObj.Play();
			if (ConfManager.BLSEnabled.Value)
			{
				musicObj.gameObject.SetActive(false);
			}
			MelonCoroutines.Start(CustomLoadingAudios.MInit());
			yield break;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000136B7 File Offset: 0x000118B7
		public static IEnumerator MInit()
		{
			while (GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound") == null)
			{
				yield return null;
			}
			yield return new WaitForSeconds(2f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound").SetActive(false);
			new GameObject("LoadingSounds").transform.parent = GameObject.Find("MenuContent/Popups/LoadingPopup/").transform;
			GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSounds").AddComponent<AudioSource>();
			GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSounds").GetComponent<AudioSource>().loop = true;
			string path = Path.Combine(Directory.CreateDirectory("DeepClient/LoadingMusic").FullName, "LoginScreenMusic.ogg");
			if (!File.Exists(path))
			{
				yield return new UnityWebRequest("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/LoginScreenMusic.ogg", UnityWebRequest.kHttpVerbGET)
				{
					downloadHandler = new DownloadHandlerFile(path)
				}.SendWebRequest();
			}
			UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
			yield return localfile.SendWebRequest();
			CustomLoadingAudios.clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
			AudioSource musicObj = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSounds").GetComponent<AudioSource>();
			while (GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSounds") == null)
			{
				yield return new WaitForEndOfFrame();
			}
			musicObj.clip = CustomLoadingAudios.clip;
			musicObj.volume = 100f;
			musicObj.Play();
			if (ConfManager.BLSEnabled.Value)
			{
				musicObj.gameObject.SetActive(false);
			}
			yield break;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000136C0 File Offset: 0x000118C0
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

		// Token: 0x040001F2 RID: 498
		public static AudioClip clip;
	}
}
