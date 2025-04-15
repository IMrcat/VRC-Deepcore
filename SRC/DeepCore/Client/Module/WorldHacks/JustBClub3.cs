using System;
using System.Collections;
using MelonLoader;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x02000028 RID: 40
	internal class JustBClub3
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000070C0 File Offset: 0x000052C0
		public static void JBC3Menu(ReMenuPage reCategoryPage)
		{
			ReCategoryPage reCategoryPage2 = reCategoryPage.AddCategoryPage("JBC3", "", null, "#ffffff");
			reCategoryPage2.AddCategory("Room Unlocker");
			ReMenuCategory category = reCategoryPage2.GetCategory("Room Unlocker");
			category.AddButton("Unlock Summer", "Unlock Summer", delegate
			{
				MelonCoroutines.Start(JustBClub3.UnlockRoom("Summer"));
			}, null, "#ffffff");
			category.AddButton("Unlock Spring", "Unlock Spring", delegate
			{
				MelonCoroutines.Start(JustBClub3.UnlockRoom("Spring"));
			}, null, "#ffffff");
			category.AddButton("Unlock Winter", "Unlock Winter", delegate
			{
				MelonCoroutines.Start(JustBClub3.UnlockRoom("Winter"));
			}, null, "#ffffff");
			category.AddButton("Unlock Autumn", "Unlock Autumn", delegate
			{
				MelonCoroutines.Start(JustBClub3.UnlockRoom("Autumn"));
			}, null, "#ffffff");
			category.AddButton("Unlock VIPBed", "Unlock VIPBed", delegate
			{
				MelonCoroutines.Start(JustBClub3.UnlockRoom("VIP Bedroom"));
			}, null, "#ffffff");
			reCategoryPage2.AddCategory("Room Sound Annoyer");
			ReMenuCategory category2 = reCategoryPage2.GetCategory("Room Sound Annoyer");
			category2.AddButton("Stop all spam", "Stop all spam.", delegate
			{
				JustBClub3.SpamSound = false;
			}, null, "#ffffff");
			category2.AddButton("Spam Summer", "Spam summer of sound.", delegate
			{
				JustBClub3.SpamSound = true;
				MelonCoroutines.Start(JustBClub3.SpamRoomSound("Summer"));
			}, null, "#ffffff");
			category2.AddButton("Spam Spring", "Spam spring of sounds.", delegate
			{
				JustBClub3.SpamSound = true;
				MelonCoroutines.Start(JustBClub3.SpamRoomSound("Spring"));
			}, null, "#ffffff");
			category2.AddButton("Spam Winter", "Spam winter of sounds.", delegate
			{
				JustBClub3.SpamSound = true;
				MelonCoroutines.Start(JustBClub3.SpamRoomSound("Winter"));
			}, null, "#ffffff");
			category2.AddButton("Spam Autumn", "Spam autumn of sounds.", delegate
			{
				JustBClub3.SpamSound = true;
				MelonCoroutines.Start(JustBClub3.SpamRoomSound("Autumn"));
			}, null, "#ffffff");
			category2.AddButton("Spam VIPBed", "Spam vipbed of sounds.", delegate
			{
				JustBClub3.SpamSound = true;
				MelonCoroutines.Start(JustBClub3.SpamRoomSound("VIP Bedroom"));
			}, null, "#ffffff");
			reCategoryPage2.AddCategory("Room Telepoter");
			ReMenuCategory category3 = reCategoryPage2.GetCategory("Room Telepoter");
			category3.AddButton("TP Room 1", "", new Action(JustBClub3.TPTROOM1), null, "#ffffff");
			category3.AddButton("TP viproom", "", new Action(JustBClub3.TPVIPROOM), null, "#ffffff");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000073BC File Offset: 0x000055BC
		public static void TPVIPROOM()
		{
			DeepConsole.Log("JBC3", "Trying to teleport LocalPlayer.");
			foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (gameObject.name.Contains("VIP Lounge"))
				{
					gameObject.SetActive(true);
				}
			}
			Networking.LocalPlayer.gameObject.transform.position = new Vector3(-39.1f, 20f, 300f);
			DeepConsole.Log("JBC3", "LocalPlayer has been teleported -> VipRoom.");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007460 File Offset: 0x00005660
		public static void TPTROOM1()
		{
			DeepConsole.Log("JBC3", "Trying to teleport LocalPlayer.");
			foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (gameObject.name.Contains("Bedroom"))
				{
					gameObject.SetActive(true);
				}
			}
			Networking.LocalPlayer.gameObject.transform.position = new Vector3(-29.1f, 19.8f, -200f);
			DeepConsole.Log("JBC3", "LocalPlayer has been teleported -> Room 1.");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007504 File Offset: 0x00005704
		public static IEnumerator UnlockRoom(string room)
		{
			float startTime = Time.time;
			while (Time.time < startTime + 0.2f)
			{
				yield return null;
			}
			UdonBehaviour component = GameObject.Find(room).GetComponent<UdonBehaviour>();
			if (component.gameObject != null)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, component.gameObject);
				component.SendCustomEvent("_ResetRoom");
			}
			yield break;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007513 File Offset: 0x00005713
		public static IEnumerator SpamRoomSound(string room)
		{
			float startTime = Time.time;
			while (Time.time < startTime + 0.2f)
			{
				yield return null;
			}
			UdonBehaviour ObjectToSendTo = GameObject.Find(room).GetComponent<UdonBehaviour>();
			if (ObjectToSendTo.gameObject != null)
			{
				while (JustBClub3.SpamSound)
				{
					if (Networking.GetOwner(ObjectToSendTo.gameObject).displayName != Networking.LocalPlayer.displayName)
					{
						Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, ObjectToSendTo.gameObject);
					}
					ObjectToSendTo.SendCustomNetworkEvent(0, "StartIntercom");
					while (Time.time < startTime + 0.5f)
					{
						yield return null;
					}
					startTime = Time.time;
				}
			}
			yield break;
		}

		// Token: 0x04000083 RID: 131
		public static bool SpamSound;
	}
}
