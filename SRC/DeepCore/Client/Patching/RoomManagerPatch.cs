using System;
using DeepCore.Client.Module;
using MelonLoader;
using VRC.Core;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000020 RID: 32
	internal class RoomManagerPatch
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00005A24 File Offset: 0x00003C24
		public static void Patch()
		{
			EasyPatching.EasyPatchMethodPost(typeof(RoomManager), "Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0", typeof(RoomManagerPatch), "EnterWorldPatch");
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005A4C File Offset: 0x00003C4C
		private static void EnterWorldPatch(ApiWorld __0, ApiWorldInstance __1)
		{
			if (__0 != null && __1 != null)
			{
				DeepConsole.Log("RoomManager", string.Concat(new string[]
				{
					"Joining ",
					RoomManager.field_Internal_Static_ApiWorld_0.name,
					" by ",
					RoomManager.field_Internal_Static_ApiWorld_0.authorName,
					"..."
				}));
				MelonCoroutines.Start(OnLoadedScaneManager.WaitForLocalPlayer());
				if (ConfManager.AntiAvatarScallingdisabler.Value && __0.tags.Contains("feature_avatar_scaling_disabled"))
				{
					__0.tags.Remove("feature_avatar_scaling_disabled");
				}
			}
		}
	}
}
