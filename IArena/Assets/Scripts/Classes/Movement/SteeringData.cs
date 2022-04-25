using UnityEngine;

namespace Movement
{
    [System.Serializable]
    public class SteeringData
    {
        private Vector3 linear;
        private float angular;

        public SteeringData()
        {
            linear = Vector3.zero;
            angular = 0f;
        }

        public Vector3 Linear { get => linear; set => linear = value; }
        public float Angular { get => angular; set => angular = value; }
    }
}