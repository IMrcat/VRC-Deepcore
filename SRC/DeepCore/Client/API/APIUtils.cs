using System;
using UnityEngine;
using VRC.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace DeepCore.Client.API
{
	// Token: 0x020000AC RID: 172
	public static class APIUtils
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000173FB File Offset: 0x000155FB
		public static QuickMenu QuickMenuInstance
		{
			get
			{
				if (APIUtils._quickMenu == null)
				{
					APIUtils._quickMenu = Resources.FindObjectsOfTypeAll<QuickMenu>()[0];
				}
				return APIUtils._quickMenu;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001741F File Offset: 0x0001561F
		public static MenuStateController MenuStateControllerInstance
		{
			get
			{
				if (APIUtils._menuStateController == null)
				{
					APIUtils._menuStateController = APIUtils.QuickMenuInstance.GetComponent<MenuStateController>();
				}
				return APIUtils._menuStateController;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00017442 File Offset: 0x00015642
		public static VRCUiPopupManager VRCUiPopupManagerInstance
		{
			get
			{
				if (APIUtils._vrcUiPopupManager == null)
				{
					APIUtils._vrcUiPopupManager = Resources.FindObjectsOfTypeAll<VRCUiPopupManager>()[0];
				}
				return APIUtils._vrcUiPopupManager;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00017466 File Offset: 0x00015666
		public static GameObject GetUserInterface()
		{
			if (APIUtils._userInterface == null)
			{
				APIUtils._userInterface = APIUtils.QuickMenuInstance.transform.parent.gameObject;
			}
			return APIUtils._userInterface;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00017493 File Offset: 0x00015693
		public static GameObject GetQMButtonTemplate()
		{
			if (APIUtils._qmButtonTemplate == null)
			{
				APIUtils._qmButtonTemplate = APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds").gameObject;
			}
			return APIUtils._qmButtonTemplate;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000174C5 File Offset: 0x000156C5
		public static GameObject GetQMMenuTemplate()
		{
			if (APIUtils._qmMenuTemplate == null)
			{
				APIUtils._qmMenuTemplate = APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard").gameObject;
			}
			return APIUtils._qmMenuTemplate;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000174F7 File Offset: 0x000156F7
		public static GameObject GetQMTabButtonTemplate()
		{
			if (APIUtils._qmTabTemplate == null)
			{
				APIUtils._qmTabTemplate = APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
			}
			return APIUtils._qmTabTemplate;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00017529 File Offset: 0x00015729
		public static Sprite OnIconSprite()
		{
			if (APIUtils._onSprite == null)
			{
				APIUtils._onSprite = APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<ImageEx>().sprite;
			}
			return APIUtils._onSprite;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00017560 File Offset: 0x00015760
		public static Sprite OffIconSprite()
		{
			if (APIUtils._offSprite == null)
			{
				APIUtils._offSprite = APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo/Icon_Off").GetComponent<ImageEx>().sprite;
			}
			return APIUtils._offSprite;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00017597 File Offset: 0x00015797
		public static int RandomNumbers()
		{
			return APIUtils.rnd.Next(100000, 999999);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000175B0 File Offset: 0x000157B0
		public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				if (exclude == null || exclude(transform.GetChild(i)))
				{
					Object.DestroyImmediate(transform.GetChild(i).gameObject);
				}
			}
		}

		// Token: 0x0400022C RID: 556
		private static Sprite _onSprite;

		// Token: 0x0400022D RID: 557
		private static Sprite _offSprite;

		// Token: 0x0400022E RID: 558
		internal const string Identifier = "DClient";

		// Token: 0x0400022F RID: 559
		private static readonly Random rnd = new Random();

		// Token: 0x04000230 RID: 560
		private static QuickMenu _quickMenu;

		// Token: 0x04000231 RID: 561
		private static MenuStateController _menuStateController;

		// Token: 0x04000232 RID: 562
		private static VRCUiPopupManager _vrcUiPopupManager;

		// Token: 0x04000233 RID: 563
		private static GameObject _userInterface;

		// Token: 0x04000234 RID: 564
		public static GameObject _userInterface2;

		// Token: 0x04000235 RID: 565
		private static GameObject _qmButtonTemplate;

		// Token: 0x04000236 RID: 566
		private static GameObject _qmMenuTemplate;

		// Token: 0x04000237 RID: 567
		private static GameObject _qmTabTemplate;
	}
}
