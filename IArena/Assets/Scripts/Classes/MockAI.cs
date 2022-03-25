using UnityEngine;

public class MockAI: BrainBase {

    Transform target = null;

    private void Start() {
        
    }

    private void Update() {
        if (target == null) {
            GetVision();
            target = objectsInRange[0];
        } else {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                chara.GetSpeed() * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other == target && other.gameObject.layer == 6) {
            Collect(other.GetComponent<Collectable>());
        }
    }
}