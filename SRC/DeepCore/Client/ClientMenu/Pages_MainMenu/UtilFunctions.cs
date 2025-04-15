using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Module.Funnies;
using DeepCore.Client.UI.QM;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.Udon;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x020000A8 RID: 168
	internal class UtilFunctions
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x00016D5C File Offset: 0x00014F5C
		public static void UtilFunctionsMenu(UiManager UIManager)
		{
			ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Utils Functions", null, null, "#ffffff");
			reCategoryPage.AddCategory("Functions");
			ReMenuCategory category = reCategoryPage.GetCategory("Functions");
			category.AddButton("Backup FrindIDs", "Allow you to bacups all of your frinds to a TXT.", delegate
			{
				ClientUtils.SaveFrinds();
			}, null, "#ffffff");
			category.AddButton("Russian Roulette", "Suka Blyat :3", delegate
			{
				RussianRoulette.RouletteStart();
			}, null, "#ffffff");
			category.AddButton("Log Udon", "Allow you to log all udon in a TXT.", delegate
			{
				ClientUtils.LogUdon();
			}, null, "#ffffff");
			category.AddButton("Dump\nAll\nUdon", "Allow you to dump all udon in a TXT.", delegate
			{
				foreach (UdonBehaviour udonBehaviour in Object.FindObjectsOfType<UdonBehaviour>())
				{
					UdonDisassembler.Disassemble(udonBehaviour, udonBehaviour.name);
				}
			}, null, "#ffffff");
			category.AddButton("Log VRCPickups", "Allow you to log all pickups in a TXT.", delegate
			{
				ClientUtils.LogItems();
			}, null, "#ffffff");
			category.AddButton("Clear Log", "Allow you to clear all log in debug/console.", delegate
			{
				Console.Clear();
				QMConsole.ClearLog();
			}, null, "#ffffff");
			category.AddToggle("HideShow QMConsole", "Allow you to show and hide qmconsole.", delegate(bool s)
			{
				QMConsole.ConsoleVisuabillity(s);
			}, false);
			category.AddButton("Switch to DCAvatar", "Allow you to the client avi :fire:.", delegate
			{
				VrcExtensions.ChangeAvatar("avtr_14c10067-2a13-4d6c-8e38-d4359813af5a");
			}, null, "#ffffff");
		}
	}
}
