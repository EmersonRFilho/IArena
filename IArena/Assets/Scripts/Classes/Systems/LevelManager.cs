using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {

        ActionQueue queue;

        private void Awake() {
            queue = new ActionQueue();
        }

        public void QueueCommand(ICommand command)
        {
            queue.AddCommand(command);
        }

        public bool RemoveCommand(ICommand command)
        {
            return queue.PopCommand(command);
        }
    }
}