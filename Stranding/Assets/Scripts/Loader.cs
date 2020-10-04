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
        private readonly string filename = "Text/content";

        [SerializeField]
        private GameObject circleMasterDirectory;
        private List<Block> circle = new List<Block>(Capacity.BLOCKS);
        private BlockEvent currentEvent;
        private int counter;
        private int loop;
        
        private void Start()
        {
            counter = 0;
            loop = 0;
            // Parse out the blocks in the circle
            Block[] blocksUnderDirectory = circleMasterDirectory.GetComponentsInChildren<Block>();
            foreach (Block b in blocksUnderDirectory)
            {
                circle.Add(b);
            }

            TextAsset contentFile = Resources.Load<TextAsset>(filename);
            if (contentFile == null)
            {
                Debug.LogError("Content file not found");
            }
            List<string> lines = ParseLinesAndCleanup(contentFile.text);

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Length > 2 && line[0] == '[')
                {
                    // the line is a statement
                    // Parse out tag
                    string tag = line.Substring(1, line.IndexOf(']') - 1);

                    switch (tag)
                    {
                        case "Notif":
                            currentEvent = ParseNotificationEvent(line);
                            break;
                        case "Choice":
                            currentEvent = ParseChoiceEvent(line);
                            break;
                        case "Null":
                            currentEvent = null;
                            break;
                        default:
                            Debug.LogError("Error paring out input text file : invalid tag");
                            break;
                    }

                    while (counter >= circle.Count)
                    {
                        counter -= circle.Count;
                        loop++;
                    }
                    circle[counter].Events.Enqueue(currentEvent);
                    counter++;
                }
            }
        }

        private List<string> ParseLinesAndCleanup(string inputFromFile)
        {
            // Parse out each line
            List<string> lines = new List<string>(inputFromFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            // Remove comments
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string cleaned = line;
                for (int j = 0; j < line.Length - 1; j++)
                {
                    if (line[j] == '/' && line[j + 1] == '/')
                    {
                        cleaned = line.Substring(0, j);
                    }
                }
                lines[i] = cleaned;
            }
            return lines;
        }

        /// <summary>
        /// Take the message to be everything behind the tag
        /// [Notif]payload
        /// </summary>
        private NotificationEvent ParseNotificationEvent(string line)
        {
            string message = line.Substring(line.IndexOf(']') + 1);
            return new NotificationEvent(message);
        }

        /// <summary>
        /// Follows this form
        /// [Choice]prompt [Option1]payload1 [Option2]payload2
        /// </summary>
        private ChoiceEvent ParseChoiceEvent(string line)
        {
            string[] parts = line.Split('[');
            string prompt = parts[1].Substring(parts[1].IndexOf(']') + 1);
            List<EventOption> payload = new List<EventOption>();
            for (int i = 0; i < Capacity.OPTIONS; i++)
            {
                string option = parts[2 + i];
                string buttonText = option.Substring(0, option.IndexOf(']'));
                string outcomeText = option.Substring(option.IndexOf(']') + 1);
                payload.Add(new EventOption(buttonText, new NotificationEvent(outcomeText)));
            }

            return new ChoiceEvent(prompt, payload);
        }
    }
}
