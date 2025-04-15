using System;
using System.Collections;
using System.IO;
using System.Net;
using DeepCore.Client.Misc;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000093 RID: 147
	internal class LoadingParticules
	{
		// Token: 0x0600034B RID: 843 RVA: 0x000135C5 File Offset: 0x000117C5
		public static IEnumerator loadparticles()
		{
			if (!File.Exists("DeepClient\\AssetBundles\\loadingscreen"))
			{
				LoadingParticules.DownloadFiles("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/loadingscreen", "DeepClient\\AssetBundles\\loadingscreen");
			}
			byte[] array = File.ReadAllBytes(MelonUtils.GameDirectory + "\\DeepClient\\AssetBundles\\loadingscreen");
			AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromMemory(array);
			if (myLoadedAssetBundle == null)
			{
				DeepConsole.Log("LS", "Failed to load AssetBundle!");
				yield break;
			}
			LoadingParticules.partsystem = myLoadedAssetBundle.LoadAsset<GameObject>("ParticleLoader");
			GameObject gameObject = Object.Instantiate<GameObject>(LoadingParticules.partsystem, GameObject.Find("MenuContent/Popups/LoadingPopup").transform);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 8000f);
			gameObject.transform.Find("finished").gameObject.transform.localPosition = new Vector3(0f, 0f, 10000f);
			gameObject.transform.Find("finished/Other").gameObject.transform.localPosition = new Vector3(0f, 0f, 3000f);
			gameObject.transform.Find("middle").gameObject.transform.localPosition = new Vector3(-50f, 0f, 10000f);
			gameObject.transform.Find("cirlce mid").gameObject.transform.localPosition = new Vector3(-673.8608f, 0f, 4000f);
			gameObject.transform.Find("spawn").gameObject.transform.localPosition = new Vector3(800f, 0f, -8500f);
			foreach (ParticleSystem particleSystem in gameObject.GetComponentsInChildren<ParticleSystem>(true))
			{
				particleSystem.startColor = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
				particleSystem.trails.colorOverTrail = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			}
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements").gameObject.SetActive(false);
			while (GameObject.Find("DesktopUImanager") == null)
			{
				yield return null;
			}
			GameObject gameObject2 = myLoadedAssetBundle.LoadAsset<GameObject>("Holder");
			myLoadedAssetBundle.Unload(false);
			GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject2, GameObject.Find("DesktopUImanager").transform);
			gameObject3.transform.localPosition = new Vector3(0f, 360.621f, 700f);
			gameObject3.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			gameObject3.transform.localScale = new Vector3(1f, 1f, 1f);
			Transform transform = gameObject3.transform.Find("Particle System").transform;
			transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
			transform.localPosition = new Vector3(0f, 64.16f, 7.2f);
			Transform transform2 = gameObject3.transform.Find("Particle System (1)").transform;
			transform2.localScale = new Vector3(0.06f, 0.06f, 0.06f);
			transform2.localPosition = new Vector3(-30.78f, -321.5403f, 8.54f);
			yield return null;
			yield break;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000135D0 File Offset: 0x000117D0
		public static byte[] DownloadFiles(string downloadUrl, string savePath)
		{
			byte[] array = null;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					array = webClient.DownloadData(downloadUrl);
					File.WriteAllBytes(savePath, array);
					DeepConsole.Log("LoadingManager", "Downloaded: " + savePath);
				}
				catch (Exception ex)
				{
					DeepConsole.Log("LoadingManager", string.Format("An error occurred while downloading: {0}", ex));
				}
			}
			return array;
		}

		// Token: 0x040001F1 RID: 497
		public static GameObject partsystem;
	}
}
