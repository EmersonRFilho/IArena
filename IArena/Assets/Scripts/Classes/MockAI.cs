using UnityEngine;

public class MockAI: BrainBase {

    Transform target = null;

    private void Start() {
        
    }

    private void Update() {
        if (chara.IsDead) return;
        GetVision();
        if (objectsInRange.Count > 0) {
            closest();
            target = objectsInRange[0];
        }
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                chara.GetSpeed() * Time.deltaTime);
        }
        if ((chara.GetHealth() < 3
            || chara.GetHunger() < 7) && chara.FoodBag.Count != 0) {
            EatFood(chara.FoodBag[0]);
        }
        if (target.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (Vector2.Distance(transform.position, target.position) > chara.Weapon.Range) {
                transform.position = Vector2.MoveTowards(transform.position, target.position, chara.GetSpeed() * Time.deltaTime);
            } else {
                Attack(target.GetComponent<CharacterBehaviors>(), chara.Weapon);
            }
            if (target.GetComponent<CharacterBehaviors>().IsDead) {
                target = null;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform == target && other.gameObject.layer == LayerMask.NameToLayer("Collectable")) {
            Collect(other.GetComponent<Collectable>());
            target = null;
        }
    }

    void closest() {
        objectsInRange.Sort((x, y) => Vector2.Distance(transform.position, x.position).CompareTo(Vector2.Distance(transform.position, y.position)));
    }
}