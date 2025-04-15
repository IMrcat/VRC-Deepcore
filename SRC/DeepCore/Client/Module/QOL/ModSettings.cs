using System;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x02000043 RID: 67
	public static class ModSettings
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B398 File Offset: 0x00009598
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000B39F File Offset: 0x0000959F
		public static KeyCode KeyBind { get; private set; } = 116;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B3A7 File Offset: 0x000095A7
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000B3AE File Offset: 0x000095AE
		public static KeyCode FreeformKeyBind { get; private set; } = 0;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B3B6 File Offset: 0x000095B6
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B3BD File Offset: 0x000095BD
		public static KeyCode MoveRearCameraLeftKeyBind { get; private set; } = 113;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B3C5 File Offset: 0x000095C5
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000B3CC File Offset: 0x000095CC
		public static KeyCode MoveRearCameraRightKeyBind { get; private set; } = 101;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B3D4 File Offset: 0x000095D4
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000B3DB File Offset: 0x000095DB
		public static float FOV { get; private set; } = 80f;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B3E3 File Offset: 0x000095E3
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000B3EA File Offset: 0x000095EA
		public static float NearClipPlane { get; private set; } = 0.01f;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B3F2 File Offset: 0x000095F2
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000B3F9 File Offset: 0x000095F9
		public static bool Enabled { get; private set; } = true;

		// Token: 0x06000193 RID: 403 RVA: 0x0000B404 File Offset: 0x00009604
		public static void RegisterSettings()
		{
			MelonPreferences_Category melonPreferences_Category = MelonPreferences.CreateCategory(ModSettings.categoryName, ModSettings.categoryName);
			ModSettings.keyBind = melonPreferences_Category.CreateEntry<string>("Keybind", ModSettings.KeyBind.ToString(), "Keybind", null, false, false, null, null);
			ModSettings.freeformKeyBind = melonPreferences_Category.CreateEntry<string>("Freeform Keybind", ModSettings.FreeformKeyBind.ToString(), "Freeform Keybind", null, false, false, null, null);
			ModSettings.fov = melonPreferences_Category.CreateEntry<float>("Camera FOV", ModSettings.FOV, "Camera FOV", null, false, false, null, null);
			ModSettings.nearClipPlane = melonPreferences_Category.CreateEntry<float>("Camera NearClipPlane Value", ModSettings.NearClipPlane, "Camera NearClipPlane Value", null, false, false, null, null);
			ModSettings.enabled = melonPreferences_Category.CreateEntry<bool>("Mod Enabled", ModSettings.Enabled, "Mod Enabled", null, false, false, null, null);
			ModSettings.rearCameraChangerEnabled = melonPreferences_Category.CreateEntry<bool>("Rear Camera Changer Enabled", ModSettings.RearCameraChangedEnabled, "Rear Camera Changer Enabled", null, false, false, null, null);
			ModSettings.moveRearCameraLeftKeyBind = melonPreferences_Category.CreateEntry<string>("Move Rear Camera Left KeyBind", ModSettings.MoveRearCameraLeftKeyBind.ToString(), "Move Rear Camera Left KeyBind", null, false, false, null, null);
			ModSettings.moveRearCameraRightKeyBind = melonPreferences_Category.CreateEntry<string>("Move Rear Camera Right KeyBind", ModSettings.MoveRearCameraRightKeyBind.ToString(), "Move Rear Camera Right KeyBind", null, false, false, null, null);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B550 File Offset: 0x00009750
		public static void LoadSettings()
		{
			ModSettings.KeyBind = ModSettings.keyBind.TryParseKeyCodePref(false);
			ModSettings.FreeformKeyBind = ModSettings.freeformKeyBind.TryParseKeyCodePref(true);
			ModSettings.MoveRearCameraLeftKeyBind = ModSettings.moveRearCameraLeftKeyBind.TryParseKeyCodePref(false);
			ModSettings.MoveRearCameraRightKeyBind = ModSettings.moveRearCameraRightKeyBind.TryParseKeyCodePref(false);
			ModSettings.NearClipPlane = ModSettings.nearClipPlane.Value;
			ModSettings.FOV = ModSettings.fov.Value;
			ModSettings.Enabled = ModSettings.enabled.Value;
			ModSettings.RearCameraChangedEnabled = ModSettings.rearCameraChangerEnabled.Value;
			ThirdPersonView.UpdateCameraSettings();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B5E0 File Offset: 0x000097E0
		private static KeyCode TryParseKeyCodePref(this MelonPreferences_Entry<string> pref, bool canBeNone = false)
		{
			KeyCode keyCode;
			try
			{
				if (!canBeNone && pref.Value.Equals("None"))
				{
					throw new ArgumentException();
				}
				keyCode = ModSettings.ParseKeyCode(pref.Value);
			}
			catch (ArgumentException)
			{
				MelonLogger.Error("Failed to parse keybind defaulting back to: " + pref.DefaultValue);
				pref.Value = pref.DefaultValue;
				keyCode = ModSettings.ParseKeyCode(pref.Value);
			}
			return keyCode;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B658 File Offset: 0x00009858
		private static KeyCode ParseKeyCode(string value)
		{
			return (KeyCode)Enum.Parse(typeof(KeyCode), value);
		}

		// Token: 0x040000B9 RID: 185
		private static readonly string categoryName = "StandaloneThirdPerson";

		// Token: 0x040000BA RID: 186
		private static MelonPreferences_Entry<string> keyBind;

		// Token: 0x040000BB RID: 187
		private static MelonPreferences_Entry<string> freeformKeyBind;

		// Token: 0x040000BC RID: 188
		private static MelonPreferences_Entry<string> moveRearCameraLeftKeyBind;

		// Token: 0x040000BD RID: 189
		private static MelonPreferences_Entry<string> moveRearCameraRightKeyBind;

		// Token: 0x040000BE RID: 190
		private static MelonPreferences_Entry<float> fov;

		// Token: 0x040000BF RID: 191
		private static MelonPreferences_Entry<float> nearClipPlane;

		// Token: 0x040000C0 RID: 192
		private static MelonPreferences_Entry<bool> enabled;

		// Token: 0x040000C1 RID: 193
		private static MelonPreferences_Entry<bool> rearCameraChangerEnabled;

		// Token: 0x040000C2 RID: 194
		public static bool RearCameraChangedEnabled = true;
	}
}
