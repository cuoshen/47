using Stranding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop_up_system : MonoBehaviour
{
    [SerializeField]
    private Data_storage storage;
    [SerializeField]
    private GameObject popupNotification;
    [SerializeField]
    private GameObject choicesTab;

    private void Start()
    {
        popupNotification.SetActive(false);
        choicesTab.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnClosingNotification()
    {

    }
}
