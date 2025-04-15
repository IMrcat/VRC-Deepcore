using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.GUI
{
	// Token: 0x02000089 RID: 137
	internal class LineESP
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00012A03 File Offset: 0x00010C03
		public static void LineState(bool s)
		{
			if (s)
			{
				if (LineESP.localPlayer == null)
				{
					LineESP.localPlayer = Player.Method_Internal_Static_get_Player_0().transform;
				}
				LineESP.FindOtherPlayers();
				LineESP.GUILinepMyESP = true;
				return;
			}
			LineESP.ClearLines();
			LineESP.GUILinepMyESP = false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012A3B File Offset: 0x00010C3B
		public static void ClearLines()
		{
			LineESP.otherPlayers.Clear();
			LineESP.lineObjects.ForEach(new Action<GameObject>(Object.Destroy));
			LineESP.lineObjects.Clear();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00012A68 File Offset: 0x00010C68
		public static void FindOtherPlayers()
		{
			LineESP.otherPlayers.Clear();
			Player[] allPlayers = VrcExtensions.GetAllPlayers();
			for (int i = 0; i < allPlayers.Length; i++)
			{
				GameObject gameObject = allPlayers[i].gameObject;
				Transform transform = ((gameObject != null) ? gameObject.transform : null);
				if (transform != null)
				{
					Object @object = transform;
					VRCPlayerApi vrcplayerApi = Networking.LocalPlayer;
					Object object2;
					if (vrcplayerApi == null)
					{
						object2 = null;
					}
					else
					{
						GameObject gameObject2 = vrcplayerApi.gameObject;
						object2 = ((gameObject2 != null) ? gameObject2.transform : null);
					}
					if (@object != object2)
					{
						LineESP.otherPlayers.Add(transform);
					}
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00012AE4 File Offset: 0x00010CE4
		public static void Render()
		{
			if (!LineESP.GUILinepMyESP || Networking.LocalPlayer == null || Camera.main == null)
			{
				return;
			}
			Vector3 vector;
			vector..ctor((float)Screen.width / 2f, (float)Screen.height / 2f, 0f);
			foreach (Transform transform in LineESP.otherPlayers)
			{
				if (!(transform == null))
				{
					Vector3 vector2 = Camera.main.WorldToScreenPoint(transform.position);
					vector2.y = (float)Screen.height - vector2.y;
					LineESP.DrawLine(vector, vector2, Color.green, 2f);
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			Color color2 = GUI.color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);
			if (pointA.y > pointB.y)
			{
				num = -num;
			}
			float magnitude = (pointB - pointA).magnitude;
			GUI.color = color;
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, magnitude, width), Texture2D.whiteTexture);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		// Token: 0x040001BE RID: 446
		public static List<Transform> otherPlayers = new List<Transform>();

		// Token: 0x040001BF RID: 447
		public static bool GUILinepMyESP = false;

		// Token: 0x040001C0 RID: 448
		public static Transform localPlayer;

		// Token: 0x040001C1 RID: 449
		private static readonly List<GameObject> lineObjects = new List<GameObject>();
	}
}
