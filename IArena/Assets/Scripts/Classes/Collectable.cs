using UnityEngine;
using Commands;

public abstract class Collectable : MonoBehaviour, ICollectable {
    
    // [SerializeField] private int value;
    // [SerializeField] private IncreasesStat stat = IncreasesStat.none;
    [SerializeField] private CollectableType type;

    // public IncreasesStat Stat { get => stat; }
    // public int Value { get => value; }
    public CollectableType Type { get => type; }

    public abstract void Collect(CollectCommand _command);
    public abstract void Drop(CollectCommand _command);

    public enum IncreasesStat {
        Attack,
        Speed,
        Vision,
        none
    }

    public enum CollectableType
    {
        treasure,
        weapon,
        food,
        equipment
    }


}