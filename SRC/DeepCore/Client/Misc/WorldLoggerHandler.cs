using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VRC.Core;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007B RID: 123
	public class WorldLoggerHandler
	{
		// Token: 0x06000274 RID: 628 RVA: 0x000109B4 File Offset: 0x0000EBB4
		public static void Log()
		{
			string text = "DeepClient/WorldLogger.json";
			List<Instance> list = new List<Instance>();
			if (File.Exists(text))
			{
				string text2 = File.ReadAllText(text);
				if (!string.IsNullOrWhiteSpace(text2))
				{
					list = JsonConvert.DeserializeObject<List<Instance>>(text2) ?? new List<Instance>();
				}
			}
			list.Add(new Instance
			{
				InstanceID = APIUser.CurrentUser.location,
				InstanceType = RoomManager.Method_Internal_Static_get_ApiWorldInstance_0().type.ToString(),
				InstanceName = RoomManager.Method_Internal_Static_get_ApiWorldInstance_0().name,
				InstanceOwner = "idk",
				WorldName = RoomManager.field_Internal_Static_ApiWorld_0.name
			});
			string text3 = JsonConvert.SerializeObject(list, 1);
			File.WriteAllText(text, text3);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010A68 File Offset: 0x0000EC68
		internal static void Clear()
		{
			if (!File.Exists(WorldLoggerHandler.filePath))
			{
				return;
			}
			File.Delete(WorldLoggerHandler.filePath);
			DeepConsole.Log("WLogger", "World Log file cleared.");
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010A90 File Offset: 0x0000EC90
		internal static List<World_Object> Fetch()
		{
			return WorldLoggerHandler.FetchFile();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00010A98 File Offset: 0x0000EC98
		private static List<World_Object> FetchFile()
		{
			List<World_Object> list = null;
			if (File.Exists(WorldLoggerHandler.filePath))
			{
				list = JsonConvert.DeserializeObject<List<World_Object>>(File.ReadAllText(WorldLoggerHandler.filePath));
			}
			return list ?? new List<World_Object>();
		}

		// Token: 0x04000160 RID: 352
		private const string fileName = "/WorldLogger.json";

		// Token: 0x04000161 RID: 353
		private static readonly string filePath = ConfManager.getResourcePathFull() + "/WorldLogger.json";

		// Token: 0x04000162 RID: 354
		private static readonly int maxEntries = 96;
	}
}
