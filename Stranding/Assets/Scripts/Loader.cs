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
        enum EventType
        {
            NOTIFICATION,
            CHOICE
        }
        private readonly string filename = "Text/content.txt";
        private EventType currentEventType;
        private BlockEvent currentEvent;

        private void Start()
        {
            // Read text file
            TextAsset contentFile = Resources.Load<TextAsset>(filename);

            // Parse out each line
            List<string> lines = new List<string>(contentFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            // Remove comment lines
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string cleaned = line;
                for (int j = 0; j < line.Length - 1; j++)
                {
                    if (line[j] == '/' && line[j+1] == '/')
                    {
                        cleaned = line.Substring(0, j);
                    }
                }
                lines[i] = cleaned;
            }

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Length > 2 && line[0] == '[')
                {
                    // the line is a statement
                    // Parse out tag
                    string tag = line.Substring(1, line.IndexOf('['));

                    switch (tag)
                    {
                        case "Notif":
                            currentEventType = EventType.NOTIFICATION;
                            break;
                        case "Choice":
                            currentEventType = EventType.CHOICE;
                            break;
                        case "Null":
                            currentEvent = null;
                            break;
                        default:
                            Debug.LogError("Error paring out input text file : invalid tag");
                            break;
                    }

                    // Parse out payload

                    // Add event to block
                }
            }
        }
    }
}
