using UnityEngine;
using Commands;
using System.Threading.Tasks;
using System;

public class Weapon : Collectable
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private float attackTime;
    private bool attacked;

    public bool Attacked { get => attacked; }
    public int Damage { get => damage; }
    public float Cooldown { get => cooldown; }
    public float Range { get => range; }

    protected override void Awake() {
        base.Awake();
        setType(CollectableType.weapon);
    }

    public override void Collect(CollectCommand _command)
    {
        //range check
        // if(GetComponent<Collider2D>().IsTouching(_command.Self.GetComponent<Collider2D>())) {
            hitbox.enabled = false;
            gameObject.SetActive(false);
            // transform.position = new Vector2(transform.position.x + 10000, transform.position.y + 10000);
        // }
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }

    public async Task Attack(AttackCommand _command) {
        float distance = Vector2.Distance(_command.Self.transform.position, _command.Target.transform.position);
        // print(String.Format("distance from {0} to {1}: {2}", _command.Self.name, _command.Target.name, distance));
        if(distance <= range && !attacked) {
            attacked = true;
            Rigidbody2D selfRigid = _command.Self.GetComponent<Rigidbody2D>();
            selfRigid.constraints = RigidbodyConstraints2D.FreezePosition;
            await Task.Delay((int) attackTime*1000);
            if (_command.Self.IsDead) return;
            _command.Target.SendMessage("TakeDamage", _command);
            print(_command.Self.name + " dealt damage to " + _command.Target.name);
            StartCoroutine("AttackTimer");
            await Task.Yield();
        }
    }

    IEnumerator AttackTimer() {
        yield return new WaitForSeconds(cooldown);
        attacked = false;
    }

    // private async Task AttackTimer() {
    //     await Task.Delay((int) cooldown * 1000);
    //     attacked = false;
    //     await Task.Yield();
    // }
}