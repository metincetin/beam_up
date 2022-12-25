using BeamUp.PlayerStates.Tutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;

        [SerializeField]
        private Player _player;
        
        private void OnEnable()
        {
            _enemySpawner.EnemySpawned += OnEnemySpawned;
        }

		private void OnDisable()
		{
			_enemySpawner.EnemySpawned -= OnEnemySpawned;
		}

        private void OnEnemySpawned(Enemy enemy)
        {
            var state = _player.StateMachine.GetState<TutorialBoxState>();
            _player.StateMachine.CurrentState = state;
        }
    }
}