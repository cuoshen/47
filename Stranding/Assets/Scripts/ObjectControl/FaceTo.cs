using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stranding.Testing
{
    /// <summary>
    /// always face to target
    /// </summary>
    public class FaceTo : MonoBehaviour
    {

        public Transform target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(target);
        }
    }
}