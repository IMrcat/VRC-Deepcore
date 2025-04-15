using System;
using System.Diagnostics;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x02000070 RID: 112
	internal class BotAPI
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000F338 File Offset: 0x0000D538
		public static void LaunchBot(string[] args)
		{
			string text = "";
			foreach (string text2 in args)
			{
				text = text + " " + text2;
			}
			new Process
			{
				StartInfo = 
				{
					FileName = "VRChat.exe",
					Arguments = text,
					UseShellExecute = true
				}
			}.Start();
		}
	}
}
