using System;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.Core;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x0200006E RID: 110
	internal class AppBotMenu
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public static void APBFunctionsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("AppBot Functions", null, null, "#ffffff");
			reCategoryPage.AddCategory("Bot Controls");
			ReMenuCategory category = reCategoryPage.GetCategory("Bot Controls");
			category.AddButton("Start bot 1", "", delegate
			{
				if (!AppBotMenu.ServerStart)
				{
					SocketConnection.StartServer();
					AppBotMenu.ServerStart = true;
				}
				Bot.MakeBot(8, AppBotMenu.WithGUi, false);
			}, null, "#ffffff");
			category.AddToggle("With GUI", "With gui or not?", delegate(bool s)
			{
				AppBotMenu.WithGUi = s;
			});
			reCategoryPage.AddCategory("Other Controls");
			ReMenuCategory category2 = reCategoryPage.GetCategory("Other Controls");
			category2.AddButton("Join To Me [All]", "", delegate
			{
				string location = APIUser.CurrentUser.location;
				SocketConnection.SendCommandToClients("JoinWorld " + location);
			}, null, "#ffffff");
			category2.AddButton("Join WorldID [All]", "", delegate
			{
				PopupHelper.PopupCall("Join WorldID", "Enter ID", "Set", false, delegate(string userInput)
				{
					SocketConnection.SendCommandToClients("JoinWorld " + userInput);
				});
			}, null, "#ffffff");
			category2.AddButton("Set Avatar [All]", "", delegate
			{
				PopupHelper.PopupCall("Set Avatar", "Enter ID", "Set", false, delegate(string userInput)
				{
					SocketConnection.SendCommandToClients("SetAvatar " + userInput);
				});
			}, null, "#ffffff");
			category2.AddButton("Clone My Avatar [All]", "", delegate
			{
				string avatarId = APIUser.CurrentUser.avatarId;
				SocketConnection.SendCommandToClients("SetAvatar " + avatarId);
			}, null, "#ffffff");
			category2.AddButton("Send Chat Msg [All]", "", delegate
			{
				PopupHelper.PopupCall("Chat Message", "Enter Text", "Send", false, delegate(string userInput)
				{
					SocketConnection.SendCommandToClients("SendChatMsg " + userInput);
				});
			}, null, "#ffffff");
			reCategoryPage.AddCategory("Movements Controls");
			reCategoryPage.GetCategory("Movements Controls");
			reCategoryPage.AddCategory("Server Controls");
			ReMenuCategory category3 = reCategoryPage.GetCategory("Server Controls");
			category3.AddButton("Start Server", "", delegate
			{
				SocketConnection.StartServer();
			}, null, "#ffffff");
			category3.AddButton("Stop Server", "", delegate
			{
				SocketConnection.StopServer();
			}, null, "#ffffff");
		}

		// Token: 0x04000132 RID: 306
		public static bool ServerStart;

		// Token: 0x04000133 RID: 307
		public static bool WithGUi;
	}
}
