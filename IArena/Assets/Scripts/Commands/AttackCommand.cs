using UnityEngine;
namespace Commands
{
    public class AttackCommand : ICommand
    {

        CharacterBehaviors self, target;
        Weapon weapon;

        public AttackCommand(CharacterBehaviors self, CharacterBehaviors target, Weapon weapon)
        {
            this.self = self;
            this.target = target;
            this.weapon = weapon;
        }

        public CharacterBehaviors Self { get => self; }
        public CharacterBehaviors Target { get => target; }
        public Weapon Weapon { get => weapon; }

        public async void Execute()
        {
            if (self.IsDead) return;
            // deal damage
            await weapon.Attack(this);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}