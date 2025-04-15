using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000097 RID: 151
	internal class StoreLoginPrompt
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00013855 File Offset: 0x00011A55
		public static IEnumerator Init()
		{
			while (GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt") == null)
			{
				yield return null;
			}
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/ButtonAboutUs (1)").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/StoreButtonLogin (1)").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/StoreButtonLogin (1)/Text").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/ButtonAboutUs (1)/Text").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/VRChatButtonLogin").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/VRChatButtonLogin/Text").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/TextWelcome").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/VRChat_LOGO (1)").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/TextLoginWith").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/TextOr").SetActive(false);
			GameObject.Find("MenuContent/Screens/Authentication/StoreLoginPrompt/ButtonCreate").SetActive(false);
			yield break;
		}
	}
}
