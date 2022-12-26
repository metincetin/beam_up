using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public static class DynamicallySpawnedGameObjectContainer
    {
        private static List<GameObject> _objects = new List<GameObject>();
        public static void Add(GameObject o)
        {
            _objects.Add(o);
        }
        public static void Remove(GameObject o)
        {
            _objects.Remove(o);
        }

        public static void DestroyAll(float duration = 0f)
        {
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                GameObject obj = _objects[i];
                Object.Destroy(obj, duration);
            }
        }
    }
}
