using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace DeepCore.Client.Mono
{
	// Token: 0x02000023 RID: 35
	public class CustomNameplate : MonoBehaviour, IDisposable
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005C06 File Offset: 0x00003E06
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00005C0E File Offset: 0x00003E0E
		public Player Player { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00005C17 File Offset: 0x00003E17
		public CustomNameplate(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005C44 File Offset: 0x00003E44
		public void Dispose()
		{
			if (this._statsText != null)
			{
				this._statsText.text = null;
				this._statsText.OnDisable();
				Object.Destroy(this._statsText.gameObject);
				this._statsText = null;
			}
			base.enabled = false;
			CustomNameplate.nameplates.Remove(this);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005CA0 File Offset: 0x00003EA0
		private void Start()
		{
			try
			{
				PlayerNameplate field_Public_PlayerNameplate_ = this.Player._vrcplayer.field_Public_PlayerNameplate_0;
				if (field_Public_PlayerNameplate_ != null)
				{
					Transform transform = field_Public_PlayerNameplate_.field_Public_GameObject_5.transform;
					if (transform.Find("Trust Text").GetComponent<TextMeshProUGUI>() != null)
					{
						Transform transform2 = Object.Instantiate<Transform>(transform, transform.parent);
						transform2.localPosition = new Vector3(0f, -75f, 0f);
						transform2.gameObject.SetActive(true);
						this._statsText = transform2.Find("Trust Text").GetComponent<TextMeshProUGUI>();
						if (this._statsText != null)
						{
							this._statsText.color = Color.white;
							this._statsText.isOverlay = this.OverRender && this.Enabled;
						}
						Transform transform3 = transform2.Find("Trust Icon");
						if (transform3 != null)
						{
							transform3.gameObject.SetActive(false);
						}
						Transform transform4 = transform2.Find("Performance Icon");
						if (transform4 != null)
						{
							transform4.gameObject.SetActive(false);
						}
						Transform transform5 = transform2.Find("Performance Text");
						if (transform5 != null)
						{
							transform5.gameObject.SetActive(false);
						}
						Transform transform6 = transform2.Find("Friend Anchor Stats");
						if (transform6 != null)
						{
							transform6.gameObject.SetActive(true);
						}
					}
				}
				this.StartTime = Time.time;
				this.looptime = Time.time;
				this.ColorShit();
			}
			catch (Exception ex)
			{
				DeepConsole.Log("CNP", ex.Message);
			}
			switch (PlayerUtil.GetPlayerRank(this.Player._vrcplayer))
			{
			case 1:
				this.ranktext = "<color=#6495ED>New User</color>";
				break;
			case 2:
				this.ranktext = "<color=#90EE90>User</color>";
				break;
			case 3:
				this.ranktext = "<color=#ffca5d>Known</color>";
				break;
			case 4:
				this.ranktext = "<color=#d472ff>Trusted</color>";
				break;
			case 5:
				this.ranktext = "<color=#ff7575>Troll</color>";
				break;
			case 6:
				this.ranktext = "<color=#fffd81>Friend</color>";
				break;
			}
			if (this.Player.field_Private_VRCPlayerApi_0.IsUserInVR())
			{
				if (this.Player.field_Private_APIUser_0.last_platform.ToLower() != "standalonewindows")
				{
					this.platform = "Q";
				}
				else
				{
					this.platform = "VR";
				}
			}
			else if (this.Player.field_Private_APIUser_0.last_platform.ToLower() != "standalonewindows")
			{
				this.platform = "Android";
			}
			else
			{
				this.platform = "D";
			}
			if (this.Player._vrcplayer._player.field_Private_APIUser_0.id == "")
			{
				this.IsDcOwner = true;
			}
			CustomNameplate.nameplates.Add(this, this.Player.field_Private_APIUser_0.id);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005FA0 File Offset: 0x000041A0
		private void Update()
		{
			if (this._statsText == null)
			{
				return;
			}
			if (Time.time >= this.looptime + 3f)
			{
				int num = (int)(Time.time - this.StartTime);
				int num2 = num / 3600;
				int num3 = num % 3600 / 60;
				int num4 = num % 60;
				string text;
				if (num2 > 0)
				{
					float num5 = (float)num / 3600f;
					text = string.Format("{0:F1}h", num5);
				}
				else if (num3 > 0)
				{
					text = string.Format("{0}m", num3);
				}
				else
				{
					text = string.Format("{0}s", num4);
				}
				string text2 = this.platform;
				this._statsText.text = string.Concat(new string[]
				{
					"[",
					"<color=#00ffd5>",
					text,
					"</color>]:[",
					text2,
					"]:[",
					this.ranktext,
					"]:[F:",
					PlayerUtil.ColourFPS(this.Player),
					"]:[P:",
					this.Player.GetPingColord(),
					"]:[",
					this.Player.IsPlayerMicDisabled(),
					"]",
					this.FormattedDate
				});
				this.looptime = Time.time;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000060FC File Offset: 0x000042FC
		public static CustomNameplate FindById(string Id)
		{
			foreach (KeyValuePair<CustomNameplate, string> keyValuePair in CustomNameplate.nameplates)
			{
				if (keyValuePair.Value == Id)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006164 File Offset: 0x00004364
		public static void PlayerLeft(string Id)
		{
			foreach (KeyValuePair<CustomNameplate, string> keyValuePair in CustomNameplate.nameplates)
			{
				if (keyValuePair.Value == Id)
				{
					CustomNameplate.nameplates.Remove(keyValuePair.Key);
					break;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000061D4 File Offset: 0x000043D4
		private void ColorShit()
		{
			Transform transform = this.Player._vrcplayer.field_Public_PlayerNameplate_0.field_Public_GameObject_0.transform;
			if (transform.Find("Main/Background"))
			{
				transform.Find("Main/Background").GetComponent<ImageThreeSlice>().gameObject.SetActive(false);
			}
			if (transform.Find("Main/Glow"))
			{
				transform.Find("Main/Glow").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
			if (transform.Find("Main/Pulse"))
			{
				transform.Find("Main/Pulse").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
			if (transform.Find("Text Container/Name"))
			{
				transform.Find("Text Container/Name").GetComponent<NameplateTextMeshProUGUI>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
				transform.Find("Text Container/Name").GetComponent<NameplateTextMeshProUGUI>().m_Color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
			if (transform.Find("Icon/Background"))
			{
				transform.Find("Icon/Background").GetComponent<Image>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
			if (transform.Find("Icon/Glow"))
			{
				transform.Find("Icon/Glow").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
			if (transform.Find("Icon/Pulse"))
			{
				transform.Find("Icon/Pulse").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
			}
		}

		// Token: 0x0400005C RID: 92
		private static Dictionary<CustomNameplate, string> nameplates = new Dictionary<CustomNameplate, string>();

		// Token: 0x0400005D RID: 93
		private TextMeshProUGUI _statsText;

		// Token: 0x0400005E RID: 94
		public bool OverRender = true;

		// Token: 0x0400005F RID: 95
		public bool Enabled = true;

		// Token: 0x04000060 RID: 96
		public string Role = string.Empty;

		// Token: 0x04000061 RID: 97
		private float StartTime;

		// Token: 0x04000062 RID: 98
		private float looptime;

		// Token: 0x04000063 RID: 99
		public string DateCreated;

		// Token: 0x04000064 RID: 100
		private string FormattedDate;

		// Token: 0x04000065 RID: 101
		private string platform;

		// Token: 0x04000066 RID: 102
		public bool IsDcOwner;

		// Token: 0x04000067 RID: 103
		public bool isdcuser;

		// Token: 0x04000068 RID: 104
		private string ranktext = "Visitor";
	}
}
