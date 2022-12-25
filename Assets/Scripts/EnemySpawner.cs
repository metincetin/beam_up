using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BeamUp
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		private LightbeamController _lightbeamController;

		private enum SpawnType { Clustered, Singular };

		public Enemy EnemyPrefab;
		public Vector2Int SpawnRange;

		public Vector2 SpawnDurationRange;

		public bool AllowSpawn;

		public event Action<Enemy> EnemySpawned;
		private Vector2 GetRandomPointOutsideCamera(Camera camera)
		{
			float width = Mathf.Sign(Random.Range(-1, 2)) * Random.value * camera.orthographicSize + 1;
			float height =  camera.orthographicSize * camera.aspect + 1;

			return camera.transform.position + new Vector3(width, height);
		}
		private SpawnType RandomSpawnType => (SpawnType)Random.Range(0, 2);

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
					yield return new WaitForSeconds(Random.Range(SpawnDurationRange.x, SpawnDurationRange.y));
					var spawnCount = Random.Range(SpawnRange.x, SpawnRange.y);

					var cam = Camera.main;
					switch (RandomSpawnType)
					{
						case SpawnType.Clustered:
							{
								var pos = GetRandomPointOutsideCamera(cam);
								for (int i = 0; i < spawnCount; i++)
								{
									Spawn(pos + ((Vector2)Random.insideUnitSphere).normalized * 3);
								}
							}
							break;
						case SpawnType.Singular:
							for (int i = 0; i < spawnCount; i++)
							{
								var pos = GetRandomPointOutsideCamera(cam);
								Spawn(pos);
							}
							break;
					}
				}
				else
				{
					yield return null;
				}
			}
		}


		public void Spawn(Vector3 position)
		{
			position.z = 0;
			var inst = Instantiate(EnemyPrefab, position, Quaternion.identity);
			inst.LightbeamController = _lightbeamController;
			EnemySpawned?.Invoke(inst);
		}

	}
}