using System;
using System.Collections;
using System.IO;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000091 RID: 145
	internal class CustomVRLoadingOverlay
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00013554 File Offset: 0x00011754
		public static IEnumerator Init()
		{
			float timeout = 2f;
			float elapsedTime = 0f;
			while (GameObject.Find("TrackingVolume/VRLoadingOverlay/FlatLoadingOverlay(Clone)") == null)
			{
				if (elapsedTime >= timeout)
				{
					Debug.LogWarning("GameObject not found! Stopping coroutine.");
					DeepConsole.Log("Startup", "Can't find FlatLoadingOverlay(Clone), User may being in VR.");
					yield break;
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			GameObject gameObject = GameObject.Find("TrackingVolume/VRLoadingOverlay/FlatLoadingOverlay(Clone)/Container/Canvas/Background");
			if (gameObject == null)
			{
				DeepConsole.Log("Startup", "Can't find Overlay background.");
				yield break;
			}
			Image component = gameObject.GetComponent<Image>();
			if (component == null)
			{
				DeepConsole.Log("Startup", "Image component missing on overlay background !");
				yield break;
			}
			string text = "DeepClient/LoadingBackgrund.png";
			if (File.Exists(text))
			{
				component.color = Color.white;
				component.overrideSprite = ResourceManager.LoadSpriteFromDisk(text, 512, 512);
			}
			else
			{
				SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/LoadingBackgrund.png?raw=true", "DeepClient/DeepClient/LoadingBackgrund.png");
			}
			yield break;
		}
	}
}
