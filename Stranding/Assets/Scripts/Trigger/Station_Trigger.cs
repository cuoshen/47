using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    /// <summary>
    /// send signal to data storage when arrive at a station
    /// </summary>
    public class Station_Trigger : MonoBehaviour
    {

        // player data storage game object
        public GameObject source;
        // local data storage script object
        private Data_storage data_storage;
        

        // Start is called before the first frame update
        void Start()
        {
            // initialize data storage
            data_storage = source.GetComponent<Data_storage>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        

    }
}
