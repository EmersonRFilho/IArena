using Core;
using UnityEngine;

namespace Movement {
    /// <summary>
    /// Movement behaviour in which object will move around aimlessly
    /// </summary>
    public class WanderBehaviour : Steering
    {
        private float wanderRate = 0.4f;
        private float wanderOffset = 1.5f;
        private float wanderRadius = 4f;
        private float wanderOrientation = 0f;
        Vector3 targetPosition;

        /// <summary>
        /// Movement behaviour in which object will move around aimlessly.
        /// It basicaly creates a circle where any point in it can be a target;
        /// </summary>
        /// <param name="wanderRate">How much should object change directions</param>
        /// <param name="wanderOffset">How far away is the circle from object (Closer = more variation in direction)</param>
        /// <param name="wanderRadius">How big is the circle</param>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
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
            Rigidbody2D rigid = steeringBase.GetComponent<Rigidbody2D>();
            SteeringData steering = new SteeringData();  
            wanderOrientation += RandomBinomial() * wanderRate;  
            float characterOrientation = steeringBase.transform.rotation.eulerAngles.z * Mathf.Deg2Rad; 
            float targetOrientation = wanderOrientation + characterOrientation;
            targetPosition = steeringBase.transform.position + (wanderOffset * OrientationToVector(characterOrientation));  
            targetPosition += wanderRadius * OrientationToVector(targetOrientation);  
            steering.Linear = targetPosition - steeringBase.transform.position;  
            steering.Linear.Normalize();  
            steering.Linear *= steeringBase.MaxAcceleration;
            return steering;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPosition, 1);
        }
    }
}