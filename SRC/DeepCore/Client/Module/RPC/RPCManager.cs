using System;
using DeepCore.Client.ClientMenu.Pages_MainMenu;
using DeepCore.Client.Misc;
using Il2CppSystem;
using UnhollowerBaseLib;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.RPC
{
	// Token: 0x02000038 RID: 56
	internal class RPCManager
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00009DEC File Offset: 0x00007FEC
		public static void HandleRPC(Player param_1, VRC_EventHandler.VrcEvent param_2, VRC_EventHandler.VrcBroadcastType param_3)
		{
			if (param_2.ParameterString.StartsWith("DCChat:"))
			{
				VRCPlayerApi field_Private_VRCPlayerApi_ = param_1.field_Private_VRCPlayerApi_0;
				string text = param_2.ParameterString.Substring(7);
				ClientChat.ChatMSG(field_Private_VRCPlayerApi_.displayName, text);
			}
			if (param_2.ParameterString == "DCWarn:")
			{
				string text2 = param_2.ParameterString.Substring(7);
				PopupHelper.OpenVideoInMM("DeepClient - VideoPlayer", text2, true);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009E54 File Offset: 0x00008054
		public static void SendRPC(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return;
			}
			string text = input.Trim();
			if (string.IsNullOrEmpty(text))
			{
				VrcExtensions.AlertPopup("RPC | ALERT", "You can't send nothing!", 10);
				return;
			}
			Networking.RPC(0, OnLoadedScaneManager.funnyrpcobj, text, new Il2CppReferenceArray<Object>(0L));
		}
	}
}
