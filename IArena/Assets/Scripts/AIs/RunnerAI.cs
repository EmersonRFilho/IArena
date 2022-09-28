using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

public class RunnerAI : BrainBase
{
    [SerializeField] private int direction = 0;
    [SerializeField] private bool challengeMode = false;

    // Update is called once per frame
    void Update()
    {
        SetMovementBehaviours(new StraightLineBehaviour(direction, 1.0f));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(challengeMode)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
