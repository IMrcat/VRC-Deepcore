using System;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000034 RID: 52
	internal class ESP
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000099C8 File Offset: 0x00007BC8
		public static void OnPlayerJoin()
		{
			if (ESP.CapMyESP)
			{
				foreach (VRCPlayerApi vrcplayerApi in VRCPlayerApi.AllPlayers)
				{
					if (!vrcplayerApi.isLocal)
					{
						InputManager.EnableObjectHighlight(vrcplayerApi.gameObject.transform.Find("SelectRegion").GetComponent<Renderer>(), true);
					}
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009A20 File Offset: 0x00007C20
		public static void ObjectState(bool s)
		{
			if (Networking.LocalPlayer == null)
			{
				return;
			}
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				foreach (Renderer renderer in vrc_Pickup.GetComponentsInChildren<Renderer>())
				{
					InputManager.EnableObjectHighlight(renderer.gameObject, s);
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009AAC File Offset: 0x00007CAC
		public static void CapsuleState(bool s)
		{
			ESP.CapMyESP = s;
			foreach (VRCPlayerApi vrcplayerApi in VRCPlayerApi.AllPlayers)
			{
				if (!vrcplayerApi.isLocal)
				{
					InputManager.EnableObjectHighlight(vrcplayerApi.gameObject.transform.Find("SelectRegion").GetComponent<Renderer>(), s);
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009B04 File Offset: 0x00007D04
		public static void UdonState(bool s)
		{
			if (Networking.LocalPlayer == null)
			{
				return;
			}
			foreach (UdonBehaviour udonBehaviour in Object.FindObjectsOfType<UdonBehaviour>())
			{
				foreach (Renderer renderer in udonBehaviour.GetComponentsInChildren<Renderer>())
				{
					InputManager.EnableObjectHighlight(renderer.gameObject, s);
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009B90 File Offset: 0x00007D90
		public static void CapsuleESP(VRCPlayer player, bool Stat)
		{
			try
			{
				player.transform.Find("SelectRegion");
				HighlightsFXStandalone highlightsFXStandalone = HighlightsFX.field_Private_Static_HighlightsFX_0.gameObject.AddComponent<HighlightsFXStandalone>();
				Color field_Internal_Static_Color_ = VRCPlayer.field_Internal_Static_Color_2;
				int playerRank = PlayerUtil.GetPlayerRank(player);
				if (playerRank != -1)
				{
					switch (playerRank)
					{
					case 1:
						new Color(0.565f, 0.933f, 0.565f);
						break;
					case 2:
						new Color(0.565f, 0.933f, 0.565f);
						break;
					case 3:
						new Color(1f, 0.792f, 0.365f);
						break;
					case 4:
						new Color(0.831f, 0.447f, 1f);
						break;
					case 5:
						new Color(1f, 0.459f, 0.459f);
						break;
					case 6:
						new Color(1f, 0.984f, 0f);
						break;
					default:
						new Color(0.96f, 0.96f, 0.96f);
						break;
					}
					highlightsFXStandalone.blurDownsampleFactor = 1;
					highlightsFXStandalone.blurIterations = 2;
				}
			}
			catch (Exception ex)
			{
				DeepConsole.LogConsole("Module : ESP", ex.Message);
			}
		}

		// Token: 0x04000095 RID: 149
		public static bool CapMyESP;
	}
}
