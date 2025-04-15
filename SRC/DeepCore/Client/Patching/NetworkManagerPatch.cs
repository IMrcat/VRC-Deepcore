using System;
using DeepCore.Client.API;
using DeepCore.Client.Coroutine;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.Exploits;
using DeepCore.Client.Module.QOL;
using DeepCore.Client.Module.Visual;
using DeepCore.Client.Mono;
using UnityEngine.Events;
using VRC;
using VRC.Core;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000013 RID: 19
	internal class NetworkManagerPatch
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000042F8 File Offset: 0x000024F8
		public static void Patch()
		{
			VRCEventDelegate<Player> onPlayerJoinedDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerJoinedDelegate;
			VRCEventDelegate<Player> onPlayerAwakeDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerAwakeDelegate;
			VRCEventDelegate<Player> onPlayerLeaveDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerLeaveDelegate;
			UnityAction<Player> unityAction = delegate(Player p)
			{
				if (p != null)
				{
					NetworkManagerPatch.OnJoinEvent(p);
				}
			};
			onPlayerJoinedDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(unityAction);
			UnityAction<Player> unityAction2 = delegate(Player p)
			{
				if (p != null)
				{
					NetworkManagerPatch.OnLeaveEvent(p);
				}
			};
			onPlayerLeaveDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(unityAction2);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004388 File Offset: 0x00002588
		internal static void OnJoinEvent(Player __0)
		{
			if (__0.field_Private_VRCPlayerApi_0.isLocal)
			{
				CustomMenuBG.ApplyColors();
				LastInstanceRejoiner.SaveInstanceID();
				OwnerNameSpoofer.QuickSpoof();
				return;
			}
			ESP.OnPlayerJoin();
			if (ConfManager.playerLogger.Value)
			{
				DeepConsole.Log("PLogger", __0.field_Private_APIUser_0.displayName + " has joined.");
			}
			if (ConfManager.customnameplate.Value)
			{
				__0.gameObject.AddComponent<CustomNameplate>().Player = __0;
			}
			PlayerTagSystem.CheckPlayer(__0);
			if (ConfManager.VRCAdminStaffLogger.Value && __0.field_Private_APIUser_0.hasModerationPowers)
			{
				string text = "There is a VRChat mod in the lobby!\nName: " + __0.field_Private_APIUser_0.displayName;
				for (int i = 0; i < 3; i++)
				{
					VrcExtensions.AlertPopup("ALERT: [MODERATOR/ADMIN]", text, 20);
				}
			}
			if (ConfManager.AntiQuest.Value && __0.field_Private_APIUser_0.IsOnMobile)
			{
				__0.gameObject.SetActive(false);
				DeepConsole.Log("AntiQuest", "Locally Blocked Quest Player -> " + __0.field_Private_APIUser_0.displayName);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004491 File Offset: 0x00002691
		internal static void OnLeaveEvent(Player __0)
		{
			if (__0.field_Private_VRCPlayerApi_0.isLocal)
			{
				return;
			}
			if (ConfManager.playerLogger.Value)
			{
				DeepConsole.Log("PLogger", __0.field_Private_APIUser_0.displayName + " has left.");
			}
		}
	}
}
