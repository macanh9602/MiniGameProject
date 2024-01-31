using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Rubik
{

    public class InputController : MonoBehaviour
    {
        [SerializeField] private float orbitDampening = 10f;
        [SerializeField] private GameObject firstHit;
        [SerializeField] private Vector3 firstHitNormal;
        [SerializeField] private Vector3 firstHitCenter;
        [SerializeField] private GameObject secondHit;
        [SerializeField] private Vector3 secondHitNormal;
        [SerializeField] private Vector3 secondHitCenter;
        private Vector3 localRotation;
        private Transform cameraPivot;
        private LayerMask layerMask;
        [SerializeField] private BigCube _bigCube;
        private bool CheckOnce = false;
        // Start is called before the first frame update
        void Start()
        {
            this.cameraPivot = this.transform.parent;
            layerMask = LayerMask.GetMask("Matchbox");
        }

        // Update is called once per frame
        void Update()
        {
           
            if (Input.touchCount > 0)
            {
                //Debug.Log(GameObject.FindGameObjectsWithTag("halo").Length);
                Touch _touch = Input.GetTouch(0);
                //ray
                Ray _rayTouchCube = Camera.main.ScreenPointToRay(_touch.position);
                RaycastHit _hitCube;
                bool _isTouchCube = false;
                if (Physics.Raycast(_rayTouchCube, out _hitCube))
                {
                    _isTouchCube = true;
                }
                else
                {
                    _isTouchCube = false;
                }
                if (Input.touchCount == 1 && _isTouchCube)
                {
                    switch (_touch.phase)
                    {
                        case TouchPhase.Began:
                            Ray ray = Camera.main.ScreenPointToRay(_touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit, 100, layerMask))
                            {
                                firstHitNormal = hit.normal;
                                firstHitCenter = hit.transform.gameObject.GetComponent<Renderer>().bounds.center;
                                Debug.Log(firstHitCenter.z + " | " + firstHitCenter.normalized);
                                firstHit = hit.transform.parent.gameObject;
                            }
                            break;
                        case TouchPhase.Moved:
                            Ray rayMoved = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hitMoved;
                            if(Physics.Raycast(rayMoved, out hitMoved, 100, layerMask))
                            {
                                secondHitNormal = hitMoved.normal;
                                secondHitCenter = hitMoved.transform.gameObject.GetComponent<Renderer>().bounds.center;
                                secondHit = hitMoved.transform.parent.gameObject;
                            }
                            if(secondHit != firstHit && !CheckOnce)
                            {
                                Vector3 move = secondHitCenter - firstHitCenter;
                                //Debug.Log(move.normalized);
                                //accept to rotate
                                DoRotation(move);
                                CheckOnce = true;
                            }
                            break;
                        case TouchPhase.Ended:
                            CheckOnce = false;  
                            break;
                        default:
                            break;
                    }



                }
                else
                {
                    //Debug.Log("halo");
                    // Operate the camera orbit movement
                    localRotation.x += Input.touches[0].deltaPosition.x;
                    localRotation.y -= Input.touches[0].deltaPosition.y;

                    // Actual Camera Rig Transformation
                    Quaternion targetLocation = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
                    //Debug.Log(targetLocation + " | " + localRotation.y + " | " + localRotation.x);
                    this.cameraPivot.rotation = Quaternion.Slerp(this.cameraPivot.rotation, targetLocation, Time.deltaTime * orbitDampening);

                }               
            }

        }

        private bool CheckSum(Vector3 normal , Vector3 direction , char index)
        {
            Vector3 sum;
            switch(index)
            {
                case 'Z':
                    sum = new Vector3(Mathf.Abs(normal.x), MathF.Abs(direction.y), 1);
                    break;
                case 'Y':
                    sum = new Vector3(Mathf.Abs(normal.x) , 1 , MathF.Abs (direction.z));
                    break;
                default:
                    return false;
            }
            
            return sum == new Vector3(1,1,1);
        }

        private void DoRotation(Vector3 move)
        {
            if(CheckSum(firstHitNormal,move , 'Z'))
            {
                StartCoroutine(_bigCube.RotateAlongZ(firstHitNormal.x * move.y * 90 ,Mathf.RoundToInt(firstHitCenter.z + 1)));
            }
            if(CheckSum(firstHitNormal,move , 'Y'))
            {
                StartCoroutine(_bigCube.RotateAlongY(firstHitNormal.x * move.z * -90, Mathf.RoundToInt(firstHitCenter.y + 1)));
            }

            else
            {
                Debug.Log("sai");
            }
        }

    }
}



