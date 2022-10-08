using Managers;
using UnityEngine;

namespace Commands
{
    public class CollectCommand : ICommand
    {

        private CharacterBehaviors self;
        private Collectable item;
        private LevelManager levelManager; 
        private Collider2D selfCollider, itemCollider;

        public CollectCommand(CharacterBehaviors self, Collectable item, LevelManager levelManager)
        {
            this.self = self;
            this.item = item;
            this.levelManager = levelManager;
            this.selfCollider = self.GetComponent<Collider2D>();
            this.itemCollider = item.GetComponent<Collider2D>();
        }

        public CharacterBehaviors Self { get => self; }
        public Collectable Item { get => item; }

        public void Execute()
        {
            if (self.IsDead) return;
            if (!levelManager.RemoveCommand(this)) return;
            // if(!self.gameObject.GetComponent<Collider2D>().IsTouching(item.gameObject.GetComponent<Collider2D>())) return;
            if(!selfCollider.bounds.Intersects(itemCollider.bounds)) return;
            switch (item.Type)
            {
                case Collectable.CollectableType.treasure:
                    // self.AddScore(item.GetComponent<Treasure>());
                    self.SendMessage("AddScore", this);
                    break;
                case Collectable.CollectableType.food:
                    // self.StoreFood(item.GetComponent<Food>());
                    self.SendMessage("StoreFood", this);
                    break;
                case Collectable.CollectableType.equipment:
                    // self.equipItem(item.GetComponent<Equipment>());
                    self.SendMessage("EquipItem", this);
                    break;
                case Collectable.CollectableType.weapon:
                    // self.addScore(item.GetComponent<Weapon>());
                    self.SendMessage("CollectWeapon", this);
                    break;
                case Collectable.CollectableType.other:
                    item.SendMessage("Collect");
                    break;
                default:
                    break;
            }
            item.Collect(this);
            // item.SendMessage("Collect", this);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}