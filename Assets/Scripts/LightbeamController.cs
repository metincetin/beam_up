using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeamUp
{
    public class LightbeamController : MonoBehaviour
    {
        [SerializeField]
        private Lightbeam _prefab;

        [SerializeField]
        private bool _spawnAtStart;

        private List<Lightbeam> _instances = new List<Lightbeam>();

        public List<Lightbeam> Instances { get => _instances; }

        public event Action<Lightbeam> LightbeamSpawned;
		public event Action<Lightbeam> LightbeamDespawned;

        public event Action<Lightbeam, CelestialBody> LightbeamHit;

        public event Action<Lightbeam, GameObject> LightbeamKilled;


		public Lightbeam FurthestLightbeam
        {
            get;
            private set;
        }


        private void Start()
        {
        }

        private void Update()
        {
            if (Instances.Count == 0)
            {
                FurthestLightbeam = null;
            }
            else
            {
                FurthestLightbeam = Instances.OrderByDescending(x => x.transform.position.y).First();
            }
        }

        public Lightbeam Spawn(Vector3 atLocation)
        {
            var inst = Instantiate(_prefab);
            inst.transform.position = atLocation;
            _instances.Add(inst);
            LightbeamSpawned?.Invoke(inst);

            inst.CelestialBodyHit += (CelestialBody body) =>
            {
                LightbeamHit?.Invoke(inst, body);
            };
            inst.Duplicated += (Vector3 dir) =>
            {
                var duplicate = Instantiate(inst);
                duplicate.SetDirection(dir);
                _instances.Add(duplicate);
                LightbeamSpawned?.Invoke(duplicate);
            };
            inst.Killed += (go) =>
            {
				Despawn(inst);
                LightbeamKilled?.Invoke(inst, go);
			};
            inst.DespawnRequested += () =>
            {
                Despawn(inst);
                LightbeamDespawned?.Invoke(inst);
            };

            return inst;
        }

        public void Despawn(Lightbeam lightbeam) {
            if (_instances.Contains(lightbeam))
            {
                _instances.Remove(lightbeam);
                lightbeam.Kill();
            }
        }
    }
}