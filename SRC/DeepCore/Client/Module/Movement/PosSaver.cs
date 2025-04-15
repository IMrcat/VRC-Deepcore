using System;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000050 RID: 80
	internal class PosSaver
	{
		// Token: 0x060001BB RID: 443 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public static void State(bool s)
		{
			Player player = Player.Method_Internal_Static_get_Player_0();
			if (s)
			{
				if (player == null)
				{
					return;
				}
				PosSaver.targetPos = player.gameObject.transform.position;
				PosSaver.targetRotation = player.gameObject.transform.rotation;
				return;
			}
			else
			{
				if (player == null)
				{
					return;
				}
				player.gameObject.transform.position = PosSaver.targetPos;
				player.gameObject.transform.rotation = PosSaver.targetRotation;
				return;
			}
		}

		// Token: 0x040000E3 RID: 227
		public static Vector3 targetPos;

		// Token: 0x040000E4 RID: 228
		public static Quaternion targetRotation;
	}
}
