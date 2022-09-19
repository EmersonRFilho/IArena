using UnityEngine;
using Core;

namespace Movement
{
    [System.Serializable]
    public class StraightLineBehaviour : Steering
    {
        private float angle;

        public StraightLineBehaviour(float angle, float weight)
        {
            this.angle = angle;
            this.weight = weight;
        }

        public float Angle { get => angle; set => angle = value; }

        private Vector2 RadianToVector2(float radians)
        {
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }

        private Vector2 DegreeToVector2(float degrees)
        {
            return RadianToVector2(degrees * Mathf.Deg2Rad);
        }

        public override SteeringData GetSteering(SteeringBehaviourBase steeringBase)
        {
            SteeringData steering = new SteeringData();
            steering.Linear = DegreeToVector2(angle);
            steering.Linear.Normalize();
            steering.Linear *= steeringBase.MaxAcceleration;
            steering.Angular = 0;
            return steering;
        }
    }
}