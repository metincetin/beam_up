using System;
using System.Collections;
using System.Collections.Generic;
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
            return inst;
        }
    }
}