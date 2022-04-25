using System.Collections;
using System.Collections.Generic;
using Movement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Core;

public class MovementTests
{
    GameObject player;
    SteeringBehaviourBase movement;
    GameObject target;
    [SetUp]
    public void Setup() 
    {
        player = GameObject.Instantiate(new GameObject());
        movement = player.AddComponent<SteeringBehaviourBase>();
        var rigid = player.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        target = GameObject.Instantiate(new GameObject());
        target.transform.position = new Vector3(0,5,0);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SeeksTarget()
    {
        float interval = 0.5f;
        float initDist = Vector2.Distance(player.transform.position, target.transform.position);
        movement.Steerings.Add(new SeekBehaviour(target.transform));
        yield return new WaitForSeconds(interval);
        float currentDist = Vector2.Distance(player.transform.position, target.transform.position);
        // Use the Assert class to test conditions.
        Assert.Less(currentDist, initDist);
        Debug.Log(string.Format("Initial distance: {0}\nDistance after {2} seconds: {1}\n", initDist, currentDist, interval));
    }

    [UnityTest]
    public IEnumerator FleesTarget()
    {
        float interval = 0.5f;
        float initDist = Vector2.Distance(player.transform.position, target.transform.position);
        movement.Steerings.Add(new FleeBehaviour(target.transform));
        yield return new WaitForSeconds(interval);
        float currentDist = Vector2.Distance(player.transform.position, target.transform.position);
        // Use the Assert class to test conditions.
        Assert.Greater(currentDist, initDist);
        Debug.Log(string.Format("Initial distance: {0}\nDistance after {2} seconds: {1}\n", initDist, currentDist, interval));
    }
}
