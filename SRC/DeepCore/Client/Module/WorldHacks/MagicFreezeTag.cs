using System;
using UnityEngine;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x02000029 RID: 41
	internal class MagicFreezeTag
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000752A File Offset: 0x0000572A
		public static void StartGame()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007546 File Offset: 0x00005746
		public static void EndGame()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "NobodyVictory");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007562 File Offset: 0x00005762
		public static void RamdomTP()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TeleportToRandom");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000757E File Offset: 0x0000577E
		public static void RunnersWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "RunnerVictory");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000759A File Offset: 0x0000579A
		public static void TaggersWin()
		{
			GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TaggerVictory");
		}
	}
}
