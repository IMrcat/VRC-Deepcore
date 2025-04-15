using System;
using System.IO;
using System.Runtime.InteropServices;
using DeepCore.Client.UI.QM;
using MelonLoader;

namespace DeepCore.Client
{
	// Token: 0x0200000B RID: 11
	internal class DeepConsole
	{
		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool AllocConsole();

		// Token: 0x0600003F RID: 63 RVA: 0x000037F8 File Offset: 0x000019F8
		public static void Alloc()
		{
			if (File.Exists("Plugins/IpcPlugin.dll"))
			{
				MelonLogger.Msg("Can't alloc console cause of eac.");
			}
			if (File.Exists("Plugins/ConsolePlugin.dll"))
			{
				MelonLogger.Msg("Can't alloc console cause of ConsolePlugin.dll.");
				return;
			}
			DeepConsole.AllocConsole();
			StreamWriter streamWriter = new StreamWriter(Console.OpenStandardOutput());
			streamWriter.AutoFlush = true;
			Console.SetOut(streamWriter);
			Console.SetError(streamWriter);
			Console.CursorVisible = false;
			Console.Title = "DeepClient - v0.0.5 - Private";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003864 File Offset: 0x00001A64
		public static void Log(string Name, string Content)
		{
			DateTime now = DateTime.Now;
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] [");
			if (Name.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else if (Name.StartsWith("Bot", StringComparison.OrdinalIgnoreCase))
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			Console.Write(Name);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] " + Content + "\n");
			Console.ResetColor();
			QMConsole.PrintLog(Name, Content);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000391C File Offset: 0x00001B1C
		public static void LogConsole(string Name, string Content)
		{
			DateTime now = DateTime.Now;
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] [");
			if (Name.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else if (Name.StartsWith("Bot", StringComparison.OrdinalIgnoreCase))
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			Console.Write(Name);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] " + Content + "\n");
			Console.ResetColor();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000039CC File Offset: 0x00001BCC
		public static void E(Exception ex)
		{
			DateTime now = DateTime.Now;
			Console.ResetColor();
			string.Format("\n============ERROR============\nTIME:{0}\nERROR MESSAGE:{1}\nLAST INSTRUCTIONS:{2}\nFULL ERROR:{3}\n=============END=============\n", new object[]
			{
				now.ToString("HH:mm"),
				ex.Message,
				ex.StackTrace,
				ex
			});
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(ex);
			Console.ResetColor();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003A2C File Offset: 0x00001C2C
		public static void DebugLog(string msg)
		{
			DateTime now = DateTime.Now;
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("============DEBUG============" + msg + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("=============END=============\n");
			Console.ResetColor();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003AEC File Offset: 0x00001CEC
		public static void Warn(string msg)
		{
			DateTime now = DateTime.Now;
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.Write("============Warn===========" + msg + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Report this warning to the dev.\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(now.ToString("HH:mm"));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("=============END=============\n");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003BE9 File Offset: 0x00001DE9
		public static void ChangeTittle(string Name)
		{
			Console.Title = Name;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public static void Art(bool s)
		{
			if (s)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n       ________                      __________        __   \n       \\______ \\   ____   ____ ______\\______   \\ _____/  |_ \n       |    |  \\_/ __ \\_/ __ \\\\____ \\|    |  _//  _ \\   __\\\n       |    `   \\  ___/\\  ___/|  |_> >    |   (  <_> )  |  \n       /_______  /\\___  >\\___  >   __/|______  /\\____/|__|  \n               \\/     \\/     \\/|__|          \\/             \n\n-    When the sun explodes, all I did will be for nothing~   -\n -                        Running v0.0.5                    -\n  -                https://discord.gg/SKhrH4C8K6           -\n");
				return;
			}
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("\n________                       _________ .__  .__               __   \n\\______ \\   ____   ____ ______ \\_   ___ \\|  | |__| ____   _____/  |_ \n |    |  \\_/ __ \\_/ __ \\\\____ \\/    \\  \\/|  | |  |/ __ \\ /    \\   __\\\n |    `   \\  ___/\\  ___/|  |_> >     \\___|  |_|  \\  ___/|   |  \\  |  \n/_______  /\\___  >\\___  >   __/ \\______  /____/__|\\___  >___|  /__|  \n        \\/     \\/     \\/|__|           \\/             \\/     \\/      \n\n-    When the sun explodes, all I did will be for nothing~    -\n -                        Running v0.0.5                     -\n  -                https://discord.gg/SKhrH4C8K6            -\n");
		}
	}
}
