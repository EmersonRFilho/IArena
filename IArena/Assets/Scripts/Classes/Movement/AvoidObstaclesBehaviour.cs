using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// Movement behaviour in which the object avoids anything on the layer "Terrain"
    /// </summary>
    public class AvoidObstaclesBehaviour : Steering
    {
        private float avoidDistance = 2f;
        private float lookAhead = 2f;
        private float sideViewAngle = 45f;

        /// <summary>
        /// Movement Behaviour in which the object avoids anything on the layer "Terrain"
        /// </summary>
        /// <param name="weight">How much should this behaviour affect overall movement</param>
        public AvoidObstaclesBehaviour(float weight = 1f) {
            this.weight = weight;
        }

        public override SteeringData GetSteering(SteeringBehaviourBase steeringBase)
        {
            Rigidbody2D rigid = steeringBase.GetComponent<Rigidbody2D>();
            SteeringData steering = new SteeringData();
            Vector2[] rayVector = new Vector2[3];
            rayVector[0] = rigid.velocity.normalized * lookAhead;
            float rayOrientation = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x);
            float rightRayOrientation = rayOrientation + (sideViewAngle * Mathf.Deg2Rad);
            float leftRayOrientation = rayOrientation - (sideViewAngle * Mathf.Deg2Rad);
            rayVector[1] = new Vector2(Mathf.Cos(rightRayOrientation), Mathf.Sin(rightRayOrientation)).normalized * lookAhead;
            rayVector[2] = new Vector2(Mathf.Cos(leftRayOrientation), Mathf.Sin(leftRayOrientation)).normalized * lookAhead;
            for (int i = 0; i < rayVector.Length; i++) {
                LayerMask mask = LayerMask.GetMask("Terrain");
                RaycastHit2D hit = Physics2D.Raycast(steeringBase.transform.position, rayVector[i], lookAhead, mask);
                MonoBehaviour.print("Raycasting");
                if (hit.collider) {
                    MonoBehaviour.print("obstacle found: " + hit.collider.name);
                    Vector3 target = hit.point + (hit.normal * avoidDistance);
                    steering.Linear = target - steeringBase.transform.position;
                    steering.Linear = steering.Linear.normalized * steeringBase.MaxAcceleration;
                    break;
                }
            }
            return steering;
        }
    }
}