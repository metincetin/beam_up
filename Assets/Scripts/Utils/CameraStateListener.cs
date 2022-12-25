using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public class CameraStateListener : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SetState(CameraState.CurrentCameraState);
        }

        private void OnEnable()
        {
            CameraState.CameraStateChanged += OnCameraStateChanged;
        }
        private void OnDisable()
        {
            CameraState.CameraStateChanged -= OnCameraStateChanged;
        }

        private void OnCameraStateChanged(string value)
        {
            SetState(value);
        }

        private void SetState(string value)
        {
            _animator.Play(value);
        }
    }
}