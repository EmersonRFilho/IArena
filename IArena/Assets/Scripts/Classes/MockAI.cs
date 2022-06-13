using UnityEngine;
using Movement;
using System.Collections.Generic;

public class MockAI: BrainBase {

    Transform target = null;
    CharacterBehaviors enemy;

    private void Update() {
        if (chara.IsDead) return;
        GetVision();
        if (objectsInRange.Count > 0 && target == null) {
            closest(objectsInRange);
            target = objectsInRange[0];
        } else if(target != null && !objectsInRange.Find(x => x == target)) {
            target = null;

        } else {
            SetMovementBehaviours(true, new WanderBehaviour(1.4f, 2f, 4f, 2f));
        }
        if (enemy && enemy.GetComponent<CharacterBehaviors>().IsDead) {
            enemy = null;
        }
        List<Transform> enemies = objectsInRange.FindAll(x => x.gameObject.layer == LayerMask.NameToLayer("Player"));
        if (enemies.Count > 0)
        {
            closest(enemies);
            enemy = enemies[0].GetComponent<CharacterBehaviors>();
            if (Vector2.Distance(transform.position, enemy.transform.position) <= chara.Weapon.Range && !enemy.IsDead)
            {
                Attack(enemy.GetComponent<CharacterBehaviors>());
            }
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            SetMovementBehaviours(clear: true, new SeekBehaviour(target), new AvoidObstaclesBehaviour(.5f)/*, new FleeBehaviour(enemy.transform, 1/distance)*/);
        } else {
            SetMovementBehaviours(clear: true, new SeekBehaviour(target, 2f), new AvoidObstaclesBehaviour(.5f));
        }
        if ((chara.GetHealth() < 3
            || chara.GetHunger() < 7) && chara.FoodBag.Count != 0) {
            EatFood(chara.FoodBag[0]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform == target && other.gameObject.layer == LayerMask.NameToLayer("Collectable")) {
            Collect(other.GetComponent<Collectable>());
            target = null;
        }
    }

    void closest(List<Transform> list) {
        list.Sort((x, y) => Vector2.Distance(transform.position, x.position).CompareTo(Vector2.Distance(transform.position, y.position)));
    }
}