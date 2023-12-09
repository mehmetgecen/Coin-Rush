using System;
using UnityEngine;

namespace CoinRush.UI
{
    public class Billboard : MonoBehaviour
    {
        public Transform cam;
        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
