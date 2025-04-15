using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using VRC;

namespace DeepCore.Client.API
{
	// Token: 0x020000AA RID: 170
	internal class PlayerTagSystem
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00017198 File Offset: 0x00015398
		public static void CheckPlayer(Player player)
		{
			ValueTuple<string, string> valueTuple;
			if (!PlayerTagSystem.allowedPlayers.TryGetValue(player.field_Private_APIUser_0.id, out valueTuple))
			{
				return;
			}
			Color color;
			ColorUtility.TryParseHtmlString(valueTuple.Item2, ref color);
			PlayerTagSystem.AddTag(valueTuple.Item1, color, player);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000171DC File Offset: 0x000153DC
		public static void AddTag(string CustomTag, Color color, Player player)
		{
			PlayerNameplate field_Public_PlayerNameplate_ = player.Method_Internal_get_VRCPlayer_1().field_Public_PlayerNameplate_0;
			Transform transform = Object.Instantiate<Transform>(field_Public_PlayerNameplate_.gameObject.transform.Find("Contents/Quick Stats"), field_Public_PlayerNameplate_.gameObject.transform.Find("Contents"));
			transform.parent = field_Public_PlayerNameplate_.gameObject.transform.Find("Contents");
			transform.gameObject.SetActive(true);
			TextMeshProUGUI component = transform.Find("Trust Text").GetComponent<TextMeshProUGUI>();
			component.color = color;
			transform.Find("Trust Icon").gameObject.SetActive(false);
			transform.Find("Performance Icon").gameObject.SetActive(false);
			transform.Find("Performance Text").gameObject.SetActive(false);
			transform.Find("Friend Anchor Stats").gameObject.SetActive(false);
			transform.name = "DC Info Tag";
			transform.gameObject.transform.localPosition = new Vector3(0f, 145f, 0f);
			transform.GetComponent<ImageThreeSlice>().color = color;
			component.text = CustomTag;
		}

		// Token: 0x0400022A RID: 554
		[TupleElementNames(new string[] { "Tag", "Color" })]
		private static Dictionary<string, ValueTuple<string, string>> allowedPlayers = new Dictionary<string, ValueTuple<string, string>>
		{
			{
				"usr_a7d59ec0-4e6a-4f94-ad37-972602b72958",
				new ValueTuple<string, string>("Silly Beans :3", "#570f96")
			},
			{
				"usr_fc152d76-e45a-448e-b90a-860da7ea8b3e",
				new ValueTuple<string, string>("Débiteur halal.", "#00ff44")
			},
			{
				"usr_7dd8c2f2-6884-44c6-b9b2-490770b64a49",
				new ValueTuple<string, string>("<size=120%>HORNY FURRY<size=90%>LTG Member", "#ff0000")
			},
			{
				"usr_53df9514-8d47-4c5c-ae32-04735c392e8b",
				new ValueTuple<string, string>("SnØwfall", "#ff0000")
			},
			{
				"usr_29187716-6c4d-4215-9342-925b99fe4374",
				new ValueTuple<string, string>("<size=220%>Pédo", "#00ff51")
			},
			{
				"usr_8775051c-9e24-4710-8d60-db0aaef67534",
				new ValueTuple<string, string>("- BadUpdate Enjoyer -", "#00ff51")
			},
			{
				"usr_78d05283-0c89-4f88-a89e-757233aebb81",
				new ValueTuple<string, string>("<size=140%>Fuckable", "#e292fc")
			}
		};
	}
}
