using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Stranding
{
    public class Loader : MonoBehaviour
    {
        private readonly string filename = "Text/content.txt";

        private void Start()
        {
            // Read text file
            TextAsset contentFile = Resources.Load<TextAsset>(filename);

            // Parse out each line
            StringReader reader = new StringReader(contentFile.text);

        }
    }
}
