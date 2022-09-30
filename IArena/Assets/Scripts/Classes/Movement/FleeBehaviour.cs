using UnityEngine;
using Core;

namespace Movement
{
    /// <summary>
    /// Movement behaviour in which an object moves away from a target transform
    /// </summary>
    public class FleeBehaviour : Steering
    {
        private Transform target;

        /// <summary>
        /// Movement behaviour in which an object moves away from a target transform
        /// </summary>
        /// <param name="target">The transform the object should flee from</param>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
        public FleeBehaviour(Transform target, float weight = 1f)
        {
            this.target = target;
            this.weight = weight;
        }

        public Transform Target { get => target; }

        public override SteeringData GetSteering(SteeringBehaviourBase steeringBase)
        {
            SteeringData steering = new SteeringData();
            steering.Linear = target.position - steeringBase.transform.position;
            steering.Linear.Normalize();
            steering.Linear *= -steeringBase.MaxAcceleration;
            steering.Angular = 0;
            return steering;
        }
    }
}