using System;

namespace DeepCore.Client.GUI
{
	// Token: 0x0200008C RID: 140
	public class UIButton
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000133B4 File Offset: 0x000115B4
		public void SetLabel(string newLabel)
		{
			this.label = newLabel;
		}

		// Token: 0x040001D9 RID: 473
		public string label;

		// Token: 0x040001DA RID: 474
		public Action action;

		// Token: 0x040001DB RID: 475
		public int buttonID;
	}
}
