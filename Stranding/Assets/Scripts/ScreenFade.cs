using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Stranding
{
    class ScreenFade : MonoBehaviour
    {
        public bool isFading;
        public bool hasFaded { get; private set; }

        private float progress = 0f;
        private float speed = 0.5f;
        private SpriteRenderer image;

        private void Start()
        {
            image = GetComponent<SpriteRenderer>();
            isFading = false;
            hasFaded = false;
            Color transparent = image.color;
            transparent.a = 0;
            image.color = transparent;
        }

        private void Update()
        {
            if (isFading)
            {
                if (progress <= 1f)
                {
                    progress += Time.deltaTime * speed;
                    image.color = new Color(image.color.r, image.color.g, image.color.b, progress);
                }
                else
                {
                    hasFaded = true;
                }
            }
        }
    }
}
