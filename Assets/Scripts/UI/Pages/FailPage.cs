using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeamUp.UI.Pages
{
	public class FailPage : Page
	{
		public string FailMessage { get; set; }
		
		[SerializeField]
		private Button _retryButton;

		[SerializeField]
		private TMP_Text _failText;

		[SerializeField]
		private Player _player;

		protected override void OnOpened()
		{
			_failText.text = FailMessage;
			_retryButton.onClick.AddListener(OnRetryButtonClicked);
		}

		protected override void OnClosed()
		{
			_retryButton.onClick.RemoveListener(OnRetryButtonClicked);
		}

		private void OnRetryButtonClicked()
		{
			_player.Retry();
		}
	}
}
