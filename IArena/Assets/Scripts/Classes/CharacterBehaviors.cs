using UnityEngine;
using BaseCharacter;
public class CharacterBehaviors: Chara {
    [SerializeField] private LayerMask detectable;

    public LayerMask Detectable { get => detectable; }
}