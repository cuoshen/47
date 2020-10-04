using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stranding
{
    /// <summary>
    /// used to control dice outcome ui
    /// </summary>

    public class Dice_outcome_control : MonoBehaviour
    {
        // player data storage game object
        public GameObject source;
        // local data storage script object
        private Data_storage data_storage;

        public GameObject last_dice_outcome_text;

        private int last_dice_outcome;

        // Start is called before the first frame update
        void Start()
        {
            // initialize data storage
            data_storage = source.GetComponent<Data_storage>();
        }

        // Update is called once per frame
        void Update()
        {
            last_dice_outcome = data_storage.Last_dice_outcome;
            last_dice_outcome_text.GetComponent<Text>().text = last_dice_outcome.ToString();
        }
    }
}