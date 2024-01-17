using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem{
    
    public class MatchstickController : MonoBehaviour
    {
        [SerializeField] float Zscreen;
        [SerializeField] Vector3 offset;
        [SerializeField] Transform thisHokage;
        [SerializeField] Transform targetToLook;
        [SerializeField] Transform mouseInWorld;
        private void Update()
        {
            Zscreen = Camera.main.WorldToScreenPoint(transform.position).z;
            offset = transform.position - getMouseInWorld();

        }

        private Vector3 getMouseInWorld()
        {
            Vector3 mouse = new Vector3(Input.mousePosition.x , Input.mousePosition.y , Zscreen);

            return Camera.main.ScreenToWorldPoint(mouse);
        }

        private void OnMouseDown()
        {
            //Debug.Log("halo");
            
        }

        private void OnMouseDrag()
        {
            //Debug.Log("halo1");
            thisHokage.position = getMouseInWorld() + offset;
            mouseInWorld.position = getMouseInWorld();
            thisHokage.transform.LookAt(targetToLook);
        }
    }
    
}
