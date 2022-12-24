using BeamUp.FSM;
using BeamUp.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BeamUp.PlayerStates
{
    public class GameState : State
    {
        private Player _player;
        private Vector2 _pointerPosition;

        public GameState(Player player) {
            this._player = player;
        }
        public override void Enter()
        {
            _player.GameInput.Gameplay.PlaceSteroid.performed += OnPlaceSteroidPerformed;
        }

        public override void Exit()
        {
            _player.GameInput.Gameplay.PlaceSteroid.performed -= OnPlaceSteroidPerformed;
        }

        public override void Update(float deltaTime)
        {
            _pointerPosition = _player.GameInput.Gameplay.PointerPosition.ReadValue<Vector2>();
        }

        private void OnPlaceSteroidPerformed(InputAction.CallbackContext obj)
        {
            var cont = _player.LightbeamController;
            if (cont.Instances.Count > 0){
                var cam = Camera.main;
                var pointerWP = cam.ScreenToWorldPoint(new Vector3(_pointerPosition.x, _pointerPosition.y, -cam.transform.position.z));
                var closest = _player.LightbeamController.Instances.OrderBy(x => Vector3.Distance(pointerWP, x.transform.position)).First();
                closest.Reflect((closest.transform.position - pointerWP).normalized);
            }
        }

    }
}
