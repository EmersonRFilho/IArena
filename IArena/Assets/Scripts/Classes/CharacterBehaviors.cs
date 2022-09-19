using UnityEngine;
using Core;
using Commands;
using Managers;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BaseStats))]
public class CharacterBehaviors: Chara {
    [SerializeField] private LayerMask detectable;
    private LevelManager levelManager;

    private void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        if(Weapon == null) {
            GameObject fists = (GameObject) Instantiate(Resources.Load("Weapon/Dem Fists"), transform.position, Quaternion.identity);
            levelManager.QueueCommand(new CollectCommand(this, fists.GetComponent<Collectable>(), levelManager));
        }
    }

    public LayerMask Detectable { get => detectable; }
}