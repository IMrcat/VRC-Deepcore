using System;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000053 RID: 83
	internal class TPose
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000C776 File Offset: 0x0000A976
		public static void State()
		{
			Animator field_Internal_Animator_ = Player.Method_Internal_Static_get_Player_0()._vrcplayer.field_Internal_Animator_0;
			field_Internal_Animator_.enabled = !field_Internal_Animator_.enabled;
		}
	}
}
