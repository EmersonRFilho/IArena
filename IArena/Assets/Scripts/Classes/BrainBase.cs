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
        Collider2D[] found = Physics2D.OverlapCircleAll(chara.transform.position, chara.getVisionRange(), chara.Detectable);
        foreach (Collider2D item in found)
        {
            if (item.transform != transform)
            objectsInRange.Add(item.transform);
        }
    }

    protected void Collect(Collectable item) {
        CollectCommand command = new CollectCommand(chara, item);
        // send command to event queue
        // execute command
        command.Execute();
    }

    protected void Attack(CharacterBehaviors target, Weapon weapon) {
        AttackCommand command = new AttackCommand(chara, target, weapon);
        // send command to event queue
        // execute command
        command.Execute();
    }

    protected void EatFood(Food food) {
        //send command to event queue
        //execute command
    }
}