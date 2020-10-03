using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}

