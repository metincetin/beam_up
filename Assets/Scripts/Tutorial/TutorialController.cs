using BeamUp.PlayerStates.Tutorial;
using Cinemachine;
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
		private Transform _tutorialFollowTarget;
		[SerializeField]
		private CinemachineVirtualCamera _tutorialCamera;

		[SerializeField]
		private Player _player;

		private string _previousCameraState;
		private bool _cameraChanged;

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
		}


		public void ShowTutorial(string tutorialName, GameObject cameraTarget = null)
		{
			if (cameraTarget != null)
			{
				_previousCameraState = CameraState.CurrentCameraState;
				CameraState.CurrentCameraState = "Tutorial";
				_tutorialFollowTarget.transform.position = cameraTarget.transform.position;
				_cameraChanged = true;
			}
			_player.UIController.GetTutorial(tutorialName).SetActive(true);
			Time.timeScale = 0;
		}

		public void EndTutorial(string tutorialName)
		{
			if (_cameraChanged)
			{
				CameraState.CurrentCameraState = _previousCameraState;
				_cameraChanged = false;
			}
			_player.UIController.GetTutorial(tutorialName).SetActive(false);
			Time.timeScale = 1;
		}
	}
}