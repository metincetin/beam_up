using BeamUp.Audio;
using BeamUp.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamUp.PlayerStates
{
	public class EndState : State
	{
		private Player _player;
		public EndState(Player player)
		{
			this._player = player;
		}

		public override void Enter()
		{
			_player.PlayableDirector.Play();
			MusicPlayer.Instance.FadeOut();
		}

		public override void Exit()
		{
			_player.PlayableDirector.Stop();
		}

		public override void Update(float deltaTime)
		{
		}
	}
}
