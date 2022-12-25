using BeamUp.Audio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BeamUp
{
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        public LightbeamController LightbeamController { get; set; }

        [SerializeField]
        private Transform _graphicsTransform;

        [SerializeField]
        private float _fuelDuration = 5;

        [SerializeField]
        private float _aimSpeed;

        [SerializeField]
        private float _chargeSpeed;
        [SerializeField]
        private float _chaseSpeed;

        [SerializeField]
        private ParticleSystem _deathParticlePrefab;

        [SerializeField]
        private AudioClip[] _deathSoundClips;

        [SerializeField]
        private ParticleSystem _rocketParticle;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {

			if (_fuelDuration <= 0)
			{
                _rigidbody.velocity = Vector2.zero;
                return;
			}
			if (LightbeamController.Instances.Count == 0)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            var target = LightbeamController.Instances[0];


            var dir = (target.transform.position - transform.position).normalized;

            var d = Vector3.Dot(target.Rigidbody.velocity.normalized, dir);

            float speed = Mathf.Lerp(_chargeSpeed, _chaseSpeed, Remap(d, -1, 1, 0, 1));
            

            _rigidbody.velocity = Vector3.Slerp(_rigidbody.velocity, dir * speed, _aimSpeed * Time.deltaTime);
        }

        private float Remap(float input, float curMin, float curMax, float newMin, float newMax)
        {
            float t = Mathf.InverseLerp(curMin, curMax, input);
            return Mathf.Lerp(newMin, newMax, t);
        }

        private void Update()
        {
            _fuelDuration -= Time.deltaTime;

            if (_fuelDuration <= 0)
            {
                _rocketParticle.Stop();
                return;
            }
            _graphicsTransform.right = _rigidbody.velocity;
		}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.otherRigidbody && collision.otherRigidbody.TryGetComponent<CelestialBody>(out var _))
            {
                Kill();
            }
        }

        public void Kill()
        {
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
            SFXPlayer.Instance.Play(_deathSoundClips[Random.Range(0, _deathSoundClips.Length)], 0.3f);
            Destroy(gameObject);
        }
    }
}