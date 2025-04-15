using System;
using HarmonyLib;
using UnityEngine;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000015 RID: 21
	[HarmonyPatch(typeof(Time), "smoothDeltaTime", 1)]
	internal class TimePatch
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004DFD File Offset: 0x00002FFD
		private static void Postfix(ref float __result)
		{
			__result = 10f;
		}
	}
}
