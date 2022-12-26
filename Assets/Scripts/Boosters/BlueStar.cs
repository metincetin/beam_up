using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Boosters
{
    [CreateAssetMenu(menuName = "Boosters/Blue Star")]
    public class BlueStar : Booster
    {
        [ColorUsage(true, true)]
        public Color Color;


        protected override void OnApplied(Lightbeam lightBeam)
        {
            lightBeam.Color = Color;
            lightBeam.KillsOnImpact = true;
            lightBeam.FillEnergy();
            lightBeam.StartCoroutine(RemoveDelayed(lightBeam));
        }

        private IEnumerator RemoveDelayed(Lightbeam lightbeam)
        {
            yield return new WaitForSeconds(Duration);
            Revert(lightbeam);
        }

        protected override void OnReverted(Lightbeam lightBeam)
        {
            lightBeam.Color = lightBeam.DefaultColor;
            lightBeam.KillsOnImpact = false;
        }
    }
}
