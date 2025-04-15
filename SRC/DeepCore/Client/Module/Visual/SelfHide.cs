using System;
using DeepCore.Client.Misc;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000036 RID: 54
	internal class SelfHide
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00009D68 File Offset: 0x00007F68
		internal static void selfhidePlayer(bool s)
		{
			if (VrcExtensions.GetLocalPlayer() == null)
			{
				return;
			}
			if (s && VRCPlayer.field_Internal_Static_VRCPlayer_0._player.Method_Public_get_ApiAvatar_PDM_0().id != null)
			{
				SelfHide.backupid = VRCPlayer.field_Internal_Static_VRCPlayer_0._player.Method_Public_get_ApiAvatar_PDM_0().id;
			}
			VrcExtensions.GetLocalPlayer().transform.Find("ForwardDirection").gameObject.active = !s;
		}

		// Token: 0x04000096 RID: 150
		private static string backupid;
	}
}
