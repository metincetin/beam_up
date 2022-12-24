using UnityEngine;
using Cinemachine;

namespace BeamUp.Utils
{
    //https://forum.unity.com/threads/set-limits-for-the-position-of-the-cinemachine-camera-that-follows-the-player.1070030/

    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's X co-ordinate
    /// </summary>
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class CinemachineLockX : CinemachineExtension
    {
        [SerializeField]
        private Vector2 _xRange;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Finalize)
            {
                var pos = state.RawPosition;
                pos.x = Mathf.Clamp(pos.x, _xRange.x, _xRange.y);
                state.RawPosition = pos;
            }
        }
    }
}
