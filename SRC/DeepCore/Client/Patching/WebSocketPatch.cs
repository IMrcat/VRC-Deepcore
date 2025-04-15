using System;
using WebSocketSharp;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000018 RID: 24
	internal class WebSocketPatch
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00004F1D File Offset: 0x0000311D
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(WebSocket).GetMethod("messagec"), EasyPatching.GetLocalPatch<UdonSyncPatch>("OnMSG"), null, null, null, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004F4C File Offset: 0x0000314C
		public static void OnMSGPatch(IntPtr instance, IntPtr __0)
		{
			DeepConsole.LogConsole("Module : WebSocket", string.Format("{0}:{1}", instance, __0));
		}
	}
}
