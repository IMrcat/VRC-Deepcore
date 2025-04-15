using System;
using System.IO;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000039 RID: 57
	internal class CacheCleanr
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00009EA8 File Offset: 0x000080A8
		public static void Clean()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace("Local", "LocalLow"), "VRChat", "VRChat", "Cache-WindowsPlayer");
			if (Directory.Exists(text))
			{
				try
				{
					Directory.Delete(text, true);
					DeepConsole.Log("Cleaner", "VRChat cache cleaned successfully.");
					return;
				}
				catch (Exception ex)
				{
					DeepConsole.Log("Cleaner", "Error cleaning VRChat cache: " + ex.Message);
					return;
				}
			}
			DeepConsole.Log("Cleaner", "VRChat cache directory not found. WTF ???");
		}
	}
}
