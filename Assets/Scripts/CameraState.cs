using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public static class CameraState
    {
        private static string _currentCameraState = "Game";
        public static string CurrentCameraState {
            get => _currentCameraState;
            set
            {
                _currentCameraState = value;
                CameraStateChanged?.Invoke(value);
            }
        }
        public static event Action<string> CameraStateChanged;
    }
}
