using UnityEngine;
using Core;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BaseStats))]
public class CharacterBehaviors: Chara {
    [SerializeField] private LayerMask detectable;

    public LayerMask Detectable { get => detectable; }
}