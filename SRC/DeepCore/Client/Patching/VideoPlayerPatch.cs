using System;
using VRC.SDK3.Video.Components;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000017 RID: 23
	internal class VideoPlayerPatch
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004E85 File Offset: 0x00003085
		public static void Patch()
		{
			EasyPatching.EasyPatchMethodPost(typeof(VRCUnityVideoPlayer), "LoadURL", typeof(VideoPlayerPatch), "Patch");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004EAC File Offset: 0x000030AC
		public static void Patch(IntPtr instance, IntPtr __0)
		{
			VRCUrl vrcurl = ((__0 == IntPtr.Zero) ? null : new VRCUrl(__0));
			if (vrcurl == null)
			{
				return;
			}
			if (!ConfManager.VideoPlayerURLLogger.Value)
			{
				DeepConsole.Log("VideoPlayer", "Video loading from:" + vrcurl.url);
				return;
			}
			DeepConsole.Log("VideoPlayer", "Video tried loading from:" + vrcurl.url);
		}
	}
}
