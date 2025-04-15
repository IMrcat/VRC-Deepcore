using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.Photon;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A5 RID: 165
	internal class ClientSettings
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00016230 File Offset: 0x00014430
		public static void ClientSettingsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Client Settings", null, null, "#ffffff");
			reCategoryPage.AddCategory("Client Settings");
			ReMenuCategory category = reCategoryPage.GetCategory("Client Settings");
			category.AddToggle("Join/Leave", "Log Join/Leave.", delegate(bool s)
			{
				ConfManager.playerLogger.Value = s;
				MelonPreferences.Save();
			}, ConfManager.playerLogger.Value);
			category.AddToggle("VRC Staff Join/Leave", "Log VRCAdmin/Staff Join/Leave.", delegate(bool s)
			{
				ConfManager.VRCAdminStaffLogger.Value = s;
				MelonPreferences.Save();
			}, ConfManager.VRCAdminStaffLogger.Value);
			category.AddToggle("Avatar Logger", "Enabled/Disabled AvaLogger.", delegate(bool s)
			{
				ConfManager.avatarLogging.Value = s;
				MelonPreferences.Save();
			}, ConfManager.avatarLogging.Value);
			category.AddToggle("UdonSync Logger", "Enabled/Disabled UdonLogger.", delegate(bool s)
			{
				ConfManager.udonLogger.Value = s;
				MelonPreferences.Save();
			}, ConfManager.udonLogger.Value);
			category.AddToggle("AntiQuest", "Auto block quest users.", delegate(bool s)
			{
				ConfManager.AntiQuest.Value = s;
				MelonPreferences.Save();
			}, ConfManager.AntiQuest.Value);
			category.AddToggle("Anti\nAvatarScalling\ndisabler", "Block world that's disable avatar scalling.", delegate(bool s)
			{
				ConfManager.AntiAvatarScallingdisabler.Value = s;
				MelonPreferences.Save();
			}, ConfManager.AntiAvatarScallingdisabler.Value);
			category.AddToggle("QMFreeze", "Allow you to lock yourself when opening menu when falling down.", delegate(bool s)
			{
				ConfManager.ShouldQMFreeze.Value = s;
				MelonPreferences.Save();
			}, ConfManager.ShouldQMFreeze.Value);
			category.AddToggle("Menu Music", "Music or not?", delegate(bool s)
			{
				ConfManager.ShouldMenuMusic.Value = s;
				MelonPreferences.Save();
			}, ConfManager.ShouldMenuMusic.Value);
			category.AddButton("Set Custom Payload", "Allows you to custom e1's payloads.", delegate
			{
				PopupHelper.PopupCall("E1 Custom Payload", "Enter the B64 to set", "Confirm", false, delegate(string userInput)
				{
					ConfManager.E1Payload.Value = userInput;
					MelonPreferences.Save();
				});
			}, null, "#ffffff");
			category.AddToggle("CustomPlate", "Allow CustomNamePlate.", delegate(bool s)
			{
				ConfManager.customnameplate.Value = s;
				MelonPreferences.Save();
			}, ConfManager.customnameplate.Value);
			category.AddToggle("Save LastInstance", "Allow you to save lastinstance you where in.", delegate(bool s)
			{
				ConfManager.JoinLastInstance.Value = s;
				MelonPreferences.Save();
			}, ConfManager.JoinLastInstance.Value);
			category.AddToggle("Better Loading Screen", "Allow you to toggle BLS (Restart Needed).", delegate(bool s)
			{
				ConfManager.ThirdPersonKeyBind.Value = s;
				MelonPreferences.Save();
			}, ConfManager.ThirdPersonKeyBind.Value);
			reCategoryPage.AddCategory("Photon Settings");
			ReMenuCategory category2 = reCategoryPage.GetCategory("Photon Settings");
			category2.AddToggle("Event 43", "Log chatbox msg.", delegate(bool s)
			{
				ChatBoxLogger.isEnabled = s;
			}, ChatBoxLogger.isEnabled);
			category2.AddToggle("Log Photon OnEvent", "Log photon onevent to cmd.", delegate(bool s)
			{
				PhtonManagerUtils.isdebugtime = s;
			}, PhtonManagerUtils.isdebugtime);
			category2.AddToggle("Log Photon OnEventSent", "Log photon oneventsent to cmd.", delegate(bool s)
			{
				PhotonDebugger.IsOnEventSendDebug = s;
			}, PhotonDebugger.IsOnEventSendDebug);
			reCategoryPage.AddCategory("KeyBinds Settings");
			ReMenuCategory category3 = reCategoryPage.GetCategory("KeyBinds Settings");
			category3.AddToggle("Fly Keybind", "Allow you to toggle flight with keybind.", delegate(bool s)
			{
				ConfManager.FlyKeyBind.Value = s;
				MelonPreferences.Save();
			}, ConfManager.FlyKeyBind.Value);
			category3.AddToggle("Double tap fly Keybind", "Allow you to toggle flight with keybind.", delegate(bool s)
			{
				ConfManager.DoubleFlyKeyBind.Value = s;
				MelonPreferences.Save();
			}, ConfManager.DoubleFlyKeyBind.Value);
			category3.AddToggle("ThirdPerson Keybind", "Allow you to toggle ThirdPerson with keybind.", delegate(bool s)
			{
				ConfManager.ThirdPersonKeyBind.Value = s;
				MelonPreferences.Save();
			}, ConfManager.ThirdPersonKeyBind.Value);
			category3.AddToggle("Serialize Keybind", "Allow you to toggle serialize with keybind.", delegate(bool s)
			{
				ConfManager.SerializeKeyBind.Value = s;
				MelonPreferences.Save();
			}, ConfManager.SerializeKeyBind.Value);
		}
	}
}
