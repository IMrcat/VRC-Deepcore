using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Visual
{
	// Token: 0x02000032 RID: 50
	internal class LineESP
	{
		// Token: 0x06000131 RID: 305 RVA: 0x0000975F File Offset: 0x0000795F
		public static void LineState(bool s)
		{
			if (s)
			{
				if (LineESP.localPlayer == null)
				{
					LineESP.localPlayer = Player.Method_Internal_Static_get_Player_0().transform;
				}
				LineESP.FindOtherPlayers();
				return;
			}
			LineESP.ClearLines();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000978C File Offset: 0x0000798C
		public static void FindOtherPlayers()
		{
			LineESP.otherPlayers.Clear();
			foreach (Player player in VrcExtensions.GetAllPlayers())
			{
				if (player.gameObject.transform != LineESP.localPlayer)
				{
					LineESP.otherPlayers.Add(player.gameObject.transform);
					LineESP.CreateLineRenderer(player.gameObject.transform, player.field_Private_APIUser_0.GetPlayerColor());
					LineESP.LinepMyESP = true;
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009808 File Offset: 0x00007A08
		public static void CreateLineRenderer(Transform target, Color color)
		{
			LineRenderer lineRenderer = new GameObject("PlayerLine").AddComponent<LineRenderer>();
			lineRenderer.startWidth = 0.01f;
			lineRenderer.endWidth = 0.01f;
			lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
			lineRenderer.startColor = color;
			lineRenderer.endColor = color;
			lineRenderer.positionCount = 2;
			LineESP.lineRenderers.Add(lineRenderer);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009870 File Offset: 0x00007A70
		public static void UpdateLines()
		{
			if (LineESP.LinepMyESP)
			{
				if (LineESP.localPlayer == null || LineESP.otherPlayers.Count == 0)
				{
					return;
				}
				for (int i = 0; i < LineESP.otherPlayers.Count; i++)
				{
					if (!(LineESP.otherPlayers[i] == null) && !(LineESP.lineRenderers[i] == null))
					{
						LineESP.lineRenderers[i].SetPosition(0, LineESP.localPlayer.position);
						LineESP.lineRenderers[i].SetPosition(1, LineESP.otherPlayers[i].position);
					}
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009918 File Offset: 0x00007B18
		public static void ClearLines()
		{
			foreach (LineRenderer lineRenderer in LineESP.lineRenderers)
			{
				if (lineRenderer != null)
				{
					Object.Destroy(lineRenderer.gameObject);
				}
			}
			LineESP.lineRenderers.Clear();
			LineESP.LinepMyESP = false;
		}

		// Token: 0x04000091 RID: 145
		public static List<LineRenderer> lineRenderers = new List<LineRenderer>();

		// Token: 0x04000092 RID: 146
		public static List<Transform> otherPlayers = new List<Transform>();

		// Token: 0x04000093 RID: 147
		public static Transform localPlayer;

		// Token: 0x04000094 RID: 148
		public static bool LinepMyESP = false;
	}
}
