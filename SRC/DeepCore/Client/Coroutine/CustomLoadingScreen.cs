using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MelonLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000096 RID: 150
	internal class CustomLoadingScreen
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00013744 File Offset: 0x00011944
		public static IEnumerator Init()
		{
			while (GameObject.Find("LoadingBackground_TealGradient_Music") == null)
			{
				yield return null;
			}
			GameObject.Find("PlayerDisplay/BlackFade/inverted_sphere").SetActive(false);
			GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex", CustomLoadingScreen._skyCubeTexture);
			GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", CustomLoadingScreen._primaryColorS);
			CustomLoadingScreen.SetupParticleSystem(GameObject.Find("LoadingBackground_TealGradient_Music/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
			while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
			{
				yield return null;
			}
			while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
			{
				yield return null;
			}
			if (File.Exists("DeepClient\\LoadingVid.mp4"))
			{
				DeepConsole.Log("LoadingVid", "Video found !");
				MelonCoroutines.Start(LoadingVideo.LoadVideo());
			}
			else
			{
				DeepConsole.Log("LoadingVid", "Video was not found, Loading CustomAudio !");
				MelonCoroutines.Start(CustomLoadingAudios.Init());
				MelonCoroutines.Start(LoadingParticules.loadparticles());
			}
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex", CustomLoadingScreen._skyCubeTexture);
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", CustomLoadingScreen._primaryColor);
			CustomLoadingScreen.SetupParticleSystem(GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
			CustomLoadingScreen.SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Button>());
			CustomLoadingScreen.SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Button>());
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = Color.green;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = CustomLoadingScreen._primaryColor;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = CustomLoadingScreen._primaryColor;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = CustomLoadingScreen._primaryColor;
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = CustomLoadingScreen._primaryColor;
			using (IEnumerator<Button> enumerator = GameObject.Find("UserInterface").transform.Find("MenuContent").GetComponentsInChildren<Button>(true).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Button button = enumerator.Current;
					ColorBlock colors = button.colors;
					colors.normalColor = CustomLoadingScreen._primaryColor;
					colors.highlightedColor = CustomLoadingScreen._highlightColor;
					colors.pressedColor = CustomLoadingScreen._pressedColor;
					colors.selectedColor = CustomLoadingScreen._primaryColor;
					button.colors = colors;
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001374C File Offset: 0x0001194C
		private static void SetupParticleSystem(ParticleSystem particleSystem)
		{
			if (particleSystem == null)
			{
				return;
			}
			particleSystem.startColor = Color.white;
			particleSystem.startSize = 0.3f;
			particleSystem.gravityModifier = 1f;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001377C File Offset: 0x0001197C
		private static void SetupButton(Button button)
		{
			if (button == null)
			{
				return;
			}
			ColorBlock colors = button.colors;
			colors.pressedColor = CustomLoadingScreen._pressedColor;
			colors.highlightedColor = CustomLoadingScreen._highlightColor;
			colors.normalColor = CustomLoadingScreen._primaryColor;
			button.colors = colors;
		}

		// Token: 0x040001F3 RID: 499
		private static Texture2D _skyCubeTexture;

		// Token: 0x040001F4 RID: 500
		private static Color _primaryColor = new Color(0.25f, 0f, 0f, 1f);

		// Token: 0x040001F5 RID: 501
		private static Color _primaryColorS = new Color(0.25f, 0f, 0.25f, 1f);

		// Token: 0x040001F6 RID: 502
		private static Color _highlightColor = new Color(0.547f, 0f, 0f, 1f);

		// Token: 0x040001F7 RID: 503
		private static Color _pressedColor = new Color(0.247f, 0f, 0f, 1f);
	}
}
