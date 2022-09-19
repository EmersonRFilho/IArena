using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

public class ExampleAI : BrainBase
{
    Transform target;

    // Update is called once per frame
    void Update()
    {
        GetVision();
        SortClosest(objectsInRange);
        if(!target && objectsInRange.Count > 0)
        {
            target = objectsInRange[0];
            if(target.tag == "Weapon" && target.GetComponent<Weapon>().Damage < chara.Weapon.Damage)
            {
                objectsInRange.Remove(target);
                target = objectsInRange[0];
            }
        }
        else 
        {
            // SetMovementBehaviours(new SeekBehaviour(target));
            SetMovementBehaviours(new StraightLineBehaviour(90, 1));
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
        list.Sort((obj1, obj2) => Vector2.Distance(transform.position, obj1.position).CompareTo(Vector2.Distance(transform.position, obj2.position)));
    }
}
