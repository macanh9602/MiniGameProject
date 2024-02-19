using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Scripts.BanhRang{
    
    public class Handle : MonoBehaviour
    {
        [SerializeField] bool isRotating = false;
        [SerializeField] Transform dad;
        private float rotateSpeed;
        private float projectionX;
        private float projectionY;
        private Vector3 lastInput;
        private Vector3 v_deltaInput;
        [SerializeField] private float stopTime;
        private bool isMoving  = false;
        public bool IsMoving => isMoving;
        public bool IsRotating => isRotating;
        public float RotateSpeed => rotateSpeed;
        // Update is called once per frame
        private void Update()
        {
            RotateWernch();
        }


        private void RotateWernch()
        {
            if (isRotating)
            {
                isMoving = true;
                v_deltaInput = lastInput - Input.mousePosition;

                Vector3 v_unitX = new Vector3(1, 0, 0);
                Vector3 v_unitY = new Vector3(0, 1, 0);
                projectionX = Vector3.Dot(v_unitX , v_deltaInput);
                projectionY = Vector3.Dot(v_unitY , v_deltaInput);
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
                lastInput = Input.mousePosition;
            }
            else
            {
                rotateSpeed = Mathf.Lerp(rotateSpeed, 0f, Time.deltaTime * 3f / stopTime);
                dad.Rotate(Vector3.forward, rotateSpeed);
                if(rotateSpeed == 0)
                {
                    isMoving = false;
                }
            }
            if (Mathf.Abs(rotateSpeed) < 0.05f && !isRotating)
            {
                isRotating = false;
                rotateSpeed = 0f;

            }
            lastInput = Input.mousePosition;
        }

        private void OnMouseDown()
        {
            isRotating = true;
            rotateSpeed = 0.0f;
        }

        private void OnMouseUp()
        {
            isRotating = false;
        }
    }
    
}

