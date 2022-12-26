using BeamUp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace BeamUp
{
	public class CelestialBody : MonoBehaviour
	{
		public virtual void OnLightHit(Lightbeam lightbeam, ContactPoint2D contactPoint2D)
		{
		}

		protected virtual void OnEnable()
		{
			DynamicallySpawnedGameObjectContainer.Add(gameObject);
		}
		protected virtual void OnDisable()
		{
			DynamicallySpawnedGameObjectContainer.Remove(gameObject);
		}
	}
}
