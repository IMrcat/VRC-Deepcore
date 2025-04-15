using System;
using UnityEngine;

namespace DeepCore.Client.Mono
{
	// Token: 0x02000022 RID: 34
	internal class CoroutineHandler : MonoBehaviour
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005B5C File Offset: 0x00003D5C
		public static CoroutineHandler Instance
		{
			get
			{
				if (CoroutineHandler._instance == null)
				{
					GameObject gameObject = new GameObject("CoroutineHandler");
					CoroutineHandler._instance = gameObject.AddComponent<CoroutineHandler>();
					Object.DontDestroyOnLoad(gameObject);
					Debug.Log("[CoroutineHandler] Instance created.");
				}
				return CoroutineHandler._instance;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005B9C File Offset: 0x00003D9C
		private void Awake()
		{
			if (CoroutineHandler._instance == null)
			{
				CoroutineHandler._instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
				Debug.Log("[CoroutineHandler] Instance set in Awake.");
				return;
			}
			if (CoroutineHandler._instance != this)
			{
				Object.Destroy(base.gameObject);
				Debug.Log("[CoroutineHandler] Duplicate instance destroyed.");
			}
		}

		// Token: 0x0400005A RID: 90
		private static CoroutineHandler _instance;
	}
}
