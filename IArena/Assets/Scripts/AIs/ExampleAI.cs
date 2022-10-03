using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

public class ExampleAI : BrainBase
{
    Transform target;
    Transform hazard;

    // Update is called once per frame
    void Update()
    {
        GetVision();
        SortClosest(objectsInRange);
        foreach (Transform item in objectsInRange)
        {
            if(item.gameObject.layer == LayerMask.NameToLayer("Hazard"))
            {
                hazard = item;
                break;
            }
        }
        if(!target && objectsInRange.Count > 0)
        {
            target = objectsInRange[0];
            if(target.tag == "Weapon" && target.GetComponent<Weapon>().Damage < chara.Weapon.Damage)
            {
                objectsInRange.Remove(target);
                target = objectsInRange[0];
            }
            if(target.tag == "Hazard")
            {
                target = null;
            }
        }
        else 
        {
            // SetMovementBehaviours(new PursueBehaviour(target, 1));
            SetMovementBehaviours(
                new SeekBehaviour(target, 1/Vector2.Distance(transform.position, target.position)), 
                new AvoidObstaclesBehaviour(0.3f)
            );
            // SetMovementBehaviours(new StraightLineBehaviour(90, 1));
            // SetMovementBehaviours(new WanderBehaviour(0.2f, 2, 3, 1));
        }
        if(hazard && !target)
        {
            SetMovementBehaviours(
                new FleeBehaviour(hazard, 1/Vector2.Distance(transform.position, hazard.position)), 
                new AvoidObstaclesBehaviour(0.5f));
        }
        if(target.tag == "Player")
        {
            Attack(target.GetComponent<CharacterBehaviors>());
        }
        if(chara.GetHealth() <= 5 && chara.FoodBag.Count > 0)
        {
            EatFood(chara.FoodBag[0]);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target) 
        {
            Collect(other.GetComponent<Collectable>());
            target = null;
        }
    }

    void SortClosest(List<Transform> list)
    {
        list.Sort(
            (obj1, obj2) => Vector2.Distance(transform.position, obj1.position)
            .CompareTo(Vector2.Distance(transform.position, obj2.position))
        );
    }
}
