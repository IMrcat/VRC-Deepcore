using System;
using System.Windows.Forms;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.Exploits;
using DeepCore.Client.Module.Movement;
using DeepCore.Client.Module.Photon;
using DeepCore.Client.Module.WorldHacks;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;

namespace DeepCore.Client.ClientMenu
{
	// Token: 0x0200009B RID: 155
	internal class Target_Menu
	{
		// Token: 0x06000362 RID: 866 RVA: 0x00013A6E File Offset: 0x00011C6E
		public static void InitMainMenu(UiManager UIManager)
		{
			Target_Menu._uiManager = UIManager;
			DeepConsole.Log("ClientUI", "Initializing Target Menu...");
			IButtonPage targetMenu = Target_Menu._uiManager.TargetMenu;
			Target_Menu.UserFunctionMenu(UIManager);
			Murder4.Murder4TargetMenu(UIManager);
			AmongUs.AmongTargetMenu(UIManager);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013AA4 File Offset: 0x00011CA4
		public static void UserFunctionMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.TargetMenu.AddCategoryPage("User functions", "", null, "#ffffff");
			reCategoryPage.AddCategory("User Info");
			ReMenuCategory category = reCategoryPage.GetCategory("User Info");
			category.AddButton("Copy Name", "Copy selected player name to you clipboard.", delegate
			{
				Clipboard.SetText(VrcExtensions.QM_GetSelectedUserName());
			}, null, "#ffffff");
			category.AddButton("Copy UserID", "Copy selected player avatar to you clipboard.", delegate
			{
				Clipboard.SetText(PlayerExtensions.GetVRCPlayer()._player.Method_Internal_get_APIUser_0().id);
			}, null, "#ffffff");
			category.AddButton("Get avatar info", "Copy selected player avatar to you clipboard.", delegate
			{
				VrcExtensions.LogAvatar(PlayerExtensions.GetVRCPlayer());
			}, null, "#ffffff");
			reCategoryPage.AddCategory("User Exploits");
			ReMenuCategory category2 = reCategoryPage.GetCategory("User Exploits");
			category2.AddButton("Teleport", "Teleport to the selected user.", delegate
			{
				VrcExtensions.TpToPlayer(PlayerExtensions.GetVRCPlayer());
			}, null, "#ffffff");
			category2.AddButton("Silent\nTeleport", "Will teleport you on the target without them seeing you.", delegate
			{
				MovementSerilize.State();
				VrcExtensions.Toast("DeepClient", "MovementSerilize enabled.", null, 5f);
				VrcExtensions.TpToPlayer(PlayerExtensions.GetVRCPlayer());
			}, null, "#ffffff");
			category2.AddButton("Force clone", "wtf ass clone.", delegate
			{
				VrcExtensions.ChangeAvatar(PlayerExtensions.GetVRCPlayer().field_Private_ApiAvatar_0.id);
			}, null, "#ffffff");
			category2.AddButton("Force lewd", "...", delegate
			{
				ForceLewd.LewdPlayer(PlayerExtensions.GetVRCPlayer());
			}, null, "#ffffff");
			category2.AddButton("Reupload\nAvatar", "...", delegate
			{
				AviYoinker.askbeforeyoink();
			}, null, "#ffffff");
			category2.AddToggle("Camera Sounds", "", delegate(bool s)
			{
				CamSoundSpammer.State(s);
			});
			category2.AddToggle("Spy Camera", "Create a camera to spy on the player.", delegate(bool s)
			{
				SpyCamera.Toggle(s);
			});
			category2.AddToggle("Hear Pleayer", "Allow you to hear the player when he is aways from you.", delegate(bool s)
			{
				VrcExtensions.ListenPlayer(PlayerExtensions.GetVRCPlayer(), s);
			});
			category2.AddToggle("Item Orbit", "Orbit player with items.", delegate(bool s)
			{
				ItemOrbit.State(s);
			});
			category2.AddToggle("卍 Orbit", "Orbit player with 卍.", delegate(bool s)
			{
				SwasticaOrbit.State(s);
			});
			category2.AddToggle("Seat OnHead", "Allows you to seat on current player head.", delegate(bool s)
			{
				SeatOnHead.State(s);
			});
		}

		// Token: 0x040001FB RID: 507
		private static UiManager _uiManager;
	}
}
