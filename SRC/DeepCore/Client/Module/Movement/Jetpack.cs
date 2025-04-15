using System;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000055 RID: 85
	internal class Jetpack
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		public static void Update()
		{
			if (Jetpack.Jetpackbool && (Binds.Button_Jump.GetState(0) || Input.GetKey(32)))
			{
				Vector3 velocity = Networking.LocalPlayer.GetVelocity();
				velocity.y = Networking.LocalPlayer.GetJumpImpulse();
				Networking.LocalPlayer.SetVelocity(velocity);
			}
		}

		// Token: 0x040000EA RID: 234
		public static bool Jetpackbool;
	}
}
