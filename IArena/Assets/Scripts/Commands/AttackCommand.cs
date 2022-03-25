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

        public void Execute()
        {
            // Check Range
                // Check Attack Speed
                    //deal damage
            target.SendMessage("TakeDamage", this);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}