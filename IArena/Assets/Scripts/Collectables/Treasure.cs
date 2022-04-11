using UnityEngine;
using Commands;

public class Treasure : Collectable
{
    [SerializeField] private int value = 50;

    public int Value { get => value; }

    protected override void Awake() 
    {
        base.Awake();
        setType(CollectableType.treasure);
    }

    public override void Collect(CollectCommand _command)
    {
        hitbox.enabled = false;
        gameObject.SetActive(false);
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }
}