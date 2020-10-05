using Stranding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Stranding
{
    class Pop_up_system : MonoBehaviour
    {
        [SerializeField]
        private Data_storage storage;
        [SerializeField]
        private GameObject notification;
        [SerializeField]
        private Text notificationText;
        [SerializeField]
        private GameObject choicesTab;
        [SerializeField]
        private List<Text> choiceButtonTexts;

        private int currentNotificationIndex = 0;

        private BlockEvent _currentEvent;
        public BlockEvent CurrentEvent
        {
            get
            {
                return _currentEvent;
            }
            set
            {
                if (value is NotificationEvent)
                {
                    _currentEvent = value;
                    StartSendingNotification((value as NotificationEvent).message);
                }
                else if (value is ChoiceEvent)
                {
                    _currentEvent = value;
                    choicesTab.SetActive(true);
                    StartSendingNotification((value as ChoiceEvent).prompt);
                    // Update the option text accordingly
                    UpdateOptionText((value as ChoiceEvent));
                }
                else if (value is MultiNotificationEvent)
                {
                    _currentEvent = value;
                    currentNotificationIndex = 0;
                    StartSendingNotification((value as MultiNotificationEvent).messages[currentNotificationIndex]);
                }
                else
                {
                    // Assignment failed
                    _currentEvent = null;
                }
            }
        }

        private string message;
        private bool isSendingText = false;
        private int currentTextCounter = 0;
        private float textSendingSpeed = 50f;
        private float currentTextPosition = 0;

        private void Start()
        {
            notification.SetActive(false);
            choicesTab.SetActive(false);
        }

        private void Update()
        {
            if (notification.activeInHierarchy && isSendingText)
            {
                currentTextPosition += textSendingSpeed * Time.deltaTime;
                currentTextCounter = (int)currentTextPosition;
                if (currentTextCounter > message.Length)
                {
                    isSendingText = false;
                }
                else
                {
                    notificationText.text = message.Substring(0, currentTextCounter);
                }
            }
        }

        private void UpdateOptionText(ChoiceEvent choiceEvent)
        {
            for (int i = 0; i < choiceEvent.outcomes.Count; i ++)
            {
                choiceButtonTexts[i].text = choiceEvent.outcomes[i].optionText;
            }
        }

        private void StartSendingNotification(string message)
        {
            if (message == "")
            {
                Debug.LogError("Message cannot be empty");
            }
            else
            {
                notification.SetActive(true);
                this.message = message;
                isSendingText = true;
                currentTextPosition = 0f;
            }
        }

        public void OnClosingNotification()
        {
            notification.SetActive(false);
            choicesTab.SetActive(false);
            if (CurrentEvent != null)
            {
                if (CurrentEvent is NotificationEvent)
                {
                    CurrentEvent.isComplete = true;
                }
                else if (CurrentEvent is MultiNotificationEvent)
                {
                    if (currentNotificationIndex >= 1)
                    {
                        CurrentEvent.isComplete = true;
                    }
                    else
                    {
                        currentNotificationIndex++;
                        StartSendingNotification((CurrentEvent as MultiNotificationEvent).messages[currentNotificationIndex]);
                    }
                }
            }
        }

        public void OnChoosingOption(int choice)
        {
            // We have made a choice so close the window and be ready for execution of outcome
            notification.SetActive(false);
            choicesTab.SetActive(false);
            if (CurrentEvent is ChoiceEvent)
            {
                (CurrentEvent as ChoiceEvent).Choice = choice;
            }
        }
    }
}

