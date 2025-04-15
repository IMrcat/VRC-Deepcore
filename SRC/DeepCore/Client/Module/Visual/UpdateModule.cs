using System;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000033 RID: 51
	internal class UpdateModule
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000099AC File Offset: 0x00007BAC
		public static void OnUpdate()
		{
			OptifineZoom.Update();
			LineESP.UpdateLines();
			BoxESP.UpdateBox();
		}
	}
}
