using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stranding
{
    class NotificationEvent : BlockEvent
    {
        public readonly string message;

        public NotificationEvent(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Display some text and / or images with the pop up window
        /// </summary>
        public override void Execute(Player player)
        {
            Pop_up_system popupSystem = GameObject.FindObjectOfType<Pop_up_system>().GetComponent<Pop_up_system>();
            popupSystem.CurrentEvent = this;
        }
    }
}
