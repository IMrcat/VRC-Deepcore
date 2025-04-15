using System;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x0200002A RID: 42
	internal class Murder4
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000075C0 File Offset: 0x000057C0
		public static void Murder4Menu(ReMenuPage reCategoryPage)
		{
			ReCategoryPage reCategoryPage2 = reCategoryPage.AddCategoryPage("Murder 4", "", null, "#ffffff");
			reCategoryPage2.AddCategory("Self Event");
			ReMenuCategory category = reCategoryPage2.GetCategory("Self Event");
			category.AddButton("Become murder", "", delegate
			{
				Murder4.BeARole(Networking.LocalPlayer.displayName, "SyncAssignM");
			}, null, "#ffffff");
			category.AddButton("Become bystender", "", delegate
			{
				Murder4.BeARole(Networking.LocalPlayer.displayName, "SyncAssignB");
			}, null, "#ffffff");
			category.AddButton("Become detective", "", delegate
			{
				Murder4.BeARole(Networking.LocalPlayer.displayName, "SyncAssignD");
			}, null, "#ffffff");
			reCategoryPage2.AddCategory("Game Event");
			ReMenuCategory category2 = reCategoryPage2.GetCategory("Game Event");
			category2.AddButton("Start\nMatch", "", new Action(Murder4.StartMatch), null, "#ffffff");
			category2.AddButton("Abort\nMatch", "", new Action(Murder4.AbortMatch), null, "#ffffff");
			category2.AddButton("Show\nRoles", "", new Action(Murder4.ReshowEveryoneRoles), null, "#ffffff");
			category2.AddButton("Bystander\nWin", "", new Action(Murder4.BystandersWin), null, "#ffffff");
			category2.AddButton("Murder\nWin", "", new Action(Murder4.MurderWin), null, "#ffffff");
			category2.AddButton("Blind\nAll", "", new Action(Murder4.BlindAll), null, "#ffffff");
			category2.AddButton("Kill\nAll", "", new Action(Murder4.KillAll), null, "#ffffff");
			category2.AddButton("Close & Lock\ndoors", "", new Action(Murder4.CloseAllDoors), null, "#ffffff");
			category2.AddButton("Open & Unlock\ndoors", "", new Action(Murder4.OpenAllDoors), null, "#ffffff");
			category2.AddButton("Camera\nFlash", "", new Action(Murder4.CameraFlash), null, "#ffffff");
			reCategoryPage2.AddCategory("Guns Events");
			ReMenuCategory category3 = reCategoryPage2.GetCategory("Guns Events");
			category3.AddButton("Bring\nRevolver", "", new Action(Murder4.BringRevolver), null, "#ffffff");
			category3.AddButton("Bring\nShotgun", "", new Action(Murder4.BringShotgun), null, "#ffffff");
			category3.AddButton("Bring\nLuger", "", new Action(Murder4.BringLuger), null, "#ffffff");
			category3.AddSpacer(null);
			category3.AddButton("Fire\nRevolver", "", new Action(Murder4.firerevolver), null, "#ffffff");
			category3.AddButton("Fire\nShotgun", "", new Action(Murder4.fireShotgun), null, "#ffffff");
			category3.AddButton("Fire\nLuger", "", new Action(Murder4.fireLuger), null, "#ffffff");
			reCategoryPage2.AddCategory("Other Events");
			ReMenuCategory category4 = reCategoryPage2.GetCategory("Other Events");
			category4.AddButton("Revolver\nPatron\nSkin", "", new Action(Murder4.RevolverPatronSkin), null, "#ffffff");
			category4.AddButton("Release\nSnake", "", new Action(Murder4.ReleaseSnake), null, "#ffffff");
			category4.AddButton("Bring\nSmoke\nGrenade", "", new Action(Murder4.BringSmokeGrenade), null, "#ffffff");
			category4.AddButton("Bring\nFrag", "", delegate
			{
				Murder4.BringFrag(VRCPlayer.field_Internal_Static_VRCPlayer_0, false);
			}, null, "#ffffff");
			category4.AddButton("Explode\nFrag0", "", new Action(Murder4.Frag0Explode), null, "#ffffff");
			category4.AddButton("Explode\nTraps", "", new Action(Murder4.BringTraps), null, "#ffffff");
			category4.AddButton("Explode\nFlashCamera", "", new Action(Murder4.BringFlashCamera), null, "#ffffff");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007A1C File Offset: 0x00005C1C
		public static void Murder4TargetMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.TargetMenu.AddCategoryPage("Murder 4 functions", "", null, "#ffffff");
			reCategoryPage.AddCategory("User Roles");
			ReMenuCategory category = reCategoryPage.GetCategory("User Roles");
			category.AddButton("Make murder", "", delegate
			{
				Murder4.BeARole(VrcExtensions.QM_GetSelectedUserName(), "SyncAssignM");
			}, null, "#ffffff");
			category.AddButton("Make bystender", "", delegate
			{
				Murder4.BeARole(VrcExtensions.QM_GetSelectedUserName(), "SyncAssignB");
			}, null, "#ffffff");
			category.AddButton("Make detective", "", delegate
			{
				Murder4.BeARole(VrcExtensions.QM_GetSelectedUserName(), "SyncAssignD");
			}, null, "#ffffff");
			category.AddButton("Kill", "", delegate
			{
				Murder4.BeARole(VrcExtensions.QM_GetSelectedUserName(), "yncKill");
			}, null, "#ffffff");
			reCategoryPage.AddCategory("Other things");
			ReMenuCategory category2 = reCategoryPage.GetCategory("Other things");
			category2.AddButton("Blow up player", "", delegate
			{
				Murder4.BringFrag(PlayerExtensions.GetVRCPlayer(), true);
			}, null, "#ffffff");
			category2.AddToggle("Knife Shield", "Sigma simga boy.", delegate(bool s)
			{
				if (s)
				{
					Murder4.knifeShieldbool = true;
					MelonCoroutines.Start(Murder4.KnifeShieldCoroutine(PlayerExtensions.GetVRCPlayer()));
					return;
				}
				Murder4.knifeShieldbool = false;
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007BAD File Offset: 0x00005DAD
		public static void StartMatch()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
			gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007BDA File Offset: 0x00005DDA
		public static void AbortMatch()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007BF6 File Offset: 0x00005DF6
		public static void ReshowEveryoneRoles()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerAssignedRole");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007C12 File Offset: 0x00005E12
		public static void BystandersWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryB");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007C2E File Offset: 0x00005E2E
		public static void MurderWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryM");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static void CloseAllDoors()
		{
			foreach (Transform transform in new List<Transform>
			{
				GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact close"),
				GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact close")
			})
			{
				transform.GetComponent<UdonBehaviour>().Interact();
			}
			Murder4.LockAllDoors();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007ED4 File Offset: 0x000060D4
		public static void LockAllDoors()
		{
			foreach (Transform transform in new List<Transform>
			{
				GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact lock"),
				GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact lock")
			})
			{
				transform.GetComponent<UdonBehaviour>().Interact();
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008158 File Offset: 0x00006358
		public static void UnlockAllDoors()
		{
			foreach (Transform transform in new List<Transform>
			{
				GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact shove"),
				GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact shove")
			})
			{
				transform.GetComponent<UdonBehaviour>().Interact();
				transform.GetComponent<UdonBehaviour>().Interact();
				transform.GetComponent<UdonBehaviour>().Interact();
				transform.GetComponent<UdonBehaviour>().Interact();
			}
			Murder4.OpenAllDoors();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008400 File Offset: 0x00006600
		public static void OpenAllDoors()
		{
			foreach (Transform transform in new List<Transform>
			{
				GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact open"),
				GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact open")
			})
			{
				transform.GetComponent<UdonBehaviour>().Interact();
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008684 File Offset: 0x00006884
		public static void KillAll()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "KillLocalPlayer");
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000086B8 File Offset: 0x000068B8
		public static void BlindAll()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerBlinded");
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000086EC File Offset: 0x000068EC
		public static void CameraFlash()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerFlashbanged");
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008720 File Offset: 0x00006920
		public static void BringRevolver()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Revolver");
			Networking.SetOwner(Networking.LocalPlayer, gameObject);
			gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000877C File Offset: 0x0000697C
		public static void BringShotgun()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)");
			Networking.SetOwner(Networking.LocalPlayer, gameObject);
			gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000087D8 File Offset: 0x000069D8
		public static void BringLuger()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)");
			Networking.SetOwner(Networking.LocalPlayer, gameObject);
			gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008834 File Offset: 0x00006A34
		public static void BringSmokeGrenade()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Smoke (0)");
			if (gameObject)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
				gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008898 File Offset: 0x00006A98
		public static void BringFrag(VRCPlayer player, bool shouldblow)
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
			if (gameObject)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
				gameObject.transform.position = player.transform.position + new Vector3(0f, 0.1f, 0f);
			}
			if (shouldblow)
			{
				Murder4.Frag0Explode();
				DeepConsole.Log("M4", "Imagine " + player.field_Private_VRCPlayerApi_0.displayName + " blowed up. XD");
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008924 File Offset: 0x00006B24
		public static void BringTraps()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Bear Trap (0)");
			if (gameObject)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
				gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
			}
			GameObject gameObject2 = GameObject.Find("Game Logic/Weapons/Bear Trap (1)");
			if (gameObject2)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject2);
				gameObject2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
			}
			GameObject gameObject3 = GameObject.Find("Game Logic/Weapons/Bear Trap (2)");
			if (gameObject3)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject3);
				gameObject3.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008A34 File Offset: 0x00006C34
		public static void BringFlashCamera()
		{
			GameObject gameObject = GameObject.Find("Game Logic/Polaroids Unlock Camera/FlashCamera");
			if (gameObject)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
				gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008A97 File Offset: 0x00006C97
		public static IEnumerator KnifeShieldCoroutine(VRCPlayer player)
		{
			List<GameObject> list = new List<GameObject>
			{
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (0)").gameObject,
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (1)").gameObject,
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (2)").gameObject,
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (3)").gameObject,
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (4)").gameObject,
				GameObject.Find("Game Logic").transform.Find("Weapons/Knife (5)").gameObject
			};
			GameObject gameObject = new GameObject();
			gameObject.transform.position = player.transform.position + new Vector3(0f, 0.35f, 0f);
			while (Murder4.knifeShieldbool && Murder4.knifeShieldbool)
			{
				gameObject.transform.Rotate(new Vector3(0f, 360f * Time.time, 0f));
				gameObject.transform.position = player.transform.position + new Vector3(0f, 0.35f, 0f);
				foreach (GameObject gameObject2 in list)
				{
					Networking.LocalPlayer.TakeOwnership(gameObject2.gameObject);
					gameObject2.transform.position = gameObject.transform.position + gameObject.transform.forward;
					gameObject2.transform.LookAt(player.transform);
					gameObject.transform.Rotate(new Vector3(0f, (float)(360 / list.Count), 0f));
				}
				yield return null;
			}
			Object.Destroy(gameObject);
			gameObject = null;
			yield break;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008AA6 File Offset: 0x00006CA6
		public static void fireShotgun()
		{
			GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008AC2 File Offset: 0x00006CC2
		public static void firerevolver()
		{
			GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008ADE File Offset: 0x00006CDE
		public static void fireLuger()
		{
			GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008AFA File Offset: 0x00006CFA
		public static void Frag0Explode()
		{
			GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Explode");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008B16 File Offset: 0x00006D16
		public static void RevolverPatronSkin()
		{
			GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008B32 File Offset: 0x00006D32
		public static void ReleaseSnake()
		{
			GameObject.Find("Game Logic/Snakes/SnakeDispenser").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "DispenseSnake");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008B50 File Offset: 0x00006D50
		public static void FindMurder()
		{
			Transform[] array = Resources.FindObjectsOfTypeAll<Transform>();
			GameObject gameObject = null;
			int num;
			for (int i = 0; i < array.Length; i = num)
			{
				if (array[i].gameObject.name.Equals("Murderer Name"))
				{
					gameObject = array[i].gameObject;
				}
				num = i + 1;
			}
			DeepConsole.Log("M4", gameObject.GetComponent<TextMeshProUGUI>().text.ToString() + ", Is the murder.");
			VrcExtensions.HudNotif(gameObject.GetComponent<TextMeshProUGUI>().text.ToString() + ", Is the murder.");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public static void BeARole(string username, string role)
		{
			for (int i = 0; i < 24; i++)
			{
				string text = "Player Node (" + i.ToString() + ")";
				if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<TextMeshProUGUI>().text.Equals(username))
				{
					MelonLogger.Msg(text);
					GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008C5C File Offset: 0x00006E5C
		public static IEnumerator InitTheme()
		{
			while (GameObject.Find("Game Logic/Game Canvas") == null)
			{
				yield return null;
			}
			GameObject.Find("Game Logic/Game Canvas/Pregame/Title Text").GetComponent<TextMeshProUGUI>().text = "HABIBI 4";
			GameObject.Find("Game Logic/Game Canvas/Pregame/Title Text").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("Game Logic/Game Canvas/Pregame/Author Text").GetComponent<TextMeshProUGUI>().text = "By Osama";
			GameObject.Find("Game Logic/Game Canvas/Pregame/Author Text").GetComponent<TextMeshProUGUI>().color = Color.red;
			GameObject.Find("Game Logic/Game Canvas/Background Panel Border").GetComponent<Image>().color = Color.red;
			DeepConsole.LogConsole("M4", "Disabling useless hud...");
			GameObject.Find("Game Logic/Player HUD/Death HUD Anim").SetActive(false);
			GameObject.Find("Game Logic/Player HUD/Blind HUD Anim").SetActive(false);
			yield break;
		}

		// Token: 0x04000084 RID: 132
		public static bool knifeShieldbool;
	}
}
