using BeamUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blackhole
{
	public class Blackhole : CelestialBody
	{
		private List<Lightbeam> _pulledBeams = new List<Lightbeam>();

		[SerializeField]
		private float _pullPower;

		[SerializeField]
		private float _pullDistance = 10;

		[SerializeField]
		private float _pullDistancePow = 2;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var beam = collision.GetComponentInParent<Lightbeam>();
			if (beam)
			{
				_pulledBeams.Add(beam);
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			var beam = collision.GetComponentInParent<Lightbeam>();
			if (beam)
			{
				_pulledBeams.Remove(beam);
			}
		}

		private void FixedUpdate()
		{
			foreach (var beam in _pulledBeams)
			{
				float distNorm = 1 - Mathf.Pow(Mathf.Min(Vector3.Distance(transform.position, beam.transform.position), _pullDistance) / _pullDistance, _pullDistancePow);
				float power = distNorm * _pullPower;
				var dir = (transform.position - beam.transform.position).normalized;
				var newVelocity = Vector3.SlerpUnclamped(beam.Rigidbody.velocity, dir, 8 * Time.deltaTime * power);
				beam.SetDirection(newVelocity);
			}
		}
	}
}