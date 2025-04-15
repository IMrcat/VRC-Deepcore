using System;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A2 RID: 162
	internal class MediaControl
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00015D9C File Offset: 0x00013F9C
		public static void MediaControlMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Media Controls", null, null, "#ffffff");
			reCategoryPage.AddCategory("(my computer)");
			ReMenuCategory category = reCategoryPage.GetCategory("(my computer)");
			category.AddButton("Play/Pause", "Play/Pause current playing media.", delegate
			{
				MediaKeys.PlayPause();
			}, null, "#ffffff");
			category.AddButton("Prev Song", "Return to the previous song.", delegate
			{
				MediaKeys.PrevTrack();
			}, null, "#ffffff");
			category.AddButton("Next Song", "Go to the next song.", delegate
			{
				MediaKeys.NextTrack();
			}, null, "#ffffff");
			category.AddButton("Stop", "Stop current playing media.", delegate
			{
				MediaKeys.Stop();
			}, null, "#ffffff");
			category.AddButton("Volume Up", "Let me goo like ssssukkkaaa blyat.", delegate
			{
				MediaKeys.VolumeUp();
			}, null, "#ffffff");
			category.AddButton("Volume Down", "Ewwwwww.", delegate
			{
				MediaKeys.VolumeDown();
			}, null, "#ffffff");
			category.AddButton("Mute", "Silence.", delegate
			{
				MediaKeys.VolumeMute();
			}, null, "#ffffff");
		}
	}
}
