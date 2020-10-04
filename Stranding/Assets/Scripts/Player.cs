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
        private float speed = 5.0f;

        public Vector3 destination;

        private void Update()
        {
            // Move towards the destination
            
        }

        public readonly string Name = "Nella";
    }
}
