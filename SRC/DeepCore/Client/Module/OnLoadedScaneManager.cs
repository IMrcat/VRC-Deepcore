using System;
using System.Collections;
using System.Linq;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.WorldHacks;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.SDK.Internal;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRCSDK2;

namespace DeepCore.Client.Module
{
	// Token: 0x02000024 RID: 36
	internal class OnLoadedScaneManager
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000063A4 File Offset: 0x000045A4
		public static void LoadedScene(int buildindex, string sceneName)
		{
			if (sceneName == "Murder Nevermore")
			{
				MelonCoroutines.Start(Murder4.InitTheme());
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000063BE File Offset: 0x000045BE
		public static IEnumerator WaitForLocalPlayer()
		{
			DeepConsole.Log("SceneManager", "Waiting for localplayer...");
			while (Player.Method_Internal_Static_get_Player_0() == null)
			{
				yield return null;
			}
			OnLoadedScaneManager.funnyrpcobj = new GameObject("deepclientrpcsht");
			OnLoadedScaneManager.allTriggers = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
			OnLoadedScaneManager.allSDK2Triggers = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
			OnLoadedScaneManager.allTriggerCol = Resources.FindObjectsOfTypeAll<VRC_TriggerColliderEventTrigger>();
			OnLoadedScaneManager.allInteractable = Resources.FindObjectsOfTypeAll<VRCInteractable>();
			OnLoadedScaneManager.allBaseInteractable = Resources.FindObjectsOfTypeAll<VRC_Interactable>();
			OnLoadedScaneManager.allSDK2Interactable = Resources.FindObjectsOfTypeAll<VRC_Interactable>();
			OnLoadedScaneManager.sdk2Items = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
			OnLoadedScaneManager.sdk3Items = Resources.FindObjectsOfTypeAll<VRCPickup>();
			OnLoadedScaneManager.allSyncItems = Resources.FindObjectsOfTypeAll<VRC_ObjectSync>();
			OnLoadedScaneManager.allSDK3SyncItems = Resources.FindObjectsOfTypeAll<VRCObjectSync>();
			OnLoadedScaneManager.allPoolItems = Resources.FindObjectsOfTypeAll<VRCObjectPool>();
			OnLoadedScaneManager.sdk3Items = Object.FindObjectsOfType<VRCPickup>();
			OnLoadedScaneManager.allSyncItems = (from x in Resources.FindObjectsOfTypeAll<VRC_ObjectSync>()
				where !OnLoadedScaneManager.toSkip.Any((string y) => y.Contains(x.gameObject.name))
				select x).ToArray<VRC_ObjectSync>();
			OnLoadedScaneManager.allBaseUdonItem = (from x in Resources.FindObjectsOfTypeAll<VRC_Pickup>()
				where !OnLoadedScaneManager.toSkip.Any((string y) => y.Contains(x.gameObject.name))
				select x).ToArray<VRC_Pickup>();
			OnLoadedScaneManager.SceneDescriptor = Object.FindObjectOfType<VRC_SceneDescriptor>(true);
			OnLoadedScaneManager.SDK2SceneDescriptor = Object.FindObjectOfType<VRC_SceneDescriptor>(true);
			OnLoadedScaneManager.SDK3SceneDescriptor = Object.FindObjectOfType<VRCSceneDescriptor>(true);
			OnLoadedScaneManager.udonBehaviours = Object.FindObjectsOfType<UdonBehaviour>();
			OnLoadedScaneManager.udonSync = Resources.FindObjectsOfTypeAll<UdonSync>();
			OnLoadedScaneManager.udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();
			OnLoadedScaneManager.udonOnTrigger = Resources.FindObjectsOfTypeAll<OnTriggerStayProxy>();
			OnLoadedScaneManager.udonOnCol = Resources.FindObjectsOfTypeAll<OnCollisionStayProxy>();
			OnLoadedScaneManager.udonOnRender = Resources.FindObjectsOfTypeAll<OnRenderObjectProxy>();
			OnLoadedScaneManager.allSDKUdon = Resources.FindObjectsOfTypeAll<VRCUdonAnalytics>();
			if (OnLoadedScaneManager.highlightsFX == null)
			{
				OnLoadedScaneManager.highlightsFX = Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().FirstOrDefault<HighlightsFXStandalone>();
			}
			DeepConsole.DebugLog(string.Format("\r\nTotal VRC_Trigger:{0}\r\nTotal VRCPickup:{1}\r\nTotal VRC_TriggerColliderEventTrigger:{2}\r\nTotal VRCInteractable:{3}\r\nTotal VRC_ObjectSync:{4}\r\nTotal UdonBehaviour:{5}\r\nTotal VRCObjectSync:{6}\r\nTotal VRCObjectPool:{7}\r\nTotal BaseUdonItem:{8}\r\nTotal UdonSync:{9}\r\nTotal UdonManager:{10}\r\nTotal OnTriggerStayProxy:{11}\r\nTotal OnCollisionStayProxy:{12}\r\nTotal VRCUdonAnalytics:{13}\r\nTotal (SDK2) VRC_Trigger:{14}\r\nTotal (SDK2) VRC_Interactable:{15}\r\nTotal (SDK2) VRC_Pickup:{16}", new object[]
			{
				OnLoadedScaneManager.allTriggers.Count,
				OnLoadedScaneManager.sdk3Items.Count,
				OnLoadedScaneManager.allTriggerCol.Count,
				OnLoadedScaneManager.allInteractable.Count,
				OnLoadedScaneManager.allSyncItems.ToList<VRC_ObjectSync>().Count,
				OnLoadedScaneManager.udonBehaviours.ToList<UdonBehaviour>().Count,
				OnLoadedScaneManager.allSDK3SyncItems.Count,
				OnLoadedScaneManager.allPoolItems.Count,
				OnLoadedScaneManager.allBaseUdonItem.ToList<VRC_Pickup>().Count,
				OnLoadedScaneManager.udonSync.Count,
				OnLoadedScaneManager.udonManagers.Count,
				OnLoadedScaneManager.udonOnTrigger.Count,
				OnLoadedScaneManager.udonOnCol.Count,
				OnLoadedScaneManager.allSDK3SyncItems.Count,
				OnLoadedScaneManager.allSDK2Triggers.Count,
				OnLoadedScaneManager.allSDK2Interactable.Count,
				OnLoadedScaneManager.sdk2Items.Count
			}));
			VrcExtensions.Toast("DeepClient - World", "Loaded everything i need. >:3", null, 5f);
			yield break;
		}

		// Token: 0x04000069 RID: 105
		internal static float oldRespawnHight;

		// Token: 0x0400006A RID: 106
		internal static Il2CppArrayBase<VRC_Pickup> sdk2Items;

		// Token: 0x0400006B RID: 107
		internal static Il2CppArrayBase<VRCPickup> sdk3Items;

		// Token: 0x0400006C RID: 108
		internal static VRC_ObjectSync[] allSyncItems;

		// Token: 0x0400006D RID: 109
		internal static Il2CppArrayBase<VRCObjectSync> allSDK3SyncItems;

		// Token: 0x0400006E RID: 110
		internal static Il2CppArrayBase<VRCObjectPool> allPoolItems;

		// Token: 0x0400006F RID: 111
		internal static VRC_Pickup[] allBaseUdonItem;

		// Token: 0x04000070 RID: 112
		internal static Il2CppArrayBase<VRCInteractable> allInteractable;

		// Token: 0x04000071 RID: 113
		internal static Il2CppArrayBase<VRC_Interactable> allBaseInteractable;

		// Token: 0x04000072 RID: 114
		internal static Il2CppArrayBase<VRC_Interactable> allSDK2Interactable;

		// Token: 0x04000073 RID: 115
		internal static Il2CppArrayBase<VRC_Trigger> allTriggers;

		// Token: 0x04000074 RID: 116
		internal static Il2CppArrayBase<VRC_Trigger> allSDK2Triggers;

		// Token: 0x04000075 RID: 117
		internal static Il2CppArrayBase<VRC_TriggerColliderEventTrigger> allTriggerCol;

		// Token: 0x04000076 RID: 118
		private static VRC_SceneDescriptor SceneDescriptor;

		// Token: 0x04000077 RID: 119
		private static VRC_SceneDescriptor SDK2SceneDescriptor;

		// Token: 0x04000078 RID: 120
		private static VRCSceneDescriptor SDK3SceneDescriptor;

		// Token: 0x04000079 RID: 121
		internal static HighlightsFXStandalone highlightsFX;

		// Token: 0x0400007A RID: 122
		internal static UdonBehaviour[] udonBehaviours;

		// Token: 0x0400007B RID: 123
		internal static Il2CppArrayBase<UdonSync> udonSync;

		// Token: 0x0400007C RID: 124
		internal static Il2CppArrayBase<UdonManager> udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();

		// Token: 0x0400007D RID: 125
		internal static Il2CppArrayBase<OnTriggerStayProxy> udonOnTrigger;

		// Token: 0x0400007E RID: 126
		internal static Il2CppArrayBase<OnCollisionStayProxy> udonOnCol;

		// Token: 0x0400007F RID: 127
		internal static Il2CppArrayBase<OnRenderObjectProxy> udonOnRender;

		// Token: 0x04000080 RID: 128
		internal static Il2CppArrayBase<VRCUdonAnalytics> allSDKUdon;

		// Token: 0x04000081 RID: 129
		private static readonly string[] toSkip = new string[] { "PhotoCamera", "MirrorPickup", "ViewFinder", "AvatarDebugConsole", "OscDebugConsole" };

		// Token: 0x04000082 RID: 130
		internal static GameObject funnyrpcobj;
	}
}
