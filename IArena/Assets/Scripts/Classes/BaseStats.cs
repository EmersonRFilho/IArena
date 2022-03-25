using UnityEngine;
public class BaseStats: MonoBehaviour {
    private int health = 20;
    private int sanity = 10;
    [SerializeField]private int attack;
    [SerializeField]private int speed;
    [SerializeField]private int vision = 10;
    private int hunger = 10;
    private int statCap = 21;

    public int Health { get => health; }
    public int Attack { get => attack; }
    public int Speed { get => speed; }
    public int Vision { get => vision; }

    public void BuffStat(Equipment equipment) {
        switch (equipment.Stat)
        {
            case Equipment.IncreasesStat.Attack:
                this.attack += equipment.Buff;
                break;
            case Equipment.IncreasesStat.Speed:
                this.speed += equipment.Buff;
                break;       
            case Equipment.IncreasesStat.Vision:
                this.vision += equipment.Buff;
                break;       
            default:
                break;
        }
    }

    public void BalanceStats() {
        int sumCheck = Attack + Speed + Vision;
        if(sumCheck > statCap) {
            float highest = Mathf.Max(attack, speed, vision);
            if (attack == highest) {
                attack--;
            } else if (speed == highest) {
                speed--;
            } else if (vision == highest) {
                vision--;
            }
            BalanceStats();
        }
    }

    public void Eat(int value) {
        hunger += value;
        if(hunger > 10) hunger = 10;
    }

    public void HealHurt(int value) {
        health += value;
    }
    
}