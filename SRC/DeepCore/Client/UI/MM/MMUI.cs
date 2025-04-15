using System;
using DeepCore.Client.Module.Exploits;
using ReMod.Core.UI.MainMenu;
using ReMod.Core.VRChat;

namespace DeepCore.Client.UI.MM
{
	// Token: 0x02000011 RID: 17
	internal class MMUI
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00004294 File Offset: 0x00002494
		public static void WaitForMM()
		{
			new ReMMUserButton("Invite x10", "Invite x10.", delegate
			{
				InvSpammer.Spasm();
			}, null, MMenuPrefabs.MMUserDetailButton.transform.parent);
		}
	}
}
