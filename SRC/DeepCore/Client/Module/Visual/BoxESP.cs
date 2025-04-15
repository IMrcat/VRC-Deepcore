using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x0200002F RID: 47
	internal class BoxESP
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00009143 File Offset: 0x00007343
		public static void BoxState(bool s)
		{
			if (s)
			{
				if (BoxESP.localPlayer == null)
				{
					BoxESP.localPlayer = Player.Method_Internal_Static_get_Player_0().transform;
				}
				BoxESP.FindOtherPlayers();
				return;
			}
			BoxESP.ClearLines();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009170 File Offset: 0x00007370
		public static void FindOtherPlayers()
		{
			BoxESP.otherPlayers.Clear();
			BoxESP.ClearLines();
			foreach (Player player in VrcExtensions.GetAllPlayers())
			{
				if (player.gameObject.transform != BoxESP.localPlayer)
				{
					BoxESP.otherPlayers.Add(player.gameObject.transform);
					BoxESP.CreateBoxRenderer(player.gameObject.transform, player.field_Private_APIUser_0.GetPlayerColor());
					BoxESP.BoxpMyESP = true;
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000091F4 File Offset: 0x000073F4
		public static void CreateBoxRenderer(Transform target, Color color)
		{
			LineRenderer lineRenderer = new GameObject("PlayerBox").AddComponent<LineRenderer>();
			lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
			lineRenderer.startColor = color;
			lineRenderer.endColor = color;
			lineRenderer.startWidth = 0.01f;
			lineRenderer.endWidth = 0.01f;
			lineRenderer.loop = false;
			lineRenderer.positionCount = 16;
			BoxESP.BoxRenderers.Add(lineRenderer);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009264 File Offset: 0x00007464
		public static void UpdateBox()
		{
			if (!BoxESP.BoxpMyESP)
			{
				return;
			}
			if (BoxESP.localPlayer == null || BoxESP.otherPlayers.Count == 0)
			{
				return;
			}
			for (int i = 0; i < BoxESP.otherPlayers.Count; i++)
			{
				Transform transform = BoxESP.otherPlayers[i];
				LineRenderer lineRenderer = BoxESP.BoxRenderers[i];
				if (!(transform == null) && !(lineRenderer == null))
				{
					Vector3 vector = transform.position + Vector3.up * 1f;
					Vector3 vector2;
					vector2..ctor(0.4f, 1.6f, 0.4f);
					Vector3[] array = new Vector3[16];
					Vector3 vector3 = vector + new Vector3(-vector2.x, vector2.y, vector2.z) * 0.5f;
					Vector3 vector4 = vector + new Vector3(vector2.x, vector2.y, vector2.z) * 0.5f;
					Vector3 vector5 = vector + new Vector3(-vector2.x, vector2.y, -vector2.z) * 0.5f;
					Vector3 vector6 = vector + new Vector3(vector2.x, vector2.y, -vector2.z) * 0.5f;
					Vector3 vector7 = vector + new Vector3(-vector2.x, -vector2.y, vector2.z) * 0.5f;
					Vector3 vector8 = vector + new Vector3(vector2.x, -vector2.y, vector2.z) * 0.5f;
					Vector3 vector9 = vector + new Vector3(-vector2.x, -vector2.y, -vector2.z) * 0.5f;
					Vector3 vector10 = vector + new Vector3(vector2.x, -vector2.y, -vector2.z) * 0.5f;
					array[0] = vector3;
					array[1] = vector4;
					array[2] = vector6;
					array[3] = vector5;
					array[4] = vector3;
					array[5] = vector7;
					array[6] = vector8;
					array[7] = vector10;
					array[8] = vector9;
					array[9] = vector7;
					array[10] = vector8;
					array[11] = vector4;
					array[12] = vector6;
					array[13] = vector10;
					array[14] = vector9;
					array[15] = vector5;
					lineRenderer.positionCount = array.Length;
					lineRenderer.SetPositions(array);
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009528 File Offset: 0x00007728
		public static void ClearLines()
		{
			foreach (LineRenderer lineRenderer in BoxESP.BoxRenderers)
			{
				if (lineRenderer != null)
				{
					Object.Destroy(lineRenderer.gameObject);
				}
			}
			BoxESP.BoxRenderers.Clear();
			BoxESP.BoxpMyESP = false;
		}

		// Token: 0x04000088 RID: 136
		public static List<LineRenderer> BoxRenderers = new List<LineRenderer>();

		// Token: 0x04000089 RID: 137
		public static List<Transform> otherPlayers = new List<Transform>();

		// Token: 0x0400008A RID: 138
		public static Transform localPlayer;

		// Token: 0x0400008B RID: 139
		public static bool BoxpMyESP = false;
	}
}
