using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public class FollowTargetAssigner : MonoBehaviour
    {
        [SerializeField]
        private LightbeamController _lightbeamController;

        private CinemachineTargetGroup _group;

        private void Awake()
        {
            _group = GetComponent<CinemachineTargetGroup>();
        }

        private void OnEnable()
        {
            _lightbeamController.LightbeamSpawned += OnLightbeamSpawned;
        }
        private void OnDisable()
        {
            _lightbeamController.LightbeamSpawned -= OnLightbeamSpawned;
        }

        private void OnLightbeamSpawned(Lightbeam lightbeam)
        {
            _group.AddMember(lightbeam.transform, 1f, 1f);
        }

        private void Update()
        {
        }
    }
}