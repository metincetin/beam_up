using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public static class TutorialState
    {
        public static bool EnemyIntroduction
        {
            get => PlayerPrefs.GetInt("tutorial_enemy_introduction", 0) == 1;
            set => PlayerPrefs.SetInt("tutorial_enemy_introduction", value ? 1 : 0);
        }
    }
}