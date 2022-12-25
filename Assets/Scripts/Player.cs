using Assets.Scripts.PlayerStates;
using BeamUp.FSM;
using BeamUp.Input;
using BeamUp.PlayerStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


		private void Awake()
        {
            GameInput = new GameInput();

            _shootLineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            _shootLineRenderer.enabled = false;

            _stateMachine.AddState(new MenuState(this));
            _stateMachine.AddState(new GameState(this));


        }

        private void OnEnable()
        {
            GameInput.Enable();
            _stateMachine.GetState<MenuState>().Shot += OnMenuShot;
        }

        private void OnDisable()
        {
            GameInput.Disable();
            _stateMachine.GetState<MenuState>().Shot -= OnMenuShot;
        }

        private void OnMenuShot()
        {
            _stateMachine.SetState<GameState>();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }
    }
}