using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class GameEndingEvent : BlockEvent
    {
        public override void Execute(Player player)
        {
            Time.timeScale = 0;
        }
    }
}

