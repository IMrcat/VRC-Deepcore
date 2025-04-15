using System;
using UnityEngine.XR;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000075 RID: 117
	internal class ERPChecker
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		public static void IsMyUserERping()
		{
			if (XRSettings.isDeviceActive)
			{
				Entry.IsInVR = true;
				DeepConsole.Log("Core", "Stop erping :(");
			}
		}
	}
}
