using System;
using System.Collections;
using System.IO;
using System.Linq;
using Il2CppSystem;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003D RID: 61
	internal class LoadingScreenPictures
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000A18C File Offset: 0x0000838C
		public static void OnApplicationStart()
		{
			MelonCoroutines.Start(LoadingScreenPictures.UiManagerInitializer());
			string text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\VRChat";
			MelonPreferences.CreateCategory("LoadingScreenPictures", "Loading Screen Pictures");
			MelonPreferences.CreateEntry<string>("LoadingScreenPictures", "directory", text, "Folder to get pictures from", null, false, false, null);
			MelonPreferences.CreateEntry<bool>("LoadingScreenPictures", "enabled", true, "Enable", null, false, false, null);
			LoadingScreenPictures.folder_dir = MelonPreferences.GetEntryValue<string>("LoadingScreenPictures", "directory");
			LoadingScreenPictures.enabled = MelonPreferences.GetEntryValue<bool>("LoadingScreenPictures", "enabled");
			if (text != LoadingScreenPictures.folder_dir && !Directory.Exists(LoadingScreenPictures.folder_dir))
			{
				LoadingScreenPictures.folder_dir = text;
				DeepConsole.Log("LoadingScreenPictures", "Couldn't find configured directory, using default directory.");
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A250 File Offset: 0x00008450
		public static IEnumerator UiManagerInitializer()
		{
			while (GameObject.Find("MenuContent") == null)
			{
				yield return null;
			}
			LoadingScreenPictures.setup();
			LoadingScreenPictures.initUI = true;
			yield break;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A258 File Offset: 0x00008458
		public static void OnPreferencesSaved()
		{
			LoadingScreenPictures.enabled = MelonPreferences.GetEntryValue<bool>("LoadingScreenPictures", "enabled");
			if (LoadingScreenPictures.enabled)
			{
				LoadingScreenPictures.setup();
				return;
			}
			LoadingScreenPictures.disable();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A280 File Offset: 0x00008480
		public static void OnUpdate()
		{
			if (!LoadingScreenPictures.enabled)
			{
				return;
			}
			if (Time.time > LoadingScreenPictures.wait)
			{
				LoadingScreenPictures.wait += 5f;
				if (LoadingScreenPictures.hidden)
				{
					LoadingScreenPictures.hidden = false;
					LoadingScreenPictures.setup();
				}
			}
			if (LoadingScreenPictures.lastTexture == null)
			{
				return;
			}
			if (LoadingScreenPictures.lastTexture == LoadingScreenPictures.screenRender.material.mainTexture)
			{
				return;
			}
			LoadingScreenPictures.lastTexture = LoadingScreenPictures.screenRender.material.mainTexture;
			LoadingScreenPictures.changePic();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A306 File Offset: 0x00008506
		public static void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			if (buildIndex - 1 > 1 && LoadingScreenPictures.initUI && LoadingScreenPictures.lastTexture == null)
			{
				LoadingScreenPictures.setup();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A328 File Offset: 0x00008528
		public static void changePic()
		{
			Texture2D texture2D = new Texture2D(2, 2);
			ImageConversion.LoadImage(texture2D, File.ReadAllBytes(LoadingScreenPictures.randImage()));
			LoadingScreenPictures.pic.material.mainTexture = texture2D;
			if (LoadingScreenPictures.pic.material.mainTexture.height > LoadingScreenPictures.pic.material.mainTexture.width)
			{
				LoadingScreenPictures.cube.transform.localScale = new Vector3(0.099f, 1f, 0.175f);
				LoadingScreenPictures.mainFrame.transform.localScale = new Vector3(10.8f, 19.2f, 1f);
				return;
			}
			LoadingScreenPictures.cube.transform.localScale = new Vector3(0.175f, 1f, 0.099f);
			LoadingScreenPictures.mainFrame.transform.localScale = new Vector3(19.2f, 10.8f, 1f);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A41C File Offset: 0x0000861C
		public static void disable()
		{
			DeepConsole.Log("LoadingScreenPictures", "Disabled.");
			if (LoadingScreenPictures.mainFrame)
			{
				LoadingScreenPictures.mainFrame.transform.localScale = LoadingScreenPictures.originalSize;
			}
			if (LoadingScreenPictures.screenRender)
			{
				LoadingScreenPictures.screenRender.enabled = true;
			}
			if (LoadingScreenPictures.cube)
			{
				Object.Destroy(LoadingScreenPictures.cube);
			}
			LoadingScreenPictures.lastTexture = null;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A48C File Offset: 0x0000868C
		public static void setup()
		{
			if (!LoadingScreenPictures.enabled || LoadingScreenPictures.lastTexture != null)
			{
				return;
			}
			LoadingScreenPictures.mainFrame = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainFrame");
			LoadingScreenPictures.originalSize = LoadingScreenPictures.mainFrame.transform.localScale;
			GameObject gameObject = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainScreen");
			string text = LoadingScreenPictures.randImage();
			if (text == null)
			{
				DeepConsole.Log("LoadingScreenPictures", "No screenshots found in:" + LoadingScreenPictures.folder_dir);
				return;
			}
			GameObject gameObject2 = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN");
			LoadingScreenPictures.screenRender = gameObject.GetComponent<Renderer>();
			LoadingScreenPictures.lastTexture = LoadingScreenPictures.screenRender.material.mainTexture;
			LoadingScreenPictures.cube = GameObject.CreatePrimitive(4);
			LoadingScreenPictures.cube.transform.SetParent(gameObject2.transform);
			LoadingScreenPictures.cube.transform.rotation = gameObject.transform.rotation;
			LoadingScreenPictures.cube.transform.localPosition = new Vector3(0f, 0f, -0.19f);
			LoadingScreenPictures.cube.GetComponent<Collider>().enabled = false;
			LoadingScreenPictures.cube.layer = LayerMask.NameToLayer("InternalUI");
			Texture2D texture2D = new Texture2D(2, 2);
			ImageConversion.LoadImage(texture2D, File.ReadAllBytes(text));
			LoadingScreenPictures.pic = LoadingScreenPictures.cube.GetComponent<Renderer>();
			LoadingScreenPictures.pic.material.mainTexture = texture2D;
			LoadingScreenPictures.screenRender.enabled = false;
			if (LoadingScreenPictures.pic.material.mainTexture.height > LoadingScreenPictures.pic.material.mainTexture.width)
			{
				LoadingScreenPictures.cube.transform.localScale = new Vector3(0.099f, 1f, 0.175f);
				LoadingScreenPictures.mainFrame.transform.localScale = new Vector3(10.8f, 19.2f, 1f);
			}
			else
			{
				LoadingScreenPictures.cube.transform.localScale = new Vector3(0.175f, 1f, 0.099f);
				LoadingScreenPictures.mainFrame.transform.localScale = new Vector3(19.2f, 10.8f, 1f);
			}
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/ICON").active = false;
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/TITLE").active = false;
			DeepConsole.Log("LoadingScreenPictures", "Setup Game Objects.");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public static string randImage()
		{
			if (!Directory.Exists(LoadingScreenPictures.folder_dir))
			{
				return null;
			}
			string[] array = (from s in Directory.GetFiles(LoadingScreenPictures.folder_dir, "*.*", SearchOption.AllDirectories)
				where s.EndsWith(".png") || s.EndsWith(".jpeg")
				select s).ToArray<string>();
			if (array.Length == 0)
			{
				return null;
			}
			int num = new Random().Next(1, array.Length);
			return array[num].ToString();
		}

		// Token: 0x0400009C RID: 156
		public static GameObject mainFrame;

		// Token: 0x0400009D RID: 157
		public static GameObject cube;

		// Token: 0x0400009E RID: 158
		public static Texture lastTexture;

		// Token: 0x0400009F RID: 159
		public static Renderer screenRender;

		// Token: 0x040000A0 RID: 160
		public static Renderer pic;

		// Token: 0x040000A1 RID: 161
		public static string folder_dir;

		// Token: 0x040000A2 RID: 162
		public static bool initUI = false;

		// Token: 0x040000A3 RID: 163
		public static bool enabled = true;

		// Token: 0x040000A4 RID: 164
		public static bool IsLoaded = true;

		// Token: 0x040000A5 RID: 165
		public static Vector3 originalSize;

		// Token: 0x040000A6 RID: 166
		public static float wait = 0f;

		// Token: 0x040000A7 RID: 167
		public static bool hidden = false;
	}
}
