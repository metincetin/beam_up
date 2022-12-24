using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp
{
    public class Lightbeam : MonoBehaviour
    {
        public float StartSpeed = 10;

        public GameObject ExpectedHit;

        private Rigidbody2D _rigidbody;

        private RaycastHit2D[] _circleCastHitResults = new RaycastHit2D[10];

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector3 shootDirection)
        {
            _rigidbody.velocity = shootDirection.normalized * StartSpeed;
        }

        private void FixedUpdate()
        {
            int hitCount = Physics2D.CircleCastNonAlloc(transform.position, 1, _rigidbody.velocity.normalized, _circleCastHitResults);
            for(int i = 0; i < hitCount; i++)
            {
                var hitResult = _circleCastHitResults[i];
                if (hitResult.collider.GetComponentInParent<CelestialBody>())
                {
                    ExpectedHit = gameObject;
                    return;
                }
            }
        }
    }
}
