using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using DeepCore.Protecc;
using MelonLoader;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000041 RID: 65
	internal class RamCleaner
	{
		// Token: 0x06000176 RID: 374
		[DllImport("kernel32.dll")]
		private static extern bool SetProcessWorkingSetSize(IntPtr process, IntPtr minimumWorkingSetSize, IntPtr maximumWorkingSetSize);

		// Token: 0x06000177 RID: 375 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public static void StartMyCleaner()
		{
			DeepConsole.Log("OptiRam", string.Format("Interval: {0} seconds. WorkingSetTrim is {1}.", RamCleaner.clearInterval, RamCleaner.useWorkingSetTrim ? "enabled" : "disabled"));
			RamCleaner.clearTimer = new Timer((double)(RamCleaner.clearInterval * 1000f));
			RamCleaner.clearTimer.Elapsed += RamCleaner.ClearTimer_Elapsed;
			RamCleaner.clearTimer.AutoReset = true;
			RamCleaner.clearTimer.Start();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AD6F File Offset: 0x00008F6F
		public static void forceclear()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			if (RamCleaner.useWorkingSetTrim)
			{
				RamCleaner.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, new IntPtr(-1), new IntPtr(-1));
			}
			BaseProtecc.IsDebuggerPresent();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public static void ClearTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				if (RamCleaner.useWorkingSetTrim)
				{
					RamCleaner.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, new IntPtr(-1), new IntPtr(-1));
				}
				MelonLogger.Msg("RAM cleared successfully.");
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error during RAM clearing: " + ex.Message);
			}
		}

		// Token: 0x040000B1 RID: 177
		public static Timer clearTimer;

		// Token: 0x040000B2 RID: 178
		public static float clearInterval = 120f;

		// Token: 0x040000B3 RID: 179
		public static bool useWorkingSetTrim = true;
	}
}
