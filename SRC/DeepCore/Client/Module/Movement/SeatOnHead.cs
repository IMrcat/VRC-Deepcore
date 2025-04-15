using System;
using ReMod.Core.VRChat;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000052 RID: 82
	internal class SeatOnHead
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000C688 File Offset: 0x0000A888
		public static void State(bool s)
		{
			if (s)
			{
				SeatOnHead.TargetPlayer = PlayerExtensions.GetVRCPlayer()._player;
				SeatOnHead.IsEnable = true;
				return;
			}
			SeatOnHead.IsEnable = false;
			SeatOnHead.TargetPlayer = null;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public static void Update()
		{
			try
			{
				if (SeatOnHead.IsEnable)
				{
					if (SeatOnHead.TargetPlayer != null)
					{
						Transform boneTransform = SeatOnHead.TargetPlayer.Method_Internal_get_VRCPlayer_1().field_Internal_Animator_0.GetBoneTransform(10);
						if (boneTransform != null)
						{
							Player.Method_Internal_Static_get_Player_0().transform.position = boneTransform.position;
						}
						Physics.gravity = Vector3.zero;
					}
				}
				else
				{
					Physics.gravity = SeatOnHead.normalGravity;
				}
				if (Input.GetKeyDown(119) || Input.GetKeyDown(97) || Input.GetKeyDown(115) || Input.GetKeyDown(100))
				{
					SeatOnHead.IsEnable = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x040000E7 RID: 231
		public static Vector3 normalGravity = Physics.gravity;

		// Token: 0x040000E8 RID: 232
		public static bool IsEnable = false;

		// Token: 0x040000E9 RID: 233
		public static Player TargetPlayer;
	}
}
