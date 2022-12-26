using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public static class TutorialState
    {
		public static bool ReflectIntroduction
		{
			get => PlayerPrefs.GetInt("tutorial_reflection_introduction", 0) == 1;
			set => PlayerPrefs.SetInt("tutorial_reflection_introduction", value ? 1 : 0);
		}

		public static bool EnemyIntroduction
        {
            get => PlayerPrefs.GetInt("tutorial_enemy_introduction", 0) == 1;
            set => PlayerPrefs.SetInt("tutorial_enemy_introduction", value ? 1 : 0);
        }

		public static bool BlackholeIntroduction
		{
			get => PlayerPrefs.GetInt("tutorial_blackhole_introduction", 0) == 1;
			set => PlayerPrefs.SetInt("tutorial_blackhole_introduction", value ? 1 : 0);
		}

		public static bool PlanetIntroduction
		{
			get => PlayerPrefs.GetInt("tutorial_reflect_introduction", 0) == 1;
			set => PlayerPrefs.SetInt("tutorial_reflect_introduction", value ? 1 : 0);
		}
	}
}