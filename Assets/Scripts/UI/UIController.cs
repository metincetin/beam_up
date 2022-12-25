using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp.UI
{
	public class UIController: MonoBehaviour
	{
		private GameObject _enemyTutorialBox;
		private GameObject _blackholeTutorialBox;
		public GameObject EnemyTutorialBox => _enemyTutorialBox;
		public GameObject BlackholeTutorialBox => _blackholeTutorialBox;
	}
}
