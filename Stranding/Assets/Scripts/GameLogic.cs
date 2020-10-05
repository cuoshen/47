using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class GameLogic : MonoBehaviour
    {
        [SerializeField]
        private GameObject circleMasterDirectory;
        /// <summary>
        /// We have a circular map represented by a list of blocks,
        /// where the last in the list loops back to the first.
        /// </summary>
        private List<Block> circle = new List<Block>(Capacity.BLOCKS);
        private BlockEvent currentEvent;

        [SerializeField]
        private Player player;
        [SerializeField]
        private Data_storage storage;

        private bool Trigger_dice_roll;
        private bool Is_dice_roll_complete;
        private int Last_dice_outcome;

        public int Turn { get; private set; } = 0;
        public int TotalStep { get; private set; } = 0;
        public int Loop { get; private set; } = 0;
        public bool ReadyForNextTurn = true;
        private bool hasCooledDown = true;
        private float timeSinceLastTurn = 0f;
        private float cooldownTime = 0.5f;

        private void UpdataStorage()
        {
            storage.Health = player.Health;
            storage.Loop = Loop;
            storage.Step = TotalStep;
        }

        private void Start()
        {
            // Parse out the blocks in the circle
            Block[] blocksUnderDirectory = circleMasterDirectory.GetComponentsInChildren<Block>();
            foreach (Block b in blocksUnderDirectory)
            {
                circle.Add(b);
            }
            UpdataStorage();
        }

        private void Update()
        {
            timeSinceLastTurn += Time.deltaTime;
            while (timeSinceLastTurn >= cooldownTime)
            {
                timeSinceLastTurn -= cooldownTime;
                hasCooledDown = true;
            }

            if (player.isAtDestination)
            {
                if (currentEvent != null && currentEvent.isComplete && hasCooledDown)
                {
                    ReadyForNextTurn = true;
                }
                else if (currentEvent == null && hasCooledDown)
                {
                    ReadyForNextTurn = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && ReadyForNextTurn)
            {
                if (!storage.Is_dice_updated && !Trigger_dice_roll)
                {
                    // trigger roll dice
                    storage.Trigger_dice_roll = true;

                    // remove arrive at station limit
                    storage.Is_at_station = false;
                }
            }

            if (storage.Is_dice_updated && !Trigger_dice_roll)
            {
                TakeNextTurn(storage.Last_dice_outcome);
                UpdataStorage();
                storage.Is_dice_updated = false;
            }

        }

        private void TakeNextTurn(int movement)
        {
            ReadyForNextTurn = false;
            hasCooledDown = false;

            // replaced with pass in parameter
            //int movement = Random.Range(0, player.MaxSpeed) + 1; // TODO: maybe implement a visual dice roll


            TotalStep += movement;
            int newMapPosition = player.MapPosition + movement;

            // trying to let train stop at station
            //if (newMapPosition / 6 != player.MapPosition / 6)
            //{
            //    movement -= (newMapPosition - newMapPosition / 6 * 6);
            //    newMapPosition = newMapPosition / 6 * 6;
                
            //}

            //Debug.Log(movement + "|" + newMapPosition + "|" + circle.Count);

            if (newMapPosition > circle.Count)
            {
                Loop++;
                newMapPosition = newMapPosition % circle.Count - 1;
            }
            

            while (movement > 0)
            {
                player.MapPosition += 1;
                
                // restart from 0 after finishing a loop
                if (player.MapPosition >= circle.Count)
                {
                    player.MapPosition = 0;
                }

                player.Destination.Enqueue(circle[player.MapPosition].transform.position);
                

                movement -= 1;
            }
            


            // Execute event on the block
            if (circle[newMapPosition].Events.Count > 0)
            {
                currentEvent = circle[newMapPosition].Events.Dequeue();
                if (currentEvent != null)
                {
                    currentEvent.Execute(player);
                }
            }


            Turn++;
        }
    }
}

