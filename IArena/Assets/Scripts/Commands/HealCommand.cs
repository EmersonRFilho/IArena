using UnityEngine;
using Managers;

namespace Commands
{
    public class HealCommand : ICommand
    {
        CharacterBehaviors self;
        Food food;
        LevelManager levelManager;

        public HealCommand(CharacterBehaviors self, Food food, LevelManager levelManager)
        {
            this.self = self;
            this.food = food;
            this.levelManager = levelManager;
        }

        public CharacterBehaviors Self { get => self; set => self = value; }
        public Food Food { get => food; set => food = value; }

        public void Execute()
        {
            if (self.IsDead) return;
            if (!levelManager.RemoveCommand(this)) return;
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