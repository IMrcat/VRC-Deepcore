using System;
using UnityEngine;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x02000071 RID: 113
	internal class ObjectExtensions
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000F3A7 File Offset: 0x0000D5A7
		public static GameObject GetPlayerCamera
		{
			get
			{
				if (ObjectExtensions.CachedPlayerCamera == null)
				{
					ObjectExtensions.CachedPlayerCamera = GameObject.Find("Camera (eye)");
				}
				return ObjectExtensions.CachedPlayerCamera;
			}
		}

		// Token: 0x0400014C RID: 332
		private static GameObject CachedPlayerCamera;
	}
}
