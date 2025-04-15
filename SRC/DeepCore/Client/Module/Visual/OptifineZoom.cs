using System;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000035 RID: 53
	internal class OptifineZoom
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public static void Update()
		{
			if (ConfManager.OptifineZoom.Value && Networking.LocalPlayer != null)
			{
				if (Input.GetKey(308))
				{
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 10f, Time.deltaTime * 5f);
					return;
				}
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 5f);
			}
		}
	}
}
