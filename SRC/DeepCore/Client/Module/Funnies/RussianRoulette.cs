using System;
using System.Collections;
using DeepCore.Client.Misc;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Module.Funnies
{
	// Token: 0x02000058 RID: 88
	internal class RussianRoulette
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000C970 File Offset: 0x0000AB70
		public static void RouletteStart()
		{
			int[] array = new int[6];
			int[] array2 = new int[6];
			Random random = new Random();
			int num = random.Next(0, 6);
			int num2 = random.Next(0, 6);
			array[num] = 1;
			array2[num2] = 1;
			VrcExtensions.HudNotif("[Russian Roulette]: Good luck!");
			VrcExtensions.HudNotif("[Russian Roulette]: Ready?");
			bool flag = false;
			int num3 = 0;
			int num4 = 1;
			do
			{
				if (array2[num3] == 1)
				{
					VrcExtensions.HudNotif(string.Format("[Russian Roulette] *Bang* - Computer has lost, You win on round {0}", num4));
					flag = true;
				}
				else if (array[num3] == 1)
				{
					VrcExtensions.HudNotif(string.Format("[Russian Roulette] *Bang* - Computer wins on round {0}, you lost!", num4));
					flag = true;
					MelonCoroutines.Start(RussianRoulette.RouletteLost());
				}
				else
				{
					VrcExtensions.HudNotif(string.Format("[Russian Roulette] *Click* - You both survived round {0}", num4));
					num3++;
					num4++;
				}
			}
			while (!flag);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		private static IEnumerator RouletteLost()
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
	}
}
