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
        public BlockEvent Event
        {
            get;
            private set;
        }

        private void Start()
        {
            Event = new DebugBlockEvent();
        }
    }
}

