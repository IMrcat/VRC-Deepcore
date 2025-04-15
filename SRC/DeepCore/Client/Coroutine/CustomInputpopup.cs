using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x0200008E RID: 142
	internal class CustomInputpopup
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000133EC File Offset: 0x000115EC
		public static IEnumerator Init()
		{
			while (GameObject.Find("MenuContent/Popups/InputPopup") == null)
			{
				yield return null;
			}
			GameObject.Find("MenuContent/Popups/InputPopup/Darkness").GetComponent<Image>().gameObject.SetActive(false);
			GameObject.Find("MenuContent/Popups/InputPopup/Darkness").GetComponent<Image>().color = Color.white;
			GameObject.Find("MenuContent/Popups/InputPopup/Rectangle").gameObject.SetActive(false);
			GameObject.Find("MenuContent/Popups/InputPopup/ButtonRight").GetComponent<Image>().color = Color.blue;
			GameObject.Find("MenuContent/Popups/InputPopup/ButtonRight/Text").GetComponent<TextMeshProUGUI>().color = Color.blue;
			GameObject.Find("MenuContent/Popups/InputPopup/ButtonLeft").GetComponent<Image>().color = Color.blue;
			GameObject.Find("MenuContent/Popups/InputPopup/ButtonLeft/Text").GetComponent<TextMeshProUGUI>().color = Color.blue;
			GameObject.Find("MenuContent/Popups/InputPopup/PasswordVisibilityToggle").GetComponent<Image>().color = Color.blue;
			GameObject.Find("MenuContent/Popups/InputPopup/InputField").GetComponent<Image>().color = Color.black;
			GameObject.Find("MenuContent/Popups/InputPopup/TitleText").GetComponent<TextMeshProUGUI>().color = Color.red;
			yield break;
		}
	}
}
