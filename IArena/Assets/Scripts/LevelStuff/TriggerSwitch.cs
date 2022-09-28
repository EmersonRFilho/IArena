using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LevelElements
{
    public class TriggerSwitch : MonoBehaviour
    {
        [SerializeField] UnityEvent trigger;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player")
            {
                trigger.Invoke();
            }
        }
    }
}

