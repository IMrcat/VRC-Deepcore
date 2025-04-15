using System;
using System.Collections;
using System.Text;
using DeepCore.Client.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using VRC.SDKBase;

namespace DeepCore.Client.UI.QM
{
	// Token: 0x0200000E RID: 14
	internal class QMPlayerList
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003E6C File Offset: 0x0000206C
		public static IEnumerator StartPlayerList()
		{
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return null;
			}
			new GameObject("DPlayerList").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon").transform;
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.position = new Vector3(-8.2154f, 0.9213f, 3.0299f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localPosition = new Vector3(373.3873f, -186.1004f, -353.249f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localScale = new Vector3(7f, 7f, 7f);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").AddComponent<TextMeshProUGUI>();
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().fontSize = 3f;
			if (XRSettings.isDeviceActive)
			{
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.position = new Vector3(-0.1798f, 1.4831f, -0.3481f);
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localPosition = new Vector3(341.9142f, -14.9804f, -14.9804f);
			}
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().text = "Playerlist is having error !!!";
			QMPlayerList.IsLoaded = true;
			yield break;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003E74 File Offset: 0x00002074
		public static void UpdateList()
		{
			if (QMPlayerList.IsLoaded)
			{
				if (Networking.LocalPlayer == null)
				{
					return;
				}
				QMPlayerList.sb.Clear();
				QMPlayerList.sb.AppendLine(string.Format("<size=120%>({0}) players in room</size>\n", VRCPlayerApi.AllPlayers.Count));
				foreach (VRCPlayerApi vrcplayerApi in VRCPlayerApi.AllPlayers)
				{
					PlayerNet component = vrcplayerApi.gameObject.GetComponent<PlayerNet>();
					if (!(component == null))
					{
						int field_Private_Byte_ = (int)component.field_Private_Byte_0;
						int field_Private_Int16_ = (int)component.field_Private_Int16_0;
						Color color = Color.Lerp(Color.red, Color.green, Mathf.Clamp01((float)field_Private_Byte_ / 100f));
						string text = string.Format("<color=#{0}>[{1} FPS]</color>", ColorUtility.ToHtmlStringRGB(color), field_Private_Byte_);
						Color color2 = Color.Lerp(Color.green, Color.red, Mathf.Clamp01((float)field_Private_Int16_ / 150f));
						string text2 = string.Format("<color=#{0}>[{1} ms]</color>", ColorUtility.ToHtmlStringRGB(color2), field_Private_Int16_);
						string text3 = (vrcplayerApi.isMaster ? "<color=red>[M]</color> - " : "");
						string text4 = (component.Method_Internal_get_VRCPlayer_0()._player.field_Private_APIUser_0.isFriend ? "<color=yellow>{F]</color> - " : "");
						Color playerColor = component.Method_Internal_get_VRCPlayer_0()._player.field_Private_APIUser_0.GetPlayerColor();
						string text5 = string.Concat(new string[]
						{
							"<color=#",
							ColorUtility.ToHtmlStringRGB(playerColor),
							">",
							vrcplayerApi.displayName,
							"</color>"
						});
						QMPlayerList.sb.AppendLine(string.Concat(new string[] { text5, " - ", text3, text4, text, " - ", text2 }));
					}
				}
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().text = QMPlayerList.sb.ToString();
			}
		}

		// Token: 0x04000042 RID: 66
		public static bool IsLoaded = false;

		// Token: 0x04000043 RID: 67
		public static StringBuilder sb = new StringBuilder();
	}
}
