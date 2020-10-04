using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{

    /// <summary>
    /// allow camera to follow target position
    /// </summary>

    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        private Vector3 delta_pos;

        // Start is called before the first frame update
        void Start()
        {
            delta_pos = transform.position - target.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + delta_pos, 0.2f);
        }
    }
}