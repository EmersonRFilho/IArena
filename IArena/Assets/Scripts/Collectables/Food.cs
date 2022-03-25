using Commands;
using UnityEngine;

public class Food : Collectable
{
    [SerializeField] private int healAmmount;

    public override void Collect(CollectCommand _command)
    {
        gameObject.SetActive(false);
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }

    private void Consume(HealCommand _command) {
        gameObject.SetActive(false);
        _command.Self.SendMessage("RestoreHealth", _command);
    }
}