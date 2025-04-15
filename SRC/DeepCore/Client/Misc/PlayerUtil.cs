using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore.Client.Module.ApplicationBot;
using Il2CppSystem.Collections.Generic;
using ReMod.Core.VRChat;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000085 RID: 133
	public static class PlayerUtil
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000118AC File Offset: 0x0000FAAC
		public static bool IsInVR
		{
			get
			{
				bool flag;
				if (PlayerUtil._cachedVRState)
				{
					flag = PlayerUtil._isInVrCache;
				}
				else
				{
					PlayerUtil._cachedVRState = true;
					List<XRDisplaySubsystem> list = new List<XRDisplaySubsystem>();
					SubsystemManager.GetInstances<XRDisplaySubsystem>(list);
					List<XRDisplaySubsystem>.Enumerator enumerator = list.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.running)
						{
							PlayerUtil._isInVrCache = true;
							break;
						}
					}
					flag = PlayerUtil._isInVrCache;
				}
				return flag;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011908 File Offset: 0x0000FB08
		public static ApiAvatar GetAvatarInfo(this Player player)
		{
			if (!(player != null))
			{
				return null;
			}
			return player.Method_Public_get_ApiAvatar_PDM_0();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001191B File Offset: 0x0000FB1B
		public static bool IsInRoom
		{
			get
			{
				return RoomManager.field_Internal_Static_ApiWorld_0 != null && RoomManager.field_Private_Static_ApiWorldInstance_0 != null;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011934 File Offset: 0x0000FB34
		public static APIUser fetchbyid(string id)
		{
			APIUser usr = null;
			APIUser.FetchUser(id, delegate(APIUser a)
			{
				usr = a;
			}, delegate(string a)
			{
				DeepConsole.Log("IDFetcher", a);
			});
			return usr;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011990 File Offset: 0x0000FB90
		public static int GetPlayerRank(VRCPlayer player)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			foreach (string text in player._player.field_Private_APIUser_0.tags)
			{
				if (text.Contains("basic"))
				{
					flag = true;
				}
				else if (text.Contains("known"))
				{
					flag2 = true;
				}
				else if (text.Contains("trusted"))
				{
					flag3 = true;
				}
				else if (text.Contains("veteran"))
				{
					flag4 = true;
				}
				else if (text.Contains("troll"))
				{
					flag5 = true;
				}
			}
			if (player._player.field_Private_APIUser_0.isFriend)
			{
				return 6;
			}
			if (flag5)
			{
				return 5;
			}
			if (flag4)
			{
				return 4;
			}
			if (flag3)
			{
				return 3;
			}
			if (flag2)
			{
				return 2;
			}
			if (flag)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011A5B File Offset: 0x0000FC5B
		internal static bool AnyActionMenuesOpen()
		{
			return ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_0.field_Private_Boolean_0 || ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_1.field_Private_Boolean_0;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011A7F File Offset: 0x0000FC7F
		public static Player LocalPlayer()
		{
			return Player.Method_Internal_Static_get_Player_0();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011A88 File Offset: 0x0000FC88
		public static string ColourFPS(Player player)
		{
			float num = ((player._playerNet.field_Private_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.field_Private_Byte_0) : (-1f));
			string text;
			if (num > 55f)
			{
				text = "<color=green>";
			}
			else if (num <= 55f && num > 18f)
			{
				text = "<color=yellow>";
			}
			else if (num <= 18f && num > 5f)
			{
				text = "<color=orange>";
			}
			else
			{
				text = "<color=red>";
			}
			return text + num.ToString() + "</color>";
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public static string GetAvatarStatus(this Player player)
		{
			string text = player.GetAvatarInfo().releaseStatus.ToLower();
			string text2;
			if (text == "public")
			{
				text2 = "<color=green>" + text + "</color>";
			}
			else
			{
				text2 = "<color=red>" + text + "</color>";
			}
			return text2;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011B6C File Offset: 0x0000FD6C
		internal static bool IsAdmin(this Player player)
		{
			return player.field_Private_APIUser_0.hasModerationPowers || player.field_Private_APIUser_0.tags.Contains("admin_moderator") || player.field_Private_APIUser_0.hasSuperPowers || player.field_Private_APIUser_0.tags.Contains("admin_");
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public static string GetPlatform(this Player player)
		{
			string text;
			if (player != null)
			{
				if (player.field_Private_APIUser_0.IsOnMobile)
				{
					text = "<color=green>Q</color>";
				}
				else if (player.GetVrcPlayerApi().IsUserInVR())
				{
					text = "<color=#CE00D5>V</color>";
				}
				else
				{
					text = "<color=grey>PC</color>";
				}
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011C14 File Offset: 0x0000FE14
		public static string GetFramesColord(this Player player)
		{
			float frames = player.GetFrames();
			string text;
			if (frames > 80f)
			{
				text = "<color=green>" + frames.ToString() + "</color>";
			}
			else if (frames > 30f)
			{
				text = "<color=yellow>" + frames.ToString() + "</color>";
			}
			else
			{
				text = "<color=red>" + frames.ToString() + "</color>";
			}
			return text;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011C88 File Offset: 0x0000FE88
		internal static string GetAviColord(this Player player)
		{
			string text;
			if (player.Method_Public_get_ApiAvatar_PDM_0().releaseStatus == "internal")
			{
				text = " | [<color=green>Public</color>]";
			}
			else
			{
				text = " | [<color=red>Private</color>]";
			}
			return text;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011CBB File Offset: 0x0000FEBB
		public static string IsPlayerMicDisabled(this Player player)
		{
			if (!(player.Method_Internal_get_VRCPlayer_1().field_Internal_Animator_0 == null) && player.Method_Internal_get_VRCPlayer_1().field_Internal_Animator_0.GetBool("MuteSelf"))
			{
				return "<color=red>\ud83d\udd07</color>";
			}
			return "<color=green>\ud83c\udfa4</color>";
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00011CF2 File Offset: 0x0000FEF2
		public static float GetQuality(this VRCPlayer Instance)
		{
			return PlayerExtensions.GetPlayerNet(Instance).Method_Internal_get_Single_PDM_1();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011D00 File Offset: 0x0000FF00
		public static string GetPingColord(this Player player)
		{
			short ping = player.GetPing();
			string text;
			if (ping > 150)
			{
				text = "<color=red>" + ping.ToString() + "</color>";
			}
			else if (ping > 75)
			{
				text = "<color=yellow>" + ping.ToString() + "</color>";
			}
			else
			{
				text = "<color=green>" + ping.ToString() + "</color>";
			}
			return text;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00011D6F File Offset: 0x0000FF6F
		public static Color Friend()
		{
			return VRCPlayer.field_Internal_Static_Color_7;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011D76 File Offset: 0x0000FF76
		public static Color Trusted()
		{
			return VRCPlayer.field_Internal_Static_Color_6;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011D7D File Offset: 0x0000FF7D
		public static Color Known()
		{
			return VRCPlayer.field_Internal_Static_Color_5;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00011D84 File Offset: 0x0000FF84
		public static Color User()
		{
			return VRCPlayer.field_Internal_Static_Color_4;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011D8B File Offset: 0x0000FF8B
		public static Color NewUser()
		{
			return VRCPlayer.field_Internal_Static_Color_3;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011D92 File Offset: 0x0000FF92
		public static Color Visitor()
		{
			return VRCPlayer.field_Internal_Static_Color_2;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011D99 File Offset: 0x0000FF99
		public static Color Troll()
		{
			return VRCPlayer.field_Internal_Static_Color_0;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		public static float GetFrames(this Player player)
		{
			if (player._playerNet.field_Private_Byte_0 == 0)
			{
				return -1f;
			}
			return Mathf.Floor(1000f / (float)player._playerNet.field_Private_Byte_0);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public static short GetPing(this Player player)
		{
			return player._playerNet.field_Private_Int16_0;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011DD9 File Offset: 0x0000FFD9
		public static bool ClientDetect(this Player player)
		{
			return player.GetFrames() > 200f || player.GetFrames() < 1f || player.GetPing() > 665 || player.GetPing() < 0;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00011E10 File Offset: 0x00010010
		public static bool GetIsMaster(this Player player)
		{
			if (player != null)
			{
				try
				{
					return player.GetVrcPlayerApi().isMaster;
				}
				catch
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00011E4C File Offset: 0x0001004C
		public static VRCPlayer GetLocalVRCPlayer()
		{
			return VRCPlayer.field_Internal_Static_VRCPlayer_0;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011E53 File Offset: 0x00010053
		public static VRCPlayerApi GetVrcPlayerApi(this Player player)
		{
			if (!(player != null))
			{
				return null;
			}
			return player.field_Private_VRCPlayerApi_0;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011E66 File Offset: 0x00010066
		public static VRCPlayer GetVRCPlayer()
		{
			return VRCPlayer.field_Internal_Static_VRCPlayer_0;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00011E6D File Offset: 0x0001006D
		public static Player[] GetAllPlayers()
		{
			return PlayerManager.Method_Public_Static_get_PlayerManager_0().field_Private_List_1_Player_0.ToArray();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011E84 File Offset: 0x00010084
		public static Player GetPlayerNewtworkId(this int id)
		{
			return (from player in PlayerUtil.GetAllPlayers()
				where player._vrcplayer.field_Private_VRCPlayerApi_0.playerId == id
				select player).FirstOrDefault<Player>();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00011EBC File Offset: 0x000100BC
		public static Player GetPlayerNewtworkedId(this int id)
		{
			return (from player in PlayerUtil.GetAllPlayers()
				where player.Method_Internal_get_PlayerNet_0().field_Private_Int32_0 == id
				select player).FirstOrDefault<Player>();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011EF4 File Offset: 0x000100F4
		public static APIUser GetUserById(this string userid)
		{
			return (from player in PlayerManager.Method_Public_Static_get_PlayerManager_0().field_Private_List_1_Player_0.ToArray()
				where player.field_Private_APIUser_0.id == userid
				select player).FirstOrDefault<Player>().field_Private_APIUser_0;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00011F38 File Offset: 0x00010138
		public static bool IsFriend(this Player player)
		{
			return APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00011F54 File Offset: 0x00010154
		public static string phontonnum(this Player player)
		{
			return player.Method_Public_get_Int32_PDM_0().ToString();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00011F6F File Offset: 0x0001016F
		public static string Current_World_ID
		{
			get
			{
				return RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + RoomManager.field_Private_Static_ApiWorldInstance_0.instanceId;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011F8F File Offset: 0x0001018F
		internal static APIUser GetAPIUser(this Player player)
		{
			return player.field_Private_APIUser_0;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011F97 File Offset: 0x00010197
		public static Player GetPhotonPlayer(this Player player)
		{
			return player.field_Private_Player_0.field_Public_Player_0;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011FA4 File Offset: 0x000101A4
		public static Player GetPlayerInformationById(int index)
		{
			foreach (Player player in PlayerUtil.GetAllPlayers())
			{
				if (player._playerNet.field_Private_Int32_0 == index)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011FDC File Offset: 0x000101DC
		internal static string GetRankColord(this Player player)
		{
			bool flag = player.field_Private_APIUser_0.hasModerationPowers || player.field_Private_APIUser_0.tags.Contains("admin_moderator");
			string text;
			if (player.field_Private_APIUser_0.hasSuperPowers || player.field_Private_APIUser_0.tags.Contains("admin_"))
			{
				text = "<color=#ff0000>[Admin User]</color>";
			}
			else if (flag)
			{
				text = "<color=red>[Moderation User]</color>";
			}
			else if (player.field_Private_APIUser_0.hasVeteranTrustLevel)
			{
				text = "<color=#864EDD>Trusted</color>";
			}
			else if (player.field_Private_APIUser_0.hasTrustedTrustLevel)
			{
				text = "<color=yellow>Known</color>";
			}
			else if (player.field_Private_APIUser_0.hasKnownTrustLevel)
			{
				text = "<color=green>User</color>";
			}
			else if (player.field_Private_APIUser_0.hasBasicTrustLevel)
			{
				text = "<color=blue>New</color>";
			}
			else
			{
				text = "<color=white>Vistor</color>";
			}
			return text;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000120A4 File Offset: 0x000102A4
		public static List<Player> AllPlayers(this PlayerManager Instance)
		{
			return Instance.field_Private_List_1_Player_0;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000120AC File Offset: 0x000102AC
		public static Player[] GetAllPlayers(this PlayerManager instance)
		{
			return instance.Method_Public_get_PooledArray_1_Player_0().Array;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000120BE File Offset: 0x000102BE
		public static Player GetPlayer(this PlayerManager Instance, int Index)
		{
			return Instance.AllPlayers()[Index];
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000120CC File Offset: 0x000102CC
		public static Player GetPlayerWithPlayerID(this PlayerManager Instance, int playerID)
		{
			foreach (Player player in Instance.AllPlayers().ToArray())
			{
				if (player.GetVRCPlayerApi().playerId == playerID)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x040001B4 RID: 436
		public static bool _isInVrCache;

		// Token: 0x040001B5 RID: 437
		public static bool _cachedVRState;

		// Token: 0x040001B6 RID: 438
		public static Color defaultNameplateColor;

		// Token: 0x040001B7 RID: 439
		public static List<string> knownBlocks = new List<string>();

		// Token: 0x040001B8 RID: 440
		public static List<string> knownMutes = new List<string>();

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000522 RID: 1314
		internal delegate void AlignTrackingToPlayerDelegate();
	}
}
