using System;
using DeepCore.Client.Module.Exploits;
using DeepCore.Client.Module.Movement;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x0200002B RID: 43
	internal class PickupMenu
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00008C6C File Offset: 0x00006E6C
		public static void PickupsMenu(ReMenuPage reCategoryPage)
		{
			ReCategoryPage reCategoryPage2 = reCategoryPage.AddCategoryPage("Pickups Menu", "", null, "#ffffff");
			reCategoryPage2.AddCategory("Exploits");
			ReMenuCategory category = reCategoryPage2.GetCategory("Exploits");
			category.AddToggle("Beyblade Pickups", "", delegate(bool s)
			{
				BeyBladePickup.State(s);
			});
			category.AddToggle("Item 2 Click", "", delegate(bool s)
			{
				RayCastTP.Pickup2Click = s;
			});
			category.AddToggle("Pen Crash", "", delegate(bool s)
			{
				PenClapper.State(s);
			});
			category.AddButton("Respawn All", "Respawn all pickups.", delegate
			{
				PickupUtils.Respawn();
			}, null, "#ffffff");
			category.AddButton("Bring to me", "Bring all pickups to you.", delegate
			{
				PickupUtils.BringPickups();
			}, null, "#ffffff");
		}
	}
}
