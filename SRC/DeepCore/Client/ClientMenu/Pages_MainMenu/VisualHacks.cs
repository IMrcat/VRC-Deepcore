using System;
using DeepCore.Client.GUI;
using DeepCore.Client.Module.Visual;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A9 RID: 169
	internal class VisualHacks
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00016F48 File Offset: 0x00015148
		public static void VisualSettingsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Visuals Functions", null, null, "#ffffff");
			reCategoryPage.AddCategory("InGame ESP");
			ReMenuCategory category = reCategoryPage.GetCategory("InGame ESP");
			category.AddToggle("Object ESP", "Allow you to see all pickups.", delegate(bool s)
			{
				ESP.ObjectState(s);
			});
			category.AddToggle("Capsules ESP", "Allow you to see all players.", delegate(bool s)
			{
				ESP.CapsuleState(s);
			});
			category.AddToggle("Udon ESP", "Allow you to see all udon.", delegate(bool s)
			{
				ESP.UdonState(s);
			});
			category.AddToggle("Line ESP", "Allow you to see line from you to player.", delegate(bool s)
			{
				DeepCore.Client.Module.Visual.LineESP.LineState(s);
			});
			category.AddToggle("Box ESP", "Allow you to see box from you to player.", delegate(bool s)
			{
				BoxESP.BoxState(s);
			});
			reCategoryPage.AddCategory("OnGUI ESP");
			reCategoryPage.GetCategory("OnGUI ESP").AddToggle("GUILine ESP", "Allow you to see line from you to player ongui.", delegate(bool s)
			{
				DeepCore.Client.GUI.LineESP.LineState(s);
			});
			reCategoryPage.AddCategory("Others");
			ReMenuCategory category2 = reCategoryPage.GetCategory("Others");
			category2.AddToggle("Optifine Zoom", "Hold ALT to zoom.", delegate(bool s)
			{
				ConfManager.OptifineZoom.Value = s;
				MelonPreferences.Save();
			});
			category2.AddToggle("SelfHide", "Allow you to hide yourself (if you use crash avatar).", delegate(bool s)
			{
				SelfHide.selfhidePlayer(s);
			});
			category2.AddToggle("Flashlight", "Allow you to see in the dark.", delegate(bool s)
			{
				Flashlight.State(s);
			});
			category2.AddToggle("FlipScreen", "Allow you to rotate your pc cam by holding [R-CTRL + Scrolling].", delegate(bool s)
			{
				FlipScreen.IsEnabled = s;
			});
		}
	}
}
