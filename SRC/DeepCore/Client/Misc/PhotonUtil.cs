using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Il2CppSystem;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000084 RID: 132
	internal class PhotonUtil
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x00011540 File Offset: 0x0000F740
		public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			Object @object = SerializationUtil.FromManagedToIL2CPP<Object>(customObject);
			PhotonUtil.OpRaiseEvent(code, @object, RaiseEventOptions, sendOptions);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001155D File Offset: 0x0000F75D
		public static void OpRaiseEvent(byte code, Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject, RaiseEventOptions, sendOptions);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001156C File Offset: 0x0000F76C
		public static void SendChatBoxMessage(string message)
		{
			PhotonUtil.OpRaiseEvent(43, message, new RaiseEventOptions
			{
				field_Public_EventCaching_0 = 0,
				field_Public_ReceiverGroup_0 = 0
			}, default(SendOptions));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public static void RaiseTypingIndicator(byte Type)
		{
			PhotonUtil.OpRaiseEvent(44, Type, new RaiseEventOptions
			{
				field_Public_ReceiverGroup_0 = 0
			}, default(SendOptions));
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000115D4 File Offset: 0x0000F7D4
		internal static byte[] Vector3ToBytes(Vector3 vector3)
		{
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.z), 0, array, 8, 4);
			return array;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011628 File Offset: 0x0000F828
		internal static int ReadActor(byte[] buffer)
		{
			int num;
			if (buffer == null)
			{
				num = 0;
			}
			else if (buffer.Length < 4)
			{
				num = 0;
			}
			else
			{
				num = int.Parse(BitConverter.ToInt32(buffer, 0).ToString().Replace("00001", string.Empty));
			}
			return num;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011670 File Offset: 0x0000F870
		public static void CreatePortal(string InstanceID, Vector3 Position, float Rotation)
		{
			if (InstanceID != null)
			{
				PhotonUtil.OpRaiseEvent(70, new Dictionary<byte, object>
				{
					{ 0, 0 },
					{ 5, InstanceID },
					{
						6,
						PhotonUtil.Vector3ToBytes(Position)
					},
					{ 7, Rotation }
				}, new RaiseEventOptions(), SendOptions.SendReliable);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000116CC File Offset: 0x0000F8CC
		public static void InVRMode(bool VR)
		{
			PhotonUtil.OpRaiseEvent(44, new Hashtable { { "inVRMode", VR } }, new RaiseEventOptions
			{
				field_Public_ReceiverGroup_0 = 0
			}, default(SendOptions));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001170C File Offset: 0x0000F90C
		public static void RaiseBlock(string userid, bool Block)
		{
			if (userid.StartsWith("usr_"))
			{
				PhotonUtil.OpRaiseEvent(33, new Hashtable
				{
					{ 3, Block },
					{ 0, 22 },
					{ 1, userid }
				}, new RaiseEventOptions
				{
					field_Public_ReceiverGroup_0 = 0
				}, default(SendOptions));
				return;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001177C File Offset: 0x0000F97C
		public static void RaisePortalCreate(string InstanceID, Vector3 Position, float Rotation)
		{
			if (InstanceID == null)
			{
				return;
			}
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(Position.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(Position.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(Position.z), 0, array, 8, 4);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public static void RaiseEmojiCreate(int ID)
		{
			PhotonUtil.OpRaiseEvent(71, new Hashtable
			{
				{ 0, 0 },
				{ 2, ID }
			}, new RaiseEventOptions
			{
				field_Public_ReceiverGroup_0 = 0
			}, default(SendOptions));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011828 File Offset: 0x0000FA28
		public static void SendRPC(VRC_EventHandler.VrcEventType EventType, string Name, bool ParameterBool, VRC_EventHandler.VrcBooleanOp BoolOP, GameObject ParamObject, GameObject[] ParamObjects, string ParamString, float Float, int Int, byte[] bytes, VRC_EventHandler.VrcBroadcastType BroadcastType, float Fastforward = 0f)
		{
			VRC_EventHandler.VrcEvent vrcEvent = new VRC_EventHandler.VrcEvent
			{
				EventType = EventType,
				Name = Name,
				ParameterBool = ParameterBool,
				ParameterBoolOp = BoolOP,
				ParameterBytes = bytes,
				ParameterObject = ParamObject,
				ParameterObjects = ParamObjects,
				ParameterString = ParamString,
				ParameterFloat = Float,
				ParameterInt = Int
			};
			Networking.SceneEventHandler.TriggerEvent(vrcEvent, BroadcastType, ParamObject, Fastforward);
		}
	}
}
