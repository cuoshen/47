using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Stranding
{
    /// <summary>
    /// The title scene also handles the very first part (pure visual novel)
    /// </summary>
    public class TitleSceneControl : MonoBehaviour
    {
        [SerializeField]
        private GameObject titleScreen;
        [SerializeField]
        private GameObject vnSuite;
        [SerializeField]
        private Text textBox;
        [SerializeField]
        private ScreenFade fade;
        [SerializeField]
        private Sprite nellaCalm;
        [SerializeField]
        private Sprite nellaSurprised;
        [SerializeField]
        private Sprite nellaHappy;
        [SerializeField]
        private SpriteRenderer characterArt;
        [SerializeField]
        private GameObject vnBackground;

        public Queue<string> Lines;
        private bool isAtTitleScreen;
        private readonly string filename = "Text/firstPart";

        private void Start()
        {
            titleScreen.SetActive(true);
            vnSuite.SetActive(false);
            vnBackground.SetActive(false);
            isAtTitleScreen = true;

            // Parse out contents
            TextAsset contentFile = Resources.Load<TextAsset>(filename);
            if (contentFile == null)
            {
                Debug.LogError("Content file not found");
            }
            List<string> lines = new List<string>(contentFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            Lines = new Queue<string>(lines);
        }

        private void Update()
        {
            if (!isAtTitleScreen && Input.anyKeyDown)
            {
                if (Lines.Count > 0)
                {
                    string line = Lines.Dequeue();
                    string output = line;
                    // Update character art accordingly
                    if (line.Contains("]"))
                    {
                        output = output.Substring(output.IndexOf(']') + 1);
                        string tag = line.Substring(1, line.IndexOf(']') - 1);
                        switch (tag)
                        {
                            case "Nella, Surprised":
                                characterArt.sprite = nellaSurprised;
                                break;
                            case "Nella, Happy":
                                characterArt.sprite = nellaHappy;
                                break;
                            case "Nella, Calm":
                                characterArt.sprite = nellaCalm;
                                break;
                            default:
                                characterArt.sprite = null;
                                output = line;
                                break;
                        }
                    }
                    else
                    {
                        characterArt.sprite = null;
                    }

                    textBox.text = output;
                }
                else
                {
                    // Screen fade away
                    fade.isFading = true;
                }
            }

            if (Input.anyKeyDown && isAtTitleScreen)
            {
                titleScreen.SetActive(false);
                vnSuite.SetActive(true);
                vnBackground.SetActive(true);
                isAtTitleScreen = false;
            }

            if (fade.hasFaded)
            {
                // Change scene
                SceneManager.LoadScene(1);
            }
        }
    }
}

