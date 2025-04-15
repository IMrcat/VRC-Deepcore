using System;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x0200002E RID: 46
	internal class WorldHackMainMenu
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000910B File Offset: 0x0000730B
		public static void WorldHacksMenu(UiManager UIManager)
		{
			ReMenuPage reMenuPage = UIManager.QMMenu.AddMenuPage("WorldHacks Functions", null, null, "#ffffff");
			AmongUs.AmongusMenu(reMenuPage);
			Murder4.Murder4Menu(reMenuPage);
			JustBClub3.JBC3Menu(reMenuPage);
			PickupMenu.PickupsMenu(reMenuPage);
		}
	}
}
