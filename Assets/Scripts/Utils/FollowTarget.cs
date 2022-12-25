using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField]
        private LightbeamController _lightbeamController;

        private Transform _target;

        private void Update()
        {
            var furthest = _lightbeamController.FurthestLightbeam;
            if (furthest)
            {
                _target = furthest.transform;
            }
        }

        private void FixedUpdate()
        {
            if (_target)
            {
                transform.position = _target.position;
            }
        }
    }
}