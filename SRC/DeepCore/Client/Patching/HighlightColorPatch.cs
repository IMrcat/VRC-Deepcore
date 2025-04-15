using System;
using Harmony;
using UnityEngine;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001C RID: 28
	internal class HighlightColorPatch
	{
		// Token: 0x020000B9 RID: 185
		[HarmonyPatch(typeof(HighlightsFXStandalone), "Awake")]
		[Obsolete]
		private class HighlightsFXStandalonePatch
		{
			// Token: 0x060003EF RID: 1007 RVA: 0x00017E70 File Offset: 0x00016070
			private static void Postfix(HighlightsFXStandalone __instance)
			{
				__instance.highlightColor = Color.white;
			}
		}
	}
}
