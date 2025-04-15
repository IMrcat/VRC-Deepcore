using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A1 RID: 161
	internal class LastSeenAvatars
	{
		// Token: 0x06000399 RID: 921 RVA: 0x00015B9C File Offset: 0x00013D9C
		internal static void LoggedAvatarsMenu(UiManager uiManager)
		{
			LastSeenAvatars._uiManager = uiManager;
			LastSeenAvatars._LastSeenAvatarsMenu = LastSeenAvatars._uiManager.QMMenu.AddCategoryPage("Avatars Logger", "Avatars Logger", null, "#ffffff");
			LastSeenAvatars._LastSeenAvatarsMenu.OnOpen += LastSeenAvatars.lastSeenAction;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00015BEC File Offset: 0x00013DEC
		private static void lastSeenAction()
		{
			if (LastSeenAvatars.calledTwice)
			{
				LastSeenAvatars.calledTwice = false;
				return;
			}
			LastSeenAvatars.calledTwice = true;
			if (LastSeenAvatars.tempPage != 0)
			{
				LastSeenAvatars._LastSeenAvatars.Active = false;
			}
			int num = ++LastSeenAvatars.tempPage;
			LastSeenAvatars._LastSeenAvatarsMenu.AddCategory("Logged avi (" + num.ToString() + ")");
			LastSeenAvatars._LastSeenAvatars = LastSeenAvatars._LastSeenAvatarsMenu.GetCategory("Logged avi (" + num.ToString() + ")");
			List<Avatar_Object> list = AvatarLoggerHandler.Fetch();
			if (list == null || list.Count == 0)
			{
				return;
			}
			list.Reverse();
			int num2 = 1;
			using (IEnumerator<Avatar_Object> enumerator = ((IEnumerable<Avatar_Object>)list).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Avatar_Object avatarObject = enumerator.Current;
					num2++;
					if (!(avatarObject.releaseStatus != "public") && avatarObject.supportedPlatforms != 2 && !avatarObject.isblackListed)
					{
						string text = string.Concat(new string[] { "Id: (", avatarObject.id, ") By: (", avatarObject.authorName, ") Description: ", avatarObject.description });
						LastSeenAvatars._LastSeenAvatars.AddButton(avatarObject.name, text, delegate
						{
							VrcExtensions.ChangeAvatar(avatarObject.id);
						}, null, "#ffffff");
					}
				}
			}
		}

		// Token: 0x04000225 RID: 549
		private static UiManager _uiManager;

		// Token: 0x04000226 RID: 550
		private static ReCategoryPage _LastSeenAvatarsMenu;

		// Token: 0x04000227 RID: 551
		private static ReMenuCategory _LastSeenAvatars;

		// Token: 0x04000228 RID: 552
		private static int tempPage;

		// Token: 0x04000229 RID: 553
		private static bool calledTwice;
	}
}
