using UnityEngine;
using UnitySpring.ExplicitRK4;

namespace BeamUp
{
    [System.Serializable]
    public class SpringParameters
    {
        public float StartValue;
        public float EndValue;
        public float Stiffness = 1;
        public float Damping = 1;
        public float Mass = 1;
    }

    public class ScaleAnimation : MonoBehaviour
    {
        public SpringParameters SpringParameters;

        private Spring _spring = new Spring();
        public Transform GraphicsTransform;



        private void Update()
        {
            _spring.startValue = SpringParameters.StartValue;
            _spring.endValue = SpringParameters.EndValue;
            _spring.damping = SpringParameters.Damping;
            _spring.stiffness = SpringParameters.Stiffness;
            _spring.mass = SpringParameters.Mass;

            var x = Mathf.Abs(_spring.Evaluate(Time.deltaTime));
            GraphicsTransform.localScale = new Vector3(x, x, x);
        }
    }
}
