using BeamUp.FSM;
using BeamUp.Input;
using BeamUp.UI.Pages;
using System;
using System.Collections;
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

        private Coroutine _reflectTutorialCoroutine;
        private Coroutine _enemySpawnerEnablerCoroutine;

        public GameState(Player player) {
            this._player = player;
        }
        public override void Enter()
        {
            _player.GameInput.Gameplay.ReflectLightbeam.performed += OnBouncePerformed;
            _player.BoosterSpawner.AllowSpawn = true;

            _player.UIController.GetPage<GamePage>().Open();

            if (!TutorialState.ReflectIntroduction)
            {
                _reflectTutorialCoroutine = _player.StartCoroutine(ShowReflectTutorialDelayed());
                TutorialState.ReflectIntroduction = true;
            }

            _enemySpawnerEnablerCoroutine = _player.StartCoroutine(EnableEnemySpawnDelayed());
        }

        private IEnumerator EnableEnemySpawnDelayed()
        {
            yield return new WaitForSeconds(4);
			_player.EnemySpawner.AllowSpawn = true;
		}

        public override void Exit()
        {
            _player.GameInput.Gameplay.ReflectLightbeam.performed -= OnBouncePerformed;
			_player.EnemySpawner.AllowSpawn = false;
			_player.BoosterSpawner.AllowSpawn = false;
		    if (_reflectTutorialCoroutine != null)
            {
                _player.StopCoroutine(_reflectTutorialCoroutine);
            }
            if (_enemySpawnerEnablerCoroutine != null)
            {
                _player.StopCoroutine(_enemySpawnerEnablerCoroutine);
            }
			_player.UIController.GetPage<GamePage>().Close();

		}

		private IEnumerator ShowReflectTutorialDelayed()
        {
            yield return new WaitForSeconds(4);
            _player.TutorialController.ShowTutorial("Reflect");
        }

        public override void Update(float deltaTime)
        {
            _pointerPosition = _player.GameInput.Gameplay.PointerPosition.ReadValue<Vector2>();
        }

        private void OnBouncePerformed(InputAction.CallbackContext obj)
        {
            var cont = _player.LightbeamController;
            if (cont.Instances.Count > 0) {
                foreach (var inst in cont.Instances){
                    var cam = Camera.main;
                    var pointerWP = cam.ScreenToWorldPoint(new Vector3(_pointerPosition.x, _pointerPosition.y, -cam.transform.position.z));
                    //var closest = _player.LightbeamController.Instances.OrderBy(x => Vector3.Distance(pointerWP, x.transform.position)).First();
                    inst.Reflect((inst.transform.position - pointerWP).normalized);
                }
            }
        }

    }
}
