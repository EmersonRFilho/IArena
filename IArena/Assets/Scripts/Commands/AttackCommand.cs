using UnityEngine;
namespace Commands
{
    public class AttackCommand : ICommand
    {

        CharacterBehaviors self, target;

        public AttackCommand(CharacterBehaviors self, CharacterBehaviors target)
        {
            this.self = self;
            this.target = target;
        }

        public CharacterBehaviors Self { get => self; }
        public CharacterBehaviors Target { get => target; }

        public async void Execute()
        {
            if (self.IsDead) return;
            // deal damage
            if(!self.Weapon.Attacked){
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