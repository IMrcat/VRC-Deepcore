using System;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;

namespace DeepCore.Client.ClientMenu
{
	// Token: 0x0200009A RID: 154
	public class MenuBuilder
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00013A10 File Offset: 0x00011C10
		internal static void MenuStart()
		{
			DeepConsole.Log("ClientUI", "Initializing UI...");
			MenuBuilder._uiManager = new UiManager("DeepClient", SpriteManager.clientIcon, true, true, false, "#ffffff");
			LaunchPad_Menu.InitLaunchPadMenu(MenuBuilder._uiManager);
			Target_Menu.InitMainMenu(MenuBuilder._uiManager);
			Main_Menu.InitMainMenu(MenuBuilder._uiManager);
		}

		// Token: 0x040001FA RID: 506
		internal static UiManager _uiManager;
	}
}
