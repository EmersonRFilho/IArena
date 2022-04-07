using UnityEngine;
using System.Collections.Generic;
using Commands;

public abstract class BrainBase : MonoBehaviour {
    [SerializeField] protected CharacterBehaviors chara = null;
    protected List<Transform> objectsInRange = new List<Transform>();

    private void Awake() {
        chara = GetComponent<CharacterBehaviors>();
    }

    protected void GetVision() {
        if (chara.IsDead) return;
        objectsInRange.Clear();
        Collider2D[] found = Physics2D.OverlapCircleAll(chara.transform.position, chara.GetVisionRange(), chara.Detectable);
        foreach (Collider2D item in found)
        {
            if (item.transform != transform) {
                objectsInRange.Add(item.transform);
            }
        }
    }

    protected void Collect(Collectable item) {
        if (chara.IsDead) return;
        CollectCommand command = new CollectCommand(chara, item);
        // send command to event queue
        // execute command
        command.Execute();
    }

    protected void EatFood(Food food) {
        if (chara.IsDead) return;
        HealCommand command = new HealCommand(chara, food);
        //send command to event queue
        //execute command
        command.Execute();
    }

    protected void Attack(CharacterBehaviors target, Weapon weapon) {
        if (chara.IsDead) return;
        AttackCommand command = new AttackCommand(chara, target, weapon);
        // send command to event queue
        // execute command
        command.Execute();
    }
}