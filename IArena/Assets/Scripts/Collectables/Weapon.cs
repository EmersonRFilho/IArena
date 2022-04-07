using UnityEngine;
using Commands;

public class Weapon : Collectable
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    private float attackTime;
    private bool canAttack;

    public bool CanAttack { get => canAttack; }
    public int Damage { get => damage; }
    public float Cooldown { get => cooldown; }
    public float Range { get => range; }

    private void Start() {
        // GetComponent<SpriteRenderer>().sprite = weaponData.Sprite;
        attackTime = cooldown;
    }
    private void Update() {
        canAttack = attackTime >= cooldown;
        if(attackTime < cooldown) {
            attackTime += Time.deltaTime;
        }
    }

    public override void Collect(CollectCommand _command)
    {
        //range check
        if(GetComponent<Collider2D>().IsTouching(_command.Self.GetComponent<Collider2D>())) {
            hitbox.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }

    public void Attack(AttackCommand _command) {
        if(Vector2.Distance(_command.Self.transform.position, _command.Target.transform.position) <= range) {
            attackTime = 0;
            _command.Target.SendMessage("TakeDamage", _command);
        }
    }
}