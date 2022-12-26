using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BeamUp.UI.Pages
{
    public class CreditsPage : Page, IPointerClickHandler
    {
        [SerializeField]
        private MenuPage _menuPage;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Close();
            _menuPage.Open();
        }

        protected override void OnClosed()
        {
        }

        protected override void OnOpened()
        {
        }
    }
}