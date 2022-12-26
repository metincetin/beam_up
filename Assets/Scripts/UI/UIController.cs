using BeamUp.UI;
using System;
using TMPro;
using UnityEngine;

namespace BeamUp.UI
{
	public class UIController: MonoBehaviour
	{

		[SerializeField]
		private Transform _tutorialContainer;
		
		[SerializeField]
		private Page[] _pages;

		[SerializeField]
		private TMP_Text _dragToBeginTutorialText;
		public TMP_Text DragToBeginTutorialText { get => _dragToBeginTutorialText; }

		public T GetPage<T>() where T: Page
		{
			foreach(var p in _pages)
			{
				if (p is T)
					return (T)p;
			}
			return null;
		}

		public GameObject GetTutorial(string tutorialName)
		{
			return _tutorialContainer.transform.Find(tutorialName).gameObject;
		}
	}
}
