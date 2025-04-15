using System;
using DeepCore.Client.Misc;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Module.Photon
{
	// Token: 0x0200004A RID: 74
	internal class MovementSerilize
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public static void State()
		{
			MovementSerilize.IsEnabled = !MovementSerilize.IsEnabled;
			if (MovementSerilize.IsEnabled)
			{
				DeepConsole.Log("Photon", "Serilizer is enabled.");
				VrcExtensions.HudNotif("Serilizer is enabled.");
				return;
			}
			MovementSerilize.IsEnabled = false;
			DeepConsole.Log("Photon", "Serilizer is disabled.");
			VrcExtensions.HudNotif("Serilizer is disabled.");
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000BD52 File Offset: 0x00009F52
		public static bool OnEventSent(byte code, object data, RaiseEventOptions options, SendOptions sendOptions)
		{
			return code != 12 || !MovementSerilize.IsEnabled;
		}

		// Token: 0x040000D8 RID: 216
		public static bool IsEnabled;
	}
}
