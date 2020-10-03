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
        private List<BlockEvent> circle = new List<BlockEvent>(desirableMapSize);
        private BlockEvent currentPosition;

        [SerializeField]
        private Player player;

        public int Turn { get; private set; } = 0;
        public bool ReadyForNextTurn = true;

        private void Start()
        {
            // Parse out the blocks in the circle
            Block[] blocksUnderDirectory = circleMasterDirectory.GetComponentsInChildren<Block>();
            foreach (Block b in blocksUnderDirectory)
            {
                circle.Add(b.Event);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && ReadyForNextTurn)
            {
                TakeNextTurn();
            }
        }

        private void TakeNextTurn()
        {
            int newMapPosition = player.MapPosition + Random.Range(0, player.MaxSpeed) + 1; // TODO: maybe implement a visual dice roll
            while (newMapPosition >= circle.Count)
            {
                newMapPosition -= circle.Count;
            }
            player.MapPosition = newMapPosition;

            // Execute event on the block
            currentPosition = circle[newMapPosition];
            currentPosition.Execute(player);

            Turn++;
        }
    }
}

