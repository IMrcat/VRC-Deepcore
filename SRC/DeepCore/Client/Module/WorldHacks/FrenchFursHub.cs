using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Module.WorldHacks
{
	// Token: 0x02000027 RID: 39
	internal class FrenchFursHub
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000708C File Offset: 0x0000528C
		public static void SendMsg(string msg)
		{
			GameObject.Find("Menu/Container/Panels/Chat/Input").GetComponent<InputField>().text = msg;
			GameObject.Find("Menu/Container/Panels/Chat/Keyboard/Row_2/Key_Enter").GetComponent<Button>().Press();
		}
	}
}
