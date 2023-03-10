using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Audio
{
	[RequireComponent(typeof(AudioSource))]
	public class SFXPlayer : MonoBehaviour
	{
		private static SFXPlayer _instance;
		public static SFXPlayer Instance => _instance;

		[SerializeField]
		private AudioSource _audioSource;


		private void Awake()
		{
			_instance = this;
			_audioSource = GetComponent<AudioSource>();
		}


		public void Play(AudioClip clip, float volumeScale = 1f)
		{
			_audioSource.PlayOneShot(clip, volumeScale);
		}
		public void PlayAtLocation(AudioClip clip, Vector3 position)
		{
			var o = new GameObject("TempAudio");
			var audio = o.AddComponent<AudioSource>();
			audio.clip = clip;
			audio.spatialBlend = 1f;
			o.transform.position = position;
			audio.Play();
			Destroy(o, clip.length);
		}
	}
}