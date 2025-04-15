using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000090 RID: 144
	internal class CustomStandardPopup
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00013544 File Offset: 0x00011744
		public static IEnumerator Init()
		{
			while (GameObject.Find("MenuContent/Popups/StandardPopup") == null)
			{
				yield return null;
			}
			GameObject.Find("MenuContent/Popups/StandardPopup/TitleText").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Popups/StandardPopup/BodyText").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("MenuContent/Popups/StandardPopup/RingGlow").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Popups/StandardPopup/InnerDashRing").GetComponent<Image>().color = Color.red;
			GameObject.Find("MenuContent/Popups/StandardPopup/LowPercent").SetActive(false);
			GameObject.Find("MenuContent/Popups/StandardPopup/HighPercent").SetActive(false);
			GameObject.Find("MenuContent/Popups/StandardPopup/ProgressLine").SetActive(false);
			GameObject.Find("MenuContent/Popups/StandardPopup/ArrowLeft").SetActive(false);
			GameObject.Find("MenuContent/Popups/StandardPopup/Rectangle").SetActive(false);
			yield break;
		}
	}
}
