using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeamUp
{
    public class Lightbeam : MonoBehaviour
    {
        public float StartSpeed = 10;

        public GameObject ExpectedHit;

        private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody => _rigidbody;

        private RaycastHit2D[] _circleCastHitResults = new RaycastHit2D[10];


        public event Action<CelestialBody> CelestialBodyHit;
        public event Action<Vector3> Duplicated;

        public bool QueuedDespawn { get; private set; }
		public bool KillsOnImpact { get; internal set; }

        

        [SerializeField]
        [ColorUsage(false, true)]
        private Color _defaultColor;
        private Color _color;
		public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _trailRenderer.material.SetColor("_EmissionColor", value);
                _graphics.material.SetColor("_EmissionColor", value);
				//_graphics.material.SetColor("_BaseColor", value);
			}
        }

        public Color DefaultColor => _defaultColor;

        [Header("Effects")]
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private MeshRenderer _graphics;

		private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _color = _defaultColor;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var hit = collision.collider.GetComponentInParent<CelestialBody>();

            if (hit)
            {
                CelestialBodyHit?.Invoke(hit);
				hit.OnLightHit(this, collision.GetContact(0));
            }
        }

        public void Reflect(Vector3 direction)
        {
            _rigidbody.velocity = direction * StartSpeed;
        }

        public void Despawn()
        {
            QueuedDespawn = true;
        }

        public void SetDirection(Vector3 direction)
        {
            _rigidbody.velocity = direction.normalized * StartSpeed;
        }

        public void Duplicate(Vector3 withDirection)
        {
            Duplicated?.Invoke(withDirection);
        }
    }
}
