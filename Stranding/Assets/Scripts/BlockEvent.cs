using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding 
{
    /// <summary>
    /// Base class for a block
    /// </summary>
    abstract class BlockEvent
    {
        public bool isComplete = false;
        public abstract void Execute(Player player);
    }
}
