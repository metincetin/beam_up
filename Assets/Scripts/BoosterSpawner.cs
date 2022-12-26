using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
	public class BoosterSpawner : MonoBehaviour
	{

		[SerializeField]
		private BoosterComponent _boosterComponent;

		[SerializeField]
		private bool _allowSpawn;
		public bool AllowSpawn
		{
			get => _allowSpawn;
			set => _allowSpawn = value;
		}

		[SerializeField]
		private Booster[] _boosters;

		[SerializeField]
		private Vector2 _spawnDurationRange;

		private Camera _camera;

		[SerializeField]
		private Vector2 _spawnDistanceHeightRange;

		[SerializeField]
		private Vector2 _spawnDistanceWidthRange;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Start()
		{
			StartCoroutine(SpawnTimer());
		}

		private IEnumerator SpawnTimer()
		{
			while (true)
			{

				if (AllowSpawn)
				{
					yield return new WaitForSeconds(Random.Range(_spawnDurationRange.x, _spawnDurationRange.y));
					Vector2 pos = _camera.transform.position;
					pos.y += Random.Range(_spawnDistanceHeightRange.x, _spawnDistanceHeightRange.y);
					pos.x += Random.Range(_spawnDistanceWidthRange.x, _spawnDistanceWidthRange.y);


					Spawn(pos);
				}
				else
				{
					yield return null;
				}


			}
		}

		private void Spawn(Vector3 pos)
		{
			var inst = Instantiate(_boosterComponent, pos, Quaternion.identity);
			inst.Booster = _boosters[Random.Range(0, _boosters.Length)];
		}
	}
}