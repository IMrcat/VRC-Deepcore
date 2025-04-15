using System;
using DeepCore.Client.API;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Funnies
{
	// Token: 0x02000057 RID: 87
	internal class FagFinder
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000C864 File Offset: 0x0000AA64
		public static void FindTheFag(Player player)
		{
			string text = "";
			if (player.field_Private_APIUser_0.bio.Contains(string.Format("{0}", FagFinder.FagThings)))
			{
				text += "Faggot - ";
			}
			foreach (string text2 in FagFinder.Pronoms)
			{
				if (player.field_Private_APIUser_0.bio.Contains(text2))
				{
					text += text2;
				}
			}
			PlayerTagSystem.AddTag(text, Color.red, player);
		}

		// Token: 0x040000ED RID: 237
		public static string[] FagThings = new string[] { "i'm trans", "im trans", "i am trans", "i an trans", "gender\u02f8 trans", "i'm male", "im male", "i am male" };

		// Token: 0x040000EE RID: 238
		public static string[] Pronoms = new string[] { "he⁄him", "he⁄them", "he⁄they", "them⁄him" };
	}
}
