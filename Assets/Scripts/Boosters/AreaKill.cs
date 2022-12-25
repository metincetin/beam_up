using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Boosters
{
	[CreateAssetMenu(menuName = "Boosters/Area Kill")]
	public class AreaKill : Booster
    {
        public GameObject EffectPrefab;

        public float EffectRange;

        protected override void OnApplied(Lightbeam lightBeam)
        {
            var pos = lightBeam.transform.position;

            var hits = Physics2D.OverlapCircleAll(pos, EffectRange);
            foreach(var hit in hits)
            {
                if (hit.attachedRigidbody && hit.attachedRigidbody.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.Kill();
                }
            }
        }

        protected override void OnReverted(Lightbeam lightBeam)
        {
        }
    }
}