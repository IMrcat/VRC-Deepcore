using System;
using DeepCore.Client.ClientMenu.Pages_MainMenu;
using DeepCore.Client.Module.ApplicationBot;
using DeepCore.Client.Module.WorldHacks;
using ReMod.Core.Managers;

namespace DeepCore.Client.ClientMenu
{
	// Token: 0x02000099 RID: 153
	internal class Main_Menu
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00013940 File Offset: 0x00011B40
		public static void InitMainMenu(UiManager UIManager)
		{
			Main_Menu._uiManager = UIManager;
			DeepConsole.Log("ClientUI", "Initializing Main Menu...");
			VisualHacks.VisualSettingsMenu(Main_Menu._uiManager);
			MovementHacks.MovementHacksMenu(Main_Menu._uiManager);
			WorldHackMainMenu.WorldHacksMenu(Main_Menu._uiManager);
			ExploitHacks.ExploitHacksMenu(Main_Menu._uiManager);
			UtilFunctions.UtilFunctionsMenu(Main_Menu._uiManager);
			AppBotMenu.APBFunctionsMenu(Main_Menu._uiManager);
			LastSeenAvatars.LoggedAvatarsMenu(Main_Menu._uiManager);
			InstaceHistory.InstaceHistoryHacksMenu(Main_Menu._uiManager);
			ItemManipulator.ManipulatorHacksMenu(Main_Menu._uiManager);
			MediaControl.MediaControlMenu(Main_Menu._uiManager);
			ClientChat.ClientChatMenu(Main_Menu._uiManager);
			ClientSettings.ClientSettingsMenu(Main_Menu._uiManager);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000139DC File Offset: 0x00011BDC
		public static void AddSpacers()
		{
			for (int i = 0; i < 8; i++)
			{
				Main_Menu._uiManager.QMMenu.AddSpacer(null);
			}
		}

		// Token: 0x040001F9 RID: 505
		private static UiManager _uiManager;
	}
}
