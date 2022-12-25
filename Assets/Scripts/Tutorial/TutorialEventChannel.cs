using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Tutorial
{
    public static class TutorialEventChannel
    {
        public static event Action<Enemy> EnemyTutorialRequested;
		public static event Action<Enemy> BlackholeTutorialRequested;
	}
}