using System;
using System.Collections;
using UnityEngine;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x02000026 RID: 38
	internal class BlackCat
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000701E File Offset: 0x0000521E
		public static IEnumerator InitTheme()
		{
			while (GameObject.Find("UI") == null)
			{
				yield return null;
			}
			DeepConsole.LogConsole("BL", "Disabling useless shit...");
			if (GameObject.Find("pin_pedastal"))
			{
				GameObject.Find("pin_pedastal").SetActive(false);
			}
			if (GameObject.Find("UI/Pins merch canvas"))
			{
				GameObject.Find("UI/Pins merch canvas").SetActive(false);
			}
			if (GameObject.Find("UI/Pins merch canvas (1)"))
			{
				GameObject.Find("UI/Pins merch canvas (1)").SetActive(false);
			}
			DeepConsole.LogConsole("BL", "Enabling GoodShit...");
			BlackCat.GameObjectToggle("patreon Toggles", true);
			BlackCat.GameObjectToggle("Sync Button", true);
			BlackCat.GameObjectToggle("Buy", false);
			BlackCat.GameObjectToggle("Owned", true);
			while (GameObject.Find("CREATOR ECONOMY") == null)
			{
				yield return null;
			}
			GameObject.Find("CREATOR ECONOMY/ITEMS/MOVEMENT Canvas").SetActive(true);
			yield break;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007028 File Offset: 0x00005228
		public static void GameObjectToggle(string gameObjectName, bool toogle)
		{
			foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (gameObject.name.Contains(gameObjectName))
				{
					gameObject.active = toogle;
				}
			}
		}
	}
}
