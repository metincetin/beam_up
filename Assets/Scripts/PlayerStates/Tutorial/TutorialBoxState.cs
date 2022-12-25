using BeamUp.FSM;
using BeamUp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp.PlayerStates.Tutorial
{
	public class TutorialBoxState : State
	{
		public UIController _UIController;
		public TutorialBoxState(UIController UIController)
		{
			_UIController = UIController;
		}

		public override void Enter()
		{
			Time.timeScale = 0;
		}

		public override void Exit()
		{
			Time.timeScale = 0;
		}

		public override void Update(float deltaTime)
		{
		}
	}
}
