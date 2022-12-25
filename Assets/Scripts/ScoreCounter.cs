using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public class ScoreCounter : MonoBehaviour
    {
        public float DistanceTraveled { get; private set; }
        public int PlanetsLit { get; private set; }

        [SerializeField]
        private LightbeamController _lightBeamController;

        private void OnEnable()
        {
            _lightBeamController.LightbeamHit += OnLightbeamHit;
        }

        private void OnDisable()
        {
			_lightBeamController.LightbeamHit -= OnLightbeamHit;
		}


		private void OnLightbeamHit(Lightbeam light, CelestialBody body)
		{
            if (body is Planet planet && !planet.IsLit)
            {
                PlanetsLit++;
            }
		}

		private void Update()
        {
            var lb = _lightBeamController.FurthestLightbeam;
            if (lb)
            {
                DistanceTraveled = Mathf.Max(lb.transform.position.y, DistanceTraveled);
            }
        }
    }
}