using System;
using DeepCore.Client.ClientMenu;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.QOL;
using MelonLoader;
using UnityEngine;
using VRC.Core;

namespace DeepCore.Client.UI.QM
{
	// Token: 0x02000010 RID: 16
	internal class QMUI
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000041AC File Offset: 0x000023AC
		public static void InitQM()
		{
			VrcExtensions.SetQmDashbordPageTittle(ClientUtils.GetGreeting() + "!");
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer").SetActive(false);
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").SetActive(false);
			Object.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlus"));
			MenuBuilder.MenuStart();
			ThirdPersonView.OnStart();
			MelonCoroutines.Start(QMConsole.StartConsole());
			MelonCoroutines.Start(QMPlayerList.StartPlayerList());
			if (ConfManager.ShouldMenuMusic.Value)
			{
				MelonCoroutines.Start(MenuMusic.MenuMusicInit());
			}
			DeepConsole.Log("Startup", "Welcome Back, " + APIUser.CurrentUser.displayName + ".");
			VrcExtensions.Toast("DeepClient", "Welcome Back, " + APIUser.CurrentUser.displayName + ".", null, 5f);
			VrcExtensions.HudNotif("Why i'm still fixing it ?...");
		}
	}
}
