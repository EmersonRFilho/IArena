using Commands;
using UnityEngine;

public class Food : Collectable
{
    [SerializeField] private int healAmmount = 10;

    protected override void Awake() {
        base.Awake();
        setType(CollectableType.food);
    }

    public int HealAmmount { get => healAmmount; }

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

    public void Consume(HealCommand _command) {
        gameObject.SetActive(false);
        // _command.Self.SendMessage("RestoreHealth", _command);
    }
}