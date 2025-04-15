using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MelonLoader;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003A RID: 58
	internal class CoreLimiter
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00009F44 File Offset: 0x00008144
		public static void Start()
		{
			MelonPreferences.CreateCategory("CoreLimiter", "Core Limiter");
			MelonPreferences.CreateEntry<int>("CoreLimiter", "MaxCores", 4, "Maximum cores", null, false, false, null);
			MelonPreferences.CreateEntry<bool>("CoreLimiter", "SkipHyperThreads", true, "Don't use both threads of a core", null, false, false, null);
			DeepConsole.Log("CoreLimiter", string.Format("Have {0} processor cores.", Environment.ProcessorCount));
			CoreLimiter.ApplyAffinity();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009FB8 File Offset: 0x000081B8
		public static void ApplyAffinity()
		{
			int processorCount = Environment.ProcessorCount;
			long num = 0L;
			int entryValue = MelonPreferences.GetEntryValue<int>("CoreLimiter", "MaxCores");
			int num2 = processorCount - 1;
			int num3 = (MelonPreferences.GetEntryValue<bool>("CoreLimiter", "SkipHyperThreads") ? 2 : 1);
			int num4 = 0;
			while (num4 < entryValue && num2 >= 0)
			{
				num |= 1L << num2;
				num2 -= num3;
				num4++;
			}
			IntPtr handle = Process.GetCurrentProcess().Handle;
			DeepConsole.Log("CoreLimiter", string.Format("Assigning affinity mask: {0}.", num));
			CoreLimiter.SetProcessAffinityMask(handle, new IntPtr(num));
		}

		// Token: 0x0600014D RID: 333
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool SetProcessAffinityMask(IntPtr hProcess, IntPtr dwProcessAffinityMask);

		// Token: 0x04000097 RID: 151
		private const string CoreLimiterPrefCategory = "CoreLimiter";

		// Token: 0x04000098 RID: 152
		private const string MaxCoresPref = "MaxCores";

		// Token: 0x04000099 RID: 153
		private const string SkipHyperThreadsPref = "SkipHyperThreads";
	}
}
