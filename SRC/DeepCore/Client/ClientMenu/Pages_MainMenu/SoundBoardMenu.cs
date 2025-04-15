using System;
using DeepCore.Client.Module.QOL;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A3 RID: 163
	internal class SoundBoardMenu
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00015F58 File Offset: 0x00014158
		public static void SBMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Networking SoundBoard", null, null, "#ffffff");
			reCategoryPage.AddCategory("Available Sounds");
			ReMenuCategory category = reCategoryPage.GetCategory("Available Sounds");
			category.AddButton("Capybara", "", delegate
			{
				UdonSoundBoard.SendSound("DCCapybara");
			}, null, "#ffffff");
			category.AddButton("Test", "", delegate
			{
				UdonSoundBoard.SendSound("DC");
			}, null, "#ffffff");
		}
	}
}
