using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000087 RID: 135
	internal class TextButton
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00012259 File Offset: 0x00010459
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00012261 File Offset: 0x00010461
		private GameObject _ButtonGameobject { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001226A File Offset: 0x0001046A
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00012272 File Offset: 0x00010472
		private Button _ButtonComponent { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001227B File Offset: 0x0001047B
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00012283 File Offset: 0x00010483
		private GameObject _ButtonIcon { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001228C File Offset: 0x0001048C
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00012294 File Offset: 0x00010494
		private GameObject _Text { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001229D File Offset: 0x0001049D
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000122A5 File Offset: 0x000104A5
		private TextMeshProUGUI _TextComponent { get; set; }

		// Token: 0x0600030C RID: 780 RVA: 0x000122B0 File Offset: 0x000104B0
		~TextButton()
		{
			this._ButtonGameobject = null;
			this._ButtonComponent = null;
			this._ButtonIcon = null;
			this._Text = null;
			this._TextComponent = null;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000122FC File Offset: 0x000104FC
		public TextButton(Transform path, string text, string id, Player user)
		{
			this._ButtonGameobject = new GameObject("BTN_" + id);
			this._ButtonGameobject.transform.parent = path;
			this._ButtonGameobject.transform.localScale = new Vector3(8.7f, 0.7f, 1f);
			this._ButtonGameobject.transform.localPosition = Vector3.zero;
			this._ButtonGameobject.transform.localEulerAngles = Vector3.zero;
			this._ButtonIcon = Object.Instantiate<GameObject>(this._ButtonGameobject, this._ButtonGameobject.transform).gameObject;
			this._Text = Object.Instantiate<GameObject>(this._ButtonIcon, this._ButtonIcon.transform).gameObject;
			this._ButtonIcon.transform.localPosition = new Vector3(-1.6f, 0f, 0f);
			this._ButtonIcon.transform.localScale = new Vector3(0.93f, 0.7f, 1f);
			this._ButtonIcon.gameObject.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
			this._ButtonComponent = this._ButtonGameobject.AddComponent<Button>();
			this._ButtonGameobject.AddComponent<Button>();
			this._ButtonGameobject.AddComponent<LayoutElement>();
			this._TextComponent = this._Text.AddComponent<TextMeshProUGUI>();
			this._TextComponent.richText = true;
			this._TextComponent.enableWordWrapping = false;
			this._TextComponent.text = text;
			this._TextComponent.alignment = 513;
			this._TextComponent.transform.localScale = new Vector3(0.1f, 1.2f, 1f);
			this._TextComponent.fontSize = 49f;
			this._TextComponent.transform.localPosition = new Vector3(-40f, 0f, 0f);
		}
	}
}
