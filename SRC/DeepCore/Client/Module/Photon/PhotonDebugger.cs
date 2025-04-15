using System;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Module.Photon
{
	// Token: 0x0200004B RID: 75
	internal class PhotonDebugger
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000BD70 File Offset: 0x00009F70
		public static bool OnEventSent(byte code, object data, RaiseEventOptions options, SendOptions sendOptions)
		{
			DeepConsole.LogConsole("Photon:OnEventSent", "----------------------");
			DeepConsole.LogConsole("Photon:OnEventSent", string.Format("Code:{0}", code));
			DeepConsole.LogConsole("Photon:OnEventSent", string.Format("Data:{0}", data));
			DeepConsole.LogConsole("Photon:OnEventSent", string.Format("Data:{0}", data));
			DeepConsole.LogConsole("Photon:OnEventSent", string.Format("RaiseEventOptions:{0}", options));
			DeepConsole.LogConsole("Photon:OnEventSent", string.Format("SendOptions:{0}", sendOptions));
			DeepConsole.LogConsole("Photon:OnEventSent", "----------------------");
			return true;
		}

		// Token: 0x040000D9 RID: 217
		public static bool IsOnEventSendDebug;
	}
}
