using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace BeamUp
{
	[System.Serializable]
	public class Occurance
	{
		public string Name;

		public GameObject[] SequentalPrefabs;
		public GameObject[] RandomPrefabs;
		
		public float HorizontalRandomness;

		public float OccuranceByMeters;
		public float RandomOffset;
		public float LastHeight;

		public int OccuranceCount;

		public bool AddOffsetWhenOccured;

		public int OccuranceLimit = 0;

		public float CalculateHorizontalPosition()
		{
			return (Random.value * 2 - 1) * HorizontalRandomness;
		}

		public float CalculateHeight()
		{
			float y = LastHeight + OccuranceByMeters + RandomOffset * (Random.value * 2 - 1);
			return y;
		}

		public GameObject NextPrefab
		{
			get
			{
				if (OccuranceCount < SequentalPrefabs.Length)
					return SequentalPrefabs[OccuranceCount];
				if (RandomPrefabs.Length == 0) return null;
				return RandomPrefabs[Random.Range(0, RandomPrefabs.Length)];
			}
		}
	}

	public class WorldGenerator : MonoBehaviour
	{


		public Occurance[] Occurances;

		private float _highestDistance;
		private float CurrentDistance => _camera.transform.position.y;



		private Camera _camera;
		private void Awake()
		{
			_camera = Camera.main;
		}

		public void Reset()
		{
			foreach(var occr in Occurances)
			{
				occr.OccuranceCount = 0;
				occr.LastHeight = 0;
			}
			_highestDistance = 0;
		}

		private void Update()
		{
			_highestDistance = Mathf.Max(CurrentDistance, _highestDistance);

			foreach (var occurance in Occurances)
			{
				if (occurance.LastHeight <= CurrentDistance && (occurance.OccuranceLimit == 0 || occurance.OccuranceCount < occurance.OccuranceLimit))
				{
					float y = occurance.CalculateHeight();
					float x = occurance.CalculateHorizontalPosition();

					var inst = Instantiate(occurance.NextPrefab, new Vector3(x, y), Quaternion.Euler(0, 0, 0));
					occurance.LastHeight = y;
					occurance.OccuranceCount++;
					if (occurance.AddOffsetWhenOccured)
					{
						foreach(var o in Occurances)
						{
							o.LastHeight += y;
						}
					}
				}
			}
		}
	}

}
