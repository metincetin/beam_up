using Assets.Scripts.PlayerStates;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeamUp.UI.Pages
{
    public class DescriptionPage : Page
    {

        [SerializeField]
        private Button _nextButton;

        [SerializeField]
        private Player _player;
        
        protected override void OnClosed()
        {
        }

        protected override void OnOpened()
        {
            GetComponent<CanvasGroup>().DOFade(1,1f).From(0);
            _nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            _player.StateMachine.SetState<ShootState>();
            Close();
        }
    }
}