using UnityEngine;
using System.Collections.Generic;
using Commands;
using Movement;
using Core;

[RequireComponent(typeof(CharacterBehaviors))]
[RequireComponent(typeof(SteeringBehaviourBase))]
public abstract class BrainBase : MonoBehaviour {
    [SerializeField] protected CharacterBehaviors chara = null;
    private SteeringBehaviourBase movement;
    protected List<Transform> objectsInRange = new List<Transform>();

    private void Start() {
        chara = GetComponent<CharacterBehaviors>();
        movement = GetComponent<SteeringBehaviourBase>();
        movement.MaxAcceleration = chara.GetSpeed();
    }

    protected void GetVision() {
        if (chara.IsDead) return;
        objectsInRange.Clear();
        Collider2D[] found = Physics2D.OverlapCircleAll(transform.position, chara.GetVisionRange(), chara.Detectable);
        foreach (Collider2D item in found)
        {
            if (item.transform != transform) {
                objectsInRange.Add(item.transform);
            }
        }
    }

    protected void SetMovementBehaviours(bool clear = true, params Steering[] behaviours) {
        if (clear) movement.Steerings.Clear();
        foreach(Steering behaviour in behaviours) {
            movement.Steerings.Add(behaviour);
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

    protected void Attack(CharacterBehaviors target) {
        if (chara.IsDead) return;
        AttackCommand command = new AttackCommand(chara, target);
        // send command to event queue
        // execute command
        command.Execute();
    }
}