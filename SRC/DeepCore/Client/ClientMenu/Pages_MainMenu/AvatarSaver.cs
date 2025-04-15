using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using DeepCore.Client.Misc;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
	// Token: 0x0200009C RID: 156
	internal class AvatarSaver
	{
		// Token: 0x06000365 RID: 869
		[DllImport("user32.dll")]
		private static extern bool OpenClipboard(IntPtr hWndNewOwner);

		// Token: 0x06000366 RID: 870
		[DllImport("user32.dll")]
		private static extern bool EmptyClipboard();

		// Token: 0x06000367 RID: 871
		[DllImport("user32.dll")]
		private static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

		// Token: 0x06000368 RID: 872
		[DllImport("user32.dll")]
		private static extern bool CloseClipboard();

		// Token: 0x06000369 RID: 873
		[DllImport("kernel32.dll")]
		private static extern IntPtr GlobalAlloc(uint flags, int size);

		// Token: 0x0600036A RID: 874
		[DllImport("kernel32.dll")]
		private static extern IntPtr GlobalLock(IntPtr hMem);

		// Token: 0x0600036B RID: 875
		[DllImport("kernel32.dll")]
		private static extern bool GlobalUnlock(IntPtr hMem);

		// Token: 0x0600036C RID: 876
		[DllImport("kernel32.dll")]
		private static extern IntPtr GlobalFree(IntPtr hMem);

		// Token: 0x0600036D RID: 877 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public static void AvSaverFunctionsMenu(UiManager UIManager)
		{
			AvatarSaver.menuPage = UIManager.QMMenu.GetCategoryPage("Utils Functions").GetCategory("Functions").AddMenuPage("Avatar Saver", "Save and manage avatars", null, "#ffffff");
			AvatarSaver.menuPage.AddButton("Save Current Avatar", "Save your current avatar", new Action(AvatarSaver.SaveCurrentAvatar), null, "#ffffff");
			AvatarSaver.menuPage.AddButton("View Saved Avatars", "Show saved avatars", new Action(AvatarSaver.BuildAndOpenAvatarList), null, "#ffffff");
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00013E60 File Offset: 0x00012060
		public static void BuildAndOpenAvatarList()
		{
			try
			{
				ReMenuPage reMenuPage = AvatarSaver.menuPage.AddMenuPage("Saved Avatars", "List of saved avatars", null, "#ffffff");
				using (List<AvatarSaver.SavedAvatar>.Enumerator enumerator = AvatarSaver.savedAvatars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AvatarSaver.SavedAvatar avatar = enumerator.Current;
						reMenuPage.AddMenuPage(avatar.AvatarName, "By: " + avatar.AuthorName + "\nSaved: " + avatar.SaveDate, delegate(ReMenuPage page)
						{
							AvatarSaver.CreateAvatarButtons(page, avatar);
						}, null, "#ffffff");
					}
				}
				reMenuPage.Open();
			}
			catch (Exception ex)
			{
				MelonLogger.Error(string.Format("[AvatarSaver] Menu build error: {0}", ex));
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013F40 File Offset: 0x00012140
		public static void CreateAvatarButtons(ReMenuPage page, AvatarSaver.SavedAvatar avatar)
		{
			page.AddButton("Switch To Avatar", "Use this avatar", delegate
			{
				AvatarSaver.SwitchToAvatar(avatar);
			}, null, "#ffffff");
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00013F80 File Offset: 0x00012180
		public static void SaveCurrentAvatar()
		{
			try
			{
				AvatarSaver.SaveAvatarInfo(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCAvatarManager_0.field_Private_ApiAvatar_0, true);
			}
			catch (TypeLoadException ex)
			{
				DeepConsole.LogConsole("AvaSaver", "TypeLoadException: " + ex.Message);
			}
			catch (NullReferenceException ex2)
			{
				DeepConsole.LogConsole("AvaSaver", "NullReferenceException: " + ex2.Message);
			}
			catch (Exception ex3)
			{
				DeepConsole.LogConsole("AvaSaver", string.Format("Unexpected error: {0}", ex3));
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00014020 File Offset: 0x00012220
		public static bool SaveAvatarInfo(ApiAvatar avatar, bool notify)
		{
			bool flag;
			try
			{
				AvatarSaver.SavedAvatar savedAvatar = AvatarSaver.savedAvatars.FirstOrDefault((AvatarSaver.SavedAvatar a) => a.AvatarId == avatar.id);
				if (savedAvatar != null)
				{
					savedAvatar.UpdateFromApiAvatar(avatar);
					AvatarSaver.SaveToFile();
					if (notify)
					{
						DeepConsole.Log("AvaSaver", "Updated: " + avatar.name);
					}
					flag = false;
				}
				else
				{
					AvatarSaver.SavedAvatar savedAvatar2 = new AvatarSaver.SavedAvatar
					{
						AvatarId = avatar.id,
						AvatarName = avatar.name,
						AuthorName = avatar.authorName,
						SaveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
						AssetUrl = avatar.assetUrl,
						ThumbnailUrl = avatar.thumbnailImageUrl,
						IsPublic = (avatar.releaseStatus == "public"),
						HasVRCA = false
					};
					AvatarSaver.savedAvatars.Add(savedAvatar2);
					AvatarSaver.SaveToFile();
					if (notify)
					{
						DeepConsole.Log("AvaSaver", "Added: " + avatar.name);
					}
					flag = true;
				}
			}
			catch (Exception ex)
			{
				DeepConsole.LogConsole("AvaSaver", string.Format("Save avatar error: {0}", ex));
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00014198 File Offset: 0x00012398
		public static void SaveToFile()
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(AvatarSaver.SAVE_PATH));
				File.WriteAllLines(AvatarSaver.SAVE_PATH, AvatarSaver.savedAvatars.Select((AvatarSaver.SavedAvatar a) => a.ToString()));
			}
			catch (Exception ex)
			{
				DeepConsole.LogConsole("AvaSaver", string.Format("Save to file error: {0}", ex));
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00014214 File Offset: 0x00012414
		public static void SwitchToAvatar(AvatarSaver.SavedAvatar avatar)
		{
			try
			{
				ApiAvatar apiAvatar = new ApiAvatar();
				apiAvatar.id = avatar.AvatarId;
				Action<ApiContainer> action = delegate(ApiContainer container)
				{
					try
					{
						ApiAvatar apiAvatar2 = container.Model as ApiAvatar;
						if (apiAvatar2 == null)
						{
							DeepConsole.LogConsole("AvaSaver", "Downloaded avatar is null.");
						}
						else
						{
							VRCPlayer localVRCPlayer = PlayerUtil.GetLocalVRCPlayer();
							VRCAvatarManager vrcavatarManager = ((localVRCPlayer != null) ? localVRCPlayer.field_Private_VRCAvatarManager_0 : null);
							if (vrcavatarManager == null)
							{
								DeepConsole.LogConsole("AvaSaver", "Avatar Manager is null.");
							}
							else
							{
								MethodInfo method = typeof(VRCAvatarManager).GetMethod("SetAvatar", BindingFlags.Instance | BindingFlags.Public);
								if (method == null)
								{
									DeepConsole.LogConsole("AvaSaver", "Unable to switch avatar. Method not found.");
								}
								else
								{
									method.Invoke(vrcavatarManager, new object[] { apiAvatar2 });
									VRCPlayerApi localPlayer = Networking.LocalPlayer;
									if (localPlayer != null && localPlayer.IsValid())
									{
										AvatarSaver.SetAvatarScale(localPlayer, 1f);
									}
									DeepConsole.Log("AvaSaver", "Changed to " + avatar.AvatarName);
								}
							}
						}
					}
					catch (Exception ex2)
					{
						DeepConsole.LogConsole("AvaSaver", "Failed to switch avatar: " + ex2.Message);
					}
				};
				apiAvatar.Get(action, null, null, false, 0);
			}
			catch (Exception ex)
			{
				DeepConsole.LogConsole("AvaSaver", "Failed to initiate avatar switch: " + ex.Message);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00014290 File Offset: 0x00012490
		public static void SetAvatarScale(VRCPlayerApi player, float scale)
		{
			try
			{
				GameObject gameObject = player.gameObject;
				if (gameObject != null)
				{
					gameObject.transform.localScale = new Vector3(scale, scale, scale);
					MelonLogger.Msg(string.Format("[AvatarSaver] Set avatar scale to {0}", scale));
				}
				else
				{
					MelonLogger.Warning("[AvatarSaver] Player GameObject is null.");
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error(string.Format("[AvatarSaver] SetAvatarScale error: {0}", ex));
			}
		}

		// Token: 0x040001FC RID: 508
		public static ReMenuPage menuPage;

		// Token: 0x040001FD RID: 509
		public static readonly string SAVE_PATH = Path.Combine(Environment.CurrentDirectory, "DeepClient", "SavedAvatars.txt");

		// Token: 0x040001FE RID: 510
		public static readonly string VRCA_PATH = Path.Combine(Environment.CurrentDirectory, "DeepClient", "SavedVRCAs");

		// Token: 0x040001FF RID: 511
		public static List<AvatarSaver.SavedAvatar> savedAvatars = new List<AvatarSaver.SavedAvatar>();

		// Token: 0x04000200 RID: 512
		public static bool autoSave;

		// Token: 0x04000201 RID: 513
		public static uint GMEM_MOVEABLE = 2U;

		// Token: 0x04000202 RID: 514
		public static uint CF_UNICODETEXT = 13U;

		// Token: 0x0200010E RID: 270
		public class SavedAvatar
		{
			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001CB61 File Offset: 0x0001AD61
			// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001CB69 File Offset: 0x0001AD69
			public string AvatarId { get; set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001CB72 File Offset: 0x0001AD72
			// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001CB7A File Offset: 0x0001AD7A
			public string AvatarName { get; set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001CB83 File Offset: 0x0001AD83
			// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001CB8B File Offset: 0x0001AD8B
			public string AuthorName { get; set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001CB94 File Offset: 0x0001AD94
			// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001CB9C File Offset: 0x0001AD9C
			public string SaveDate { get; set; }

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001CBA5 File Offset: 0x0001ADA5
			// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001CBAD File Offset: 0x0001ADAD
			public string AssetUrl { get; set; }

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001CBB6 File Offset: 0x0001ADB6
			// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001CBBE File Offset: 0x0001ADBE
			public string ThumbnailUrl { get; set; }

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001CBC7 File Offset: 0x0001ADC7
			// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001CBCF File Offset: 0x0001ADCF
			public bool IsPublic { get; set; }

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
			// (set) Token: 0x0600059F RID: 1439 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
			public bool HasVRCA { get; set; }

			// Token: 0x060005A0 RID: 1440 RVA: 0x0001CBEC File Offset: 0x0001ADEC
			public override string ToString()
			{
				return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[] { this.AvatarId, this.AvatarName, this.AuthorName, this.SaveDate, this.AssetUrl, this.ThumbnailUrl, this.IsPublic, this.HasVRCA });
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0001CC5C File Offset: 0x0001AE5C
			public static AvatarSaver.SavedAvatar FromString(string line)
			{
				string[] array = line.Split(new char[] { '|' });
				if (array.Length < 8)
				{
					throw new FormatException("Invalid saved avatar format.");
				}
				return new AvatarSaver.SavedAvatar
				{
					AvatarId = array[0],
					AvatarName = array[1],
					AuthorName = array[2],
					SaveDate = array[3],
					AssetUrl = array[4],
					ThumbnailUrl = array[5],
					IsPublic = bool.Parse(array[6]),
					HasVRCA = bool.Parse(array[7])
				};
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
			public void UpdateFromApiAvatar(ApiAvatar avatar)
			{
				this.AvatarName = avatar.name;
				this.AuthorName = avatar.authorName;
				this.SaveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				this.AssetUrl = avatar.assetUrl;
				this.ThumbnailUrl = avatar.thumbnailImageUrl;
				this.IsPublic = avatar.releaseStatus == "public";
			}
		}
	}
}
