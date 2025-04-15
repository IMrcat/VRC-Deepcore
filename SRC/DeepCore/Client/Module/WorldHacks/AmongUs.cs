using System;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x02000025 RID: 37
	internal class AmongUs
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00006410 File Offset: 0x00004610
		public static void AmongusMenu(ReMenuPage reCategoryPage)
		{
			ReCategoryPage reCategoryPage2 = reCategoryPage.AddCategoryPage("Among Us", "", null, "#ffffff");
			reCategoryPage2.AddCategory("Self Event");
			ReMenuCategory category = reCategoryPage2.GetCategory("Self Event");
			category.AddButton("Self\nImposter", "", delegate
			{
				AmongUs.SelfImposter(Networking.LocalPlayer.displayName);
			}, null, "#ffffff");
			category.AddButton("Self\nCrewmate", "", delegate
			{
				AmongUs.SelfCrewmate(Networking.LocalPlayer.displayName);
			}, null, "#ffffff");
			reCategoryPage2.AddCategory("Game Event");
			ReMenuCategory category2 = reCategoryPage2.GetCategory("Game Event");
			category2.AddButton("Start\nMatch\nCountdown", "", new Action(AmongUs.StartMatchCountdown), null, "#ffffff");
			category2.AddButton("Force\nStart\nMatch", "", new Action(AmongUs.ForceStartMatch), null, "#ffffff");
			category2.AddButton("Abort\nMatch", "", new Action(AmongUs.AbortMatch), null, "#ffffff");
			category2.AddButton("Kill\nAll", "", new Action(AmongUs.KillAll), null, "#ffffff");
			category2.AddButton("Start\nVote out\nEveryone", "", new Action(AmongUs.StartVoteOutEveryone), null, "#ffffff");
			category2.AddButton("Emergency\nButton", "", new Action(AmongUs.EmergencyButton), null, "#ffffff");
			category2.AddButton("All\nTasks\nDone", "", new Action(AmongUs.AllTasksDone), null, "#ffffff");
			category2.AddButton("Log\nImpostor", "", new Action(AmongUs.LogImpostor), null, "#ffffff");
			category2.AddButton("Crewmate\nWin", "", new Action(AmongUs.CrewmateWin), null, "#ffffff");
			category2.AddButton("Imposter\nWin", "", new Action(AmongUs.ImposterWin), null, "#ffffff");
			category2.AddButton("Everyone\nImposter", "", new Action(AmongUs.StartEveryoneInpostor), null, "#ffffff");
			category2.AddButton("Vote\nResult\nTie", "", new Action(AmongUs.SyncVoteResultTie), null, "#ffffff");
			category2.AddButton("Vote\nResult\nSkip", "", new Action(AmongUs.SyncVoteResultSkip), null, "#ffffff");
			category2.AddButton("End\nVoting\nPhase", "", new Action(AmongUs.SyncEndVotingPhase), null, "#ffffff");
			reCategoryPage2.AddCategory("Sabo Event");
			ReMenuCategory category3 = reCategoryPage2.GetCategory("Sabo Event");
			category3.AddButton("Stop All", "", new Action(AmongUs.StopSabbo), null, "#ffffff");
			category3.AddButton("Repair\nComs", "", new Action(AmongUs.SyncRepairComms), null, "#ffffff");
			category3.AddButton("Repair\nLights", "", new Action(AmongUs.SyncRepairLights), null, "#ffffff");
			category3.AddButton("Repair\nOxygen", "", new Action(AmongUs.SyncRepairOxygenAB), null, "#ffffff");
			category3.AddButton("Repair\nReactor", "", new Action(AmongUs.SyncRepairReactor), null, "#ffffff");
			category3.AddButton("Sabo\nDoor", "", new Action(AmongUs.SaboDoor), null, "#ffffff");
			category3.AddButton("Sabo\nLower\nDoor", "", new Action(AmongUs.SaboLowerDoor), null, "#ffffff");
			category3.AddButton("Sabo\nUpper\nDoor", "", new Action(AmongUs.SaboUpperDoor), null, "#ffffff");
			category3.AddButton("Sabo\nCafeteria\nDoor", "", new Action(AmongUs.SaboCafeteriaDoor), null, "#ffffff");
			category3.AddButton("Sabo\nStorage\nDoor", "", new Action(AmongUs.SabStorageDoor), null, "#ffffff");
			category3.AddButton("Sabo\nSecurity\nDoor", "", new Action(AmongUs.SabSecurityDoor), null, "#ffffff");
			category3.AddButton("Sabo\nElectrical\nDoor", "", new Action(AmongUs.SabElectricalDoor), null, "#ffffff");
			category3.AddButton("Sabo\nOxygen", "", new Action(AmongUs.SabOxygen), null, "#ffffff");
			category3.AddButton("Sabo\nComs", "", new Action(AmongUs.SabComs), null, "#ffffff");
			category3.AddButton("Sabo\nReactor", "", new Action(AmongUs.SabReactor), null, "#ffffff");
			category3.AddButton("Sabo\nLights", "", new Action(AmongUs.SabLights), null, "#ffffff");
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000068FC File Offset: 0x00004AFC
		public static void AmongTargetMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.TargetMenu.AddCategoryPage("Among Us functions", "", null, "#ffffff");
			reCategoryPage.AddCategory("User Roles");
			ReMenuCategory category = reCategoryPage.GetCategory("User Roles");
			category.AddButton("Make crewmate", "", delegate
			{
				AmongUs.SelfCrewmate(VrcExtensions.QM_GetSelectedUserName());
			}, null, "#ffffff");
			category.AddButton("Make Imposter", "", delegate
			{
				AmongUs.SelfImposter(VrcExtensions.QM_GetSelectedUserName());
			}, null, "#ffffff");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000069A8 File Offset: 0x00004BA8
		public static void StartMatchCountdown()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000069DC File Offset: 0x00004BDC
		public static void ForceStartMatch()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "_start");
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStart");
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006A30 File Offset: 0x00004C30
		public static void ForceStartMatchNML()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "_start");
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStart");
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006A84 File Offset: 0x00004C84
		public static void AbortMatch()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public static void KillAll()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "KillLocalPlayer");
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006AEC File Offset: 0x00004CEC
		public static void StartVoteOutEveryone()
		{
			for (int i = 0; i < 25; i++)
			{
				GameObject gameObject = GameObject.Find("Player Node (" + i.ToString() + ")");
				if (gameObject)
				{
					gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVotedOut");
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006B3C File Offset: 0x00004D3C
		public static void StartEveryoneInpostor()
		{
			for (int i = 0; i < 25; i++)
			{
				GameObject gameObject = GameObject.Find("Player Node (" + i.ToString() + ")");
				if (gameObject)
				{
					gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006B8C File Offset: 0x00004D8C
		public static void EmergencyButton()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "StartMeeting");
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncEmergencyMeeting");
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public static void AllTasksDone()
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerCompletedTask");
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006C04 File Offset: 0x00004E04
		public static void LogImpostor()
		{
			foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (gameObject.name.Contains("Player Entry") && gameObject.GetComponentInChildren<Text>().text != "PlayerName" && gameObject.GetComponent<Image>().color.r > 0f)
				{
					VrcExtensions.HudNotif(gameObject.GetComponentInChildren<Text>().text + " is the imposter");
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public static void CrewmateWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryC");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public static void ImposterWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryI");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006CDC File Offset: 0x00004EDC
		public static void SaboDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsMedbay");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public static void SaboLowerDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsLower");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006D14 File Offset: 0x00004F14
		public static void SaboUpperDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsUpper");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006D30 File Offset: 0x00004F30
		public static void SaboCafeteriaDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsCafeteria");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006D4C File Offset: 0x00004F4C
		public static void SabStorageDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsStorage");
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006D68 File Offset: 0x00004F68
		public static void SabSecurityDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsSecurity");
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006D84 File Offset: 0x00004F84
		public static void SabElectricalDoor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsElectrical");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public static void SabOxygen()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageOxygen");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006DBC File Offset: 0x00004FBC
		public static void SabComs()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageComms");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static void SabReactor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageReactor");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public static void SabLights()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageLights");
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006E10 File Offset: 0x00005010
		public static void StopSabbo()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CancelAllSabotage");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006E2C File Offset: 0x0000502C
		public static void SyncRepairComms()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairComms");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006E48 File Offset: 0x00005048
		public static void SyncRepairLights()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairLights");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006E64 File Offset: 0x00005064
		public static void SyncRepairOxygenAB()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairOxygenA");
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairOxygenB");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006E9A File Offset: 0x0000509A
		public static void SyncRepairReactor()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairReactor");
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006EB6 File Offset: 0x000050B6
		public static void SyncVoteResultTie()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultTie");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006ED2 File Offset: 0x000050D2
		public static void SyncVoteResultSkip()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultSkip");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006EEE File Offset: 0x000050EE
		public static void SyncVoteResultNobody()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultNobody");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006F0A File Offset: 0x0000510A
		public static void SyncEndVotingPhase()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncEndVotingPhase");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006F28 File Offset: 0x00005128
		public static void SelfImposter(string username)
		{
			for (int i = 0; i < 24; i++)
			{
				string text = "Player Node (" + i.ToString() + ")";
				if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(username))
				{
					GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static void SelfCrewmate(string username)
		{
			for (int i = 0; i < 24; i++)
			{
				string text = "Player Node (" + i.ToString() + ")";
				if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(username))
				{
					GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignB");
				}
			}
		}
	}
}
