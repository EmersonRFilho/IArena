using UnityEngine;
using Core;

namespace Movement
{
    /// <summary>
    /// Movement behaviour in which object moves towards the target transform's position
    /// </summary>
    [System.Serializable]
    public class SeekBehaviour : Steering
    {
        private Transform target;

        /// <summary>
        /// Movement behaviour in which object moves towards the target transform's position
        /// </summary>
        /// <param name="target">The transform obj will move towards to</param>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
        public SeekBehaviour(Transform target, float weight = 1)
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
            steering.Linear *= steeringBase.MaxAcceleration;
            steering.Angular = 0;
            return steering;
        }
    }
}