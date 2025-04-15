using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007A RID: 122
	internal class WMessageBox
	{
		// Token: 0x06000271 RID: 625
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr MessageBox(int hWnd, string text, string caption, uint type);

		// Token: 0x06000272 RID: 626 RVA: 0x00010983 File Offset: 0x0000EB83
		internal static void HandleInternalFailure(string message, bool shouldclose)
		{
			SystemSounds.Asterisk.Play();
			WMessageBox.MessageBox(0, message, "DeepClient - INTERNAL FAILURE!", 0U);
			while (shouldclose)
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}
}
