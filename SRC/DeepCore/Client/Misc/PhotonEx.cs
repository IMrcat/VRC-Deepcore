using System;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007E RID: 126
	internal class PhotonEx
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public static void SendChatBoxMessage(string message)
		{
			PhotonUtil.OpRaiseEvent(43, message, new RaiseEventOptions
			{
				field_Public_EventCaching_0 = 0,
				field_Public_ReceiverGroup_0 = 0
			}, default(SendOptions));
		}
	}
}
