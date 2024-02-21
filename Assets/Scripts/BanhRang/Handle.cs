using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Scripts.BanhRang
{

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
        public bool IsRotating { get => isRotating; set => isRotating = value; }
        public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

        private bool isTouchHandle = false;
        public bool IsTouchHandle => isTouchHandle;

        private bool isMoved = false;
        // Update is called once per frame
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (Input.touchCount == 1)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            break;
                        case TouchPhase.Moved:
                            isMoved = true;
                            break;
                        case TouchPhase.Ended:
                            isMoved = false;
                            break;
                    }
                }
            }
            RotateWernch();
        }


        private void RotateWernch()
        {
            if (isMoved)
            {
                isRotating = true;
                v_deltaInput = lastInput - Input.mousePosition;
                Debug.Log(v_deltaInput + " | " + Input.mousePosition + " | " + lastInput);
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
            else
            {
                float speed = 2f;
                rotateSpeed = Mathf.Lerp(rotateSpeed, 0f, Time.deltaTime * speed / stopTime);
                dad.Rotate(Vector3.forward, rotateSpeed);
                if (rotateSpeed == 0)
                {
                    isRotating = false;
                }
            }
            if (Mathf.Abs(rotateSpeed) < 0.05f && !isMoved)
            {
                isMoved = false;
                rotateSpeed = 0f;
            }

            lastInput = Input.mousePosition;
        }


        private void OnMouseDown()
        {
            //isMoved =true;
            isTouchHandle = true;
            rotateSpeed = 0.0f;
        }

        private void OnMouseUp()
        {
            //isMoved =false;
            isTouchHandle = false;
        }
    }

}

