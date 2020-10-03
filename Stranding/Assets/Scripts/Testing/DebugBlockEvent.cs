using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding.Testing
{
    class DebugBlockEvent : BlockEvent
    {
        public override void Execute(Player player)
        {
            Debug.Log("Debug event triggered by player " + player.Name);
        }
    }
}
