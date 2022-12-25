using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Audio
{
    [RequireComponent(typeof(AudioSource))]
	public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer _instance;
        public static MusicPlayer Instance => _instance;

        [SerializeField]
        private AudioSource _audioSource;


        private void Awake()
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
        }


        public void Play()
        {

        }
        public void Stop()
        {

        }

        public void Transition(AudioClip target)
        {
            _audioSource.DOFade(0, .2f).OnComplete(() =>
            {
                _audioSource.clip = target;
            });
        }

		public void FadeOut()
        {
            _audioSource.DOFade(0, .2f);
        }
    }
}