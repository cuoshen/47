using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stranding.Testing; // Delete upon finish

namespace Stranding
{
    /// <summary>
    /// Engine attachable container for events
    /// </summary>
    class Block : MonoBehaviour
    {
        public Queue<BlockEvent> Events = new Queue<BlockEvent>();
    }
}

