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
        }

        private void OnPlaceSteroidPerformed(InputAction.CallbackContext obj)
        {
            _player.SpawnSteroid(_player.GameInput.Gameplay.PointerPosition.ReadValue<Vector2>());
        }

    }
}
