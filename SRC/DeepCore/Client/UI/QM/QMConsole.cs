using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace DeepCore.Client.UI.QM
{
	// Token: 0x0200000F RID: 15
	internal class QMConsole
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000407C File Offset: 0x0000227C
		public static IEnumerator StartConsole()
		{
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return null;
			}
			new GameObject("DConsole").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon").transform;
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.position = new Vector3(-1.2201f, 0.7617f, 252.4173f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localPosition = new Vector3(615.0677f, -185.995f, -353.3717f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localScale = new Vector3(7f, 7f, 7f);
			if (XRSettings.isDeviceActive)
			{
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.position = new Vector3(-0.2903f, 1.6423f, 7.7845f);
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localPosition = new Vector3(756.9377f, -155.0943f, -6.1556f);
			}
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").AddComponent<TextMeshProUGUI>();
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().fontSize = 3f;
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().text = "If you see this it mean the code is fked";
			QMConsole.isloaded = true;
			yield break;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004084 File Offset: 0x00002284
		public static void ConsoleVisuabillity(bool s)
		{
			if (QMConsole.isloaded)
			{
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").SetActive(s);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000040A0 File Offset: 0x000022A0
		public static void ClearLog()
		{
			QMConsole.logContent = "";
			QMConsole.currentLines = 0;
			if (QMConsole.logText != null)
			{
				QMConsole.logText.text = "";
			}
			GameObject gameObject = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole");
			if (gameObject != null)
			{
				TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
				if (component != null)
				{
					component.text = "";
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004108 File Offset: 0x00002308
		public static void PrintLog(string Name, string Content)
		{
			if (QMConsole.isloaded)
			{
				QMConsole.LogMessage(Name, Content);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004118 File Offset: 0x00002318
		public static void LogMessage(string yes, string message)
		{
			DateTime now = DateTime.Now;
			if (QMConsole.currentLines >= QMConsole.maxLines)
			{
				QMConsole.ClearLog();
			}
			QMConsole.logContent += string.Format("<color=white>[</color><color=yellow>{0:HH:mm}</color><color=white>]</color> <color=white>[</color><color=green>{1}</color><color=white>]</color> {2}\n", now, yes, message);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().text = QMConsole.logContent;
			QMConsole.currentLines++;
		}

		// Token: 0x04000044 RID: 68
		public static TextMeshProUGUI logText;

		// Token: 0x04000045 RID: 69
		public static int maxLines = 28;

		// Token: 0x04000046 RID: 70
		public static string logContent = "";

		// Token: 0x04000047 RID: 71
		public static int currentLines = 0;

		// Token: 0x04000048 RID: 72
		public static bool isloaded = false;
	}
}
