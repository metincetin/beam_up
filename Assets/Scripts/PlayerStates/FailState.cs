using BeamUp.FSM;
using BeamUp.UI;
using BeamUp.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamUp.PlayerStates
{
	public class FailState : State
	{
		private Player _player;

		public string FailMessage { get; set; } = "You Failed!";
		
		public FailState(Player player)
		{
			_player = player;
		}
		public override void Enter()
		{
			var failPage = _player.UIController.GetPage<FailPage>();
			failPage.FailMessage = FailMessage;
			failPage.Open();
		}

		public override void Exit()
		{
			_player.UIController.GetPage<FailPage>().Close();
		}

		public override void Update(float deltaTime)
		{
		}
	}
}
