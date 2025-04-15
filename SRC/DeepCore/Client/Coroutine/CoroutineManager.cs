using System;
using DeepCore.Client.Misc;
using MelonLoader;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000094 RID: 148
	internal class CoroutineManager
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00013654 File Offset: 0x00011854
		public static void Init()
		{
			DeepConsole.Log("CoroutineManager", "Starting Coroutines...");
			MelonCoroutines.Start(VrcExtensions.SetMicColor());
			MelonCoroutines.Start(CustomStandardPopup.Init());
			MelonCoroutines.Start(CustomLoadingScreen.Init());
			MelonCoroutines.Start(CustomVRLoadingOverlay.Init());
			MelonCoroutines.Start(CustomMenuBG.Init());
		}
	}
}
