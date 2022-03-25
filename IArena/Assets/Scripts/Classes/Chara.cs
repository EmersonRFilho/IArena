using System.Collections.Generic;
using UnityEngine;
using Commands;

namespace BaseCharacter{
    public abstract class Chara : MonoBehaviour
    {
        private bool isDead;
        private int score;

        private BaseStats stats;
        
        public bool IsDead { get => isDead; }

        private Weapon weapon;

        private List<Collectable> backpack = new List<Collectable>();

        private void Awake() {
            stats = GetComponent<BaseStats>();
            stats.BalanceStats();
        }

        // Start is called before the first frame update
        void Start()
        {
            // if(target == null)
            //     target = FindObjectOfType<Collectable>().transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void EquipItem(CollectCommand _command) {
            stats.BuffStat((Equipment) _command.Item);
            backpack.Add(_command.Item);
        }

        private void StoreFood(CollectCommand _command) {
            backpack.Add(_command.Item);
        }

        private void TakeDamage(AttackCommand _command) {
            CharacterBehaviors _attacker = _command.Self;
            Weapon _weapon = _command.Weapon;
            stats.HealHurt((_weapon.Damage + _attacker.GetAttack())*-1);
        }

        private void RestoreHealth(HealCommand _command) {
            backpack.Remove(_command.Food);
            stats.HealHurt(_command.Food.HealAmmount);
        }

        private void AddScore(CollectCommand _command) {
            try
            {
                Treasure treasure = _command.Item.GetComponent<Treasure>();
                // this.score += treasure.Value;
                backpack.Add(treasure);
                print(score);
            } catch {
                print("not a treasure");
            }
        }

        private void CollectWeapon(CollectCommand _command) {
            if(weapon){
                weapon.Drop(_command);
                weapon = (Weapon) _command.Item;
            }
        }

        public List<Collectable> getBackpack() {
            if (isDead)
                return backpack;
            return null;
        }

        #region Getters/Setters
        public int GetAttack() {
            return stats.Attack;
        }
        public int GetSpeed() {
            return stats.Speed;
        }
        public int GetHealth() {
            return stats.Health;
        }

        public int getVisionRange() {
            return stats.Vision;
        }
        #endregion
    }
}