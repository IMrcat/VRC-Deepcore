using System;
using System.Collections;
using DeepCore.Client.Misc;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000012 RID: 18
	internal class GameVersionSpoofer
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000042E8 File Offset: 0x000024E8
		public static IEnumerator Init()
		{
			while (VRCApplication.field_Private_Static_VRCApplication_0 == null)
			{
				yield return null;
			}
			VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_Int32_0 = 1609;
			VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_0 = "2025.1.3p3";
			VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_1 = "Release_1343";
			DeepConsole.Log("Spoofer", string.Format("Spoofed VRChat Build {0} | {1} | {2}.", VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_Int32_0, VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_0, VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_1));
			ERPChecker.IsMyUserERping();
			yield break;
		}
	}
}
