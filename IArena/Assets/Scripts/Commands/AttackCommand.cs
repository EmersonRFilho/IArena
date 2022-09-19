using UnityEngine;
using Managers;

namespace Commands
{
    public class AttackCommand : ICommand
    {

        CharacterBehaviors self, target;
        LevelManager levelManager;

        public AttackCommand(CharacterBehaviors self, CharacterBehaviors target, LevelManager levelManager)
        {
            this.self = self;
            this.target = target;
            this.levelManager = levelManager;
        }

        public CharacterBehaviors Self { get => self; }
        public CharacterBehaviors Target { get => target; }

        public async void Execute()
        {
            // death check
            if (self.IsDead) return;
            // queueCheck
            if (!levelManager.RemoveCommand(this)) return;
            // deal damage
            if (!self.Weapon.Attacked && Vector2.Distance(self.transform.position, target.transform.position) < self.Weapon.Range){
                await self.Weapon.Attack(this);
            }
            return;
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}