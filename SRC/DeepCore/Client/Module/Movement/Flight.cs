using System;
using DeepCore.Client.Misc;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
	// Token: 0x0200004D RID: 77
	internal class Flight
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public static void FlyToggle()
		{
			Flight.IsFlyEnabled = !Flight.IsFlyEnabled;
			if (Flight.IsFlyEnabled)
			{
				Flight._originalGravity = Physics.gravity;
				Flight._originalVelocity = Player.Method_Internal_Static_get_Player_0().field_Private_VRCPlayerApi_0.GetVelocity();
				Physics.gravity = Vector3.zero;
				Player.Method_Internal_Static_get_Player_0().field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
				VrcExtensions.ToggleCharacterController(false);
				VrcExtensions.HudNotif("Flight Enabled.");
				return;
			}
			Flight.fuck = !Flight.fuck;
			Flight.fuck2 = !Flight.fuck2;
			Physics.gravity = Flight._originalGravity;
			Networking.LocalPlayer.SetVelocity(Flight._originalVelocity);
			VrcExtensions.ToggleCharacterController(true);
			VrcExtensions.HudNotif("Flight Disabled.");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C054 File Offset: 0x0000A254
		public static void FlyUpdate()
		{
			if (Networking.LocalPlayer != null)
			{
				if (Flight.fuck2)
				{
					Player.Method_Internal_Static_get_Player_0().gameObject.GetComponent<CharacterController>().enabled = true;
					Flight.fuck2 = false;
				}
				if (Flight.IsFlyEnabled)
				{
					foreach (VRCPlayerApi vrcplayerApi in VRCPlayerApi.AllPlayers)
					{
						if (vrcplayerApi.isLocal)
						{
							if (Flight.fuck)
							{
								Player.Method_Internal_Static_get_Player_0().gameObject.GetComponent<CharacterController>().enabled = false;
								Flight.fuck = false;
							}
							Transform transform = vrcplayerApi.gameObject.transform;
							if (Input.GetKey(119))
							{
								transform.position += transform.forward * Flight.FlySpeed * Time.deltaTime;
							}
							if (Input.GetKey(115))
							{
								transform.position -= transform.forward * Flight.FlySpeed * Time.deltaTime;
							}
							if (Input.GetKey(97))
							{
								transform.position -= transform.right * Flight.FlySpeed * Time.deltaTime;
							}
							if (Input.GetKey(100))
							{
								transform.position += transform.right * Flight.FlySpeed * Time.deltaTime;
							}
							if (Input.GetKey(32))
							{
								transform.position += transform.up * Flight.FlySpeed * Time.deltaTime;
							}
							if (Input.GetKey(304))
							{
								transform.position -= transform.up * Flight.FlySpeed * Time.deltaTime;
							}
							else
							{
								if (XRSettings.isDeviceActive)
								{
									if (Binds.MoveJoystick.GetAxis(1).y > 0f)
									{
										transform.position += transform.forward * Flight.FlySpeed * Time.deltaTime;
									}
									if (Binds.MoveJoystick.GetAxis(1).y < -0.5f)
									{
										transform.position -= transform.forward * Flight.FlySpeed * Time.deltaTime;
									}
									if (Binds.MoveJoystick.GetAxis(1).x < 0f)
									{
										transform.position -= transform.right * Flight.FlySpeed * Time.deltaTime;
									}
									if (Binds.MoveJoystick.GetAxis(1).x > 0f)
									{
										transform.position += transform.right * Flight.FlySpeed * Time.deltaTime;
									}
									if (Binds.RotateJoystick.GetAxis(2).y > 0.5f)
									{
										transform.position += transform.up * Flight.FlySpeed * Time.deltaTime;
									}
									if (Binds.RotateJoystick.GetAxis(2).y < -0.5f)
									{
										transform.position -= transform.up * Flight.FlySpeed * Time.deltaTime;
									}
								}
								Networking.LocalPlayer.SetVelocity(Vector3.zero);
							}
						}
					}
				}
			}
		}

		// Token: 0x040000DB RID: 219
		public static float FlySpeed = 5f;

		// Token: 0x040000DC RID: 220
		public static bool fuck = false;

		// Token: 0x040000DD RID: 221
		public static bool fuck2 = false;

		// Token: 0x040000DE RID: 222
		public static bool IsFlyEnabled = false;

		// Token: 0x040000DF RID: 223
		private static Vector3 _originalGravity;

		// Token: 0x040000E0 RID: 224
		private static Vector3 _originalVelocity;
	}
}
