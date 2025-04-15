using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeepCore.Client.Misc;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client
{
	// Token: 0x0200000A RID: 10
	public static class ClientUtils
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000034C4 File Offset: 0x000016C4
		internal static GameObject FindUIObject(this GameObject parent, string name)
		{
			if (parent == null)
			{
				return null;
			}
			foreach (Transform transform in parent.GetComponentsInChildren<Transform>(true))
			{
				if (transform.name == name)
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003511 File Offset: 0x00001711
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000351E File Offset: 0x0000171E
		public static void CheckIfLoadedByLoader()
		{
			if (!File.Exists("Plugins/DeepCoreLoader.dll"))
			{
				WMessageBox.HandleInternalFailure("DC01: Failed to connect to realtime severs.", true);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003538 File Offset: 0x00001738
		public static void LogUdon()
		{
			string text = null;
			foreach (UdonBehaviour udonBehaviour in Object.FindObjectsOfType<UdonBehaviour>())
			{
				foreach (KeyValuePair<string, List<uint>> keyValuePair in udonBehaviour._eventTable)
				{
					text = string.Concat(new string[]
					{
						text,
						Environment.NewLine,
						"Name:'",
						udonBehaviour.gameObject.name,
						"' Key: '",
						keyValuePair.key,
						"'"
					});
					ClientUtils.udoncounts++;
				}
			}
			File.WriteAllText("DeepClient/UdonLog.txt", text);
			DeepConsole.Log("UdonLogger", string.Format("Logged {0} UdonKey.", ClientUtils.udoncounts));
			VrcExtensions.HudNotif(string.Format("Logged {0} UdonKey.", ClientUtils.udoncounts));
			ClientUtils.udoncounts = 0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000363C File Offset: 0x0000183C
		public static void LogItems()
		{
			string text = null;
			int num = 0;
			foreach (VRC_Pickup vrc_Pickup in Resources.FindObjectsOfTypeAll<VRC_Pickup>())
			{
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					"Name: '",
					vrc_Pickup.name,
					"' "
				});
				num++;
			}
			File.WriteAllText("DeepClient/Pickups.txt", text);
			DeepConsole.Log("ItemLogger", string.Format("Logged {0} Items.", num));
			VrcExtensions.HudNotif(string.Format("Logged {0} Items.", num));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000036F4 File Offset: 0x000018F4
		public static void SaveFrinds()
		{
			string text = "DeepClient\\FriendsIDBackups.txt";
			List<string> list = APIUser.CurrentUser.friendIDs.ToArray().ToList<string>();
			int count = list.Count;
			if (!File.Exists(text))
			{
				File.WriteAllLines(text, list);
				VrcExtensions.HudNotif(string.Format("File created and {0} friendIDs saved.", count));
				return;
			}
			string[] array = File.ReadAllLines(text);
			if (count > array.Length)
			{
				File.WriteAllLines(text, list);
				VrcExtensions.HudNotif(string.Format("{0} friendIDs saved.", count));
				return;
			}
			if (count == array.Length)
			{
				VrcExtensions.HudNotif(string.Format("No changes. {0} friendIDs already saved.", count));
				return;
			}
			VrcExtensions.HudNotif("File has more entries than the current friend list. No changes made.");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003798 File Offset: 0x00001998
		public static void HighlightColor(Color highlightcolor)
		{
			if (Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().Count != 0)
			{
				Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().FirstOrDefault<HighlightsFXStandalone>().highlightColor = highlightcolor;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000037B8 File Offset: 0x000019B8
		public static string GetGreeting()
		{
			int hour = DateTime.Now.Hour;
			if (hour >= 0 && hour < 12)
			{
				return "Good morning";
			}
			if (hour >= 12 && hour < 18)
			{
				return "Good day";
			}
			return "Good evening";
		}

		// Token: 0x0400003B RID: 59
		public static int udoncounts;
	}
}
