using System;
using System.Collections.Generic;

namespace DeepCore.Client.GUI
{
	// Token: 0x0200008B RID: 139
	public class UIPage
	{
		// Token: 0x06000337 RID: 823 RVA: 0x000133A3 File Offset: 0x000115A3
		public void SetLabel(string newLabel)
		{
			this.label = newLabel;
		}

		// Token: 0x040001D3 RID: 467
		public string label;

		// Token: 0x040001D4 RID: 468
		public int selectedItem;

		// Token: 0x040001D5 RID: 469
		public int pageID;

		// Token: 0x040001D6 RID: 470
		public bool parentIsMainMenu;

		// Token: 0x040001D7 RID: 471
		public UIPage parent;

		// Token: 0x040001D8 RID: 472
		public List<UIButton> buttons;
	}
}
