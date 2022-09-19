using UnityEngine;
using System.Collections.Generic;
using Commands;
using Movement;
using Core;
using Managers;

[RequireComponent(typeof(CharacterBehaviors))]
[RequireComponent(typeof(SteeringBehaviourBase))]
public class BrainBase : MonoBehaviour {
    [SerializeField] protected CharacterBehaviors chara = null;
    private SteeringBehaviourBase movement;
    protected List<Transform> objectsInRange = new List<Transform>();
    private LevelManager levelManager;

    private void Start() {
        chara = GetComponent<CharacterBehaviors>();
        levelManager = FindObjectOfType<LevelManager>();
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
            if (item.gameObject.layer == LayerMask.NameToLayer("Player") && item.GetComponent<Chara>().IsDead) {
                objectsInRange.Remove(item.transform);
            }
        }
    }

    protected void SetMovementBehaviours(params Steering[] behaviours) {
        if (chara.IsDead) return;
        
        movement.Steerings.Clear();
        foreach(Steering behaviour in behaviours) {
            movement.Steerings.Add(behaviour);
        }
    }

    protected void Collect(Collectable item) {
        if (chara.IsDead) return;
        CollectCommand command = new CollectCommand(chara, item, levelManager);
        // send command to event queue
        levelManager.QueueCommand(command);
        // execute command
        // command.Execute();
    }

    protected void EatFood(Food food) {
        if (chara.IsDead) return;
        HealCommand command = new HealCommand(chara, food, levelManager);
        //send command to event queue
        levelManager.QueueCommand(command);
        //execute command
        command.Execute();
    }

    protected void Attack(CharacterBehaviors target) {
        if (chara.IsDead) return;
        AttackCommand command = new AttackCommand(chara, target, levelManager);
        // send command to event queue
        levelManager.QueueCommand(command);
        // execute command
        command.Execute();
    }
}