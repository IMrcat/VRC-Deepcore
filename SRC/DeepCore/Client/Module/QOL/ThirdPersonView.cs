using System;
using DeepCore.Client.Module.Movement;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000042 RID: 66
	internal class ThirdPersonView
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000AE30 File Offset: 0x00009030
		public static bool OnStart()
		{
			bool flag;
			try
			{
				GameObject gameObject = GameObject.Find("TrackingVolume/TrackingSteam2(Clone)/SteamCamera/[CameraRig]/Neck/Camera");
				ThirdPersonView.vrcCamera = ((gameObject != null) ? gameObject.GetComponent<Camera>() : null);
				if (ThirdPersonView.vrcCamera == null)
				{
					DeepConsole.Log("ThirdPerson", "Couldn't find camera !");
					flag = false;
				}
				else
				{
					Transform transform = ThirdPersonView.vrcCamera.transform;
					ThirdPersonView.thirdPersonCamera = new GameObject("Standalone ThirdPerson Camera").AddComponent<Camera>();
					ThirdPersonView.thirdPersonCamera.fieldOfView = ModSettings.FOV;
					ThirdPersonView.thirdPersonCamera.nearClipPlane = ModSettings.NearClipPlane;
					ThirdPersonView.thirdPersonCamera.enabled = false;
					ThirdPersonView.thirdPersonCamera.transform.parent = transform.parent;
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000AEF0 File Offset: 0x000090F0
		private static void RepositionCamera(bool isBehind, CameraBehindMode cameraBehindMode)
		{
			Transform transform = ThirdPersonView.vrcCamera.transform;
			Transform transform2 = ThirdPersonView.thirdPersonCamera.transform;
			transform2.parent = transform;
			transform2.position = transform.position + (isBehind ? (-transform.forward) : transform.forward);
			transform2.LookAt(transform);
			if (isBehind)
			{
				if (cameraBehindMode == CameraBehindMode.RightShoulder)
				{
					transform2.position += transform.right * 0.5f;
				}
				if (cameraBehindMode == CameraBehindMode.LeftShoulder)
				{
					transform2.position -= transform.right * 0.5f;
				}
			}
			transform2.position += transform2.forward * 0.25f;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000AFB8 File Offset: 0x000091B8
		private static void FreeformCameraUpdate()
		{
			float num = 0f;
			if (Input.GetKey(273))
			{
				num -= 1f;
			}
			if (Input.GetKey(274))
			{
				num += 1f;
			}
			float num2 = 0f;
			if (Input.GetKey(276))
			{
				num2 -= 1f;
			}
			if (Input.GetKey(275))
			{
				num2 += 1f;
			}
			ThirdPersonView.thirdPersonCamera.transform.eulerAngles += new Vector3(num, num2, 0f);
			Vector3 vector = Vector3.zero;
			if (Input.GetKey(280))
			{
				vector += ThirdPersonView.thirdPersonCamera.transform.up;
			}
			if (Input.GetKey(281))
			{
				vector -= ThirdPersonView.thirdPersonCamera.transform.up;
			}
			if (Input.GetKey(107))
			{
				vector += ThirdPersonView.thirdPersonCamera.transform.right;
			}
			if (Input.GetKey(104))
			{
				vector -= ThirdPersonView.thirdPersonCamera.transform.right;
			}
			if (Input.GetKey(117))
			{
				vector += ThirdPersonView.thirdPersonCamera.transform.forward;
			}
			if (Input.GetKey(106))
			{
				vector -= ThirdPersonView.thirdPersonCamera.transform.forward;
			}
			ThirdPersonView.thirdPersonCamera.transform.position += vector * Time.deltaTime * 1f;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B13C File Offset: 0x0000933C
		public static void UpdateCameraSettings()
		{
			if (ThirdPersonView.thirdPersonCamera == null)
			{
				return;
			}
			ThirdPersonView.thirdPersonCamera.fieldOfView = ModSettings.FOV;
			ThirdPersonView.thirdPersonCamera.nearClipPlane = ModSettings.NearClipPlane;
			if (!ModSettings.Enabled)
			{
				ThirdPersonView.thirdPersonCamera.enabled = false;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B17C File Offset: 0x0000937C
		public static void Update()
		{
			if (ConfManager.ThirdPersonKeyBind.Value)
			{
				if (Input.GetKeyDown(270))
				{
					Camera.main.fieldOfView += 2f;
				}
				if (Input.GetKeyDown(269))
				{
					Camera.main.fieldOfView -= 2f;
				}
				if (Input.GetKeyDown(112))
				{
					ThirdPersonView.currentMode++;
					if (ThirdPersonView.currentMode > CameraMode.InFront)
					{
						ThirdPersonView.currentMode = CameraMode.Normal;
					}
					if (ThirdPersonView.currentMode != CameraMode.Normal)
					{
						ThirdPersonView.RepositionCamera(ThirdPersonView.currentMode == CameraMode.Behind, ThirdPersonView.cameraBehindMode);
						ThirdPersonView.thirdPersonCamera.enabled = true;
					}
					else
					{
						ThirdPersonView.thirdPersonCamera.enabled = false;
					}
				}
				else if (Input.GetKeyDown(111))
				{
					if (ThirdPersonView.currentMode == CameraMode.Freeform)
					{
						ThirdPersonView.currentMode = CameraMode.Normal;
						ThirdPersonView.thirdPersonCamera.enabled = false;
					}
					else
					{
						ThirdPersonView.currentMode = CameraMode.Freeform;
						ThirdPersonView.thirdPersonCamera.transform.parent = null;
						ThirdPersonView.thirdPersonCamera.enabled = true;
					}
				}
				if (ThirdPersonView.currentMode != CameraMode.Normal)
				{
					if (Input.GetKeyDown(27))
					{
						ThirdPersonView.currentMode = CameraMode.Normal;
						ThirdPersonView.thirdPersonCamera.enabled = false;
					}
					ThirdPersonView.thirdPersonCamera.transform.position += ThirdPersonView.thirdPersonCamera.transform.forward * Input.GetAxis("Mouse ScrollWheel");
					if (ThirdPersonView.currentMode == CameraMode.Freeform)
					{
						ThirdPersonView.FreeformCameraUpdate();
					}
					else if (ThirdPersonView.currentMode == CameraMode.Behind && ModSettings.RearCameraChangedEnabled)
					{
						if (Input.GetKeyDown(ModSettings.MoveRearCameraLeftKeyBind) && !Flight.IsFlyEnabled)
						{
							ThirdPersonView.cameraBehindMode--;
							if (ThirdPersonView.cameraBehindMode <= CameraBehindMode.LeftShoulder)
							{
								ThirdPersonView.cameraBehindMode = CameraBehindMode.LeftShoulder;
							}
							ThirdPersonView.RepositionCamera(true, ThirdPersonView.cameraBehindMode);
						}
						if (Input.GetKeyDown(ModSettings.MoveRearCameraRightKeyBind) && !Flight.IsFlyEnabled)
						{
							ThirdPersonView.cameraBehindMode++;
							if (ThirdPersonView.cameraBehindMode > CameraBehindMode.RightShoulder)
							{
								ThirdPersonView.cameraBehindMode = CameraBehindMode.RightShoulder;
							}
							ThirdPersonView.RepositionCamera(true, ThirdPersonView.cameraBehindMode);
						}
					}
				}
				if (ThirdPersonView.currentMode != CameraMode.Normal)
				{
					ThirdPersonView.IsEnabled = true;
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B36E File Offset: 0x0000956E
		public static void OnPreferencesLoaded()
		{
			ModSettings.LoadSettings();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B375 File Offset: 0x00009575
		public static void OnPreferencesSaved()
		{
			ModSettings.LoadSettings();
		}

		// Token: 0x040000B4 RID: 180
		private static CameraMode currentMode = CameraMode.Normal;

		// Token: 0x040000B5 RID: 181
		private static CameraBehindMode cameraBehindMode = CameraBehindMode.Center;

		// Token: 0x040000B6 RID: 182
		private static Camera thirdPersonCamera;

		// Token: 0x040000B7 RID: 183
		private static Camera vrcCamera;

		// Token: 0x040000B8 RID: 184
		public static bool IsEnabled = false;
	}
}
