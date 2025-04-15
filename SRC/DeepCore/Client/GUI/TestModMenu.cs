using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCore.Client.GUI
{
	// Token: 0x0200008A RID: 138
	internal class TestModMenu
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00012C68 File Offset: 0x00010E68
		public void Start()
		{
			UIPage uipage = this.AddPage("Visuals");
			this.AddPage("Mouvements");
			this.AddPage("World Hacks");
			this.AddPage("Exploits");
			this.AddPage("Player Selector");
			this.AddPage("Settings");
			this.AddButton(uipage, "Example Button", delegate
			{
				Console.WriteLine("Button Clicked!");
			});
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00012CEC File Offset: 0x00010EEC
		public void OnGUI()
		{
			this._menuStyle = new GUIStyle(GUI.skin.box)
			{
				alignment = 4,
				normal = 
				{
					background = TestModMenu.GeneratePixelMap(new Color(0f, 0f, 0f, 0.6f))
				},
				stretchHeight = true,
				stretchWidth = true
			};
			this._textStyle = new GUIStyle
			{
				alignment = 4,
				normal = 
				{
					textColor = Color.white
				}
			};
			if (this._menuIsOpened)
			{
				this._rect = GUILayout.Window(1324231, this._rect, new Action<int>(this.DoMyWindow), "", this._menuStyle, new GUILayoutOption[0]);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00012DB4 File Offset: 0x00010FB4
		public void Update()
		{
			if (Input.GetKeyDown(this.toggleKeyBind))
			{
				this._menuIsOpened = !this._menuIsOpened;
				if (this.logMenuStateChanged)
				{
					Console.ForegroundColor = (this._menuIsOpened ? ConsoleColor.Green : ConsoleColor.Red);
					Console.WriteLine("Menu " + (this._menuIsOpened ? "Opened" : "Closed") + "!");
					Console.ResetColor();
				}
			}
			if (this._menuIsOpened)
			{
				if (Input.GetKeyDown(this.upKeyBind))
				{
					if (this.isMainMenu)
					{
						if (this.mmItemIndex == 1)
						{
							return;
						}
						this.mmItemIndex--;
					}
					else
					{
						if (this.currentPage.selectedItem == 1)
						{
							return;
						}
						this.currentPage.selectedItem--;
					}
				}
				if (Input.GetKeyDown(this.downKeyBind))
				{
					if (this.isMainMenu)
					{
						if (this.mmItemIndex == this.pages.Count)
						{
							return;
						}
						this.mmItemIndex++;
					}
					else
					{
						if (this.currentPage.selectedItem == this.currentPage.buttons.Count)
						{
							return;
						}
						this.currentPage.selectedItem++;
					}
				}
				if (Input.GetKeyDown(this.nextKeybind))
				{
					if (this.isMainMenu)
					{
						this.isMainMenu = false;
						this.currentPage = this.pages[this.mmItemIndex - 1];
					}
					else
					{
						this.currentPage.buttons[this.currentPage.selectedItem - 1].action();
					}
				}
				if (Input.GetKeyDown(this.previousKeybind))
				{
					if (this.currentPage.parentIsMainMenu)
					{
						this.isMainMenu = true;
						return;
					}
					this.currentPage = this.currentPage.parent;
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012F7C File Offset: 0x0001117C
		private void DoMyWindow(int windowID)
		{
			if (this.isMainMenu)
			{
				GUILayout.Space(150f);
				GUILayout.Label(this.menuTitleText, this._textStyle, new GUILayoutOption[0]);
				GUILayout.Label(this.subTitleText, this._textStyle, new GUILayoutOption[0]);
				GUILayout.Space(45f);
				using (List<UIPage>.Enumerator enumerator = this.pages.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						UIPage uipage = enumerator.Current;
						GUILayout.Label("<size=30><b>" + ((this.mmItemIndex == uipage.pageID) ? string.Concat(new string[] { "<color=", this.highlightedOptionHex, ">", uipage.label, "</color>" }) : uipage.label) + "</b></size>", this._textStyle, new GUILayoutOption[0]);
						GUILayout.Space(10f);
					}
					return;
				}
			}
			GUILayout.Space(150f);
			GUILayout.Label("<size=35><b><color=red>" + this.currentPage.label + "</color></b></size>", this._textStyle, new GUILayoutOption[0]);
			GUILayout.Space(45f);
			foreach (UIButton uibutton in this.currentPage.buttons)
			{
				GUILayout.Label("<size=30><b>" + ((this.currentPage.selectedItem == uibutton.buttonID) ? string.Concat(new string[] { "<color=", this.highlightedOptionHex, ">", uibutton.label, "</color>" }) : uibutton.label) + "</b></size>", this._textStyle, new GUILayoutOption[0]);
				GUILayout.Space(10f);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000131AC File Offset: 0x000113AC
		public static Texture2D GeneratePixelMap(Color color)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixels32(new Color32[] { color });
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000131E8 File Offset: 0x000113E8
		public UIPage AddPage(string label)
		{
			UIPage uipage = new UIPage
			{
				label = label,
				buttons = new List<UIButton>(),
				pageID = this.pages.Count + 1,
				selectedItem = 1,
				parentIsMainMenu = true,
				parent = null
			};
			this.pages.Add(uipage);
			return uipage;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00013244 File Offset: 0x00011444
		public UIButton AddButton(UIPage page, string label, Action action)
		{
			UIButton uibutton = new UIButton
			{
				action = action,
				label = label,
				buttonID = page.buttons.Count + 1
			};
			page.buttons.Add(uibutton);
			return uibutton;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013288 File Offset: 0x00011488
		public UIPage AddPage(UIPage page, string label)
		{
			UIPage tmp = new UIPage
			{
				buttons = new List<UIButton>(),
				label = label,
				parent = page,
				parentIsMainMenu = false,
				selectedItem = 1,
				pageID = -1
			};
			this.AddButton(page, label, delegate
			{
				this.currentPage = tmp;
			});
			return tmp;
		}

		// Token: 0x040001C2 RID: 450
		public KeyCode toggleKeyBind = 277;

		// Token: 0x040001C3 RID: 451
		public KeyCode previousKeybind = 276;

		// Token: 0x040001C4 RID: 452
		public KeyCode nextKeybind = 275;

		// Token: 0x040001C5 RID: 453
		public KeyCode upKeyBind = 273;

		// Token: 0x040001C6 RID: 454
		public KeyCode downKeyBind = 274;

		// Token: 0x040001C7 RID: 455
		public bool logMenuStateChanged = true;

		// Token: 0x040001C8 RID: 456
		public string menuTitleText = "<size=35><b><color=red>DeepClient</color></b></size>";

		// Token: 0x040001C9 RID: 457
		public string subTitleText = "<size=25><b><color=#7d0000>by LXQtie</color></b></size>";

		// Token: 0x040001CA RID: 458
		public string highlightedOptionHex = "#7d0000";

		// Token: 0x040001CB RID: 459
		private Rect _rect = new Rect(0f, 0f, 500f, (float)Screen.height);

		// Token: 0x040001CC RID: 460
		private bool _menuIsOpened;

		// Token: 0x040001CD RID: 461
		private GUIStyle _menuStyle;

		// Token: 0x040001CE RID: 462
		private GUIStyle _textStyle;

		// Token: 0x040001CF RID: 463
		public List<UIPage> pages = new List<UIPage>();

		// Token: 0x040001D0 RID: 464
		private bool isMainMenu = true;

		// Token: 0x040001D1 RID: 465
		private UIPage currentPage;

		// Token: 0x040001D2 RID: 466
		private int mmItemIndex = 1;
	}
}
