using Managers;

namespace Commands
{
    public class CollectCommand : ICommand
    {

        private CharacterBehaviors self;
        private Collectable item;
        private LevelManager levelManager; 

        public CollectCommand(CharacterBehaviors self, Collectable item, LevelManager levelManager)
        {
            this.self = self;
            this.item = item;
            this.levelManager = levelManager;
        }

        public CharacterBehaviors Self { get => self; }
        public Collectable Item { get => item; }

        public void Execute()
        {
            if (self.IsDead) return;
            if (!levelManager.RemoveCommand(this)) return;
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