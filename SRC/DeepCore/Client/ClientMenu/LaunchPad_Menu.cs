using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.QOL;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu
{
	// Token: 0x02000098 RID: 152
	internal class LaunchPad_Menu
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00013868 File Offset: 0x00011A68
		public static void InitLaunchPadMenu(UiManager UIManager)
		{
			LaunchPad_Menu._uiManager = UIManager;
			DeepConsole.Log("ClientUI", "Initializing LaunchPad Menu...");
			IButtonPage launchPad = LaunchPad_Menu._uiManager.LaunchPad;
			launchPad.AddButton("Join By ID", "Allows you to join a world by it's ID.", delegate
			{
				PopupHelper.PopupCall("Join By ID", "Enter the ID to join", "Join", false, delegate(string userInput)
				{
					DeepConsole.Log("IDJoiner", "Joining ID: " + userInput);
					Networking.GoToRoom(userInput);
				});
			}, null, "#ffffff");
			launchPad.AddButton("Change By ID", "Allows you to change avatar by it's ID.", delegate
			{
				PopupHelper.PopupCall("Change By ID", "Enter ID", "Set", false, delegate(string userInput)
				{
					DeepConsole.Log("AviLogger", "Changing to: " + userInput);
					VrcExtensions.ChangeAvatar(userInput);
				});
			}, null, "#ffffff");
			launchPad.AddButton("Force ClearRam", "MomoHackLoaded.", delegate
			{
				RamCleaner.forceclear();
			}, null, "#ffffff");
		}

		// Token: 0x040001F8 RID: 504
		private static UiManager _uiManager;
	}
}
