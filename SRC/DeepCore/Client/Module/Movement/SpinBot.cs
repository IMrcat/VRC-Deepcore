using System;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000056 RID: 86
	internal class SpinBot
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000C819 File Offset: 0x0000AA19
		public static void Update()
		{
			if (SpinBot.SpinBotbool)
			{
				Networking.LocalPlayer.gameObject.transform.Rotate(Vector3.up * SpinBot.rotationSpeed * Time.deltaTime);
			}
		}

		// Token: 0x040000EB RID: 235
		public static float rotationSpeed = 120f;

		// Token: 0x040000EC RID: 236
		public static bool SpinBotbool;
	}
}
