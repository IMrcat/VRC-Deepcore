using System;
using System.Collections;
using MelonLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DeepCore.Client.Coroutine
{
	// Token: 0x02000092 RID: 146
	internal class LoadingVideo
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00013564 File Offset: 0x00011764
		public static IEnumerator LoadVideo()
		{
			Image component = GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").transform.GetComponent<Image>();
			Image image = Object.Instantiate<Image>(component, component.transform.parent);
			image.gameObject.name = "Video";
			Transform transform = GameObject.Find("MenuContent/Popups/LoadingPopup/Video/Text").transform;
			transform.GetComponent<TextMeshProUGUI>().text = "";
			Object.DestroyImmediate(transform);
			image.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, 0f);
			image.transform.localPosition = new Vector3(-1f, 220.4001f, 500f);
			image.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 400f);
			Image image2 = Object.Instantiate<Image>(image, image.transform);
			image2.name = "Backround";
			image2.transform.localPosition = new Vector3(0f, 0f, 0f);
			image2.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/Video/Backround").SetActive(false);
			GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.position = new Vector3(-0.0061f, 2.0535f, 1.5441f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.localPosition = new Vector3(-0.9868f, 314.19f, 500.0472f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.localScale = new Vector3(14.5228f, 2.825f, 2f);
			if (LoadingVideo.isvideo)
			{
				GameObject gameObject = GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.gameObject;
				Object.Destroy(gameObject.GetComponent<Button>());
				Object.Destroy(gameObject.transform.Find("Backround").gameObject.GetComponent<Button>());
				gameObject.GetComponent<Image>().sprite = null;
				gameObject.AddComponent<VideoPlayer>();
				VideoPlayer vidcomp = gameObject.GetComponent<VideoPlayer>();
				vidcomp.isLooping = true;
				LoadingVideo.rendert = new RenderTexture(512, 512, 16);
				LoadingVideo.rendert.Create();
				Material material = new Material(Shader.Find("Standard"));
				material.color = default(Color);
				material.EnableKeyword("_EMISSION");
				material.SetColor("_EmissionColor", Color.white);
				material.SetTexture("_EmissionMap", LoadingVideo.rendert);
				gameObject.GetComponent<Image>().material = material;
				vidcomp.targetTexture = LoadingVideo.rendert;
				vidcomp.url = MelonUtils.GameDirectory + "\\DeepClient\\LoadingVid.mp4";
				while (GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>() == null)
				{
					yield return null;
				}
				GameObject.Find("MenuContent/Popups/LoadingPopup/Video").AddComponent<AudioSource>();
				AudioSource component2 = GameObject.Find("MenuContent/Popups/LoadingPopup/Video").GetComponent<AudioSource>();
				component2.clip = null;
				vidcomp.audioOutputMode = 1;
				vidcomp.EnableAudioTrack(0, true);
				vidcomp.SetTargetAudioSource(0, component2);
				vidcomp.Stop();
				component2.Stop();
				vidcomp.Play();
				component2.Play();
				GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound").SetActive(false);
				vidcomp = null;
				vidcomp = null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x040001DE RID: 478
		public static bool Videoplayer = true;

		// Token: 0x040001DF RID: 479
		public static bool BigVideoLoadingscreen = false;

		// Token: 0x040001E0 RID: 480
		public static bool isvideo = true;

		// Token: 0x040001E1 RID: 481
		public static RenderTexture rendert;

		// Token: 0x040001E2 RID: 482
		public static AudioSource getauds;

		// Token: 0x040001E3 RID: 483
		public static float bpm = 105f;

		// Token: 0x040001E4 RID: 484
		public static float timePerBeat;

		// Token: 0x040001E5 RID: 485
		public static float nextBeatTime;

		// Token: 0x040001E6 RID: 486
		public static AudioSource audio;

		// Token: 0x040001E7 RID: 487
		public static float threshold = 0.04f;

		// Token: 0x040001E8 RID: 488
		private Vector3 initialScale;

		// Token: 0x040001E9 RID: 489
		public static float maxIntensity = 10f;

		// Token: 0x040001EA RID: 490
		public static float minIntensity = 0.1f;

		// Token: 0x040001EB RID: 491
		public static float intensityMultiplier = 0.3f;

		// Token: 0x040001EC RID: 492
		public static float[] _spectrum;

		// Token: 0x040001ED RID: 493
		public static float _lastBeatTime;

		// Token: 0x040001EE RID: 494
		public static bool _isBeat;

		// Token: 0x040001EF RID: 495
		public static float comp1;

		// Token: 0x040001F0 RID: 496
		public static float comp2;
	}
}
