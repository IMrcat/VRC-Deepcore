using System;
using System.Diagnostics;
using System.Linq;
using DeepCore.Client;
using DeepCore.Client.Coroutine;
using DeepCore.Client.GUI;
using DeepCore.Client.Misc;
using DeepCore.Client.Module;
using DeepCore.Client.Module.ApplicationBot;
using DeepCore.Client.Module.Exploits;
using DeepCore.Client.Module.Movement;
using DeepCore.Client.Module.QOL;
using DeepCore.Client.Module.Visual;
using DeepCore.Client.Mono;
using DeepCore.Client.Patching;
using DeepCore.Client.UI;
using DeepCore.Client.UI.QM;
using DeepCore.ServerAPI;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace DeepCore
{
	// Token: 0x02000005 RID: 5
	public class Entry : MelonMod
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002598 File Offset: 0x00000798
		[Obsolete]
		public override void OnInitializeMelon()
		{
			DeepConsole.Alloc();
			new AuthAPI().Auth();
			foreach (string text in Environment.GetCommandLineArgs().ToList<string>())
			{
				if (text.Contains("DCDaddyUwU"))
				{
					Entry.IsBot = true;
					Application.targetFrameRate = 10;
				}
				else if (text.StartsWith("--Number="))
				{
					Entry.NumberBot = text.Replace("--Number=", "");
				}
				else if (text.StartsWith("--profile="))
				{
					Entry.ProfileNumber = text.Replace("--profile=", "").ToLower();
				}
			}
			try
			{
				Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
			}
			catch (Exception)
			{
			}
			Entry.StartClient();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002684 File Offset: 0x00000884
		public static void StartClient()
		{
			try
			{
				if (Entry.IsBot)
				{
					SocketConnection.Client();
					DeepConsole.ChangeTittle("DeepBot - Bot:" + Entry.NumberBot + " - Profile:" + Entry.ProfileNumber);
				}
				ConfManager.initConfig();
				MelonPreferences.Load();
				DeepConsole.Art(Entry.IsBot);
				DeepConsole.Log("Startup", "Starting Client...");
				Initpatches.Start();
				Entry.Injectories();
				Entry.QOLThings();
				CoroutineManager.Init();
				SpriteManager.LoadSprite();
				DeepConsole.Log("Startup", "Waiting for QM...");
				MelonCoroutines.Start(UIController.WaitForQM());
				Entry.IsLoaded = true;
			}
			catch (Exception ex)
			{
				DeepConsole.E(ex);
				WMessageBox.HandleInternalFailure("Client Startup failed: " + ex.Message, true);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002748 File Offset: 0x00000948
		protected static void Injectories()
		{
			DeepConsole.Log("Startup", "Starting Injectories...");
			ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002760 File Offset: 0x00000960
		protected static void QOLThings()
		{
			DeepConsole.Log("Startup", "Starting QOLThings...");
			NoSteamAtAll.Start();
			CoreLimiter.Start();
			RamCleaner.StartMyCleaner();
			Binds.Register();
			MelonCoroutines.Start(GameVersionSpoofer.Init());
			if (ConfManager.BLSEnabled.Value)
			{
				OldLoadingScreenMod.OnApplicationStart();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027AC File Offset: 0x000009AC
		public override void OnUpdate()
		{
			if (Entry.IsLoaded)
			{
				if (Entry.IsBot)
				{
					Bot.OnUpdate();
				}
				DeepCore.Client.Module.Movement.UpdateModule.Update();
				DeepCore.Client.Module.Visual.UpdateModule.OnUpdate();
				KeyBindManager.OnUpdate();
				QMPlayerList.UpdateList();
				SwasticaOrbit.OnUpdate();
				ThirdPersonView.Update();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027DF File Offset: 0x000009DF
		public override void OnGUI()
		{
			DeepCore.Client.GUI.UpdateModule.UpdateGUI();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027E6 File Offset: 0x000009E6
		public override void OnSceneWasLoaded(int buildindex, string sceneName)
		{
			OnLoadedScaneManager.LoadedScene(buildindex, sceneName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027EF File Offset: 0x000009EF
		public override void OnApplicationQuit()
		{
			MelonPreferences.Save();
		}

		// Token: 0x0400002F RID: 47
		[Obsolete]
		public static bool IsBot = false;

		// Token: 0x04000030 RID: 48
		public static bool IsLoaded = false;

		// Token: 0x04000031 RID: 49
		public static bool IsInVR = false;

		// Token: 0x04000032 RID: 50
		public static string NumberBot = "0";

		// Token: 0x04000033 RID: 51
		public static string ProfileNumber = "0";
	}
}
