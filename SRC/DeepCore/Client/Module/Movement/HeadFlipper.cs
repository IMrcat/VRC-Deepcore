using System;
using VRC;
using VRC.DataModel;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x0200004F RID: 79
	internal class HeadFlipper
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000C468 File Offset: 0x0000A668
		public static void state(bool s)
		{
			Player player = Player.Method_Internal_Static_get_Player_0();
			if (s)
			{
				HeadFlipper._nexkRange = player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
				player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0f);
				return;
			}
			player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = HeadFlipper._nexkRange;
		}

		// Token: 0x040000E2 RID: 226
		private static NeckRange _nexkRange;
	}
}
