using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003F RID: 63
	internal class OldLoadingScreenMod
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000A8E6 File Offset: 0x00008AE6
		public static void OnApplicationStart()
		{
			MelonCoroutines.Start(OldLoadingScreenMod.WaitForUi());
			MelonCoroutines.Start(OldLoadingScreenMod.GetAssembly());
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A8FE File Offset: 0x00008AFE
		public static IEnumerator GetAssembly()
		{
			for (;;)
			{
				if (!(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly assembly) => assembly.GetName().Name == "Assembly-CSharp") == null))
				{
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A906 File Offset: 0x00008B06
		public static IEnumerator WaitForUi()
		{
			while (GameObject.Find("MenuContent") == null)
			{
				yield return null;
			}
			DeepConsole.Log("BLS", "Loading AssetBundle from path...");
			if (File.Exists(OldLoadingScreenMod.assetBundlePath))
			{
				OldLoadingScreenMod.assets = AssetBundle.LoadFromMemory_Internal(File.ReadAllBytes(OldLoadingScreenMod.assetBundlePath), 0U);
				OldLoadingScreenMod.assets.hideFlags |= 32;
				OldLoadingScreenMod.loadScreenPrefab = OldLoadingScreenMod.assets.LoadAsset_Internal("Assets/Bundle/LoadingBackground.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
				OldLoadingScreenMod.loadScreenPrefab.hideFlags |= 32;
				OldLoadingScreenMod.loginPrefab = OldLoadingScreenMod.assets.LoadAsset_Internal("Assets/Bundle/Login.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
				OldLoadingScreenMod.loginPrefab.hideFlags |= 32;
				OldLoadingScreenMod.CreateGameObjects();
			}
			else
			{
				DeepConsole.Log("BLS", "AssetBundle file not found at: " + OldLoadingScreenMod.assetBundlePath);
			}
			yield break;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A910 File Offset: 0x00008B10
		public static void CreateGameObjects()
		{
			DeepConsole.Log("BLS", "Finding original GameObjects");
			GameObject gameObject = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked");
			GameObject gameObject2 = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles");
			GameObject gameObject3 = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound");
			DeepConsole.Log("BLS", "Creating new GameObjects");
			OldLoadingScreenMod.loadScreenPrefab = OldLoadingScreenMod.CreateGameObject(OldLoadingScreenMod.loadScreenPrefab, new Vector3(400f, 400f, 400f), "MenuContent/Popups/", "LoadingPopup");
			OldLoadingScreenMod.loginPrefab = OldLoadingScreenMod.CreateGameObject(OldLoadingScreenMod.loginPrefab, new Vector3(0.5f, 0.5f, 0.5f), "LoadingBackground_TealGradient_Music", "");
			DeepConsole.Log("BLS", "Disabling original GameObjects");
			gameObject.active = false;
			gameObject2.active = false;
			gameObject3.active = false;
			OldLoadingScreenMod.OnPreferencesSaved();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public static void OnPreferencesSaved()
		{
			DeepConsole.Log("BLS", "Applying Preferences");
			OldLoadingScreenMod.loadScreenPrefab = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingBackground(Clone)/");
			Transform transform = OldLoadingScreenMod.loadScreenPrefab.transform.Find("MenuMusic");
			Transform transform2 = OldLoadingScreenMod.loadScreenPrefab.transform.Find("SpaceSound");
			OldLoadingScreenMod.loadScreenPrefab.transform.Find("SkyCube");
			OldLoadingScreenMod.loadScreenPrefab.transform.Find("Stars");
			Transform transform3 = OldLoadingScreenMod.loadScreenPrefab.transform.Find("Tunnel");
			Transform transform4 = OldLoadingScreenMod.loadScreenPrefab.transform.Find("VRCLogo");
			GameObject gameObject = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel");
			GameObject gameObject2 = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound");
			Transform transform5 = OldLoadingScreenMod.loadScreenPrefab.transform.Find("meme");
			GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").active = false;
			if (ConfManager.BLSModSounds.Value)
			{
				transform.gameObject.SetActive(true);
				transform2.gameObject.SetActive(true);
				gameObject2.SetActive(false);
			}
			else
			{
				transform.gameObject.SetActive(false);
				transform2.gameObject.SetActive(false);
				gameObject2.SetActive(true);
			}
			if (ConfManager.BLSWarpTunnel.Value)
			{
				transform3.gameObject.SetActive(true);
			}
			else
			{
				transform3.gameObject.SetActive(false);
			}
			if (ConfManager.BLSVrcLogo.Value)
			{
				transform4.gameObject.SetActive(true);
			}
			else
			{
				transform4.gameObject.SetActive(false);
			}
			if (ConfManager.BLShowLoadingMessages.Value)
			{
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
			if (DateTime.Today.Month == 4 && DateTime.Now.Day == 1)
			{
				transform4.gameObject.SetActive(false);
				transform5.gameObject.SetActive(true);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public static GameObject CreateGameObject(GameObject obj, Vector3 scale, string rootDest, string parent)
		{
			DeepConsole.Log("BLS", "Creating " + obj.name);
			Transform transform = GameObject.Find(rootDest).transform.Find(parent);
			GameObject gameObject = Object.Instantiate<GameObject>(obj, transform, false).Cast<GameObject>();
			gameObject.transform.parent = transform;
			gameObject.transform.localScale = scale;
			return gameObject;
		}

		// Token: 0x040000A8 RID: 168
		public static GameObject loadScreenPrefab;

		// Token: 0x040000A9 RID: 169
		public static GameObject loginPrefab;

		// Token: 0x040000AA RID: 170
		public static AssetBundle assets;

		// Token: 0x040000AB RID: 171
		private static readonly string assetBundlePath = "DeepClient/loading.assetbundle";
	}
}
