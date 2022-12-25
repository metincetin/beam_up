using System;
using System.Collections;
using UnityEngine;

namespace BeamUp
{
	public abstract class Booster : ScriptableObject
	{
		public float Duration;
		public Sprite Icon;

		public void Apply(Lightbeam lightbeam)
		{
			if (Duration >= 0)
			{
				lightbeam.StartCoroutine(RevertTimer(lightbeam));
			}
			OnApplied(lightbeam);
		}


		private IEnumerator RevertTimer(Lightbeam lightbeam)
		{
			yield return new WaitForSeconds(Duration);
			Revert(lightbeam);
		}

		public void Revert(Lightbeam lightbeam)
		{
			OnReverted(lightbeam);
		}

		protected abstract void OnApplied(Lightbeam lightBeam);
		protected abstract void OnReverted(Lightbeam lightBeam);
	}
}
