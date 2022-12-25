using BeamUp;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUP.FX
{
	public class PlanetFX : MonoBehaviour
	{
		private Planet _planet;

		[SerializeField]
		private Transform _graphicsTransform;

		[SerializeField]
		private AnimationCurve _scaleAnimationCurve;

		[SerializeField]
		private Transform _atmosphereGraphics;

		[SerializeField]
		private ParticleSystem _litParticleSystem;

		[SerializeField]
		private float _orbitalSpeed = 3;

		private void Awake()
		{
			_planet = GetComponent<Planet>();
		}

		private void Start()
		{
			_atmosphereGraphics.localScale = Vector3.zero;
		}

		private void OnEnable()
		{
			_planet.Lit += OnPlanetLit;
		}

		private void OnDisable()
		{
			_planet.Lit -= OnPlanetLit;
		}

		private void OnPlanetLit()
		{
			_graphicsTransform.DOScale(1f, .4f)
				.SetEase(_scaleAnimationCurve)
				.From(0.7f);
			if (_atmosphereGraphics)
			{
				_atmosphereGraphics.DOScale(1f, .4f)
					.SetEase(_scaleAnimationCurve)
					.From(0.7f)
					.SetDelay(.8f);
			}
			if (_litParticleSystem)
			{
				_litParticleSystem.Play();
			}
		}

		private void Update()
		{
			_graphicsTransform.Rotate(Vector3.up, _orbitalSpeed * Time.deltaTime, Space.World);
		}
	}
}