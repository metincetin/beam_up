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

        public event Action<Lightbeam, CelestialBody> LightbeamHit;

        public Lightbeam FurthestLightbeam
        {
            get
            {
                if (Instances.Count == 0) return null;
                return Instances.OrderByDescending(x => x.transform.position.y).First();
            }
        }


        private void Start()
        {
            if (_spawnAtStart)
            {
                Spawn(Vector3.zero);
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

            return inst;
        }

        public void Despawn(Lightbeam lightbeam) {
            _instances.Remove(lightbeam);
            Destroy(lightbeam);
        }
    }
}