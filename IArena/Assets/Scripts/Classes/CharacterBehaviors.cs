using UnityEngine;
using Core;
using Commands;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BaseStats))]
public class CharacterBehaviors: Chara {
    [SerializeField] private LayerMask detectable;

    private void Start() {
        if(Weapon == null) {
            GameObject fists = (GameObject) Instantiate(Resources.Load("Weapon/Dem Fists"), transform.position, Quaternion.identity);
            new CollectCommand(this, fists.GetComponent<Collectable>()).Execute();
        }
    }

    public LayerMask Detectable { get => detectable; }
}