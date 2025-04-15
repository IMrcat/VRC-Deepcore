using System;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000076 RID: 118
	internal class MediaKeys
	{
		// Token: 0x0600025B RID: 603
		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x0600025C RID: 604
		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		public static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

		// Token: 0x0600025D RID: 605 RVA: 0x0000F8FE File Offset: 0x0000DAFE
		public static void PlayPause()
		{
			MediaKeys.keybd_event(179, 179, 0, 0);
			MediaKeys.keybd_event(179, 179, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F926 File Offset: 0x0000DB26
		public static void PrevTrack()
		{
			MediaKeys.keybd_event(177, 177, 0, 0);
			MediaKeys.keybd_event(177, 177, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F94E File Offset: 0x0000DB4E
		public static void NextTrack()
		{
			MediaKeys.keybd_event(176, 176, 0, 0);
			MediaKeys.keybd_event(176, 176, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F976 File Offset: 0x0000DB76
		public static void Stop()
		{
			MediaKeys.keybd_event(178, 178, 0, 0);
			MediaKeys.keybd_event(178, 178, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F99E File Offset: 0x0000DB9E
		public static void VolumeUp()
		{
			MediaKeys.keybd_event(175, 175, 0, 0);
			MediaKeys.keybd_event(175, 175, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
		public static void VolumeDown()
		{
			MediaKeys.keybd_event(174, 174, 0, 0);
			MediaKeys.keybd_event(174, 174, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F9EE File Offset: 0x0000DBEE
		public static void VolumeMute()
		{
			MediaKeys.keybd_event(173, 173, 0, 0);
			MediaKeys.keybd_event(173, 173, MediaKeys.KEYEVENTF_KEYUP, 0);
		}

		// Token: 0x0400015A RID: 346
		private static readonly int KEYEVENTF_KEYUP = 2;
	}
}
