using System;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC.Localization;
using VRC.UI.Client;
using VRC.UI.Elements.Controls;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007F RID: 127
	public static class PopupHelper
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00010C6A File Offset: 0x0000EE6A
		public static void AlertPopup(string tittle, string content, int time)
		{
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_Single_0(LocalizableStringExtensions.Localize(tittle, null, null, null), LocalizableStringExtensions.Localize(content, null, null, null), (float)time);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010C8A File Offset: 0x0000EE8A
		public static void OpenVideoInMM(string Tittle, string url, bool s)
		{
			VRCUIManager.Method_Public_Static_get_VRCUIManager_0().Method_Public_Virtual_Final_New_Void_String_String_Boolean_0(Tittle, url, s);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00010C9C File Offset: 0x0000EE9C
		public static void StandardPopup(string TTle, string Ctent, string btext, Action value)
		{
			if (VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0 != null)
			{
				LocalizableString localizableString = LocalizableStringExtensions.Localize(TTle, null, null, null);
				LocalizableString localizableString2 = LocalizableStringExtensions.Localize(Ctent, null, null, null);
				LocalizableString localizableString3 = LocalizableStringExtensions.Localize(btext, null, null, null);
				Action action = delegate
				{
					VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
					Action value2 = value;
					if (value2 == null)
					{
						return;
					}
					value2();
				};
				VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0(localizableString, localizableString2, localizableString3, action, null);
				return;
			}
			DeepConsole.Log("VRCUiPopup", "VRCUiPopupManager is not initialized!");
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00010D14 File Offset: 0x0000EF14
		public static void StandardPopupV2(string T, string C, string B, Action AC)
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize(T, null, null, null);
			LocalizableString localizableString2 = LocalizableStringExtensions.Localize(C, null, null, null);
			LocalizableString localizableString3 = LocalizableStringExtensions.Localize(B, null, null, null);
			Action action = delegate
			{
				VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
				Action ac = AC;
				if (ac == null)
				{
					return;
				}
				ac();
			};
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0(localizableString, localizableString2, localizableString3, action, null);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public static void PopupCall(string T, string S, string BT, bool IsNumpad, Action<string> onUserInputAction = null)
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize(T, null, null, null);
			LocalizableString localizableString2 = LocalizableStringExtensions.Localize(S, null, null, null);
			LocalizableString localizableString3 = LocalizableStringExtensions.Localize("", null, null, null);
			LocalizableString localizableString4 = LocalizableStringExtensions.Localize(BT, null, null, null);
			Action<string, List<KeyCode>, TextMeshProUGUIEx> action = delegate(string userInput, List<KeyCode> keyCodes, TextMeshProUGUIEx textComponent)
			{
				if (onUserInputAction != null)
				{
					onUserInputAction(userInput);
					return;
				}
				Console.WriteLine("User Input: " + userInput);
			};
			Action action2 = delegate
			{
			};
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_InputType_Boolean_LocalizableString_Action_3_String_List_1_KeyCode_TextMeshProUGUIEx_Action_LocalizableString_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(localizableString, localizableString3, 0, IsNumpad, localizableString4, action, action2, localizableString2, true, null, false, 0);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010E04 File Offset: 0x0000F004
		public static void ConfirmNotification(string title, string Content, string okButtonName, Action onSuccess)
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize(title, null, null, null);
			LocalizableString localizableString2 = LocalizableStringExtensions.Localize(Content, null, null, null);
			LocalizableString localizableString3 = LocalizableStringExtensions.Localize(okButtonName, null, null, null);
			LocalizableString localizableString4 = LocalizableStringExtensions.Localize(okButtonName, null, null, null);
			Action action = delegate
			{
				onSuccess();
				VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
			};
			Action action2 = delegate
			{
				VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
			};
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_0(localizableString, localizableString2, localizableString3, action, localizableString4, action2, null);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010E90 File Offset: 0x0000F090
		public static void testthings()
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize("a", null, null, null);
			LocalizableString localizableString2 = LocalizableStringExtensions.Localize("n", null, null, null);
			delegate(VRCUiPopup popup)
			{
				Console.WriteLine("Popup action triggered.");
			};
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_PDM_0(localizableString, localizableString2, localizableString2, null, null, null, null);
		}
	}
}
