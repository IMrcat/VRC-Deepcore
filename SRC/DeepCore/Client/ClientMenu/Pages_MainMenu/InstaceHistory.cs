using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A0 RID: 160
	internal class InstaceHistory
	{
		// Token: 0x06000396 RID: 918 RVA: 0x000159DC File Offset: 0x00013BDC
		internal static void InstaceHistoryHacksMenu(UiManager uiManager)
		{
			InstaceHistory._uiManager = uiManager;
			InstaceHistory._InstanceHistoryMenu = InstaceHistory._uiManager.QMMenu.AddCategoryPage("Instance History", "Instance History", null, "#ffffff");
			InstaceHistory._InstanceHistoryMenu.OnOpen += InstaceHistory.instanceAction;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015A2C File Offset: 0x00013C2C
		private static void instanceAction()
		{
			if (InstaceHistory.tempPage != 0)
			{
				InstaceHistory._InstanceHistory.Active = false;
			}
			int num = ++InstaceHistory.tempPage;
			InstaceHistory._InstanceHistoryMenu.AddCategory("World History (" + num.ToString() + ")");
			InstaceHistory._InstanceHistory = InstaceHistory._InstanceHistoryMenu.GetCategory("World History (" + num.ToString() + ")");
			if (WorldLoggerHandler.Fetch() == null && WorldLoggerHandler.Fetch().Count > 0)
			{
				return;
			}
			List<World_Object> list = WorldLoggerHandler.Fetch();
			list.Reverse();
			int num2 = 1;
			using (IEnumerator<World_Object> enumerator = ((IEnumerable<World_Object>)list).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					World_Object world_object = enumerator.Current;
					num2++;
					InstaceHistory._InstanceHistory.AddButton(world_object.worldName, string.Concat(new string[] { world_object.worldName, " - ", world_object.worldID, ":", world_object.instanceName, " Type:(", world_object.instanceType, ")." }), delegate
					{
						Networking.GoToRoom(world_object.instanceID);
					}, null, "#ffffff");
				}
			}
		}

		// Token: 0x04000221 RID: 545
		private static UiManager _uiManager;

		// Token: 0x04000222 RID: 546
		private static ReCategoryPage _InstanceHistoryMenu;

		// Token: 0x04000223 RID: 547
		private static ReMenuCategory _InstanceHistory;

		// Token: 0x04000224 RID: 548
		private static int tempPage;
	}
}
