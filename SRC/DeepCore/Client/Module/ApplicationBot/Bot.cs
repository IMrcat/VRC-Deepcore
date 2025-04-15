using System;
using System.Collections.Generic;
using System.IO;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x0200006F RID: 111
	internal class Bot
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000EE4D File Offset: 0x0000D04D
		private static GameObject _camera
		{
			get
			{
				return ObjectExtensions.GetPlayerCamera;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000EE54 File Offset: 0x0000D054
		private static void MovePlayer(Vector3 pos)
		{
			if (PlayerExtensions.LocalVRCPlayer != null)
			{
				PlayerExtensions.LocalVRCPlayer.transform.position += pos;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000EE80 File Offset: 0x0000D080
		public static void MakeBot(int Profile, bool gpu, bool auth = false)
		{
			string text = "BotLaunch.exe";
			if (auth)
			{
				text = File.ReadAllText(text);
			}
			string text2 = (gpu ? string.Format("--profile={0} --no-vr -DCDaddyUwU -Number{1}", Profile, Profile) : string.Format("--profile={0} --no-vr -batchmode -DCDaddyUwU -Number{1} -batchmode -noUpm -nographics -disable-gpu-skinning -no-stereo-rendering", Profile, Profile));
			BotAPI.LaunchBot(new string[] { text, text2 });
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		public static void ReceiveCommand(string Command)
		{
			int num = Command.IndexOf(" ");
			string CMD = Command.Substring(0, num);
			string Parameters = Command.Substring(num + 1);
			Bot.HandleActionOnMainThread(delegate
			{
				Bot.Commands[CMD](Parameters);
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000EF2F File Offset: 0x0000D12F
		private static void HandleActionOnMainThread(Action action)
		{
			Bot._lastActionOnMainThread = action;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000EF38 File Offset: 0x0000D138
		public static void OnUpdate()
		{
			if (Entry.IsBot)
			{
				if (Bot._lastActionOnMainThread != null)
				{
					Bot._lastActionOnMainThread();
					Bot._lastActionOnMainThread = null;
				}
				Bot.HandleBotFunctions();
				if (Bot._followTargetPlayer != null)
				{
					PlayerUtil.LocalPlayer().transform.position = Bot._followTargetPlayer.transform.position + new Vector3(1f, 0f, 0f);
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public static void HandleBotFunctions()
		{
			if (Bot._orbitTarget != null && PlayerExtensions.LocalVRCPlayer != null)
			{
				Physics.gravity = new Vector3(0f, 0f, 0f);
				Bot.alpha += Time.deltaTime * Bot.OrbitSpeed;
				PlayerExtensions.LocalPlayer.transform.position = new Vector3(Bot._orbitTarget.transform.position.x + Bot.a * (float)Math.Cos((double)Bot.alpha), Bot._orbitTarget.transform.position.y, Bot._orbitTarget.transform.position.z + Bot.b * (float)Math.Sin((double)Bot.alpha));
			}
			if (Bot._spinbot && PlayerExtensions.LocalVRCPlayer != null)
			{
				PlayerExtensions.LocalVRCPlayer.transform.Rotate(new Vector3(0f, (float)Bot._spinbotSpeed, 0f));
			}
			if (Bot._PrefabName != "")
			{
				Bot.SpawnPrefab(Bot._PrefabName);
			}
			if (Bot._sitOn)
			{
				Bot.TeleportToIUser(Bot._target);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F0E9 File Offset: 0x0000D2E9
		public static void SpawnPrefab(string Name)
		{
			PlayerExtensions.InstantiatePrefab(Name, PlayerExtensions.LocalVRCPlayer.transform.position, new Quaternion(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue)).SetActive(false);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F11F File Offset: 0x0000D31F
		public static void OnUiManagerInit()
		{
			if (Entry.IsBot)
			{
				SocketConnection.Client();
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F130 File Offset: 0x0000D330
		public static void StandardSetup(string userId)
		{
			VRCPlayer vrcplayer = PlayerExtensions.GetPlayerByUserID(userId)._vrcplayer;
			if (!(vrcplayer == null))
			{
				Bot._target = vrcplayer;
				Bot.SetGravity();
				Bot._sitOn = true;
				Bot.TeleportToIUser(vrcplayer);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F169 File Offset: 0x0000D369
		public static void SetGravity()
		{
			if (!(Physics.gravity == Vector3.zero))
			{
				Bot._originalGravity = Physics.gravity;
				Physics.gravity = Vector3.zero;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F190 File Offset: 0x0000D390
		public static void RemoveSetGravity()
		{
			if (!(Bot._originalGravity == Vector3.zero))
			{
				Physics.gravity = Bot._originalGravity;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public static void TeleportToIUser(VRCPlayer user)
		{
			try
			{
				Vector3 vector = default(Vector3);
				vector = user.field_Internal_Animator_0.GetBoneTransform(10).position + new Vector3(0f, 0.1f, 0f);
				Transform transform = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
				if (Bot._playerLastPos != default(Vector3) && Bot._playerLastPos != transform.position)
				{
					transform.position = vector;
				}
				Bot._playerLastPos = vector;
			}
			catch
			{
				Bot._target = null;
				Bot.RemoveSetGravity();
			}
		}

		// Token: 0x04000134 RID: 308
		public const float _moveSpeed = 0.1f;

		// Token: 0x04000135 RID: 309
		public static bool _sitOn = false;

		// Token: 0x04000136 RID: 310
		public static VRCPlayer _target;

		// Token: 0x04000137 RID: 311
		public static Vector3 _originalGravity;

		// Token: 0x04000138 RID: 312
		public static Vector3 _playerLastPos;

		// Token: 0x04000139 RID: 313
		public static byte[] _e7Data;

		// Token: 0x0400013A RID: 314
		public static string _event12Target = "";

		// Token: 0x0400013B RID: 315
		public static Player _event12TargetPlayer = null;

		// Token: 0x0400013C RID: 316
		public static Player _followTargetPlayer = null;

		// Token: 0x0400013D RID: 317
		public static bool _blockEvent12FromSending = false;

		// Token: 0x0400013E RID: 318
		public static Player _orbitTarget;

		// Token: 0x0400013F RID: 319
		public static Action _lastActionOnMainThread;

		// Token: 0x04000140 RID: 320
		public static bool _spinbot = false;

		// Token: 0x04000141 RID: 321
		public static int _spinbotSpeed = 20;

		// Token: 0x04000142 RID: 322
		public static string _PrefabName = "";

		// Token: 0x04000143 RID: 323
		public static float OrbitSpeed = 5f;

		// Token: 0x04000144 RID: 324
		public static float alpha = 0f;

		// Token: 0x04000145 RID: 325
		public static float a = 1f;

		// Token: 0x04000146 RID: 326
		public static float b = 1f;

		// Token: 0x04000147 RID: 327
		public static float Range = 1f;

		// Token: 0x04000148 RID: 328
		public static float Height = 0f;

		// Token: 0x04000149 RID: 329
		public VRCPlayer currentPlayer;

		// Token: 0x0400014A RID: 330
		public Player selectedPlayer;

		// Token: 0x0400014B RID: 331
		private static Dictionary<string, Action<string>> Commands = new Dictionary<string, Action<string>>
		{
			{
				"JoinWorld",
				delegate(string WorldID)
				{
					DeepConsole.Log("Bot", "Joining World " + WorldID);
					if (RoomManager.field_Internal_Static_ApiWorld_0 != null)
					{
						Networking.GoToRoom(WorldID);
					}
				}
			},
			{
				"SetAvatar",
				delegate(string AvatarID)
				{
					VrcExtensions.ChangeAvatar(AvatarID);
				}
			},
			{
				"SendChatMsg",
				delegate(string msg)
				{
					PhotonEx.SendChatBoxMessage(msg.ToString() ?? "");
				}
			}
		};
	}
}
