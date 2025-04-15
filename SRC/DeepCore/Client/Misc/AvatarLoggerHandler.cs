using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VRC.Core;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000081 RID: 129
	internal class AvatarLoggerHandler
	{
		// Token: 0x06000299 RID: 665 RVA: 0x00011064 File Offset: 0x0000F264
		internal static void Log(ApiAvatar avtr)
		{
			AvatarLoggerHandler.Handle(new Avatar_Object(avtr));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00011071 File Offset: 0x0000F271
		internal static void Log(Avatar_Object avtr)
		{
			AvatarLoggerHandler.Handle(avtr);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011079 File Offset: 0x0000F279
		internal static List<Avatar_Object> Fetch()
		{
			return AvatarLoggerHandler.FetchFile();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00011080 File Offset: 0x0000F280
		internal static void Clear()
		{
			if (!File.Exists(AvatarLoggerHandler.filePath))
			{
				return;
			}
			File.Delete(AvatarLoggerHandler.filePath);
			DeepConsole.Log("AvaLogger", "Avatar Logger file cleared!");
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000110A8 File Offset: 0x0000F2A8
		private static void Handle(Avatar_Object avtr)
		{
			try
			{
				List<Avatar_Object> list = AvatarLoggerHandler.FetchFile();
				list.RemoveAll((Avatar_Object avatarObject) => avatarObject.id == avtr.id);
				list.Add(avtr);
				if (list.Count > AvatarLoggerHandler.maxEntries)
				{
					list.RemoveRange(0, list.Count - AvatarLoggerHandler.maxEntries);
				}
				File.WriteAllText(AvatarLoggerHandler.filePath, JsonConvert.SerializeObject(list));
			}
			catch (Exception ex)
			{
				DeepConsole.Log("AvaLogger", "Failed fetching/writing to avatar log file. " + ex.Message);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00011148 File Offset: 0x0000F348
		private static List<Avatar_Object> FetchFile()
		{
			List<Avatar_Object> list = null;
			if (File.Exists(AvatarLoggerHandler.filePath))
			{
				list = JsonConvert.DeserializeObject<List<Avatar_Object>>(File.ReadAllText(AvatarLoggerHandler.filePath));
			}
			return list ?? new List<Avatar_Object>();
		}

		// Token: 0x0400016B RID: 363
		private const string fileName = "/AvatarLogger.json";

		// Token: 0x0400016C RID: 364
		private static readonly int maxEntries = ConfManager.maxAvatarLogToFile.Value;

		// Token: 0x0400016D RID: 365
		private static readonly string filePath = ConfManager.getResourcePathFull() + "/AvatarLogger.json";
	}
}
