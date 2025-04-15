using System;
using System.Collections;
using DeepCore.Client.UI.MM;
using DeepCore.Client.UI.QM;
using UnityEngine;

namespace DeepCore.Client.UI
{
	// Token: 0x0200000C RID: 12
	internal class UIController
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003C2A File Offset: 0x00001E2A
		public static IEnumerator WaitForQM()
		{
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return null;
			}
			QMUI.InitQM();
			while (GameObject.Find("Canvas_MainMenu(Clone)") == null)
			{
				yield return null;
			}
			MMUI.WaitForMM();
			yield break;
		}
	}
}
