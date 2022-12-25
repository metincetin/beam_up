using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
	public class Asteroid : CelestialBody
	{
		[SerializeField]
		private Vector2 _speedRange;

		private float _speed;

		private Rigidbody2D _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_speed = Random.Range(_speedRange.x, _speedRange.y);	
		}

		private void Start()
		{
			_rigidbody.angularVelocity = Random.value * 170;
		}

		public void SetDirection(Vector3 direction)
		{
			_rigidbody.velocity = direction * _speed;
		}
	}
}
