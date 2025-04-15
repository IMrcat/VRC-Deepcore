using System;
using System.Reflection;
using DeepCore.Client.Coroutine;
using DeepCore.Client.Module.Photon;
using DeepCore.Client.Module.QOL;
using DeepCore.Client.Module.RPC;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using Photon.Realtime;
using VRC;
using VRC.Economy;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001D RID: 29
	internal class Initpatches
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000521C File Offset: 0x0000341C
		private static HarmonyMethod GetPreFix(string methodName)
		{
			return new HarmonyMethod(typeof(Initpatches).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005238 File Offset: 0x00003438
		[Obsolete]
		public static void Start()
		{
			DeepConsole.Log("Startup", "Starting Hooks...");
			try
			{
				ClonePatch.Patch();
				Initpatches.pass++;
			}
			catch (Exception ex)
			{
				DeepConsole.Log(Initpatches.ModuleName, "allowAvatarCopying:" + ex.Message);
				Initpatches.fail++;
			}
			try
			{
				SpooferPatch.InitSpoofs();
				Initpatches.pass++;
			}
			catch (Exception ex2)
			{
				DeepConsole.Log(Initpatches.ModuleName, "Spoofer:" + ex2.Message);
				Initpatches.fail++;
			}
			try
			{
				EasyPatching.DeepCoreInstance.PatchAll(typeof(HighlightColorPatch));
				Initpatches.pass++;
			}
			catch (Exception ex3)
			{
				DeepConsole.Log(Initpatches.ModuleName, "HighlightColor:" + ex3.Message);
				Initpatches.fail++;
			}
			try
			{
				LoadBalancingClientPatch.Patch();
				Initpatches.pass++;
			}
			catch (Exception ex4)
			{
				DeepConsole.Log(Initpatches.ModuleName, "LoadBalancingClient.OnEvent:" + ex4.Message);
				Initpatches.fail++;
			}
			try
			{
				EasyPatching.DeepCoreInstance.Patch(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), Initpatches.GetLocalPatch("Patch_OnEventSent"), null, null, null, null);
				Initpatches.pass++;
			}
			catch (Exception ex5)
			{
				DeepConsole.Log(Initpatches.ModuleName, "LoadBalancingClient.OnEventSent:" + ex5.Message);
				Initpatches.fail++;
			}
			try
			{
				Initpatches.instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Boolean_Player_VrcEvent_VrcBroadcastType_0"), new HarmonyMethod(typeof(Initpatches).GetMethod("RPCPatch", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
				DeepConsole.LogConsole("Hook", "Look daddy, I can see rpc now.");
				Initpatches.pass++;
			}
			catch (Exception ex6)
			{
				DeepConsole.Log(Initpatches.ModuleName, "VRC_EventDispatcherRFC.RPC:" + ex6.Message);
				Initpatches.fail++;
			}
			try
			{
				OnAvatarChangedPatch.Patch();
				Initpatches.pass++;
			}
			catch (Exception ex7)
			{
				DeepConsole.Log(Initpatches.ModuleName, "OnAvatarChanged:" + ex7.Message);
				Initpatches.fail++;
			}
			try
			{
				RoomManagerPatch.Patch();
				Initpatches.pass++;
			}
			catch (Exception ex8)
			{
				DeepConsole.Log(Initpatches.ModuleName, "RoomManager:" + ex8.Message);
				Initpatches.fail++;
			}
			try
			{
				UdonSyncPatch.Patch();
				Initpatches.pass++;
			}
			catch (Exception ex9)
			{
				DeepConsole.Log(Initpatches.ModuleName, "UdonSync:" + ex9.Message);
				Initpatches.fail++;
			}
			try
			{
				EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlusStatus).GetProperty("prop_Object1PublicTYBoTYUnique_1_Boolean_0").GetGetMethod(), null, Initpatches.GetLocalPatch("GetVRCPlusStatus"), null, null, null);
				Initpatches.pass++;
			}
			catch (Exception ex10)
			{
				DeepConsole.Log(Initpatches.ModuleName, "VRCPlusStatus:" + ex10.Message);
				Initpatches.fail++;
			}
			try
			{
				Initpatches.instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_VRCPlayerApi_IProduct_PDM_0"), Initpatches.GetPreFix("RetrunPrefix"), null, null, null, null);
				Initpatches.instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_IProduct_PDM_0"), Initpatches.GetPreFix("RetrunPrefix"), null, null, null, null);
				Initpatches.pass++;
			}
			catch (Exception ex11)
			{
				DeepConsole.Log(Initpatches.ModuleName, "Store:" + ex11.Message);
				Initpatches.fail++;
			}
			try
			{
				Initpatches.pass++;
			}
			catch (Exception ex12)
			{
				DeepConsole.Log(Initpatches.ModuleName, "QuickMenu:" + ex12.Message);
				Initpatches.fail++;
			}
			DeepConsole.Log(Initpatches.ModuleName, string.Format("Placed {0} hook successfully, with {1} failed.", Initpatches.pass, Initpatches.fail));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000056F8 File Offset: 0x000038F8
		private static bool RPCPatch(Player param_1, VRC_EventHandler.VrcEvent param_2, VRC_EventHandler.VrcBroadcastType param_3)
		{
			try
			{
				RPCManager.HandleRPC(param_1, param_2, param_3);
				return true;
			}
			catch (Exception ex)
			{
				MelonLogger.Msg(ex.ToString());
			}
			return true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005730 File Offset: 0x00003930
		private static void QmOpen()
		{
			if (ConfManager.ShouldQMFreeze.Value)
			{
				QMFreeze.State(true);
			}
			CustomMenuBG.ApplyColors();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005749 File Offset: 0x00003949
		private static void QmClose()
		{
			if (ConfManager.ShouldQMFreeze.Value)
			{
				QMFreeze.State(false);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000575D File Offset: 0x0000395D
		private static void GetVRCPlusStatus(ref Object1PublicTYBoTYUnique<bool> __result)
		{
			if (__result != null)
			{
				__result.Method_Public_Virtual_New_set_Void_TYPE_0(true);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000576E File Offset: 0x0000396E
		private static bool MarketPatch(VRCPlayerApi __0, IProduct __1, ref bool __result)
		{
			__result = true;
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005774 File Offset: 0x00003974
		private static bool RetrunPrefix(ref bool __result)
		{
			__result = true;
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000577A File Offset: 0x0000397A
		internal static bool Patch_OnEventSent(byte __0, object __1, RaiseEventOptions __2, SendOptions __3)
		{
			if (MovementSerilize.IsEnabled)
			{
				return MovementSerilize.OnEventSent(__0, __1, __2, __3);
			}
			return !PhotonDebugger.IsOnEventSendDebug || PhotonDebugger.OnEventSent(__0, __1, __2, __3);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000057A0 File Offset: 0x000039A0
		public static HarmonyMethod GetLocalPatch(string name)
		{
			HarmonyMethod harmonyMethod;
			try
			{
				harmonyMethod = MelonUtils.ToNewHarmonyMethod(typeof(Initpatches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
			}
			catch (Exception ex)
			{
				DeepConsole.Log(Initpatches.ModuleName, string.Format("{0}: {1}", name, ex));
				harmonyMethod = null;
			}
			return harmonyMethod;
		}

		// Token: 0x04000055 RID: 85
		public static string ModuleName = "HookManager";

		// Token: 0x04000056 RID: 86
		public static readonly Harmony instance = new Harmony("DeepClient.ultrapatch");

		// Token: 0x04000057 RID: 87
		public static int pass = 0;

		// Token: 0x04000058 RID: 88
		public static int fail = 0;
	}
}
