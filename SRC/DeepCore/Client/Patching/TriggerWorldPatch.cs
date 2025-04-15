using System;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000021 RID: 33
	internal class TriggerWorldPatch
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00005AF1 File Offset: 0x00003CF1
		[Obsolete]
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(VRC_EventHandler).GetMethod("InternalTriggerEvent"), EasyPatching.GetLocalPatch<TriggerWorldPatch>("OnTriggerWorld"), null, null, null, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B20 File Offset: 0x00003D20
		internal static bool OnTriggerWorld(ref VRC_EventHandler.VrcEvent __0, ref VRC_EventHandler.VrcBroadcastType __1, ref int __2)
		{
			try
			{
				if (ConfManager.blockWorldTriggers.Value)
				{
					__1 = 4;
				}
			}
			catch
			{
			}
			return true;
		}
	}
}
