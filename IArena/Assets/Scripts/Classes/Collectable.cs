using UnityEngine;
using Commands;

public abstract class Collectable : MonoBehaviour,ICollectable {
    
    // [SerializeField] private int value;
    [SerializeField] private CollectableType type;
    protected Collider2D hitbox;

    // public int Value { get => value; }
    public CollectableType Type { get => type; }

    public abstract void Collect(CollectCommand _command);
    public abstract void Drop(CollectCommand _command);

    private void Awake() {
        hitbox = GetComponent<Collider2D>();
    }

    // private void Collect(CollectCommand _command) {
    //     gameObject.SetActive(false);
    // }

    // private void Drop(CollectCommand _command) {
    //     transform.position = _command.Self.transform.position;
    //     gameObject.SetActive(true);
    // }

    public enum CollectableType
    {
        treasure,
        weapon,
        food,
        equipment
    }


}