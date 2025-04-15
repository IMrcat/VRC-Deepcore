using System;
using System.Collections;
using ReMod.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x0200008F RID: 143
	internal class CustomMenuBG
	{
		// Token: 0x06000341 RID: 833 RVA: 0x000133FC File Offset: 0x000115FC
		public static IEnumerator Init()
		{
			while (GameObject.Find("MenuContent/Backdrop/Backdrop/Background") == null)
			{
				yield return null;
			}
			GameObject.Find("MenuContent/Backdrop/Backdrop/Background").GetComponent<Image>().overrideSprite = ResourceManager.LoadSpriteFromDisk("DeepClient/MMBG.png", 512, 512);
			GameObject.Find("MenuContent/Backdrop/Backdrop/Background").GetComponent<Image>().color = Color.white;
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return null;
			}
			GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().overrideSprite = ResourceManager.LoadSpriteFromDisk("DeepClient/QMBG.png", 512, 512);
			while (GameObject.Find("Canvas_MainMenu(Clone)") == null)
			{
				yield return null;
			}
			GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().overrideSprite = ResourceManager.LoadSpriteFromDisk("DeepClient/MMBG.png", 512, 512);
			CustomMenuBG.IsLoaded = true;
			yield break;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00013404 File Offset: 0x00011604
		public static void ApplyColors()
		{
			if (CustomMenuBG.IsLoaded)
			{
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().color = Color.white;
				GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().m_Color = Color.white;
				GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().color = Color.white;
				GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().m_Color = Color.white;
				if (GameObject.Find("UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main/Background"))
				{
					GameObject.Find("UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main/Background").GetComponent<PedalGraphic>().color = Color.black;
				}
				if (GameObject.Find("UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main/Center"))
				{
					GameObject.Find("UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main/Center").GetComponent<PedalGraphic>().color = Color.black;
				}
				foreach (PedalGraphic pedalGraphic in Resources.FindObjectsOfTypeAll<PedalGraphic>())
				{
					if (pedalGraphic._texture != null)
					{
						pedalGraphic.color = new Color(0.35f, 0.35f, 0.35f);
					}
				}
				CustomMenuBG.IsLoaded = true;
			}
		}

		// Token: 0x040001DD RID: 477
		public static bool IsLoaded;
	}
}
