using UnityEngine;
using Commands;

public class Treasure : Collectable
{
    [SerializeField] private int value;

    public int Value { get => value; }

    public override void Collect(CollectCommand _command)
    {
        gameObject.SetActive(false);
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }
}