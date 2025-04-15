using System;
using System.IO;
using System.Runtime.InteropServices;
using HarmonyLib;
using MelonLoader;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003E RID: 62
	internal class NoSteamAtAll
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000A778 File Offset: 0x00008978
		public unsafe static void Start()
		{
			string text = MelonUtils.GetGameDataDirectory() + "\\Plugins\\steam_api64.dll";
			if (!File.Exists(text))
			{
				text = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll";
			}
			if (!File.Exists(text))
			{
				text = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86\\steam_api64.dll";
			}
			IntPtr intPtr = NoSteamAtAll.LoadLibrary(text);
			if (intPtr == IntPtr.Zero)
			{
				DeepConsole.Log("NoSteamAtAll", "Library load failed; used path: " + text);
				return;
			}
			string[] array = new string[] { "SteamAPI_Init", "SteamAPI_RestartAppIfNecessary", "SteamAPI_GetHSteamUser", "SteamAPI_RegisterCallback", "SteamAPI_UnregisterCallback", "SteamAPI_RunCallbacks", "SteamAPI_Shutdown" };
			bool flag = false;
			if (ConfManager.ShouldSteamAPI.Value)
			{
				return;
			}
			foreach (string text2 in array)
			{
				IntPtr procAddress = NoSteamAtAll.GetProcAddress(intPtr, text2);
				if (procAddress == IntPtr.Zero)
				{
					DeepConsole.Log("NoSteamAtAll", "Procedure " + text2 + " not found");
				}
				else
				{
					MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress)), AccessTools.Method(typeof(NoSteamAtAll), "InitFail", null, null).MethodHandle.GetFunctionPointer());
					flag = true;
				}
			}
			if (flag)
			{
				DeepConsole.Log("NoSteamAtAll", "Disabled SteamAPI");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public static string OriginalAuthor
		{
			get
			{
				return "knah";
			}
		}

		// Token: 0x06000163 RID: 355
		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

		// Token: 0x06000164 RID: 356
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000165 RID: 357 RVA: 0x0000A8DB File Offset: 0x00008ADB
		public static bool InitFail()
		{
			return false;
		}
	}
}
