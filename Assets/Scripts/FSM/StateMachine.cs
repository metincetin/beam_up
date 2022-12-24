using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp.FSM
{
    public class StateMachine
    {
        private List<State> _states = new List<State>();

        private State _currentState;
        public State CurrentState
        {
            get => _currentState;
            set
            {
                if (value == null)
                {
                    Debug.LogWarning("Can't enter to a null state");
                    return;
                }
                if (_currentState == value)
                {
                    Debug.LogWarningFormat("Attempting to enter already active state {0}", _currentState.ToString());
                    return;
                }

                if (_currentState != null)
                {
                    _currentState.Exit();
                }

                _currentState = value;
                _currentState.Enter();
            }
        }

        public void AddState(State state)
        {
            if (_currentState == null)
            {
                CurrentState = state;
            }

            _states.Add(state);
        }
        public void RemoveState(State state)
        {
            if (CurrentState == state)
            {
                Debug.LogWarningFormat("Can't remove current state {0}", state.ToString());
                return;
            }
            _states.Remove(state);
        }
        public void SetState<T>() where T: State
        {
            foreach(var s in _states)
            {
                if (s is T)
                {
                    CurrentState = s;
                    return;
                }
            }
        }
        public T GetState<T>() where T: State
        {
            foreach(var s in _states)
            {
                if (s is T)
                {
                    return (T)s;
                }
            }
            return null;
        }

        public void Update(float deltaTime)
        {
            CurrentState?.Update(deltaTime);
        }
    }
}
