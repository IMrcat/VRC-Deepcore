using System;
using DeepCore.Client.Misc;
using ExitGames.Client.Photon;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using VRC;

namespace DeepCore.Client.Module.Photon
{
	// Token: 0x02000049 RID: 73
	internal class ModerationNotice
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static bool OnEventPatch(EventData __0)
		{
			if (__0.Code == 33)
			{
				try
				{
					Dictionary<byte, Object> dictionary = __0.Parameters[__0.CustomDataKey].Cast<Dictionary<byte, Object>>();
					if (dictionary[0].Unbox<byte>() == 21 && dictionary.ContainsKey(1))
					{
						bool flag = dictionary[10].Unbox<bool>();
						bool flag2 = dictionary[11].Unbox<bool>();
						Player playerNewtworkedId = dictionary[1].Unbox<int>().GetPlayerNewtworkedId();
						if (playerNewtworkedId != null)
						{
							if (flag)
							{
								VrcExtensions.HudNotif(playerNewtworkedId.field_Private_APIUser_0.displayName + " has blocked you.");
								PlayerUtil.knownBlocks.Add(playerNewtworkedId.field_Private_APIUser_0.displayName);
								return false;
							}
							if (flag2)
							{
								VrcExtensions.HudNotif(playerNewtworkedId.field_Private_APIUser_0.displayName + " has muted you.");
								PlayerUtil.knownMutes.Add(playerNewtworkedId.field_Private_APIUser_0.displayName);
								return false;
							}
							if (PlayerUtil.knownMutes.Contains(playerNewtworkedId.field_Private_APIUser_0.displayName) && playerNewtworkedId.field_Private_APIUser_0.displayName != PlayerUtil.GetLocalVRCPlayer().field_Private_VRCPlayerApi_0.displayName)
							{
								PlayerUtil.knownMutes.Remove(playerNewtworkedId.field_Private_APIUser_0.displayName);
								VrcExtensions.HudNotif(playerNewtworkedId.field_Private_APIUser_0.displayName + " has unmuted you.");
							}
							if (PlayerUtil.knownBlocks.Contains(playerNewtworkedId.field_Private_APIUser_0.displayName) && playerNewtworkedId.field_Private_APIUser_0.displayName != PlayerUtil.GetLocalVRCPlayer().field_Private_VRCPlayerApi_0.displayName)
							{
								PlayerUtil.knownBlocks.Remove(playerNewtworkedId.field_Private_APIUser_0.displayName);
								VrcExtensions.HudNotif(playerNewtworkedId.field_Private_APIUser_0.displayName + " has unblocked you.");
							}
						}
					}
				}
				catch
				{
				}
				return true;
			}
			return true;
		}
	}
}
