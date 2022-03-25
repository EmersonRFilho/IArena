using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "IArena/FoodData", order = 0)]
public class FoodData : ScriptableObject {
    [SerializeField] Sprite sprite;
    [SerializeField] int healAmmount;

    public Sprite Sprite { get => sprite; }
    public int HealAmmount { get => healAmmount; }
}