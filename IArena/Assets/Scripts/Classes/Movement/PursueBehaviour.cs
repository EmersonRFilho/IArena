using Core;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// Movement behaviour in which object moves towards a target transform in order to intercept it
    /// </summary>
    [System.Serializable]
    public class PursueBehaviour : Steering
    {
        private float maxPrediction = 2f;
        private Transform target;
        /// <summary>
        /// Movement behaviour in which object moves towards a target transform in order to intercept it
        /// </summary>
        /// <param name="target">The transform object will try to intercept</param>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
        public PursueBehaviour(Transform target, float weight)
        {
            this.target = target;
            this.weight = weight;
        }

        public Transform Target { get => target; }

        public override SteeringData GetSteering(SteeringBehaviourBase steeringBase)
        {
            SteeringData steering = new SteeringData();

            Vector2 direction = target.transform.position - steeringBase.transform.position;
            float distance = direction.magnitude;
            float speed = steeringBase.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            float prediction;
            if(speed <= (distance/maxPrediction))
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance/speed;
            }

            Vector2 predictedTarget = (Vector2) target.transform.position + (target.GetComponent<Rigidbody2D>().velocity * prediction);

            steering.Linear = predictedTarget - (Vector2)steeringBase.transform.position;
            steering.Linear.Normalize();
            steering.Linear *= steeringBase.MaxAcceleration;
            steering.Angular = 0;

            return steering;
        }
    }
}
