using Core;
using UnityEngine;

namespace Movement {
    public class WanderBehaviour : Steering
    {
        private float wanderRate = 0.4f;
        private float wanderOffset = 1.5f;
        private float wanderRadius = 4f;
        private float wanderOrientation = 0f;

        public WanderBehaviour(float wanderRate, float wanderOffset, float wanderRadius, float weight)
        {
            this.wanderRate = wanderRate;
            this.wanderOffset = wanderOffset;
            this.wanderRadius = wanderRadius;
            this.weight = weight;
        }

        private float RandomBinomial() {
            return Random.value - Random.value;
        }
        private Vector3 OrientationToVector(float orientation) {
            return new Vector3(Mathf.Cos(orientation), Mathf.Sin(orientation));
        }
        public override SteeringData GetSteering(SteeringBehaviourBase steeringBase)
        {
            SteeringData steering = new SteeringData();  
            wanderOrientation += RandomBinomial() * wanderRate;  
            float characterOrientation = steeringBase.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;  
            float targetOrientation = wanderOrientation + characterOrientation;  
            Vector3 targetPosition = steeringBase.transform.position + (wanderOffset *  
            OrientationToVector(characterOrientation));  
            targetPosition += wanderRadius * OrientationToVector(targetOrientation);  
            steering.Linear = targetPosition - steeringBase.transform.position;  
            steering.Linear.Normalize();  
            steering.Linear *= steeringBase.MaxAcceleration;  
            return steering;
        }
    }
}