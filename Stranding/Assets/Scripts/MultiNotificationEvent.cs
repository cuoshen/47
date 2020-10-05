using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stranding
{
    class MultiNotificationEvent : BlockEvent
    {
        public readonly List<string> messages = new List<string>();

        public MultiNotificationEvent(List<string> messages)
        {
            this.messages = messages;
        }

        public override void Execute(Player player)
        {
            Pop_up_system popupSystem = GameObject.FindObjectOfType<Pop_up_system>().GetComponent<Pop_up_system>();
            popupSystem.CurrentEvent = this;
        }
    }
}
