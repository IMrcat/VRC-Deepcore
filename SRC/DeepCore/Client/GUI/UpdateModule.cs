using System;

namespace DeepCore.Client.GUI
{
	// Token: 0x0200008D RID: 141
	internal class UpdateModule
	{
		// Token: 0x0600033B RID: 827 RVA: 0x000133C5 File Offset: 0x000115C5
		public static void StartAll()
		{
			UpdateModule.modMenu = new TestModMenu();
			UpdateModule.modMenu.Start();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000133DB File Offset: 0x000115DB
		public static void UpdateGUI()
		{
			LineESP.Render();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000133E2 File Offset: 0x000115E2
		public static void OnUpdate()
		{
		}

		// Token: 0x040001DC RID: 476
		private static TestModMenu modMenu;
	}
}
