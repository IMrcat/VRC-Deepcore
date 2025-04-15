using System;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x0200002C RID: 44
	internal class PickupUtils
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public static void TakeOwnerShipPickup(VRC_Pickup pickup)
		{
			if (!(pickup == null))
			{
				Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public static void Respawn()
		{
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
				vrc_Pickup.transform.localPosition = new Vector3(0f, -100000f, 0f);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008E38 File Offset: 0x00007038
		public static void BringPickups()
		{
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
				vrc_Pickup.transform.position = Networking.LocalPlayer.gameObject.transform.position;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008EAC File Offset: 0x000070AC
		public static void rotateobjse()
		{
			PickupUtils.rotationAngle += 45f;
			if (PickupUtils.rotationAngle >= 360f)
			{
				PickupUtils.rotationAngle -= 360f;
			}
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
				vrc_Pickup.transform.rotation = Quaternion.Euler(0f, PickupUtils.rotationAngle, 0f);
			}
		}

		// Token: 0x04000085 RID: 133
		public static VRC_Pickup[] array;

		// Token: 0x04000086 RID: 134
		public static float rotationAngle;
	}
}
