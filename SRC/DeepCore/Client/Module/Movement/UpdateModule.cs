using System;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000054 RID: 84
	internal class UpdateModule
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000C79D File Offset: 0x0000A99D
		public static void Update()
		{
			Jetpack.Update();
			SpinBot.Update();
			Flight.FlyUpdate();
			SeatOnHead.Update();
			RayCastTP.Update();
		}
	}
}
