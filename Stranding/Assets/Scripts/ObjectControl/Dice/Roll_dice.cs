using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    /// <summary>
    /// roll a dice
    /// </summary>

    public class Roll_dice : MonoBehaviour
    {
        // player data storage game object
        public GameObject source;
        // local data storage script object
        private Data_storage data_storage;


        private Rigidbody rigidbody;
        private Vector3 init_position;

        private bool _is_roll_finished;
        public bool is_roll_finished
        {
            get { return _is_roll_finished; }
        }

        // stores the outcome of last roll
        // if dice still rolling, this is the result of last roll
        // until current roll finished
        private int _last_roll_outcome;
        public int last_roll_outcome
        {
            get { return last_roll_outcome; }
        }

        // Start is called before the first frame update
        void Start()
        {
            // initialize data storage
            data_storage = source.GetComponent<Data_storage>();


            // get dice rigidbody
            rigidbody = GetComponent<Rigidbody>();
            init_position = transform.position;
            _last_roll_outcome = 0;
            roll();
        }

        // Update is called once per frame
        void Update()
        {
            _is_roll_finished = rigidbody.velocity.magnitude < 0.01f;
            // if dice stops
            if (_is_roll_finished)
            {
                GetDiceCount();
                // push dice roll outcome to data storage
                data_storage.Last_dice_outcome = _last_roll_outcome;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                roll();
            }
        }

        private void roll()
        {
            // reset position
            transform.position = init_position;

            // random init state to get random result
            transform.rotation = Random.rotation;

            // give roll speed and random torque
            rigidbody.velocity = new Vector3(0, -10, 0);
            rigidbody.AddTorque(new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30)));
        }

        //Coroutine to get dice count
        void GetDiceCount()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) > 1)
                _last_roll_outcome = 3;

            if (Vector3.Dot(-transform.forward, Vector3.up) > 1)
                _last_roll_outcome = 4;

            if (Vector3.Dot(transform.up, Vector3.up) >= 1)
                _last_roll_outcome = 2;

            if (Vector3.Dot(-transform.up, Vector3.up) > 1)
                _last_roll_outcome = 5;

            if (Vector3.Dot(transform.right, Vector3.up) > 1)
                _last_roll_outcome = 6;

            if (Vector3.Dot(-transform.right, Vector3.up) > 1)
                _last_roll_outcome = 1;
            
        }
    }
}
