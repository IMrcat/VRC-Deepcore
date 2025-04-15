using System;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000030 RID: 48
	internal class Flashlight
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000095BC File Offset: 0x000077BC
		public static void State(bool s)
		{
			Player player = Player.Method_Internal_Static_get_Player_0();
			if (s)
			{
				if (player == null)
				{
					return;
				}
				Flashlight.light = player.gameObject.AddComponent<Light>();
				Flashlight.light.type = 1;
				Flashlight.light.intensity = (float)Flashlight.intensity;
				Flashlight.light.range = (float)Flashlight.range;
				return;
			}
			else
			{
				if (player == null)
				{
					return;
				}
				if (Flashlight.light != null)
				{
					Object.Destroy(Flashlight.light);
				}
				return;
			}
		}

		// Token: 0x0400008C RID: 140
		public static Light light;

		// Token: 0x0400008D RID: 141
		public static int intensity = 1;

		// Token: 0x0400008E RID: 142
		public static int range = 60;
	}
}
