using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using VRC.Udon;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000046 RID: 70
	internal class UdonSoundBoard
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000B6C2 File Offset: 0x000098C2
		public static void Setup()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public static void SetupObj()
		{
			new GameObject("SoundBoard").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/").transform;
			GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").AddComponent<UdonBehaviour>();
			GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").AddComponent<AudioSource>();
			UdonSoundBoard.IsLoaded = true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B71A File Offset: 0x0000991A
		public static void SendSound(string EventThing)
		{
			if (UdonSoundBoard.IsLoaded)
			{
				GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").GetComponent<UdonBehaviour>().SendCustomEvent("OnCustomEvent");
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B73C File Offset: 0x0000993C
		public static void OnCustomEvent(string EventThing)
		{
			if (UdonSoundBoard.IsLoaded)
			{
				if (UdonSoundBoard.AllowedSoundEvent.Contains(EventThing))
				{
					DeepConsole.Log("SoundBoard", "Received A");
				}
				if (UdonSoundBoard.AllowedSoundEvent.Contains(EventThing))
				{
					DeepConsole.Log("SoundBoard", "Received D");
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B788 File Offset: 0x00009988
		public static void PlaySound(string sound)
		{
			string text = Path.Combine(Directory.CreateDirectory("DeepClient/SoundBoard").FullName, sound + ".ogg");
			UnityWebRequest unityWebRequest = UnityWebRequest.Get("file://" + text);
			UdonSoundBoard.clip = WebRequestWWW.InternalCreateAudioClipUsingDH(unityWebRequest.downloadHandler, unityWebRequest.url, false, false, 0);
			AudioSource component = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound").GetComponent<AudioSource>();
			component.clip = UdonSoundBoard.clip;
			component.volume = 100f;
			component.Play();
		}

		// Token: 0x040000D3 RID: 211
		public static bool IsLoaded = false;

		// Token: 0x040000D4 RID: 212
		public static AudioClip clip;

		// Token: 0x040000D5 RID: 213
		public static readonly HashSet<string> AllowedSoundEvent = new HashSet<string> { "DCCapybara", "DC" };
	}
}
