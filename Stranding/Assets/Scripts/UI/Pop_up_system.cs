using Stranding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private float textSendingSpeed = 14f;
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

        public void StartSendingNotification(string message)
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
            if (CurrentEvent != null)
            {
                if (CurrentEvent is NotificationEvent)
                {
                    CurrentEvent.isComplete = true;
                }
            }
        }

        public void OnChoosingOption()
        {

        }
    }
}

