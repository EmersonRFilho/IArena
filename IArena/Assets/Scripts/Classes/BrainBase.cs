using UnityEngine;
using System.Collections.Generic;
using Commands;
using Movement;
using Core;
using Managers;

/// <summary>
/// Any player object must inherit from this class in order to function properly.
/// </summary>
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

    /// <summary>
    /// Returns all detectable transforms in a list called objectsInRange
    /// which can be accessed by the object and manipulated as you please.
    /// </summary>
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

    /// <summary>
    /// Updates object's movement list.
    /// </summary>
    /// <param name="behaviours">
    /// MovementBehaviour object that the object will perform,
    /// you may pass as many as you like at once.
    /// </param>
    protected void SetMovementBehaviours(params Steering[] behaviours) {
        if (chara.IsDead) return;
        
        movement.Steerings.Clear();
        foreach(Steering behaviour in behaviours) {
            movement.Steerings.Add(behaviour);
        }
    }

    /// <summary>
    /// Collects a Collectable object and stores it depending on the type o collectable
    /// Food objects will go to the FoodBag
    /// Treasures and Equipment will go to the BackPack
    /// Weapons will go to the Weapon slot, picking a new one drops the current Weapon
    /// </summary>
    /// <param name="item"></param>
    protected void Collect(Collectable item) {
        if (chara.IsDead) return;
        CollectCommand command = new CollectCommand(chara, item, levelManager);
        // send command to event queue
        levelManager.QueueCommand(command);
        // execute command
        // command.Execute();
    }

    /// <summary>
    /// Consumes a food from your FoodBag.
    /// A Food restores health and lessens hunger.
    /// </summary>
    /// <param name="food">A Food object from your FoodBag</param>
    protected void EatFood(Food food) {
        if (chara.IsDead) return;
        HealCommand command = new HealCommand(chara, food, levelManager);
        //send command to event queue
        levelManager.QueueCommand(command);
        //execute command
        command.Execute();
    }

    /// <summary>
    /// Attacks another Character when in weapon range.
    /// </summary>
    /// <param name="target">Character that will receive the attack.</param>
    protected void Attack(CharacterBehaviors target) {
        if (chara.IsDead) return;
        AttackCommand command = new AttackCommand(chara, target, levelManager);
        // send command to event queue
        levelManager.QueueCommand(command);
        // execute command
        command.Execute();
    }
}