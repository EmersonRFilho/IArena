using UnityEngine;
using Commands;

public abstract class Collectable : MonoBehaviour, ICollectable {
    
    // [SerializeField] private int value;
    [SerializeField] private CollectableType type;

    // public int Value { get => value; }
    public CollectableType Type { get => type; }

    public abstract void Collect(CollectCommand _command);
    public abstract void Drop(CollectCommand _command);

    public enum CollectableType
    {
        treasure,
        weapon,
        food,
        equipment
    }


}