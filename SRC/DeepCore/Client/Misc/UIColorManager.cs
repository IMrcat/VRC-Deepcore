using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000079 RID: 121
	internal class UIColorManager
	{
		// Token: 0x0600026D RID: 621 RVA: 0x000106B5 File Offset: 0x0000E8B5
		public static void SetRed()
		{
			PopupHelper.PopupCall("UI Color", "Enter color values with commas (e.g., 0.10, 0.25, 0.75)...", "Set", false, delegate(string userInput)
			{
				string[] array = userInput.Split(new char[] { ',' });
				if (array.Length != 3)
				{
					DeepConsole.Log("UIColor", "Please enter exactly three values.");
					return;
				}
				float num;
				float num2;
				float num3;
				if (float.TryParse(array[0], out num) && float.TryParse(array[1], out num2) && float.TryParse(array[2], out num3))
				{
					UIColorManager.HRed = num;
					UIColorManager.HGreen = num2;
					UIColorManager.HBlue = num3;
					UIColorManager.Recolorthem();
					return;
				}
				DeepConsole.Log("UIColor", "Invalid color values entered. Please enter decimal numbers.");
			});
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000106EC File Offset: 0x0000E8EC
		public static void Recolorthem()
		{
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton/Text").GetComponent<TextMeshProUGUI>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponent<TextMeshProUGUI>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size").GetComponent<TextMeshProUGUI>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
		}

		// Token: 0x0400015D RID: 349
		public static float HRed = 0f;

		// Token: 0x0400015E RID: 350
		public static float HGreen = 0.43885034f;

		// Token: 0x0400015F RID: 351
		public static float HBlue = 0.712937f;
	}
}
