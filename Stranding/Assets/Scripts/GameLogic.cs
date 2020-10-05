﻿using System.Collections;
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
                TakeNextTurn();
                UpdataStorage();
            }
        }

        private void TakeNextTurn()
        {
            ReadyForNextTurn = false;
            hasCooledDown = false;

            int movement = Random.Range(0, player.MaxSpeed) + 1; // TODO: maybe implement a visual dice roll
            TotalStep += movement;
            int newMapPosition = player.MapPosition + movement;
            while (newMapPosition >= circle.Count)
            {
                Loop++;
                newMapPosition -= circle.Count;
            }
            player.MapPosition = newMapPosition;

            player.Destination = circle[newMapPosition].transform.position + Vector3.up; 

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

