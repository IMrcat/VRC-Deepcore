using System;
using VRC.Core;

namespace DeepCore.Client.Misc
{
	// Token: 0x0200007C RID: 124
	internal class World_Object
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00010AF2 File Offset: 0x0000ECF2
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00010AFA File Offset: 0x0000ECFA
		public string worldName { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00010B03 File Offset: 0x0000ED03
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00010B0B File Offset: 0x0000ED0B
		public string worldID { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00010B14 File Offset: 0x0000ED14
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public string worldImageURL { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00010B25 File Offset: 0x0000ED25
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00010B2D File Offset: 0x0000ED2D
		public string instanceID { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00010B36 File Offset: 0x0000ED36
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00010B3E File Offset: 0x0000ED3E
		public string instanceName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00010B47 File Offset: 0x0000ED47
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00010B4F File Offset: 0x0000ED4F
		public string instanceType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00010B58 File Offset: 0x0000ED58
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00010B60 File Offset: 0x0000ED60
		public string instanceOwner { get; set; }

		// Token: 0x06000288 RID: 648 RVA: 0x00010B69 File Offset: 0x0000ED69
		internal World_Object()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00010B74 File Offset: 0x0000ED74
		internal World_Object(ApiWorld wrld, ApiWorldInstance worldInstance)
		{
			this.worldName = wrld.name;
			this.worldID = wrld.id;
			this.worldImageURL = wrld.imageUrl;
			this.instanceID = worldInstance.instanceId;
			this.instanceName = worldInstance.name;
			this.instanceType = worldInstance.type.ToString();
			this.instanceOwner = worldInstance.ownerId ?? "";
		}
	}
}
