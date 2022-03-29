using UnityEngine;

public class MockAI: BrainBase {

    Transform target = null;

    private void Start() {
        
    }

    private void Update() {
        GetVision();
        if (target == null) {
            if (objectsInRange.Count > 0) {
                target = objectsInRange[0];
            }
        } else {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                chara.GetSpeed() * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform == target && other.gameObject.layer == LayerMask.NameToLayer("Collectable")) {
            Collect(other.GetComponent<Collectable>());
            target = null;
        }
    }
}