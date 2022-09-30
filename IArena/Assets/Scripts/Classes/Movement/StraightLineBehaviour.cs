using UnityEngine;
using Core;

namespace Movement
{
    /// <summary>
    /// Movement behaviour in which, given an angle, object will move in that direction.
    /// For reference:
    /// 0 = right;
    /// 90 = up;
    /// 180 = left;
    /// 270 = down.
    /// </summary>
    [System.Serializable]
    public class StraightLineBehaviour : Steering
    {
        private float angle;

        /// <summary>
        /// Movement behaviour in which, given an angle, object will move in that direction.
        /// For reference:
        /// 0 = right;
        /// 90 = up;
        /// 180 = left;
        /// 270 = down.
        /// </summary>
        /// <param name="angle">The angle in degrees representing the direction object will move</param>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
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