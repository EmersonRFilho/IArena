using UnityEngine;
using Core;

namespace Movement
{
    [System.Serializable]
    public abstract class Steering
    {
        protected float weight = 1f;

        public float GetWeight() {
            return weight;
        }

        public abstract SteeringData GetSteering(SteeringBehaviourBase steeringBase);
    }
}