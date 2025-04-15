using System;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000040 RID: 64
	internal class QMFreeze
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000AC2E File Offset: 0x00008E2E
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000AC35 File Offset: 0x00008E35
		public static bool Enabled { get; set; } = true;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AC3D File Offset: 0x00008E3D
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static bool RestoreVelocity { get; set; } = false;

		// Token: 0x06000173 RID: 371 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public static void State(bool s)
		{
			if (!s)
			{
				Physics.gravity = QMFreeze._originalGravity;
				if (QMFreeze.RestoreVelocity)
				{
					Networking.LocalPlayer.SetVelocity(QMFreeze._originalVelocity);
				}
				QMFreeze.Frozen = false;
				return;
			}
			QMFreeze._originalGravity = Physics.gravity;
			QMFreeze._originalVelocity = Player.Method_Internal_Static_get_Player_0().field_Private_VRCPlayerApi_0.GetVelocity();
			if (QMFreeze._originalVelocity == Vector3.zero)
			{
				return;
			}
			Physics.gravity = Vector3.zero;
			Player.Method_Internal_Static_get_Player_0().field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
			QMFreeze.Frozen = true;
		}

		// Token: 0x040000AC RID: 172
		public static bool Frozen;

		// Token: 0x040000AD RID: 173
		private static Vector3 _originalGravity;

		// Token: 0x040000AE RID: 174
		private static Vector3 _originalVelocity;
	}
}
