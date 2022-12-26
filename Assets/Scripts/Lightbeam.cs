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

        private Vector2 _previousVelocity;

        private int _energy;
        [SerializeField]
        private int _maxEnergy;


        public event Action<CelestialBody> CelestialBodyHit;
        public event Action<GameObject> Killed;
        public event Action<Vector3> Duplicated;
        public event Action DespawnRequested;

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
            _energy = _maxEnergy;
        }

        public void Shoot(Vector3 shootDirection)
        {
            _rigidbody.velocity = shootDirection.normalized * StartSpeed;
        }

        private void Update()
        {
            _trailRenderer.time = Mathf.Lerp(_trailRenderer.time, (_energy - 1) / (float)_maxEnergy + 0.2f, 8f * Time.deltaTime);
		}
        private void FixedUpdate()
        {
            _previousVelocity = _rigidbody.velocity;
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
            {
                var hit = collision.collider.GetComponentInParent<CelestialBody>();

                if (hit)
                {
                    CelestialBodyHit?.Invoke(hit);
                    hit.OnLightHit(this, collision.GetContact(0));
                }
            }

            if (collision.rigidbody && collision.rigidbody.TryGetComponent<Enemy>(out var enemy))
            {
                if (KillsOnImpact)
                {
                    enemy.Kill();
                    _rigidbody.velocity = _previousVelocity;
                }
                else
                {
                    if (_energy > 0)
                    {
                        _energy--;;
                    }
                    else
                    {
                        Killed?.Invoke(enemy.gameObject);
                        Despawn();
                    }
                }
            }
            if (collision.rigidbody && collision.rigidbody.TryGetComponent<Blackhole>(out var blackhole))
            {
                Killed?.Invoke(blackhole.gameObject);
                Despawn();
            }
        }

        public void Reflect(Vector3 direction)
        {
            _rigidbody.velocity = direction * StartSpeed;
        }

        public void Despawn()
        {
            DespawnRequested?.Invoke();
        }

        public void SetDirection(Vector3 direction)
        {
            _rigidbody.velocity = direction.normalized * StartSpeed;
        }

        public void Duplicate(Vector3 withDirection)
        {
            Duplicated?.Invoke(withDirection);
        }

        public void Kill()
        {
            _trailRenderer.transform.parent = null;
            _trailRenderer.autodestruct = true;
            Destroy(gameObject);
        }

		public void FillEnergy()
		{
            _energy = _maxEnergy;
		}
	}
}
