using System;
using VRC;
using VRC.Networking;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000016 RID: 22
	internal class UdonSyncPatch
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004E0E File Offset: 0x0000300E
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), EasyPatching.GetLocalPatch<UdonSyncPatch>("OnUdon"), null, null, null, null);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004E3D File Offset: 0x0000303D
		internal static bool OnUdon(string __0, Player __1)
		{
			if (ConfManager.udonLogger.Value)
			{
				DeepConsole.Log("UdonSync", "Type: " + __0 + " | From " + __1.field_Private_VRCPlayerApi_0.displayName);
			}
			return !ConfManager.antiUdon.Value;
		}
	}
}
