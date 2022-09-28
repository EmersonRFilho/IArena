using UnityEngine;
using Managers;
using Movement;
using Core;

namespace LevelElements
{
    public class WallOfPain : MonoBehaviour
    {
        LevelManager levelManager;
        [SerializeField] SteeringBehaviourBase movement;
        [SerializeField] float maxAcceleration;
        bool moving = false;

        private void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            if(!movement)
            {
                movement = FindObjectOfType<SteeringBehaviourBase>();
            }
            movement.MaxAcceleration = maxAcceleration;
            // StartMoving();
        }

        public void StartMoving()
        {
            // moving = !moving;
            movement.Steerings.Clear();
            movement.Steerings.Add(new StraightLineBehaviour(180, 1));
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player")
            {
                other.gameObject.SetActive(false);
                // levelManager.QueueCommand(new AttackCommand())
            }
        }
    }
}
