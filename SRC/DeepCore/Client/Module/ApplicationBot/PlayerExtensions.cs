using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x02000072 RID: 114
	internal static class PlayerExtensions
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000F3D2 File Offset: 0x0000D5D2
		public static VRCPlayer LocalVRCPlayer
		{
			get
			{
				return VRCPlayer.field_Internal_Static_VRCPlayer_0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000F3D9 File Offset: 0x0000D5D9
		public static Player LocalPlayer
		{
			get
			{
				return Player.Method_Internal_Static_get_Player_0();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public static PlayerManager PManager
		{
			get
			{
				return PlayerManager.Method_Public_Static_get_PlayerManager_0();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000F3E7 File Offset: 0x0000D5E7
		public static List<Player> AllPlayers
		{
			get
			{
				return PlayerExtensions.PManager.field_Private_List_1_Player_0.ToArray().ToList<Player>();
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		public static VRCPlayerApi GetVRCPlayerApi(this Player player)
		{
			return player.field_Private_VRCPlayerApi_0;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000F405 File Offset: 0x0000D605
		public static APIUser GetAPIUser(this Player player)
		{
			return player.field_Private_APIUser_0;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000F40D File Offset: 0x0000D60D
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000F414 File Offset: 0x0000D614
		public static float LocalGain
		{
			get
			{
				return USpeaker.field_Internal_Static_Single_1;
			}
			set
			{
				USpeaker.field_Internal_Static_Single_1 = value;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F41C File Offset: 0x0000D61C
		public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator)
		{
			if (PlayerExtensions.handler == null)
			{
				PlayerExtensions.handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault<VRC_EventHandler>();
			}
			vrcEvent.ParameterObject = PlayerExtensions.handler.gameObject;
			PlayerExtensions.handler.TriggerEvent(vrcEvent, type, instagator, 0f);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F45C File Offset: 0x0000D65C
		public static GameObject InstantiatePrefab(string PrefabNAME, Vector3 position, Quaternion rotation)
		{
			return Networking.Instantiate(0, PrefabNAME, position, rotation);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F468 File Offset: 0x0000D668
		public static Player GetPlayerByUserID(string UserID)
		{
			return PlayerExtensions.AllPlayers.Where((Player p) => p.GetAPIUser().id == UserID).FirstOrDefault<Player>();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000F49D File Offset: 0x0000D69D
		public static void SetGain(float Gain)
		{
			PlayerExtensions.LocalGain = Gain;
		}

		// Token: 0x0400014D RID: 333
		private static VRC_EventHandler handler;
	}
}
