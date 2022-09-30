using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelElements
{
    public class Tracker : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float smoothTime;
        [SerializeField] Vector2 boundsMax;
        [SerializeField] Vector2 boundsMin;
        private Vector3 velocity = Vector3.zero;
        private float initialHeight;

        private void Start() {
            initialHeight = transform.position.z;
        }
        
        private void Update() {
            float xClamp = Mathf.Clamp(target.position.x, boundsMin.x, boundsMax.x);
            float yClamp = Mathf.Clamp(target.position.y, boundsMin.y, boundsMax.y);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, initialHeight);
            transform.position = Vector3.SmoothDamp(
                pos,
                new Vector3(
                    xClamp,
                    yClamp,
                    initialHeight
                ),
                ref velocity,
                smoothTime
            );
        }
    }
}
