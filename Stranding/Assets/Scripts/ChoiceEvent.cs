using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stranding
{
    struct EventOption
    {
        public EventOption(string text, NotificationEvent outcome)
        {
            optionText = text;
            this.eventOutcome = outcome;
        }
        public string optionText;
        public NotificationEvent eventOutcome;
    }

    class ChoiceEvent : BlockEvent
    {
        private Player player;
        public readonly string prompt;
        private int optionsCount;
        public readonly List<EventOption> outcomes;
        private int _choice = -1;
        public int Choice
        {
            get
            {
                return _choice;
            }
            set
            {
                if (value >= 0 && value < optionsCount)
                {
                    _choice = value;
                    if (outcomes[_choice].eventOutcome.message == "End")
                    {
                        GameLogic logic = GameObject.FindObjectOfType<GameLogic>().GetComponent<GameLogic>();
                        logic.EndGame();
                    }
                    outcomes[_choice].eventOutcome.Execute(player);
                }
                isComplete = true;
            }
        }

        public ChoiceEvent(string prompt, List<EventOption> outcomes)
        {
            this.prompt = prompt;
            this.outcomes = outcomes;
            optionsCount = outcomes.Count;
        }

        /// <summary>
        /// Force the player to make a choice
        /// </summary>
        public override void Execute(Player player)
        {
            Pop_up_system popupSystem = GameObject.FindObjectOfType<Pop_up_system>().GetComponent<Pop_up_system>();
            popupSystem.CurrentEvent = this;
            this.player = player;
        }
    }
}
