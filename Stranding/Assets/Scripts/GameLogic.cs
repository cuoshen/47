using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class GameLogic : MonoBehaviour
    {
        /// <summary>
        /// Predefine a desirable size for better performance
        /// </summary>
        private const int desirableMapSize = 16;

        [SerializeField]
        private GameObject circleMasterDirectory;
        /// <summary>
        /// We have a circular map represented by a list of blocks,
        /// where the last in the list loops back to the first.
        /// </summary>
        private List<Block> circle = new List<Block>(desirableMapSize);
        private BlockEvent currentEvent;

        [SerializeField]
        private Player player;
        [SerializeField]
        private Data_storage storage;

        public int Turn { get; private set; } = 0;
        public bool ReadyForNextTurn = true;
        private float timeSinceLastTurn = 0f;
        private float cooldownTime = 0.5f;

        private void Start()
        {
            // Parse out the blocks in the circle
            Block[] blocksUnderDirectory = circleMasterDirectory.GetComponentsInChildren<Block>();
            foreach (Block b in blocksUnderDirectory)
            {
                circle.Add(b);
            }
        }

        private void Update()
        {
            timeSinceLastTurn += Time.deltaTime;
            while (timeSinceLastTurn >= cooldownTime)
            {
                timeSinceLastTurn -= cooldownTime;
                ReadyForNextTurn = true;
            }
            if (Input.GetKey(KeyCode.Space) && ReadyForNextTurn)
            {
                TakeNextTurn();
            }
        }

        private void TakeNextTurn()
        {
            ReadyForNextTurn = false;
            int newMapPosition = player.MapPosition + Random.Range(0, player.MaxSpeed) + 1; // TODO: maybe implement a visual dice roll
            while (newMapPosition >= circle.Count)
            {
                newMapPosition -= circle.Count;
            }
            player.MapPosition = newMapPosition;

            player.transform.position = circle[newMapPosition].transform.position + Vector3.up; // TODO: Smooth animation to move player to block

            // Execute event on the block
            currentEvent = circle[newMapPosition].Event;
            currentEvent.Execute(player);

            Turn++;
        }
    }
}

