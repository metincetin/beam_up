using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
	public class AsteroidSpawner : MonoBehaviour
	{
		[SerializeField]
		private Asteroid _asteroidPrefab;

		[SerializeField]
		private Vector2Int _spawnNumberRange;
		
		[SerializeField]
		private Vector2 _spawnDurationRange;

		[SerializeField]
		private bool _allowSpawn;
		public bool AllowSpawn { get => _allowSpawn; set => _allowSpawn = value; }

		[SerializeField]
		private float _spawnDistance;

		[SerializeField]
		private float _cameraAimRadius;

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
					var spawnCount = Random.Range(_spawnNumberRange.x, _spawnNumberRange.y);

					for (int i = 0; i < spawnCount; i++)
					{
						var cam = Camera.main;
						Vector2 pos = cam.transform.position;
						var dir = Random.insideUnitCircle.normalized;
						Vector2 aimPos = cam.transform.position + (Vector3)Random.insideUnitCircle.normalized * _cameraAimRadius;
						var aimDir = (aimPos - pos).normalized;

						Spawn(pos + dir * _spawnDistance, aimDir);
					}
				}
				else
				{
					yield return null;
				}
			}
		}



		public void Spawn(Vector3 position, Vector3 direction)
		{
			position.z = 0;
			var inst = Instantiate(_asteroidPrefab, position, Quaternion.identity);
			inst.SetDirection(direction);
		}
	}
}