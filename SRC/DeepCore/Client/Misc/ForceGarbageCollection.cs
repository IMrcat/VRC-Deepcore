using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007D RID: 125
	public static class ForceGarbageCollection
	{
		// Token: 0x0600028A RID: 650
		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		internal static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

		// Token: 0x0600028B RID: 651
		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		internal static extern bool SetProcessWorkingSetSize64Bit(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);

		// Token: 0x0600028C RID: 652 RVA: 0x00010BF2 File Offset: 0x0000EDF2
		public static void RamClear()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				ForceGarbageCollection.SetProcessWorkingSetSize32Bit(Process.GetCurrentProcess().Handle, -1, -1);
				VrcExtensions.HudNotif("Cleared GarbageCollector.");
			}
		}
	}
}
