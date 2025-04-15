using System;
using System.Linq;
using System.Reflection;
using DeepCore.Client.Module.Photon;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001E RID: 30
	internal class LoadBalancingClientPatch
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00005824 File Offset: 0x00003A24
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(LoadBalancingClient).GetMethods().LastOrDefault((MethodInfo x) => x.Name.Equals("OnEvent")), new HarmonyMethod(AccessTools.Method(typeof(LoadBalancingClientPatch), "OnEvent", null, null)), null, null, null, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000588E File Offset: 0x00003A8E
		internal static bool OnEvent(EventData param_1)
		{
			return PhtonManagerUtils.PhotonEvent(param_1);
		}
	}
}
