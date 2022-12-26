using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BeamUp.UI
{
    public class LinkButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private string _url;

        public void OnPointerClick(PointerEventData eventData)
        {
            Application.OpenURL(_url);
        }
    }
}
