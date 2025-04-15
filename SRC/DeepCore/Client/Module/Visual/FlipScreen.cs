using System;
using UnityEngine;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000031 RID: 49
	internal class FlipScreen
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00009650 File Offset: 0x00007850
		public static void State(bool s)
		{
			if (s)
			{
				FlipScreen.DeflautRotation = Camera.main.gameObject.transform.rotation;
				FlipScreen.IsEnabled = true;
				return;
			}
			FlipScreen.IsEnabled = false;
			Camera.main.gameObject.transform.rotation = FlipScreen.DeflautRotation;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000096A0 File Offset: 0x000078A0
		public static void OnUpdate()
		{
			if (!Input.GetKey(306))
			{
				return;
			}
			Camera main = Camera.main;
			if (main == null)
			{
				return;
			}
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if ((double)axis > 0.1)
			{
				Vector3 vector = main.transform.rotation.eulerAngles;
				vector -= Vector3.forward;
				main.transform.rotation = Quaternion.Euler(vector);
				return;
			}
			if ((double)axis < -0.1)
			{
				Vector3 vector2 = main.transform.rotation.eulerAngles;
				vector2 += Vector3.forward;
				main.transform.rotation = Quaternion.Euler(vector2);
			}
		}

		// Token: 0x0400008F RID: 143
		public static bool IsEnabled;

		// Token: 0x04000090 RID: 144
		public static Quaternion DeflautRotation;
	}
}
