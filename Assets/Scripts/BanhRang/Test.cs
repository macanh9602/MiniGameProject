using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.BanhRang{
    
    public class Test : MonoBehaviour
    {
        private float rotateSpeed;
        [SerializeField] Transform a;
        [SerializeField] Transform b;
        public Vector3 v_deltaInput;
        private float projectionX;
        private float projectionY;
        [SerializeField] Transform dad;
        private Vector3 lastInput;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Vector3.Dot(Vector2.up, new Vector3(3, 4)));
        }

        // Update is called once per frame
        void Update()
        {
            if(isMoving)
            {
                v_deltaInput = lastInput - Input.mousePosition;
                Debug.Log(v_deltaInput + " | " + Input.mousePosition + " | " + lastInput );
                Debug.DrawRay(Vector3.zero, v_deltaInput, Color.yellow);
                Vector3 v_unitX = new Vector3(1, 0, 0);
                Vector3 v_unitY = new Vector3(0, 1, 0);
                projectionX = Vector3.Dot(v_unitX, v_deltaInput);
                projectionY = Vector3.Dot(v_unitY, v_deltaInput);
                Debug.DrawLine(Vector3.zero, v_unitX, Color.red);
                Debug.DrawLine(Vector3.zero, v_unitY, Color.green);
                Debug.Log(projectionX + ", " + projectionY);
                float yInScreen = Camera.main.WorldToScreenPoint(dad.position).y;
                float xInScreen = Camera.main.WorldToScreenPoint(dad.position).x;
                if (Input.mousePosition.y < yInScreen)
                    projectionX = -projectionX;
                if (Input.mousePosition.x > xInScreen)
                    projectionY = -projectionY;

                rotateSpeed = (projectionX + projectionY) / 10f;
                int range = 10;
                rotateSpeed = (rotateSpeed > range) ? range : (rotateSpeed < -range) ? -range : rotateSpeed;

                dad.Rotate(Vector3.forward, rotateSpeed);
                //Debug.Log(rotateSpeed);
                lastInput = Input.mousePosition;
            }
            lastInput = Input.mousePosition;
        }
        public bool isMoving = false;
        private void OnMouseDown()
        {
            a.position = Extensions.getMouseInWorld(this.transform);
            isMoving = true;

        }
        private void OnMouseDrag()
        {
            b.position = Extensions.getMouseInWorld(this.transform);
        }

        private void OnMouseEnter()
        {
            
        }

        private void OnMouseUp()
        {
            a.position = b.position;
            isMoving = false;
        }
    }
    
}
