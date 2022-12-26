using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeamUp.UI.Pages
{
    public class MenuPage : Page
    {

        [SerializeField] 
        private Button _playButton;
        [SerializeField] 
        private Button _creditsButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private CreditsPage _creditsPage;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private DescriptionPage _descriptionPage;

        protected override void OnOpened()
        {
            _playButton.onClick.AddListener(OnPlayPressed);
			_creditsButton.onClick.AddListener(OnCreditsPressed);
            _quitButton.onClick.AddListener(OnQuitPressed);

		}

		protected override void OnClosed()
        {
			_playButton.onClick.RemoveListener(OnPlayPressed);
			_creditsButton.onClick.RemoveListener(OnCreditsPressed);
			_quitButton.onClick.RemoveListener(OnQuitPressed);

		}

		private void OnCreditsPressed()
        {
            _creditsPage.Open();
            Close();
        }

        private void OnPlayPressed()
        {
            _descriptionPage.Open();
            Close();
        }

        private void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}