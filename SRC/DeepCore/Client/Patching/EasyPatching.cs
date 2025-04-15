using System;
using System.Reflection;
using Harmony;
using HarmonyLib;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001A RID: 26
	internal class EasyPatching
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00004FD5 File Offset: 0x000031D5
		public static void EasyPatchPropertyPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, null, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004FFA File Offset: 0x000031FA
		public static void EasyPatchPropertyPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000501F File Offset: 0x0000321F
		public static void EasyPatchMethodPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null, null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005045 File Offset: 0x00003245
		public static void EasyPatchMethodPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), null, new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000506B File Offset: 0x0000326B
		[Obsolete]
		internal static HarmonyMethod GetLocalPatch<T>(string name)
		{
			return new HarmonyMethod(typeof(T).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
		}

		// Token: 0x04000053 RID: 83
		public static global::HarmonyLib.Harmony DeepCoreInstance = new global::HarmonyLib.Harmony("DeePatch");
	}
}
