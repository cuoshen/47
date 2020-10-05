using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class Player : MonoBehaviour
    {
        // player data storage game object
        public GameObject source;
        // local data storage script object
        private Data_storage data_storage;


        public int MaxSpeed { get; set; } = 6;
        public int MapPosition { get; set; } = 0; // Start at -1 so that we land on the correct block
        private int _health;
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _health = value;
                }
                else
                {
                    Debug.LogError("Health must be a value between 0 and 100");
                }
            }
        }

        [SerializeField]
        private float speed = 10.0f;
        [SerializeField]
        private float displacementError = 0.1f;

        private Queue<Vector3> _destination;
        public Queue<Vector3> Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                isAtDestination = false;
                _destination = value;
            }
        }
        public bool isAtDestination { get; private set; } = true;
        private Vector3 current_destination;

        private void Start()
        {
            // initialize data storage
            data_storage = source.GetComponent<Data_storage>();

            _destination = new Queue<Vector3>();
            current_destination = transform.position;
        }

        private void Update()
        {
            if (data_storage.Is_at_station)
            {
                // clear all further steps if arrive at a station
                while(Destination.Count > 0)
                {
                    Destination.Dequeue();
                }
            }


            if (Destination.Count > 0 && isAtDestination)
            {
                current_destination = Destination.Dequeue();
            }

            // Move towards the destination
            if (Vector3.Distance(transform.position, current_destination) <= displacementError)
            {
                isAtDestination = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, current_destination, speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(current_destination - transform.position, Vector3.up);
                isAtDestination = false;
            }
        }

        public readonly string Name = "Nella";
    }
}
