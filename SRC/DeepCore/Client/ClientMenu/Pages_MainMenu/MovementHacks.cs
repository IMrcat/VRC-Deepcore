using System;
using DeepCore.Client.Module.Movement;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A7 RID: 167
	internal class MovementHacks
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00016A24 File Offset: 0x00014C24
		public static void MovementHacksMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Movements Functions", null, null, "#ffffff");
			reCategoryPage.AddCategory("Functions");
			ReMenuCategory category = reCategoryPage.GetCategory("Functions");
			category.AddToggle("Jetpack", "I discovered the jetpack.", delegate(bool s)
			{
				Jetpack.Jetpackbool = s;
			});
			category.AddButton("Flight/Noclip", "Allow you to fly like a bird.", delegate
			{
				Flight.FlyToggle();
			}, null, "#ffffff");
			category.AddToggle("SpinBot", "Spin like a blablade.", delegate(bool s)
			{
				SpinBot.SpinBotbool = s;
			});
			category.AddToggle("RayCast TP", "Hold ctrl and RClick.", delegate(bool s)
			{
				RayCastTP.Enabled = s;
			});
			category.AddToggle("HeadFlipper", "Make your head flipping.", delegate(bool s)
			{
				HeadFlipper.state(s);
			});
			category.AddToggle("PosSaver", "Allow you to save your currnt pos and reset it when disabled.", delegate(bool s)
			{
				PosSaver.State(s);
			});
			category.AddToggle("Force Jump", "Allow you to jump in game worlds.", delegate(bool s)
			{
				ForceJump.State(s);
			});
			category.AddButton("T-Pose", "Allow you to T-Pose.", delegate
			{
				TPose.State();
			}, null, "#ffffff");
			reCategoryPage.AddSliderCategory("Movements Settings");
			ReMenuSliderCategory sliderCategory = reCategoryPage.GetSliderCategory("Movements Settings");
			sliderCategory.AddSlider("Flight Speed", "Set walk speed.", delegate(float s)
			{
				Flight.FlySpeed = s;
			}, 10f, 10f, 50f, "#ffffff");
			sliderCategory.AddSlider("SpinBot Speed", "Set spinbot speed.", delegate(float s)
			{
				SpinBot.rotationSpeed = s;
			}, 120f, 120f, 390f, "#ffffff");
			sliderCategory.AddSlider("Walk Speed", "Set walk speed.", delegate(float s)
			{
				Networking.LocalPlayer.SetWalkSpeed(s);
			}, 2f, 2f, 15f, "#ffffff");
			sliderCategory.AddSlider("Run Speed", "Set run speed.", delegate(float s)
			{
				Networking.LocalPlayer.SetRunSpeed(s);
			}, 4f, 2f, 20f, "#ffffff");
			sliderCategory.AddSlider("Strafe Speed", "Set strafe speed.", delegate(float s)
			{
				Networking.LocalPlayer.SetStrafeSpeed(s);
			}, 4f, 2f, 20f, "#ffffff");
		}
	}
}
