using System;
using System.Linq;
using System.Reflection;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001F RID: 31
	internal class OnAvatarChangedPatch
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000058A0 File Offset: 0x00003AA0
		public static void Patch()
		{
			(from m in typeof(VRCPlayer).GetMethods()
				where m.Name.StartsWith("Method_Private_Void_GameObject_VRC_AvatarDescriptor_")
				select m).ToList<MethodInfo>().ForEach(delegate(MethodInfo m)
			{
				EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlayer).GetMethod(m.Name), EasyPatching.GetLocalPatch<OnAvatarChangedPatch>("OnAvaLoaded"), null, null, null, null);
			});
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000590C File Offset: 0x00003B0C
		[Obsolete]
		internal static void OnAvaLoaded(GameObject __0, VRC_AvatarDescriptor __1, VRCPlayer __instance)
		{
			try
			{
				if (ConfManager.avatarLogging.Value)
				{
					Player player = __instance._player;
					string text = string.Concat(new string[]
					{
						"\nUser: (",
						player.field_Private_VRCPlayerApi_0.displayName,
						")\nswitched to avatar: ",
						player.Method_Public_get_ApiAvatar_PDM_0().name,
						"(",
						player.Method_Public_get_ApiAvatar_PDM_0().id,
						")\nAuthor: (",
						player.Method_Public_get_ApiAvatar_PDM_0().authorName,
						") \nCreated: (",
						player.Method_Public_get_ApiAvatar_PDM_0().created_at.ToString(),
						") \nLast Updated: (",
						player.Method_Public_get_ApiAvatar_PDM_0().updated_at.ToString(),
						")"
					});
					if (!(OnAvatarChangedPatch.last_log == text))
					{
						OnAvatarChangedPatch.last_log = text;
						AvatarLoggerHandler.Log(player.Method_Public_get_ApiAvatar_PDM_0());
						DeepConsole.LogConsole("AvaLogger", text);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000059 RID: 89
		private static string last_log;
	}
}
