using UnityEngine;

namespace Commands
{
    public class HealCommand : ICommand
    {
        CharacterBehaviors self;
        Food food;

        public HealCommand(CharacterBehaviors self, Food food)
        {
            this.self = self;
            this.food = food;
        }

        public CharacterBehaviors Self { get => self; set => self = value; }
        public Food Food { get => food; set => food = value; }

        public void Execute()
        {
            if (self.IsDead) return;
            if (self.FoodBag.Contains(food)) {
                self.SendMessage("RestoreHealth", this);
                // food.SendMessage("Consume", this);
                food.Consume(this);
            }
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}