using System.Collections.Generic;
using UnityEngine;
using Commands;

namespace Core{
    public abstract class Chara : MonoBehaviour
    {
        private bool isDead;
        [SerializeField] private int score;

        private BaseStats stats;
        
        public bool IsDead { get => isDead; }
        public int Score { get => score; }
        public List<Food> FoodBag { get => foodBag; }
        public List<Collectable> Backpack { get => backpack; }
        public Weapon Weapon { get => weapon; }

        private Rigidbody2D rigid;
        private Collider2D col;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private Weapon weapon;
        private List<Food> foodBag = new List<Food>();
        private List<Collectable> backpack = new List<Collectable>();
        private float hungerTimer = 0f;

        private void Awake() {
            rigid = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            stats = GetComponent<BaseStats>();
            stats.BalanceStats();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;
            if (stats.Hunger > 0) {
                hungerTimer += Time.deltaTime;
                if (hungerTimer > 10) {
                    hungerTimer = 0f;
                    stats.Eat(-1);
                    print("Hunger level:" + stats.Hunger);
                }
            } else {
                KillPlayer();
            }
            if (stats.Health <= 0) {
                stats.HealHurt(-stats.Health);
                KillPlayer();
            }
        }

        private void KillPlayer()
        {
            isDead = true;
            rigid.bodyType = RigidbodyType2D.Static;
            col.enabled = false;
            spriteRenderer.color = Color.gray;
        }

        private void EquipItem(CollectCommand _command) {
            stats.BuffStat((Equipment) _command.Item);
            backpack.Add(_command.Item);
        }

        private void StoreFood(CollectCommand _command) {
            // if(!_command.Self.GetComponent<Collider2D>().IsTouching(_command.Item.GetComponent<Collider2D>())) return;
            foodBag.Add((Food) _command.Item);
        }

        private void TakeDamage(AttackCommand _command) {
            CharacterBehaviors _attacker = _command.Self;
            Weapon _weapon = _command.Self.Weapon;
            stats.HealHurt((_weapon.Damage + _attacker.GetAttack())*-1);
        }

        private void RestoreHealth(HealCommand _command) {
            print("eating food");
            foodBag.Remove(_command.Food);
            stats.HealHurt(_command.Food.HealAmmount);
            stats.Eat(10 - stats.Hunger);
        }

        private void AddScore(CollectCommand _command) {
                Treasure treasure = _command.Item.GetComponent<Treasure>();
                this.score += treasure.Value;
                backpack.Add(treasure);
                print(score);
        }

        private void CollectWeapon(CollectCommand _command) {
            // if(!_command.Item.GetComponent<Collider2D>().IsTouching(_command.Self.GetComponent<Collider2D>())) return;
            if(weapon){
                // weapon.SendMessage("Drop", _command);
                weapon.Drop(_command);
            }
            weapon = (Weapon) _command.Item;
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

        public int GetVisionRange() {
            return stats.Vision;
        }

        public int GetHunger() {
            return stats.Hunger;
        }
        #endregion
    }
}