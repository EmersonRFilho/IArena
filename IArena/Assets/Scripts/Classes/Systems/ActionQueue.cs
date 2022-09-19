using System.Collections;
using System.Collections.Generic;
using Commands;

namespace Managers
{
    public class ActionQueue
    {

        private List<ICommand> queue;
        private List<ICommand> history;

        public ActionQueue()
        {
            queue = new List<ICommand>();
            history = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            queue.Add(command);
            command.Execute();
        }

        public bool PopCommand(ICommand command)
        {
            if(queue.Contains(command))
            {
                queue.Remove(command);
                history.Add(command);
                return true;
            }
            return false;
        }
    }
}