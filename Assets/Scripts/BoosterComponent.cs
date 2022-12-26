using BeamUp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
	public class BoosterComponent : MonoBehaviour
	{
		[SerializeField]
		private Booster _booster;

		public Booster Booster
		{
			get => _booster;
			set
			{
				_booster = value;
				UpdateVisuals();
			}
		}


		[SerializeField]
		private SpriteRenderer _icon;

		private void Start()
		{
			UpdateVisuals();
		}

		private void OnEnable()
		{
			DynamicallySpawnedGameObjectContainer.Add(gameObject);
		}
		private void OnDisable()
		{
			DynamicallySpawnedGameObjectContainer.Remove(gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent<Lightbeam>(out var lightbeam))
			{
				OnBoosterAdded(lightbeam);
				Destroy(gameObject);
			}
		}

		private void OnBoosterAdded(Lightbeam lightbeam)
		{
			var booster = Instantiate(_booster);
			booster.Apply(lightbeam);
		}
		private void UpdateVisuals()
		{
			_icon.sprite = _booster.Icon;
		}
	}
}
