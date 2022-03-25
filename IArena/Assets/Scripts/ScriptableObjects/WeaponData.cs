using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "IArena/WeaponData", order = 0)]
public class WeaponData : ScriptableObject {
    [SerializeField] private Sprite sprite;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;

    public Sprite Sprite { get => sprite; }
    public int Damage { get => damage; }
    public float Range { get => range; }
    public float Cooldown { get => cooldown; }
}