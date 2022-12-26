using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public class GiantHole : Blackhole
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.rigidbody && collision.rigidbody.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Kill();
            }
        }

    }
}