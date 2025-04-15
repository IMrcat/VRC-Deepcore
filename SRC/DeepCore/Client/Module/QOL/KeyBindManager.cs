using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.Movement;
using DeepCore.Client.Module.Photon;
using DeepCore.Client.Module.Visual;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
	// Token: 0x0200003B RID: 59
	internal class KeyBindManager
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000A050 File Offset: 0x00008250
		public static void OnUpdate()
		{
			if (ConfManager.FlyKeyBind.Value && Input.GetKey(306) && Input.GetKeyDown(102))
			{
				Flight.FlyToggle();
			}
			if (ConfManager.DoubleFlyKeyBind.Value)
			{
				if (Entry.IsInVR)
				{
					if (Binds.Button_Jump.stateDown)
					{
						if (Time.time - KeyBindManager.lastPressTime <= KeyBindManager.doublePressTime)
						{
							Flight.FlyToggle();
						}
						KeyBindManager.lastPressTime = Time.time;
					}
				}
				else if (Input.GetKeyDown(32))
				{
					if (Time.time - KeyBindManager.lastPressTime <= KeyBindManager.doublePressTime)
					{
						Flight.FlyToggle();
					}
					KeyBindManager.lastPressTime = Time.time;
				}
			}
			if (ConfManager.SerializeKeyBind.Value && Input.GetKeyDown(96))
			{
				MovementSerilize.State();
			}
			if (FlipScreen.IsEnabled)
			{
				FlipScreen.OnUpdate();
			}
		}

		// Token: 0x0400009A RID: 154
		public static float doublePressTime = 0.3f;

		// Token: 0x0400009B RID: 155
		private static float lastPressTime = 0f;
	}
}
