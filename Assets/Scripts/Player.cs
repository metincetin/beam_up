using Assets.Scripts.PlayerStates;
using Assets.Scripts.Utils;
using BeamUp.FSM;
using BeamUp.Input;
using BeamUp.PlayerStates;
using BeamUp.Tutorial;
using BeamUp.UI;
using BeamUp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BeamUp
{
	public class Player : MonoBehaviour
	{
		public GameInput GameInput;


		private StateMachine _stateMachine = new StateMachine();
		public StateMachine StateMachine => _stateMachine;

		[SerializeField]
		private LineRenderer _shootLineRenderer;
		public LineRenderer ShootLineRenderer => _shootLineRenderer;

		[SerializeField]
		private LightbeamController _lightbeamController;
		public LightbeamController LightbeamController => _lightbeamController;

		[SerializeField]
		private UIController _UIController;
		public UIController UIController => _UIController;

		[SerializeField]
		private FollowTarget _followTarget;
		public FollowTarget FollowTarget => _followTarget;

		[SerializeField]
		private EnemySpawner _enemySpawner;
		public EnemySpawner EnemySpawner => _enemySpawner;
		
		[SerializeField]
		private BoosterSpawner _boosterSpawner;
		public BoosterSpawner BoosterSpawner => _boosterSpawner;

		[SerializeField]
		private UnityEngine.Playables.PlayableDirector _playableDirector;
		public UnityEngine.Playables.PlayableDirector PlayableDirector => _playableDirector;

		[SerializeField]
		private TutorialController _tutorialController;
		public TutorialController TutorialController => _tutorialController;

		[SerializeField]
		private ScoreCounter _scoreCounter;

		[SerializeField]
		private WorldGenerator _worldGenerator;


		private void Awake()
		{
			GameInput = new GameInput();

			_shootLineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
			_shootLineRenderer.enabled = false;

			_stateMachine.AddState(new MenuState());
			_stateMachine.AddState(new ShootState(this));
			_stateMachine.AddState(new GameState(this));
			_stateMachine.AddState(new FailState(this));
			_stateMachine.AddState(new EndState(this));

		}

		private void OnEnable()
		{
			GameInput.Enable();
			_stateMachine.GetState<ShootState>().Shot += OnMenuShot;
			_lightbeamController.LightbeamKilled += OnLightbeamKilled;
		}

		private void OnDisable()
		{
			GameInput.Disable();
			_stateMachine.GetState<ShootState>().Shot -= OnMenuShot;
			_lightbeamController.LightbeamKilled -= OnLightbeamKilled;
		}

		private void OnLightbeamKilled(Lightbeam inst, GameObject killer)
		{
			if (killer.TryGetComponent<GiantHole>(out var _))
			{
				StartEndGame();
				return;
			}
			if (_lightbeamController.Instances.Count == 0)
			{
				var state = _stateMachine.GetState<FailState>();
				if (killer.TryGetComponent<IKiller>(out var castedKiller))
				{
					state.FailMessage = castedKiller.DeathMessage;
				}
				_stateMachine.SetState<FailState>();
			}
		}

		private void OnMenuShot()
		{
			_stateMachine.SetState<GameState>();
		}

		private void Update()
		{
			_stateMachine.Update(Time.deltaTime);
		}

		public void Retry()
		{
			_scoreCounter.Reset();
			_worldGenerator.Reset();
			DynamicallySpawnedGameObjectContainer.DestroyAll();
			_stateMachine.SetState<ShootState>();
		}

		public void StartEndGame()
		{
			StateMachine.SetState<EndState>();
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(0);
		}
	}
}