using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BanhRang
{

    public class ColliderGears : MonoBehaviour
    {
        [SerializeField] Handle handle;
        private void OnMouseDown()
        {
            if (!handle.IsTouchHandle)
            {
                handle.IsRotating = false;
                handle.RotateSpeed = 0;

            }
        }
    }

}
