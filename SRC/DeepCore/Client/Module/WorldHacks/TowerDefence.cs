using System;
using System.Collections;
using MelonLoader;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x0200002D RID: 45
	internal class TowerDefence
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00008F54 File Offset: 0x00007154
		public static void Menu(ReMenuPage reCategoryPage)
		{
			ReMenuPage reMenuPage = reCategoryPage.AddMenuPage("Tower Defence", "", null, "#ffffff");
			reMenuPage.AddToggle("God Mode", "", delegate(bool s)
			{
				TowerDefence.RegenLoopState = s;
				if (s)
				{
					MelonCoroutines.Start(TowerDefence.RegenLoop());
				}
			});
			reMenuPage.AddButton("+ Money", "MoneyHackLoaded (Make sure you have a tower placed down).", new Action(TowerDefence.MoneyHackLoaded), null, "#ffffff");
			reMenuPage.AddButton("Regenerate Health", "Give new life.", new Action(TowerDefence.RegenLife), null, "#ffffff");
			reMenuPage.AddButton("Create Last tower", "Creates the last tower you placed down (do not do if you dont have a tower placed down).", new Action(TowerDefence.CreateLastTower), null, "#ffffff");
			reMenuPage.AddButton("Upgrade Last Tower", "Upgrades the last tower you placed down.", new Action(TowerDefence.UpgradeLastTower), null, "#ffffff");
			reMenuPage.AddButton("Reset All Towers", "Clears all you towers.", new Action(TowerDefence.ResetAllTower), null, "#ffffff");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009055 File Offset: 0x00007255
		internal static IEnumerator RegenLoop()
		{
			do
			{
				GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Revive");
				yield return new WaitForSeconds(2f);
			}
			while (TowerDefence.RegenLoopState);
			yield break;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000905D File Offset: 0x0000725D
		public static void MoneyHackLoaded()
		{
			GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CreateTower");
			GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TrySellTower");
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009093 File Offset: 0x00007293
		public static void RegenLife()
		{
			GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Revive");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000090AF File Offset: 0x000072AF
		public static void CreateLastTower()
		{
			GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CreateTower");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000090CB File Offset: 0x000072CB
		public static void UpgradeLastTower()
		{
			GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TryUpgradeTower");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000090E7 File Offset: 0x000072E7
		public static void ResetAllTower()
		{
			GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "ClearTowers");
		}

		// Token: 0x04000087 RID: 135
		internal static bool RegenLoopState;
	}
}
