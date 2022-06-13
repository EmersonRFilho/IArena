using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SteeringBehaviourBase : MonoBehaviour
    {

        private Rigidbody2D rigid;
        [SerializeField] private List<Steering> steerings = new List<Steering>();
        private float maxAcceleration = 10f;
        private float maxAngularAcceleration = 5f;
        private float drag = 1f;

        public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
        public float MaxAngularAcceleration { get => maxAngularAcceleration; set => maxAngularAcceleration = value; }
        public List<Steering> Steerings { get => steerings; }

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            // steerings = GetComponents<Steering>();
            rigid.drag = drag;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 acceleration = Vector3.zero;
            float rotation = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            foreach (Steering behaviour in steerings)
            {
                SteeringData steering = behaviour.GetSteering(this);
                acceleration += steering.Linear * behaviour.GetWeight();
                rotation += steering.Angular;
            }
            if (acceleration.magnitude > maxAcceleration)
            {
                acceleration.Normalize();
                acceleration *= maxAcceleration;
            }
            rigid.AddForce(acceleration, ForceMode2D.Force);
            if (rotation != 0)
            {
                rigid.rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, rotation, MaxAngularAcceleration * Time.deltaTime);
            }
        }
    }
}