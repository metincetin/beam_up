using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public class MeshSwitcher : MonoBehaviour
    {
        [SerializeField]
        private bool _randomizeOnAwake;
        [SerializeField]
        private Mesh[] _meshes;

        private void Awake()
        {
            if (_randomizeOnAwake)
            {
                GetComponent<MeshFilter>().mesh = _meshes[Random.Range(0, _meshes.Length)];
            }
        }
    }
}