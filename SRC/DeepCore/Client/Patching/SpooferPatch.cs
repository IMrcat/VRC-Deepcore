using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AmplitudeSDKWrapper;
using HarmonyLib;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;

namespace DeepCore.Client.Patching
{
	// Token: 0x02000014 RID: 20
	internal class SpooferPatch
	{
		// Token: 0x06000065 RID: 101
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x06000067 RID: 103 RVA: 0x000044D4 File Offset: 0x000026D4
		public static void InitSpoofs()
		{
			try
			{
				EasyPatching.DeepCoreInstance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(SpooferPatch), "FakeHWID", null, null)), null, null, null, null);
				EasyPatching.DeepCoreInstance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(SpooferPatch), "VoidPatch", null, null)), null, null, null, null);
				SpooferPatch.SafetyPatch();
			}
			catch (Exception ex)
			{
				DeepConsole.Log("Spoofer", string.Format("Could not patch Analystics failed\n{0}", ex));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004594 File Offset: 0x00002794
		private static bool VoidPatch()
		{
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004598 File Offset: 0x00002798
		private static bool FakeHWID(ref string __result)
		{
			if (SpooferPatch.newHWID == string.Empty)
			{
				SpooferPatch.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
				{
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9)
				}))).Select(delegate(byte x)
				{
					byte b = x;
					return b.ToString("x2");
				})
					.Aggregate((string x, string y) => x + y);
				DeepConsole.Log("Spoofer", "HWID:" + SpooferPatch.newHWID);
			}
			__result = SpooferPatch.newHWID;
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000046E0 File Offset: 0x000028E0
		public unsafe static void SafetyPatch()
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = SpooferPatch.LoadLibrary(MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll");
			}
			catch (Exception)
			{
				DeepConsole.Log("Spoofer", "Can't Spoof Steam");
			}
			if (intPtr == IntPtr.Zero)
			{
				DeepConsole.Log("Spoofer", "Can't Spoof Steam");
			}
			IntPtr procAddress = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_Init");
			IntPtr procAddress2 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_RestartAppIfNecessary");
			IntPtr procAddress3 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_GetHSteamUser");
			IntPtr procAddress4 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_RegisterCallback");
			IntPtr procAddress5 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_UnregisterCallback");
			IntPtr procAddress6 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_RunCallbacks");
			IntPtr procAddress7 = SpooferPatch.GetProcAddress(intPtr, "SteamAPI_Shutdown");
			try
			{
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress2)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress3)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress4)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress5)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress6)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
				MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress7)), AccessTools.Method(typeof(SpooferPatch), "SteamSpoof", null, null).MethodHandle.GetFunctionPointer());
			}
			catch
			{
				DeepConsole.Log("Spoofer", "Cant Spoof Steam");
			}
			IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr2)), AccessTools.Method(typeof(SpooferPatch), "FakeModel", null, null).MethodHandle.GetFunctionPointer());
			IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr3)), AccessTools.Method(typeof(SpooferPatch), "FakeName", null, null).MethodHandle.GetFunctionPointer());
			IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr4)), AccessTools.Method(typeof(SpooferPatch), "FakeGBU", null, null).MethodHandle.GetFunctionPointer());
			IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceID");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr5)), AccessTools.Method(typeof(SpooferPatch), "FakeGBUID", null, null).MethodHandle.GetFunctionPointer());
			IntPtr intPtr6 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr6)), AccessTools.Method(typeof(SpooferPatch), "FakeProcessor", null, null).MethodHandle.GetFunctionPointer());
			IntPtr intPtr7 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetOperatingSystem");
			MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr7)), AccessTools.Method(typeof(SpooferPatch), "FakeOS", null, null).MethodHandle.GetFunctionPointer());
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public static IntPtr FakeModel()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp(SpooferPatch.Motherboards[new Random().Next(0, SpooferPatch.Motherboards.Length)])).Pointer;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004AC9 File Offset: 0x00002CC9
		public static IntPtr FakeName()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + SpooferPatch.RandomString(7))).Pointer;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004AEA File Offset: 0x00002CEA
		public static IntPtr FakeGBU()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp(SpooferPatch.PBU[new Random().Next(0, SpooferPatch.PBU.Length)])).Pointer;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004B13 File Offset: 0x00002D13
		public static IntPtr FakeGBUID()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp(SpooferPatch.RandomString(12))).Pointer;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004B2B File Offset: 0x00002D2B
		public static IntPtr FakeProcessor()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp(SpooferPatch.CPU[new Random().Next(0, SpooferPatch.CPU.Length)])).Pointer;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004B54 File Offset: 0x00002D54
		public static IntPtr FakeOS()
		{
			return new Object(IL2CPP.ManagedStringToIl2Cpp(SpooferPatch.OS[new Random().Next(0, SpooferPatch.OS.Length)])).Pointer;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004B7D File Offset: 0x00002D7D
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
				select s[SpooferPatch.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004BB8 File Offset: 0x00002DB8
		private static bool SteamSpoof()
		{
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004BBB File Offset: 0x00002DBB
		private static bool VoidPatchTrue(bool __result)
		{
			return false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004BC1 File Offset: 0x00002DC1
		private static bool VoidPatchFalse(bool __result)
		{
			return false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004BC7 File Offset: 0x00002DC7
		private static bool PatchFPS(ref float __result)
		{
			if (SpooferPatch.Fakefpsenabled)
			{
				return true;
			}
			__result = 1f / Math.Max(1f, (float)SpooferPatch.FakefpsValue);
			return false;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004BEB File Offset: 0x00002DEB
		private static bool PatchPing(ref int __result)
		{
			if (SpooferPatch.fakepingenabled)
			{
				return true;
			}
			__result = Math.Max(0, SpooferPatch.FakepingValue);
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004C04 File Offset: 0x00002E04
		private static bool RoundTrip(ref int __result)
		{
			if (SpooferPatch.fakepingenabled)
			{
				int num = 1;
				float num2 = Mathf.Sin(Time.realtimeSinceStartup / 14210f);
				if (num2 < 0f)
				{
					num2 = -num2;
				}
				float num3 = Mathf.Cos(Time.realtimeSinceStartup / 5f);
				if (num3 < 0f)
				{
					num3 = -num3;
				}
				float num4 = num2 * num3;
				num = (int)((float)num + (float)(num / -23193) * num4);
				__result = num;
				return false;
			}
			return false;
		}

		// Token: 0x04000049 RID: 73
		public static string newHWID = string.Empty;

		// Token: 0x0400004A RID: 74
		public static bool fakepingenabled;

		// Token: 0x0400004B RID: 75
		public static int FakepingValue = 69;

		// Token: 0x0400004C RID: 76
		public static bool Fakefpsenabled = false;

		// Token: 0x0400004D RID: 77
		public static int FakefpsValue = 69;

		// Token: 0x0400004E RID: 78
		private static Random random = new Random();

		// Token: 0x0400004F RID: 79
		private static string[] PBU = new string[]
		{
			"MSI Radeon RX 6900 XT GAMING Z TRIO 16GB", "Gigabyte Radeon RX 6700 XT Gaming OC 12GB", "ASUS DUAL GeForce RTX 2060 6GB OC EVO", "Palit GeForce GTX 1050 Ti StormX 4GB", "MSI GeForce GTX 1650 D6 Ventus XS OCV2 12GB OC", "ASUS GOLD20TH GTX 980 Ti Platinum", "ASUS TUF GeForce RTX 3060 12GB OC Gaming", "NVIDIA Quadro RTX 4000 8GB", "NVIDIA GeForce GTX 1080 Ti", "NVIDIA GeForce GTX 1080",
			"NVIDIA GeForce GTX 1070", "NVIDIA GeForce GTX 1060 6GB", "NVIDIA GeForce GTX 980 Ti"
		};

		// Token: 0x04000050 RID: 80
		private static string[] CPU = new string[] { "AMD Ryzen 5 3600", "AMD Ryzen 7 3700X", "AMD Ryzen 7 5800X", "AMD Ryzen 9 5900X", "AMD Ryzen 9 3900X 12-Core Processor", "INTEL CORE I9-10900K", "INTEL CORE I7-10700K", "INTEL CORE I9-9900K", "Intel Core i7-8700K" };

		// Token: 0x04000051 RID: 81
		private static string[] Motherboards = new string[] { "B550 AORUS PRO (Gigabyte Technology Co., Ltd.)", "Gigabyte B450M DS3H", "Asus AM4 TUF Gaming X570-Plus", "ASRock Z370 Taichi" };

		// Token: 0x04000052 RID: 82
		private static string[] OS = new string[] { "Windows 10  (10.0.0) 64bit", "Windows 10  (10.0.0) 32bit", "Windows 8  (10.0.0) 64bit", "Windows 8  (10.0.0) 32bit", "Windows 7  (10.0.0) 64bit", "Windows 7  (10.0.0) 32bit", "Windows Vista 64bit", "Windows Vista 32bit", "Windows XP 64bit", "Windows XP 32bit" };
	}
}
