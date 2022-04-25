using UnityEngine;
using Movement;
using System.Collections.Generic;

public class MockAI: BrainBase {

    Transform target = null;
    CharacterBehaviors enemy;

    private void Update() {
        if (chara.IsDead) return;
        GetVision();
        SetMovementBehaviours(true, new WanderBehaviour(1.4f, 1.5f, 4f, 1f));
        // if (objectsInRange.Count > 0 && target == null) {
        //     closest(objectsInRange);
        //     target = objectsInRange[0];
        // }
        // List<Transform> enemies = objectsInRange.FindAll(x => x.gameObject.layer == LayerMask.NameToLayer("Player"));
        // if (enemies.Count > 0)
        // {
        //     closest(enemies);
        //     enemy = enemies[0].GetComponent<CharacterBehaviors>();
        //     if (Vector2.Distance(transform.position, enemy.transform.position) <= chara.Weapon.Range)
        //     {
        //         Attack(enemy.GetComponent<CharacterBehaviors>());
        //     }
        //     if (enemy.GetComponent<CharacterBehaviors>().IsDead) {
        //         enemy = null;
        //     }
        //     float distance = Vector2.Distance(transform.position, enemy.transform.position);
        //     SetMovementBehaviours(clear: true, new SeekBehaviour(target), new FleeBehaviour(enemy.transform, 1/distance));
        // } else {
        //     SetMovementBehaviours(clear: true, new SeekBehaviour(target));
        // }
        // if ((chara.GetHealth() < 3
        //     || chara.GetHunger() < 7) && chara.FoodBag.Count != 0) {
        //     EatFood(chara.FoodBag[0]);
        // }
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