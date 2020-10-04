using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Stranding
{
    /// <summary>
    /// Loads Events into the blocks according to a text file input
    /// </summary>
    public class Loader : MonoBehaviour
    {
        private readonly string filename = "Text/content.txt";

        private void Start()
        {
            // Read text file
            TextAsset contentFile = Resources.Load<TextAsset>(filename);

            // Parse out each line
            List<string> lines = new List<string>(contentFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            // Remove comment lines
        }
    }
}
