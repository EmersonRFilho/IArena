using BaseCharacter;
namespace Commands
{
    public class CollectCommand : ICommand
    {

        private CharacterBehaviors self;
        private Collectable item;

        public CollectCommand(CharacterBehaviors self, Collectable item)
        {
            this.self = self;
            this.item = item;
        }

        public CharacterBehaviors Self { get => self; }
        public Collectable Item { get => item; }

        public void Execute()
        {
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
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}