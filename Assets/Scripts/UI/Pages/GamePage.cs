using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeamUp.UI.Pages
{
    public class GamePage : Page
    {
        [SerializeField]
        private TMP_Text _distanceText;
		[SerializeField]
        private TMP_Text _planetsLitText;
        
        [SerializeField]
        private string _distanceTextFormat;
		[SerializeField]
        private string _planetsLitTextFormat;

        [SerializeField]
        private ScoreCounter _scoreCounter;

        [SerializeField]
        private float _lightYearMultiplier = .4f;

        private void Update()
        {
            _distanceText.text = string.Format(_distanceTextFormat, ((int)_scoreCounter.DistanceTraveled) * _lightYearMultiplier).ToString();
			_planetsLitText.text = string.Format(_planetsLitTextFormat, _scoreCounter.PlanetsLit).ToString();
		}

        protected override void OnClosed()
        {
        }

        protected override void OnOpened()
        {
        }
    }
}