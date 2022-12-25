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
		public Transform ReferencePoint;


		public Occurance[] Occurances;

		private float _highestDistance;
		private float CurrentDistance => _camera.transform.position.y;

		public GameObject PlanetPrefab;

		private float _lastSpawnHeight;

		private Camera _camera;
		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			_highestDistance = Mathf.Max(CurrentDistance, _highestDistance);

			foreach (var occurance in Occurances)
			{
				if (occurance.LastHeight < CurrentDistance)
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
