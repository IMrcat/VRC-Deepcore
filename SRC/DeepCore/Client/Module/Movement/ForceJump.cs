using System;
using VRC;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x0200004E RID: 78
	internal class ForceJump
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000C404 File Offset: 0x0000A604
		public static float OriginalJumpImpulse { get; private set; }

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C40C File Offset: 0x0000A60C
		public static void State(bool s)
		{
			Player player = Player.Method_Internal_Static_get_Player_0();
			if (!s)
			{
				player.field_Private_VRCPlayerApi_0.SetJumpImpulse(-ForceJump.OriginalJumpImpulse);
				return;
			}
			if (player == null)
			{
				return;
			}
			ForceJump.OriginalJumpImpulse = player.field_Private_VRCPlayerApi_0.GetJumpImpulse();
			player.field_Private_VRCPlayerApi_0.SetJumpImpulse(2.8f);
		}
	}
}
