using System;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x02000051 RID: 81
	internal class RayCastTP
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000C560 File Offset: 0x0000A760
		public static void Update()
		{
			RaycastHit raycastHit;
			if (RayCastTP.Enabled && Input.GetKey(306) && Input.GetKeyDown(323) && Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), ref raycastHit))
			{
				Networking.LocalPlayer.gameObject.transform.position = raycastHit.point;
			}
			RaycastHit raycastHit2;
			if (RayCastTP.Pickup2Click && Input.GetKey(306) && Input.GetKeyDown(323) && Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), ref raycastHit2))
			{
				foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
				{
					Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
					vrc_Pickup.transform.position = raycastHit2.point;
				}
			}
		}

		// Token: 0x040000E5 RID: 229
		public static bool Enabled;

		// Token: 0x040000E6 RID: 230
		public static bool Pickup2Click;
	}
}
