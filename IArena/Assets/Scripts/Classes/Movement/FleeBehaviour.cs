using UnityEngine;
using Core;

namespace Movement
{
    public class FleeBehaviour : Steering
    {
        private Transform target;

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