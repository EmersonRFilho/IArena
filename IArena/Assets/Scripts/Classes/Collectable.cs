using UnityEngine;
using Commands;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Collectable : MonoBehaviour,ICollectable {
    
    // [SerializeField] private int value;
    private CollectableType type;
    protected Collider2D hitbox;

    // public int Value { get => value; }
    public CollectableType Type { get => type; }

    public abstract void Collect(CollectCommand _command);
    public abstract void Drop(CollectCommand _command);

    protected virtual void Awake() {
        hitbox = GetComponent<Collider2D>();
        if(!hitbox.isTrigger) hitbox.isTrigger = true;
    }

    protected void setType(CollectableType type) {
        this.type = type;
    }

    public enum CollectableType
    {
        treasure,
        weapon,
        food,
        equipment,
        other
    }


}