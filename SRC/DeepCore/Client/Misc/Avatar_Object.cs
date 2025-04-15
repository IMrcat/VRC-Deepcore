using System;
using System.Collections.Generic;
using System.Linq;
using VRC.Core;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000082 RID: 130
	internal class Avatar_Object
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000111AA File Offset: 0x0000F3AA
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x000111B2 File Offset: 0x0000F3B2
		public string id { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000111BB File Offset: 0x0000F3BB
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000111C3 File Offset: 0x0000F3C3
		public string name { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000111CC File Offset: 0x0000F3CC
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public string imageUrl { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000111DD File Offset: 0x0000F3DD
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000111E5 File Offset: 0x0000F3E5
		public string authorName { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000111EE File Offset: 0x0000F3EE
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000111F6 File Offset: 0x0000F3F6
		public string authorId { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000111FF File Offset: 0x0000F3FF
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00011207 File Offset: 0x0000F407
		public string assetUrl { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00011210 File Offset: 0x0000F410
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00011218 File Offset: 0x0000F418
		public string description { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00011221 File Offset: 0x0000F421
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00011229 File Offset: 0x0000F429
		public string thumbnailImageUrl { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00011232 File Offset: 0x0000F432
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0001123A File Offset: 0x0000F43A
		public int version { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00011243 File Offset: 0x0000F443
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0001124B File Offset: 0x0000F44B
		public int supportedPlatforms { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00011254 File Offset: 0x0000F454
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0001125C File Offset: 0x0000F45C
		public string releaseStatus { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011265 File Offset: 0x0000F465
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0001126D File Offset: 0x0000F46D
		public bool isblackListed { get; set; }

		// Token: 0x060002B9 RID: 697 RVA: 0x00011276 File Offset: 0x0000F476
		internal Avatar_Object()
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011280 File Offset: 0x0000F480
		internal Avatar_Object(ApiAvatar avtr)
		{
			this.id = avtr.id;
			this.name = avtr.name;
			this.imageUrl = avtr.imageUrl;
			this.authorName = avtr.authorName;
			this.authorId = avtr.authorId;
			this.assetUrl = avtr.assetUrl;
			this.description = avtr.description;
			this.thumbnailImageUrl = avtr.thumbnailImageUrl;
			this.version = avtr.version;
			this.supportedPlatforms = Avatar_Object.savePlatform(avtr.supportedPlatforms);
			this.releaseStatus = avtr.releaseStatus;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001131C File Offset: 0x0000F51C
		internal Avatar_Object(Avatar_Object avtr)
		{
			this.id = avtr.id;
			this.name = avtr.name;
			this.imageUrl = avtr.imageUrl;
			this.authorName = avtr.authorName;
			this.authorId = avtr.authorId;
			this.assetUrl = avtr.assetUrl;
			this.description = avtr.description;
			this.thumbnailImageUrl = avtr.thumbnailImageUrl;
			this.version = avtr.version;
			this.supportedPlatforms = avtr.supportedPlatforms;
			this.releaseStatus = avtr.releaseStatus;
			this.isblackListed = avtr.isblackListed;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000113C0 File Offset: 0x0000F5C0
		internal ApiAvatar toApiAvatar()
		{
			return new ApiAvatar
			{
				id = this.id,
				name = this.name,
				authorName = this.authorName,
				authorId = this.authorId,
				assetUrl = this.assetUrl,
				description = this.description,
				thumbnailImageUrl = this.thumbnailImageUrl,
				version = this.version,
				supportedPlatforms = Avatar_Object.getPlatform(this.supportedPlatforms),
				releaseStatus = this.releaseStatus
			};
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001144F File Offset: 0x0000F64F
		private static ApiModel.SupportedPlatforms getPlatform(int num)
		{
			switch (num)
			{
			case 0:
				return 3;
			case 1:
				return 2;
			case 2:
				return 1;
			default:
				return 0;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001146C File Offset: 0x0000F66C
		private static int savePlatform(ApiModel.SupportedPlatforms supportedPlatform)
		{
			switch (supportedPlatform)
			{
			case 1:
				return 2;
			case 2:
				return 1;
			case 3:
				return 0;
			default:
				return 3;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001148C File Offset: 0x0000F68C
		private static bool checkIfBlacklisted(List<string> tags)
		{
			List<string> list = new List<string>();
			foreach (string text in tags)
			{
				list.Add(text);
			}
			return list.Any((string x) => Avatar_Object.blackListed.Any((string y) => y == x));
		}

		// Token: 0x0400017A RID: 378
		private static readonly List<string> blackListed = new List<string> { "admin_featured_quest", "admin_quest_fallback_extended", "admin_quest_fallback_basic", "author_quest_fallback" };
	}
}
