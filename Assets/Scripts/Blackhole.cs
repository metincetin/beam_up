using Assets.Scripts.Utils;
using BeamUp;
using BeamUp.Tutorial;
using BeamUp.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
	public class Blackhole : CelestialBody, IKiller, IRenderingStateListener
	{

		[SerializeField]
		private float _pullPower;

		[SerializeField]
		private float _pullDistance = 10;

		[SerializeField]
		private float _pullDistancePow = 2;

		public string DeathMessage => "You are sucked up by a black hole";

		[SerializeField]
		private BlackholeTriggerListener _triggerListener;

		private void FixedUpdate()
		{
			foreach (var beam in _triggerListener.PulledBeams)
			{
				float distNorm = 1 - Mathf.Pow(Mathf.Min(Vector3.Distance(transform.position, beam.transform.position), _pullDistance) / _pullDistance, _pullDistancePow);
				float power = distNorm * _pullPower;
				var dir = (transform.position - beam.transform.position).normalized;
				var newVelocity = Vector3.SlerpUnclamped(beam.Rigidbody.velocity, dir, 8 * Time.deltaTime * power);
				beam.SetDirection(newVelocity);
			}
		}

		public void OnBeginRendering()
		{
			if (!TutorialState.BlackholeIntroduction)
			{
				FindObjectOfType<TutorialController>().ShowTutorial("Blackhole", gameObject);
				TutorialState.BlackholeIntroduction = true;
			}
		}

		public void OnEndRendering()
		{
		}
	}
}