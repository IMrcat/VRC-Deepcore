using System;
using System.Reflection;
using HarmonyLib;
using VRC.Core;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000019 RID: 25
	internal class ClonePatch
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00004F78 File Offset: 0x00003178
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), new HarmonyMethod(typeof(ClonePatch).GetMethod("Hook", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static void Hook(ref bool __0)
		{
			__0 = true;
		}
	}
}
