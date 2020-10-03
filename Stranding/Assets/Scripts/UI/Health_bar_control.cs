using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    /// <summary>
    /// used to control progress info ui
    /// </summary>
    
    public class Health_bar_control : MonoBehaviour
    {

        // player data storage game object
        public GameObject source;
        // local data storage script object
        private Data_storage data_storage;

        public GameObject health_image;

        private int health;

        // Start is called before the first frame update
        void Start()
        {
            // initialize data storage
            data_storage = source.GetComponent<Data_storage>();
        }

        // Update is called once per frame
        void Update()
        {
            // pull data from data storage
            health = data_storage.Health;

            // update health bar image
            health_image.GetComponent<RectTransform>().sizeDelta = new Vector2(health, 20);
        }
    }
}

