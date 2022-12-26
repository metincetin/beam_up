using Assets.Scripts.Utils;
using BeamUp.Audio;
using BeamUp.Tutorial;
using BeamUp.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BeamUp
{
    public class Enemy : MonoBehaviour, IKiller, IRenderingStateListener
    {
        private Rigidbody2D _rigidbody;
        public LightbeamController LightbeamController { get; set; }

        public string DeathMessage => "You are killed by the Shadowbringer";

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

        private bool _waitingDeath;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {

			if (_fuelDuration <= 0 || LightbeamController.Instances.Count == 0)
			{
                _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 8f * Time.deltaTime);
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            var target = LightbeamController.FurthestLightbeam;


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

        private void OnEnable()
        {
            DynamicallySpawnedGameObjectContainer.Add(gameObject);
        }
        private void OnDisable()
        {
            DynamicallySpawnedGameObjectContainer.Remove(gameObject);
        }

        private void Update()
        {
            _fuelDuration -= Time.deltaTime;

            if (_fuelDuration <= 0)
            {
                _rocketParticle.Stop();

                if (!_waitingDeath)
                {
                    StartCoroutine(KillDelayed());
                }

                _waitingDeath = true;
                return;
            }
            _graphicsTransform.right = _rigidbody.velocity;
		}

        private IEnumerator KillDelayed()
        {
            yield return new WaitForSeconds(5f);
            Kill();
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
            SFXPlayer.Instance.PlayAtLocation(_deathSoundClips[Random.Range(0, _deathSoundClips.Length)], transform.position);
            Destroy(gameObject);
        }

        public void OnBeginRendering()
        {
            if (!TutorialState.EnemyIntroduction)
            {
                FindObjectOfType<TutorialController>().ShowTutorial("Enemy", gameObject);
                TutorialState.EnemyIntroduction = true;
            }
        }

        public void OnEndRendering()
        {
        }
    }
}