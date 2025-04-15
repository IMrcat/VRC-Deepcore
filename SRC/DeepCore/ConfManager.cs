using System;
using System.IO;
using DeepCore.Client;
using MelonLoader;

namespace DeepCore
{
	// Token: 0x02000004 RID: 4
	internal class ConfManager
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020B0 File Offset: 0x000002B0
		public static void FolderCheck()
		{
			string text = "DeepClient";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
				DeepConsole.Log("Config", "Created " + text + " folder.");
			}
			if (!Directory.Exists(text + "/LoadingMusic"))
			{
				Directory.CreateDirectory(text + "/LoadingMusic");
				DeepConsole.Log("Config", "Created " + text + "/LoadingMusic folder.");
			}
			if (!Directory.Exists(text + "/AssetBundles"))
			{
				Directory.CreateDirectory(text + "/AssetBundles");
				DeepConsole.Log("Config", "Created " + text + "/AssetBundles folder.");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002168 File Offset: 0x00000368
		public static void initConfig()
		{
			DeepConsole.Log("Config", "Initializing Config...");
			ConfManager.FolderCheck();
			MelonPreferences_Category melonPreferences_Category = MelonPreferences.CreateCategory("DeepClient", "DeepClient");
			ConfManager.OptifineZoom = melonPreferences_Category.CreateEntry<bool>("optifinezoom", false, "Allow you to zoom like opf when hold alt.", null, false, false, null, null);
			ConfManager.antiUdon = melonPreferences_Category.CreateEntry<bool>("antiUdon", false, "Block Udon Events", null, false, false, null, null);
			ConfManager.blockWorldTriggers = melonPreferences_Category.CreateEntry<bool>("blockWorldTriggers", false, "Block WorldTriggers things.", null, false, false, null, null);
			ConfManager.avatarLogging = melonPreferences_Category.CreateEntry<bool>("avatarLogging", false, "Log Avatar Change.", null, false, false, null, null);
			ConfManager.antiInvalidRPC = melonPreferences_Category.CreateEntry<bool>("antiInvalidRPC", false, "Blocks Invalid RPC.", null, false, false, null, null);
			ConfManager.playerLogger = melonPreferences_Category.CreateEntry<bool>("playerLogger", false, "Log Player Join/Leave Events.", null, false, false, null, null);
			ConfManager.VRCAdminStaffLogger = melonPreferences_Category.CreateEntry<bool>("vrcadminstafflogger", false, "Log AdminStaff Join/Leave.", null, false, false, null, null);
			ConfManager.udonLogger = melonPreferences_Category.CreateEntry<bool>("udonLogger", false, "Log Udon Events", null, false, false, null, null);
			ConfManager.customnameplate = melonPreferences_Category.CreateEntry<bool>("customnameplate", false, "Allow you to see player FPS/PING/Platform.", null, false, false, null, null);
			ConfManager.OwnerSpoof = melonPreferences_Category.CreateEntry<bool>("ownerspoof", false, "Allow you to spoof your username as the world owner.", null, false, false, null, null);
			ConfManager.AntiQuest = melonPreferences_Category.CreateEntry<bool>("antiquest", false, "auto block quest user.", null, false, false, null, null);
			ConfManager.AntiAvatarScallingdisabler = melonPreferences_Category.CreateEntry<bool>("AntiAvatarScallingdisabler", false, "bloack avatar scalling.", null, false, false, null, null);
			ConfManager.VideoPlayerURLLogger = melonPreferences_Category.CreateEntry<bool>("VideoPlayerURLLogger", false, "Log loading utl.", null, false, false, null, null);
			ConfManager.ShouldQMFreeze = melonPreferences_Category.CreateEntry<bool>("QMFreeze", false, "Allow you to freeze youself when opening your menu when falling down.", null, false, false, null, null);
			ConfManager.ShouldMenuMusic = melonPreferences_Category.CreateEntry<bool>("MenuMusic", false, "Allow you to have a music when opening your menu..", null, false, false, null, null);
			ConfManager.ShouldSteamAPI = melonPreferences_Category.CreateEntry<bool>("SteamAPI", false, "Allow you to disabled steamapi.", null, false, false, null, null);
			ConfManager.flySpeedValue = melonPreferences_Category.CreateEntry<float>("flySpeedValue", 8f, "Fly Speed Value.", null, false, false, null, null);
			ConfManager.maxAvatarLogToFile = melonPreferences_Category.CreateEntry<int>("maxAvatarLogToFile", 96, "Max avatar entries for avatar file logging.", null, false, false, null, null);
			ConfManager.fakePing = melonPreferences_Category.CreateEntry<int>("fakePing", 30, "Fake Ping Value", null, false, false, null, null);
			ConfManager.fakeFPS = melonPreferences_Category.CreateEntry<float>("fakeFPS", 80f, "Fake FPS Value", null, false, false, null, null);
			ConfManager.fakePingEnabled = melonPreferences_Category.CreateEntry<bool>("fakePingEnabled", false, "Fake Ping Enabled", null, false, false, null, null);
			ConfManager.fakeFPSEnabled = melonPreferences_Category.CreateEntry<bool>("fakeFPSEnabled", false, "Fake FPS Enabled", null, false, false, null, null);
			ConfManager.E1Payload = melonPreferences_Category.CreateEntry<string>("E1Payload", "AAAAAGfp+Lv2GRkA+MrI08yxTwBkxqwATk9LRU0wTk9LM00wTg==", "Allow you to set a e1 payload that's will be used for E1.", null, false, false, null, null);
			ConfManager.LastInstanceID = melonPreferences_Category.CreateEntry<string>("LastInstanceID", "", "Last Instance you did join.", null, false, false, null, null);
			ConfManager.JoinLastInstance = melonPreferences_Category.CreateEntry<bool>("JoinLastInstance", false, "Allow you to rejoin last instance you where in.", null, false, false, null, null);
			ConfManager.FlyKeyBind = melonPreferences_Category.CreateEntry<bool>("FlyKeyBind", false, "Allow you to use ctrl and f for toggling flight.", null, false, false, null, null);
			ConfManager.DoubleFlyKeyBind = melonPreferences_Category.CreateEntry<bool>("DoubleFlyKeyBind", false, "Allow you to use ctrl and f for toggling flight.", null, false, false, null, null);
			ConfManager.ThirdPersonKeyBind = melonPreferences_Category.CreateEntry<bool>("ThirdPersonKeyBind", false, "Allow you to use ThirdPerson keybind.", null, false, false, null, null);
			ConfManager.SerializeKeyBind = melonPreferences_Category.CreateEntry<bool>("SerializeKeyBind", false, "Allow you to toggle serialize shit with '~'.", null, false, false, null, null);
			ConfManager.BLSEnabled = melonPreferences_Category.CreateEntry<bool>("BLSEnabled", false, "BLSEnabled. (Enable for LoadingScreenPictures compatibility)", null, false, false, null, null);
			ConfManager.BLShowLoadingMessages = melonPreferences_Category.CreateEntry<bool>("LoadingMessages", false, "Show loading messages. (Enable for LoadingScreenPictures compatibility)", null, false, false, null, null);
			ConfManager.BLSWarpTunnel = melonPreferences_Category.CreateEntry<bool>("Warp Tunnel", true, "Toggle warp tunnel (good for reducing motion)", null, false, false, null, null);
			ConfManager.BLSVrcLogo = melonPreferences_Category.CreateEntry<bool>("Vrchat Logo", true, "Toggle VRChat logo", null, false, false, null, null);
			ConfManager.BLSModSounds = melonPreferences_Category.CreateEntry<bool>("Mod Sounds", true, "Toggle mod music", null, false, false, null, null);
			ConfManager.resourcePath = melonPreferences_Category.CreateEntry<string>("resourcePath", "DeepClient", "Location for Folder.", null, false, false, null, null);
			melonPreferences_Category.SetFilePath(ConfManager.getResourcePathFull() + "//DeepConfig.cfg");
			melonPreferences_Category.SaveToFile(true);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000257A File Offset: 0x0000077A
		internal static string getResourcePathFull()
		{
			return Path.Combine(new string[] { "DeepClient" });
		}

		// Token: 0x0400000C RID: 12
		internal static MelonPreferences_Entry<bool> OptifineZoom;

		// Token: 0x0400000D RID: 13
		internal static MelonPreferences_Entry<bool> udonLogger;

		// Token: 0x0400000E RID: 14
		internal static MelonPreferences_Entry<bool> antiUdon;

		// Token: 0x0400000F RID: 15
		internal static MelonPreferences_Entry<bool> blockWorldTriggers;

		// Token: 0x04000010 RID: 16
		internal static MelonPreferences_Entry<bool> avatarLogging;

		// Token: 0x04000011 RID: 17
		internal static MelonPreferences_Entry<bool> antiInvalidRPC;

		// Token: 0x04000012 RID: 18
		internal static MelonPreferences_Entry<bool> playerLogger;

		// Token: 0x04000013 RID: 19
		internal static MelonPreferences_Entry<bool> VRCAdminStaffLogger;

		// Token: 0x04000014 RID: 20
		internal static MelonPreferences_Entry<bool> customnameplate;

		// Token: 0x04000015 RID: 21
		internal static MelonPreferences_Entry<bool> OwnerSpoof;

		// Token: 0x04000016 RID: 22
		internal static MelonPreferences_Entry<bool> AntiQuest;

		// Token: 0x04000017 RID: 23
		internal static MelonPreferences_Entry<bool> AntiAvatarScallingdisabler;

		// Token: 0x04000018 RID: 24
		internal static MelonPreferences_Entry<bool> VideoPlayerURLLogger;

		// Token: 0x04000019 RID: 25
		internal static MelonPreferences_Entry<bool> ShouldQMFreeze;

		// Token: 0x0400001A RID: 26
		internal static MelonPreferences_Entry<bool> ShouldMenuMusic;

		// Token: 0x0400001B RID: 27
		internal static MelonPreferences_Entry<bool> ShouldSteamAPI;

		// Token: 0x0400001C RID: 28
		internal static MelonPreferences_Entry<bool> fakePingEnabled;

		// Token: 0x0400001D RID: 29
		internal static MelonPreferences_Entry<bool> fakeFPSEnabled;

		// Token: 0x0400001E RID: 30
		internal static MelonPreferences_Entry<float> flySpeedValue;

		// Token: 0x0400001F RID: 31
		internal static MelonPreferences_Entry<int> maxAvatarLogToFile;

		// Token: 0x04000020 RID: 32
		internal static MelonPreferences_Entry<int> fakePing;

		// Token: 0x04000021 RID: 33
		internal static MelonPreferences_Entry<float> fakeFPS;

		// Token: 0x04000022 RID: 34
		internal static MelonPreferences_Entry<string> E1Payload;

		// Token: 0x04000023 RID: 35
		internal static MelonPreferences_Entry<string> LastInstanceID;

		// Token: 0x04000024 RID: 36
		internal static MelonPreferences_Entry<bool> JoinLastInstance;

		// Token: 0x04000025 RID: 37
		public static MelonPreferences_Entry<bool> BLShowLoadingMessages;

		// Token: 0x04000026 RID: 38
		public static MelonPreferences_Entry<bool> BLSWarpTunnel;

		// Token: 0x04000027 RID: 39
		public static MelonPreferences_Entry<bool> BLSVrcLogo;

		// Token: 0x04000028 RID: 40
		public static MelonPreferences_Entry<bool> BLSModSounds;

		// Token: 0x04000029 RID: 41
		public static MelonPreferences_Entry<bool> BLSEnabled;

		// Token: 0x0400002A RID: 42
		internal static MelonPreferences_Entry<bool> FlyKeyBind;

		// Token: 0x0400002B RID: 43
		internal static MelonPreferences_Entry<bool> DoubleFlyKeyBind;

		// Token: 0x0400002C RID: 44
		internal static MelonPreferences_Entry<bool> ThirdPersonKeyBind;

		// Token: 0x0400002D RID: 45
		internal static MelonPreferences_Entry<bool> SerializeKeyBind;

		// Token: 0x0400002E RID: 46
		private static MelonPreferences_Entry<string> resourcePath;
	}
}
