using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    class Player : MonoBehaviour
    {
        public int MaxSpeed { get; set; } = 1;
        public int MapPosition { get; set; } = 0;

        public readonly string Name = "Meme man";
    }
}
