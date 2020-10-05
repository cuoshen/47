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

        public Queue<string> Lines;
        private bool isAtTitleScreen;
        private readonly string filename = "Text/firstPart";

        private void Start()
        {
            titleScreen.SetActive(true);
            vnSuite.SetActive(false);
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
                    textBox.text = Lines.Dequeue();
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

