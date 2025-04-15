using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x0200009E RID: 158
	internal class ItemManipulator
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0001492F File Offset: 0x00012B2F
		private static bool checkIfBlacklisted(VRC_Pickup item)
		{
			return ItemManipulator.blacklistedPickups.Contains(item.name);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00014944 File Offset: 0x00012B44
		private static void setActiveAll()
		{
			foreach (VRC_Pickup vrc_Pickup in ItemManipulator._pickups)
			{
				if (!ItemManipulator.checkIfBlacklisted(vrc_Pickup))
				{
					ItemManipulator.setActive(vrc_Pickup);
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00014977 File Offset: 0x00012B77
		private static void setInactive(VRC_Pickup item)
		{
			item.gameObject.SetActive(false);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00014988 File Offset: 0x00012B88
		private static void setInactiveAll()
		{
			foreach (VRC_Pickup vrc_Pickup in ItemManipulator._pickups)
			{
				if (!ItemManipulator.checkIfBlacklisted(vrc_Pickup))
				{
					ItemManipulator.setInactive(vrc_Pickup);
				}
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000149BC File Offset: 0x00012BBC
		internal static void ManipulatorHacksMenu(UiManager uiManager)
		{
			ItemManipulator._uiManager = uiManager;
			ItemManipulator._itemGrabberMenu = ItemManipulator._uiManager.QMMenu.AddCategoryPage("Manipulator", "Manipulator", null, "#ffffff");
			ItemManipulator._itemGrabberMenu.AddCategory("Manipulator Options");
			ItemManipulator._itemGrabber = ItemManipulator._itemGrabberMenu.GetCategory("Manipulator Options");
			ItemManipulator._itemGrabber.AddButton("Select Force Object ON (Local)", "", delegate
			{
				ItemManipulator.Select("Active");
			}, null, "#ffffff");
			ItemManipulator._itemGrabber.AddButton("Select Force Object OFF (Local)", "", delegate
			{
				ItemManipulator.Select("Inactive");
			}, null, "#ffffff");
			ItemManipulator._itemGrabber.AddButton("Select Bring Pickup", "", delegate
			{
				ItemManipulator.Select("BringItem");
			}, null, "#ffffff");
			ItemManipulator._itemGrabber.AddButton("Select Udon", "", delegate
			{
				ItemManipulator.Select("UdonSelect");
			}, null, "#ffffff");
			ItemManipulator._itemGrabber.AddButton("Freeze Item (max: " + 4.ToString() + ")", "Freeze Item", delegate
			{
				ItemManipulator.Select("Freeze");
			}, null, "#ffffff");
			ItemManipulator._itemGrabberMenu.AddCategory("Item Selector List");
			ItemManipulator._itemGrabberSelector = ItemManipulator._itemGrabberMenu.GetCategory("Item Selector List");
			ItemManipulator._itemGrabberOptionsMenu = ItemManipulator._itemGrabberSelector.AddCategoryPage("Selector", "Selector", null, "#ffffff");
			ItemManipulator._itemGrabberOptionsMenu2 = ItemManipulator._itemGrabberSelector.AddCategoryPage("Udon Event Selector", "Udon Event Selector", null, "#ffffff");
			ItemManipulator._itemGrabberOptionsMenu2.OnOpen += ItemManipulator.SelectUdon;
			ItemManipulator._itemGrabberOptionsMenu3 = ItemManipulator._itemGrabberSelector.AddCategoryPage("Unfreeze Selector", "Unfreeze Selector", null, "#ffffff");
			ItemManipulator._itemGrabberOptionsMenu3.OnOpen += ItemManipulator.SelectUnfeeze;
			ItemManipulator._itemGrabberMenu.AddCategory("Manipulator Settings");
			ItemManipulator._itemSettingsOptions = ItemManipulator._itemGrabberMenu.GetCategory("Manipulator Settings");
			ItemManipulator._itemSettingsOptions.AddButton("Force Refresh", "Force Refresh", new Action(ItemManipulator.refreshPickups), null, "#ffffff");
			ItemManipulator._itemSettingsOptions.AddToggle("Set Global Udon Event", "Set Global Udon Event", new Action<bool>(ItemManipulator.UdonNetworkTargetToggle), true);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00014C66 File Offset: 0x00012E66
		internal static void UdonNetworkTargetToggle(bool value)
		{
			ItemManipulator.udonNetworkTarget = !value;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00014C71 File Offset: 0x00012E71
		internal static void refreshPickups()
		{
			ItemManipulator._pickups = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
			ItemManipulator.udonBehaviourEntries.Clear();
			ItemManipulator.PrepareUdonBehaviours();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00014C94 File Offset: 0x00012E94
		private static void SelectUnfeeze()
		{
			if (ItemManipulator.calledTwice2)
			{
				ItemManipulator.calledTwice2 = false;
				return;
			}
			ItemManipulator.calledTwice2 = true;
			if (ItemManipulator.tempPage3 != 0)
			{
				ItemManipulator._itemGrabberOptions3.Active = false;
			}
			int num = ++ItemManipulator.tempPage3;
			ItemManipulator._itemGrabberOptionsMenu3.AddCategory("Unfreeze Selector (" + num.ToString() + ")");
			ItemManipulator._itemGrabberOptions3 = ItemManipulator._itemGrabberOptionsMenu3.GetCategory("Unfreeze Selector (" + num.ToString() + ")");
			if (ItemManipulator._frozenItems == null)
			{
				return;
			}
			foreach (KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> keyValuePair in ItemManipulator._frozenItems.OrderBy(delegate(KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> i)
			{
				KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> keyValuePair2 = i;
				return keyValuePair2.Key.gameObject.name;
			}))
			{
				ItemManipulator._itemGrabberOptions3.AddButton(keyValuePair.Key.gameObject.name, "", delegate
				{
				}, null, "#ffffff");
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014DC8 File Offset: 0x00012FC8
		public static void PrepareUdonBehaviours()
		{
			foreach (UdonBehaviour udonBehaviour in Resources.FindObjectsOfTypeAll<UdonBehaviour>())
			{
				try
				{
					string name = udonBehaviour.gameObject.name;
				}
				catch (Exception ex)
				{
					DeepConsole.Log("MR", "Error processing UdonBehaviour: " + ex.Message + "\n" + ex.StackTrace);
					continue;
				}
				List<string> list = new List<string>();
				int num = 0;
				foreach (KeyValuePair<string, List<uint>> keyValuePair in udonBehaviour._eventTable)
				{
					if (!keyValuePair.Key.StartsWith("_"))
					{
						list.Add(keyValuePair.Key);
						num++;
					}
				}
				if (num > 0)
				{
					ItemManipulator.udonBehaviourEntries.Add(udonBehaviour, list);
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00014EB8 File Offset: 0x000130B8
		internal static void disableSelection()
		{
			if (ItemManipulator._itemGrabberOptions != null)
			{
				ItemManipulator._itemGrabberOptions.Active = false;
			}
			if (ItemManipulator._itemGrabberOptions2 != null)
			{
				ItemManipulator._itemGrabberOptions2.Active = false;
			}
			ItemManipulator.udonSelected = null;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00014EE4 File Offset: 0x000130E4
		private static void SelectUdon()
		{
			if (ItemManipulator.calledTwice)
			{
				ItemManipulator.calledTwice = false;
				return;
			}
			ItemManipulator.calledTwice = true;
			if (ItemManipulator.tempPage2 != 0)
			{
				ItemManipulator._itemGrabberOptions2.Active = false;
			}
			int num = ++ItemManipulator.tempPage2;
			ItemManipulator._itemGrabberOptionsMenu2.AddCategory("Udon Event Selector (" + num.ToString() + ")");
			ItemManipulator._itemGrabberOptions2 = ItemManipulator._itemGrabberOptionsMenu2.GetCategory("Udon Event Selector (" + num.ToString() + ")");
			if (ItemManipulator.udonSelected == null)
			{
				return;
			}
			using (IEnumerator<string> enumerator = ItemManipulator.udonBehaviourEntries[ItemManipulator.udonSelected].OrderBy((string i) => i).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string eventName = enumerator.Current;
					ItemManipulator._itemGrabberOptions2.AddButton(eventName, "", delegate
					{
						ItemManipulator.sendUdonEvent(ItemManipulator.udonSelected, eventName);
					}, null, "#ffffff");
				}
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00015014 File Offset: 0x00013214
		private static void Select(string type)
		{
			if (ItemManipulator.tempPage != 0)
			{
				ItemManipulator._itemGrabberOptions.Active = false;
			}
			int num = ++ItemManipulator.tempPage;
			ItemManipulator._itemGrabberOptionsMenu.AddCategory("Selector (" + num.ToString() + ")");
			ItemManipulator._itemGrabberOptions = ItemManipulator._itemGrabberOptionsMenu.GetCategory("Selector (" + num.ToString() + ")");
			if (!(type == "Active"))
			{
				if (!(type == "Inactive"))
				{
					if (type == "UdonSelect")
					{
						goto IL_0223;
					}
					if (!(type == "Freeze"))
					{
						goto IL_0360;
					}
					goto IL_02C2;
				}
			}
			else
			{
				ItemManipulator._itemGrabberOptions.AddButton("Force Object ON All", "", new Action(ItemManipulator.setActiveAll), null, "#ffffff");
				using (IEnumerator<VRC_Pickup> enumerator = ItemManipulator._pickups.OrderBy((VRC_Pickup i) => i.gameObject.name).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						VRC_Pickup pickup2 = enumerator.Current;
						if (!ItemManipulator.checkIfBlacklisted(pickup2))
						{
							ItemManipulator._itemGrabberOptions.AddButton(pickup2.name, "", delegate
							{
								ItemManipulator.setActive(pickup2);
							}, null, "#ffffff");
						}
					}
					return;
				}
			}
			ItemManipulator._itemGrabberOptions.AddButton("Force Object OFF All", "", new Action(ItemManipulator.setInactiveAll), null, "#ffffff");
			using (IEnumerator<VRC_Pickup> enumerator2 = ItemManipulator._pickups.OrderBy((VRC_Pickup i) => i.gameObject.name).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					VRC_Pickup pickup3 = enumerator2.Current;
					if (!ItemManipulator.checkIfBlacklisted(pickup3))
					{
						ItemManipulator._itemGrabberOptions.AddButton(pickup3.name, "", delegate
						{
							ItemManipulator.setInactive(pickup3);
						}, null, "#ffffff");
					}
				}
				return;
			}
			IL_0223:
			using (IEnumerator<KeyValuePair<UdonBehaviour, List<string>>> enumerator3 = ItemManipulator.udonBehaviourEntries.OrderBy(delegate(KeyValuePair<UdonBehaviour, List<string>> i)
			{
				KeyValuePair<UdonBehaviour, List<string>> keyValuePair = i;
				return keyValuePair.Key.gameObject.name;
			}).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<UdonBehaviour, List<string>> udonBehaviourEntry = enumerator3.Current;
					try
					{
						ItemManipulator._itemGrabberOptions.AddButton(udonBehaviourEntry.Key.gameObject.name, "", delegate
						{
							ItemManipulator.udonSelected = udonBehaviourEntry.Key;
						}, null, "#ffffff");
					}
					catch (Exception)
					{
					}
				}
				return;
			}
			IL_02C2:
			using (IEnumerator<VRC_Pickup> enumerator4 = ItemManipulator._pickups.OrderBy((VRC_Pickup i) => i.gameObject.name).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					VRC_Pickup pickup4 = enumerator4.Current;
					if (!ItemManipulator.checkIfBlacklisted(pickup4))
					{
						ItemManipulator._itemGrabberOptions.AddButton(pickup4.name, "", delegate
						{
							ItemManipulator.Freeze(pickup4);
						}, null, "#ffffff");
					}
				}
				return;
			}
			IL_0360:
			ItemManipulator._itemGrabberOptions.AddButton("Bring All Pickup", "", delegate
			{
				ItemManipulator.BringPickups(null);
			}, null, "#ffffff");
			using (IEnumerator<VRC_Pickup> enumerator5 = ItemManipulator._pickups.OrderBy((VRC_Pickup i) => i.gameObject.name).GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					VRC_Pickup pickup = enumerator5.Current;
					if (!ItemManipulator.checkIfBlacklisted(pickup))
					{
						ItemManipulator._itemGrabberOptions.AddButton(pickup.name, "", delegate
						{
							ItemManipulator.BringPickups(pickup);
						}, null, "#ffffff");
					}
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000154A4 File Offset: 0x000136A4
		internal static void freezeItemUpdate()
		{
			if (ItemManipulator._frozenItems.Count == 0)
			{
				return;
			}
			try
			{
				foreach (KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> keyValuePair in ItemManipulator._frozenItems.Where(delegate(KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> item)
				{
					KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> keyValuePair2 = item;
					return Networking.GetOwner(keyValuePair2.Key.gameObject).playerId != VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0.playerId;
				}))
				{
					DeepConsole.Log("Yes", string.Concat(new string[]
					{
						"User: ",
						Networking.GetOwner(keyValuePair.Key.gameObject).displayName,
						" tried to steal (",
						keyValuePair.Key.gameObject.name,
						")"
					}));
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0, keyValuePair.Key.gameObject);
					Transform transform = keyValuePair.Key.transform;
					transform.position = keyValuePair.Value.Item1;
					transform.rotation = keyValuePair.Value.Item2;
				}
			}
			catch (Exception)
			{
				ItemManipulator._frozenItems.Clear();
				DeepConsole.Log("Yes", "Frozen Item list cleared!");
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000155F4 File Offset: 0x000137F4
		private static void setActive(VRC_Pickup item)
		{
			item.DisallowTheft = false;
			item.allowManipulationWhenEquipped = true;
			item.pickupable = true;
			item.gameObject.SetActive(true);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00015618 File Offset: 0x00013818
		private static void sendUdonEvent(UdonBehaviour item, string eventName)
		{
			try
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, item.gameObject);
				item.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(ItemManipulator.udonNetworkTarget, eventName);
			}
			catch (Exception ex)
			{
				string text = "Failed sending custom Udon event: (";
				string text2 = "). Reason: ";
				Exception ex2 = ex;
				DeepConsole.Log("Yes", text + eventName + text2 + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001568C File Offset: 0x0001388C
		private static void BringPickups(VRC_Pickup item)
		{
			if (item == null)
			{
				VRC_Pickup[] pickups = ItemManipulator._pickups;
				for (int i = 0; i < pickups.Length; i++)
				{
					ItemManipulator.PickupItem(pickups[i]);
				}
				return;
			}
			ItemManipulator.PickupItem(item);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000156C8 File Offset: 0x000138C8
		private static void Freeze(VRC_Pickup item)
		{
			if (ItemManipulator._frozenItems.Any((KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> i) => i.Key.gameObject.name == item.gameObject.name))
			{
				return;
			}
			while (ItemManipulator._frozenItems.Count >= 4)
			{
				ItemManipulator._frozenItems.Remove(ItemManipulator._frozenItems.First<KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>>>().Key);
			}
			ItemManipulator._frozenItems.Add(item, new Tuple<Vector3, Quaternion>(item.transform.position, item.transform.rotation));
			DeepConsole.Log("Ues", "Freezing Item: " + item.gameObject.name);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00015780 File Offset: 0x00013980
		private static void PickupItem(VRC_Pickup item)
		{
			try
			{
				ItemManipulator.setActive(item);
				if (Networking.GetOwner(item.gameObject).playerId != VRCPlayer.field_Internal_Static_VRCPlayer_0._playerNet._vrcPlayer.field_Private_VRCPlayerApi_0.playerId)
				{
					item.GetComponent<VRC_Pickup>().currentlyHeldBy = null;
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0, item.gameObject);
				}
			}
			catch (Exception ex)
			{
				DeepConsole.Log("YEs", string.Format("Failed to grab item {0}! {1}", item.name, ex));
			}
		}

		// Token: 0x04000209 RID: 521
		internal static readonly List<string> blacklistedPickups = new List<string> { "OscDebugConsole", "MirrorPickup", "PhotoCamera", "ViewFinder", "AvatarDebugConsole" };

		// Token: 0x0400020A RID: 522
		private static UiManager _uiManager;

		// Token: 0x0400020B RID: 523
		private static ReCategoryPage _itemGrabberMenu;

		// Token: 0x0400020C RID: 524
		private static ReCategoryPage _itemGrabberOptionsMenu;

		// Token: 0x0400020D RID: 525
		private static ReCategoryPage _itemGrabberOptionsMenu2;

		// Token: 0x0400020E RID: 526
		private static ReCategoryPage _itemGrabberOptionsMenu3;

		// Token: 0x0400020F RID: 527
		private static ReMenuCategory _itemGrabberSelector;

		// Token: 0x04000210 RID: 528
		private static ReMenuCategory _itemGrabber;

		// Token: 0x04000211 RID: 529
		private static ReMenuCategory _itemGrabberOptions;

		// Token: 0x04000212 RID: 530
		private static ReMenuCategory _itemGrabberOptions2;

		// Token: 0x04000213 RID: 531
		private static ReMenuCategory _itemGrabberOptions3;

		// Token: 0x04000214 RID: 532
		private static ReMenuCategory _itemSettingsOptions;

		// Token: 0x04000215 RID: 533
		private static Dictionary<UdonBehaviour, List<string>> udonBehaviourEntries = new Dictionary<UdonBehaviour, List<string>>();

		// Token: 0x04000216 RID: 534
		private static Dictionary<VRC_Pickup, Tuple<Vector3, Quaternion>> _frozenItems = new Dictionary<VRC_Pickup, Tuple<Vector3, Quaternion>>();

		// Token: 0x04000217 RID: 535
		private static UdonBehaviour[] _udonObjects;

		// Token: 0x04000218 RID: 536
		private static UdonBehaviour udonSelected;

		// Token: 0x04000219 RID: 537
		private static VRC_Pickup[] _pickups;

		// Token: 0x0400021A RID: 538
		internal static NetworkEventTarget udonNetworkTarget = 0;

		// Token: 0x0400021B RID: 539
		private static int tempPage;

		// Token: 0x0400021C RID: 540
		private static int tempPage2;

		// Token: 0x0400021D RID: 541
		private static int tempPage3;

		// Token: 0x0400021E RID: 542
		private const int maxFreezeCount = 4;

		// Token: 0x0400021F RID: 543
		private static bool calledTwice;

		// Token: 0x04000220 RID: 544
		private static bool calledTwice2;
	}
}
