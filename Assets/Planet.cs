using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp
{
	public class Planet: CelestialBody
	{
		public bool IsLit { get; set; }
		public event Action Lit;

		public override void OnLightHit(Lightbeam lightbeam, ContactPoint2D contactPoint2D)
		{
			if (!IsLit)
			{
				IsLit = true;
				Lit?.Invoke();
			}
		}
	}
}
