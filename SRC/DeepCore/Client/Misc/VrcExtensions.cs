using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC;
using VRC.Core;
using VRC.Localization;
using VRC.Networking;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000088 RID: 136
	public static class VrcExtensions
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00012502 File Offset: 0x00010702
		public static void AlertPopup(string tittle, string content, int time)
		{
			VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_Single_0(LocalizableStringExtensions.Localize(tittle, null, null, null), LocalizableStringExtensions.Localize(content, null, null, null), (float)time);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00012522 File Offset: 0x00010722
		internal static bool GetStreamerMode
		{
			get
			{
				return VRCInputManager.Method_Public_Static_Boolean_EnumNPublicSealedvaCoHeToTaThShPeVoViUnique_0(27);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001252C File Offset: 0x0001072C
		public static Player GetPlayerNewtworkedId(int id)
		{
			return (from player in VrcExtensions.GetAllPlayers()
				where player.Method_Internal_get_PlayerNet_0().field_Private_Int32_0 == id
				select player).FirstOrDefault<Player>();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00012561 File Offset: 0x00010761
		public static Player[] GetAllPlayers()
		{
			return PlayerManager.Method_Public_Static_get_PlayerManager_0().field_Private_List_1_Player_0.ToArray();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00012577 File Offset: 0x00010777
		public static VRCPlayer GetLocalVRCPlayer()
		{
			return VRCPlayer.field_Internal_Static_VRCPlayer_0;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00012580 File Offset: 0x00010780
		public static void Toast(string content, string description = null, Sprite icon = null, float duration = 5f)
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize(content, null, null, null);
			LocalizableString localizableString2 = LocalizableStringExtensions.Localize(description, null, null, null);
			VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.notification.Method_Public_Void_Sprite_LocalizableString_LocalizableString_Single_Object1PublicTYBoTYUnique_1_Boolean_0(SpriteManager.clientIcon, localizableString, localizableString2, duration, null);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000125C0 File Offset: 0x000107C0
		public static void HudNotif(string Text)
		{
			LocalizableString localizableString = LocalizableStringExtensions.Localize(Text, null, null, null);
			VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.userEventCarousel.Method_Public_Void_LocalizableString_Sprite_0(localizableString, SpriteManager.clientIcon);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000125F1 File Offset: 0x000107F1
		public static void ListenPlayer(VRCPlayer player, bool state)
		{
			if (!state)
			{
				player.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(25f);
				return;
			}
			player.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(float.PositiveInfinity);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00012618 File Offset: 0x00010818
		public static void LogAvatar(VRCPlayer player)
		{
			Clipboard.SetText(string.Format("\n---------------------------------------------------------------------------------------------------------------------\nAuthor:{0}\nID:{1}\nImage URL:{2}\nAvatar id:{3}\nAvatar name:{4}\nAsset url:{5}\nRelease status:{6}\nPlatform:{7}\nVersion:{8}\n---------------------------------------------------------------------------------------------------------------------", new object[]
			{
				player.field_Private_ApiAvatar_0.authorName,
				player.field_Private_ApiAvatar_0.authorId,
				player.field_Private_ApiAvatar_0.imageUrl,
				player.field_Private_ApiAvatar_0.id,
				player.field_Private_ApiAvatar_0.name,
				player.field_Private_ApiAvatar_0.assetUrl,
				player.field_Private_ApiAvatar_0.releaseStatus,
				player.field_Private_ApiAvatar_0.platform,
				player.field_Private_ApiAvatar_0.version
			}));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000126C0 File Offset: 0x000108C0
		public static void TpickupsToPlayer(VRCPlayer player)
		{
			if (Networking.LocalPlayer != null)
			{
				foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
				{
					Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
					vrc_Pickup.transform.position = player.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00012750 File Offset: 0x00010950
		public static void TpToPlayer(VRCPlayer player)
		{
			if (Networking.LocalPlayer != null)
			{
				Networking.LocalPlayer.gameObject.transform.position = player.gameObject.transform.position;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001277D File Offset: 0x0001097D
		public static void LoudMic(bool enable)
		{
			if (enable)
			{
				USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
				return;
			}
			USpeaker.field_Internal_Static_Single_1 = 1f;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00012797 File Offset: 0x00010997
		internal static void ToggleCharacterController(bool toggle)
		{
			Player.Method_Internal_Static_get_Player_0().gameObject.GetComponent<CharacterController>().enabled = toggle;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000127AE File Offset: 0x000109AE
		internal static void ToggleNetworkSerializer(bool toggle)
		{
			Player.Method_Internal_Static_get_Player_0().gameObject.GetComponent<FlatBufferNetworkSerializer>().enabled = toggle;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000127C8 File Offset: 0x000109C8
		public static VRCPlayerApi GetPlayerByUsername(string username)
		{
			foreach (VRCPlayerApi vrcplayerApi in new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()])
			{
				if (vrcplayerApi != null && vrcplayerApi.displayName == username)
				{
					return vrcplayerApi;
				}
			}
			return null;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00012806 File Offset: 0x00010A06
		public static VRCPlayerApi SelectedUserV2()
		{
			return VrcExtensions.GetPlayerByUsername(VrcExtensions.QM_GetSelectedUserName());
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012812 File Offset: 0x00010A12
		public static string QM_GetSelectedUserName()
		{
			return GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/UserProfile_Compact/PanelBG/Info/Text_Username_NonFriend").GetComponent<TextMeshProUGUIEx>().text;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00012828 File Offset: 0x00010A28
		public static void SetQmDashbordPageTittle(string name)
		{
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = name;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001283F File Offset: 0x00010A3F
		public static IEnumerator SetMicColor()
		{
			while (GameObject.Find("UnscaledUI/HudContent/HUD_UI 2(Clone)/VR Canvas/Container/Left/Icons/Mic") == null)
			{
				yield return null;
			}
			GameObject.Find("UnscaledUI/HudContent/HUD_UI 2(Clone)/VR Canvas/Container/Left/Icons/Mic").GetComponent<MonoBehaviourPublicImCoImVeCoBoVeSiAuVeUnique>().field_Public_Color_0 = Color.white;
			GameObject.Find("UnscaledUI/HudContent/HUD_UI 2(Clone)/VR Canvas/Container/Left/Icons/Mic").GetComponent<MonoBehaviourPublicImCoImVeCoBoVeSiAuVeUnique>().field_Public_Color_1 = Color.white;
			yield break;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00012847 File Offset: 0x00010A47
		public static void ChangeAvatar(string avatar_id)
		{
			PageAvatar.Method_Public_Static_Void_ApiAvatar_String_0(new ApiAvatar
			{
				id = avatar_id
			}, "AvatarMenu");
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00012860 File Offset: 0x00010A60
		private static GameObject[] GetAllRootGameObjects()
		{
			return SceneManager.GetActiveScene().GetRootGameObjects();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00012880 File Offset: 0x00010A80
		internal static GameObject GetLocalPlayer()
		{
			foreach (GameObject gameObject in VrcExtensions.GetAllRootGameObjects())
			{
				if (gameObject.name.StartsWith("VRCPlayer[Local]"))
				{
					return gameObject;
				}
			}
			return new GameObject();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000128C0 File Offset: 0x00010AC0
		public static VrcExtensions.TrustRanks GetTrustRank(this APIUser apiUser)
		{
			if (apiUser.tags.Count > 0)
			{
				if (apiUser.tags.Contains("system_trust_veteran") && apiUser.tags.Contains("system_trust_trusted"))
				{
					return VrcExtensions.TrustRanks.Trusted;
				}
				if (apiUser.tags.Contains("system_trust_trusted"))
				{
					return VrcExtensions.TrustRanks.Known;
				}
				if (apiUser.tags.Contains("system_trust_known"))
				{
					return VrcExtensions.TrustRanks.User;
				}
				if (apiUser.tags.Contains("system_trust_basic"))
				{
					return VrcExtensions.TrustRanks.NewUser;
				}
			}
			return VrcExtensions.TrustRanks.Visitor;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001293E File Offset: 0x00010B3E
		public static bool IsFriend(this APIUser a, APIUser b)
		{
			return b != null && a.friendIDs.Contains(b.id);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00012958 File Offset: 0x00010B58
		public static Color GetPlayerColor(this APIUser apiUser)
		{
			if (apiUser == null)
			{
				return Color.white;
			}
			if (APIUser.CurrentUser.IsFriend(apiUser))
			{
				return Color.yellow;
			}
			switch (apiUser.GetTrustRank())
			{
			case VrcExtensions.TrustRanks.Visitor:
				return Color.white;
			case VrcExtensions.TrustRanks.NewUser:
				return new Color(0.08235294f, 0.35686275f, 0.74509805f);
			case VrcExtensions.TrustRanks.User:
				return new Color(0.15294118f, 0.78039217f, 0.3372549f);
			case VrcExtensions.TrustRanks.Known:
				return new Color(0.9882353f, 0.47058824f, 0.25490198f);
			case VrcExtensions.TrustRanks.Trusted:
				return new Color(0.46666667f, 0.24313726f, 0.84705883f);
			default:
				return Color.white;
			}
		}

		// Token: 0x020000FD RID: 253
		public enum TrustRanks
		{
			// Token: 0x04000367 RID: 871
			Visitor,
			// Token: 0x04000368 RID: 872
			NewUser,
			// Token: 0x04000369 RID: 873
			User,
			// Token: 0x0400036A RID: 874
			Known,
			// Token: 0x0400036B RID: 875
			Trusted
		}
	}
}
