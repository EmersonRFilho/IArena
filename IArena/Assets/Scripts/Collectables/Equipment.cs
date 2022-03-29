using UnityEngine;
using Commands;

public class Equipment : Collectable
{
    [SerializeField]private int buff;
    [SerializeField]private IncreasesStat stat;

    public int Buff { get => buff; }
    public IncreasesStat Stat { get => stat; }

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

    public enum IncreasesStat {
        Attack,
        Speed,
        Vision,
        none
    }
}