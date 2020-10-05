using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class Player : MonoBehaviour
    {
        public int MaxSpeed { get; set; } = 1;
        public int MapPosition { get; set; } = -1; // Start at -1 so that we land on the correct block
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

        private Vector3 _destination = Vector3.zero;
        public Vector3 Destination
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

        private void Update()
        {
            // Move towards the destination
            if (Vector3.Distance(transform.position, Destination) <= displacementError)
            {
                isAtDestination = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(Destination - transform.position, Vector3.up);
            }
        }

        public readonly string Name = "Nella";
    }
}
