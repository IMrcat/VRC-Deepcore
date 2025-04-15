using System;
using Valve.VR;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000074 RID: 116
	internal class Binds
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000F834 File Offset: 0x0000DA34
		public static void Register()
		{
			Binds.Button_Jump = SteamVR_Input.GetBooleanAction("jump", false);
			Binds.Button_Mic = SteamVR_Input.GetBooleanAction("Toggle Microphone", false);
			Binds.Button_QM = SteamVR_Input.GetBooleanAction("Menu", false);
			Binds.Button_Grab = SteamVR_Input.GetBooleanAction("Grab", false);
			Binds.Button_Interact = SteamVR_Input.GetBooleanAction("Interact", false);
			Binds.Trigger = SteamVR_Input.GetSingleAction("gesture_trigger_axis", false);
			Binds.MoveJoystick = SteamVR_Input.GetVector2Action("Move", false);
			Binds.RotateJoystick = SteamVR_Input.GetVector2Action("Rotate", false);
			DeepConsole.Log("VRBinds", "Binds Registered.");
		}

		// Token: 0x04000152 RID: 338
		public static SteamVR_Action_Boolean Button_Jump;

		// Token: 0x04000153 RID: 339
		public static SteamVR_Action_Boolean Button_QM;

		// Token: 0x04000154 RID: 340
		public static SteamVR_Action_Boolean Button_Mic;

		// Token: 0x04000155 RID: 341
		public static SteamVR_Action_Boolean Button_Grab;

		// Token: 0x04000156 RID: 342
		public static SteamVR_Action_Boolean Button_Interact;

		// Token: 0x04000157 RID: 343
		public static SteamVR_Action_Single Trigger;

		// Token: 0x04000158 RID: 344
		public static SteamVR_Action_Vector2 MoveJoystick;

		// Token: 0x04000159 RID: 345
		public static SteamVR_Action_Vector2 RotateJoystick;
	}
}
