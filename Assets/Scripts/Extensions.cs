using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

namespace Scripts{
    
    public static class Extensions 
    {
        public static Vector3 getMouseInWorld(Transform transform)
        {
            float _zScreen = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mouseInScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zScreen);
            return Camera.main.ScreenToWorldPoint(mouseInScreen);
        }

    }

}
