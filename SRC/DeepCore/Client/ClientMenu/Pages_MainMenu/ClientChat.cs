using System;
using System.Collections.Generic;
using System.Media;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.RPC;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x0200009D RID: 157
	internal class ClientChat
	{
		// Token: 0x06000377 RID: 887 RVA: 0x00014368 File Offset: 0x00012568
		public static void ClientChatMenu(UiManager UIManager)
		{
			ReMenuPage reMenuPage = UIManager.QMMenu.AddMenuPage("Client Chat", null, null, "#ffffff");
			reMenuPage.AddButton("Send a message", "it send a message wow.", delegate
			{
				PopupHelper.PopupCall("Chat Message", "Enter an text", "Send", false, delegate(string userInput)
				{
					RPCManager.SendRPC("DCChat:" + userInput);
				});
			}, null, "#ffffff");
			reMenuPage.AddSpacer(null);
			reMenuPage.AddSpacer(null);
			ClientChat.FormChatLog();
			reMenuPage.AddButton("Clear", "Delete all message (Local).", delegate
			{
				ClientChat.logText.text = "";
			}, null, "#ffffff");
			reMenuPage.OnOpen += ClientChat.PlaceConsole;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014420 File Offset: 0x00012620
		public static void FormChatLog()
		{
			if (!ClientChat.isReady)
			{
				GameObject gameObject = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_ClientChat/Scrollrect/Viewport/VerticalLayoutGroup");
				ClientChat.logObject = new GameObject("BestChat");
				ClientChat.logObject.transform.SetParent(gameObject.transform, false);
				GameObject gameObject2 = new GameObject("Box");
				gameObject2.transform.SetParent(ClientChat.logObject.transform, false);
				gameObject2.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
				gameObject2.AddComponent<RectMask2D>();
				RectTransform component = gameObject2.GetComponent<RectTransform>();
				component.sizeDelta = ClientChat.consoleSize;
				component.anchorMin = new Vector2(0f, 1f);
				component.anchorMax = new Vector2(0f, 1f);
				component.pivot = new Vector2(0f, 1f);
				component.anchoredPosition = Vector2.zero;
				gameObject2.transform.localPosition = Vector2.zero;
				ScrollRect scrollRect = ClientChat.logObject.AddComponent<ScrollRect>();
				scrollRect.horizontal = false;
				scrollRect.vertical = true;
				GameObject gameObject3 = new GameObject("Viewport");
				gameObject3.transform.SetParent(ClientChat.logObject.transform, false);
				RectTransform rectTransform = gameObject3.AddComponent<RectTransform>();
				rectTransform.sizeDelta = ClientChat.consoleSize;
				rectTransform.anchorMin = new Vector2(0f, 1f);
				rectTransform.anchorMax = new Vector2(0f, 1f);
				rectTransform.pivot = new Vector2(0f, 1f);
				rectTransform.anchoredPosition = Vector2.zero;
				gameObject3.AddComponent<CanvasRenderer>();
				gameObject3.AddComponent<Image>();
				gameObject3.AddComponent<Mask>().showMaskGraphic = false;
				scrollRect.viewport = rectTransform;
				gameObject3.transform.localPosition = Vector2.zero;
				GameObject gameObject4 = new GameObject("Content");
				gameObject4.transform.SetParent(gameObject3.transform, false);
				RectTransform rectTransform2 = gameObject4.AddComponent<RectTransform>();
				rectTransform2.sizeDelta = ClientChat.consoleSize;
				ClientChat.logText = gameObject4.AddComponent<TextMeshProUGUI>();
				ClientChat.logText.text = "";
				ClientChat.logText.fontSize = 23f;
				ClientChat.logText.alignment = 1025;
				ClientChat.logText.color = Color.white;
				ClientChat.logText.enableWordWrapping = true;
				ClientChat.logText.rectTransform.sizeDelta = ClientChat.consoleSize;
				ClientChat.logText.rectTransform.anchorMin = new Vector2(0f, 1f);
				ClientChat.logText.rectTransform.anchorMax = new Vector2(0f, 1f);
				ClientChat.logText.rectTransform.pivot = new Vector2(0f, 1f);
				ClientChat.logText.rectTransform.anchoredPosition = Vector2.zero;
				ClientChat.logText.transform.localPosition = Vector2.zero;
				scrollRect.content = rectTransform2;
				gameObject2.transform.localPosition = Vector2.zero;
				gameObject3.transform.localPosition = Vector2.zero;
				gameObject4.transform.localPosition = Vector2.zero;
				ClientChat.logText.transform.localPosition = Vector2.zero;
				ClientChat.logObject.transform.position = new Vector3(2.8513f, 1.151f, 5.9919f);
				ClientChat.logObject.transform.localPosition = new Vector3(-154.1549f, -95.8792f, -0.3713f);
				ClientChat.isReady = true;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000147C0 File Offset: 0x000129C0
		public static void PlaceConsole()
		{
			ClientChat.logObject.transform.position = new Vector3(2.8513f, 1.151f, 5.9919f);
			ClientChat.logObject.transform.localPosition = new Vector3(-154.1549f, -95.8792f, -0.3713f);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00014814 File Offset: 0x00012A14
		internal static void ChatMSG(string username, string message)
		{
			VrcExtensions.Toast(username ?? "", message, null, 5f);
			SystemSounds.Asterisk.Play();
			if (false)
			{
				ClientChat.messages.Enqueue("<b>" + message + "<b>\n");
			}
			else
			{
				ClientChat.messages.Enqueue(string.Concat(new string[] { "[", username, "]|", message, "\n" }));
			}
			string text = string.Concat(ClientChat.messages);
			if (text.Length > 10000)
			{
				string text2 = text;
				int length = text2.Length;
				int num = length - 10000;
				text = text2.Substring(num, length - num);
			}
			if (false)
			{
				ClientChat.logText.text = "<b>" + text + "<b>\n";
				return;
			}
			ClientChat.logText.text = text ?? "";
		}

		// Token: 0x04000203 RID: 515
		internal static bool isReady = false;

		// Token: 0x04000204 RID: 516
		internal static bool isNotif = false;

		// Token: 0x04000205 RID: 517
		internal static TextMeshProUGUI logText;

		// Token: 0x04000206 RID: 518
		internal static Queue<string> messages = new Queue<string>();

		// Token: 0x04000207 RID: 519
		internal static GameObject logObject;

		// Token: 0x04000208 RID: 520
		internal static Vector2 consoleSize = new Vector2(650f, 800f);
	}
}
