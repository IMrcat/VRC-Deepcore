using System;
using DeepCore.Client.Misc;
using MelonLoader;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003C RID: 60
	internal class LastInstanceRejoiner
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000A133 File Offset: 0x00008333
		public static void Rejoin()
		{
			Networking.GoToRoom(ConfManager.LastInstanceID.Value);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A145 File Offset: 0x00008345
		public static void SaveInstanceID()
		{
			if (ConfManager.JoinLastInstance.Value)
			{
				ConfManager.LastInstanceID.Value = APIUser.CurrentUser.location;
				LastInstanceRejoiner.SaveCurrentInctance();
				MelonPreferences.Save();
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A171 File Offset: 0x00008371
		public static void SaveCurrentInctance()
		{
			if (ConfManager.JoinLastInstance.Value)
			{
				WorldLoggerHandler.Log();
			}
		}
	}
}
