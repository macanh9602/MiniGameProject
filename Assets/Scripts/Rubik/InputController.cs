using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [SerializeField] bool _isTouchCube = false;
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
                Touch _touch = Input.GetTouch(0);
                Ray _rayTouchCube = Camera.main.ScreenPointToRay(_touch.position);
                RaycastHit _hitCube;
                if (Physics.Raycast(_rayTouchCube, out _hitCube))
                {
                    _isTouchCube = true;
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
                                //Debug.Log(firstHitCenter.z + " | " + firstHitCenter.normalized);
                                firstHit = hit.transform.parent.gameObject;
                            }
                            break;
                        case TouchPhase.Moved:
                            Ray rayMoved = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hitMoved;

                            if (Physics.Raycast(rayMoved, out hitMoved, 100, layerMask))
                            {
                                secondHitNormal = hitMoved.normal;
                                secondHitCenter = hitMoved.transform.gameObject.GetComponent<Renderer>().bounds.center;
                                secondHit = hitMoved.transform.parent.gameObject;
                            }

                            if (firstHitCenter != secondHitCenter && !CheckOnce)
                            {
                                Vector3 move = secondHitCenter - firstHitCenter;
                                //Debug.Log(move.normalized);
                                //accept to rotate
                                if (!_bigCube.CurrentlyRotate)
                                {
                                    DoRotation(move);

                                }
                                CheckOnce = true;
                            }
                            break;
                        case TouchPhase.Ended:
                            CheckOnce = false;
                            _isTouchCube = false;
                            break;
                        default:
                            break;
                    }



                }
                else
                {
                    #region <--- rotateCamera --->
                    // Operate the camera orbit movement
                    localRotation.x += Input.touches[0].deltaPosition.x;
                    localRotation.y -= Input.touches[0].deltaPosition.y;

                    // Actual Camera Rig Transformation
                    Quaternion targetLocation = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
                    //Debug.Log(targetLocation + " | " + localRotation.y + " | " + localRotation.x);
                    this.cameraPivot.rotation = Quaternion.Slerp(this.cameraPivot.rotation, targetLocation, Time.deltaTime * orbitDampening);
                    #endregion
                }
            }

        }

        private bool CheckSum(Vector3 normal, Vector3 direction, Vector3 vectorBoSung, char index)
        {
            Vector3 sum = normal + vectorBoSung;
            switch (index)
            {
                case 'Y':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(direction.y), Mathf.Abs(sum.z));
                    //Debug.LogWarning(sum);
                    break;
                case 'Z':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(sum.y), Mathf.Abs(direction.z));
                    //Debug.LogWarning(sum);
                    break;
                case 'X':
                    sum = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(sum.y), Mathf.Abs(sum.z));
                    //Debug.LogWarning(sum);
                    break;
                default:
                    return false;
            }
            Vector3 _faker = new Vector3(Mathf.RoundToInt(sum.x), Mathf.RoundToInt(sum.y), Mathf.RoundToInt(sum.z));
            return _faker == Vector3.one;
        }

        private bool CheckSum2(Vector3 normal1 , Vector3 normal2 , Vector3 vectorBoSung , char index)
        {
            Vector3 sum = normal1 + vectorBoSung;
            switch(index)
            {
                case 'Y':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(normal2.y), Mathf.Abs(sum.z));
                    break;
                case 'X':
                    sum = new Vector3(Mathf.Abs(normal2.x), Mathf.Abs(sum.y), Mathf.Abs(sum.z));
                    break;
                case 'Z':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(sum.y), Mathf.Abs(normal2.z));
                    break;
                default: return false;
            }
            return sum == Vector3.one;
        }

        private void DoRotation(Vector3 move)
        {
            //check th cung 1 ben mat 
            if(firstHitNormal == secondHitNormal)
            {
                #region <--- logic1face --->
                if (CheckSum(firstHitNormal, move, new Vector3(0, 0, 1), 'Y'))
                {
                    int x = firstHitNormal.x > 0 ? 1 : -1;
                    int y = move.y > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongZ(x * y * 90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                    Debug.Log("1nd: First hit " + firstHitNormal + " | move: " + move);
                }
                else if (CheckSum(firstHitNormal, move, new Vector3(0, 1, 0), 'Z'))
                {
                    int x = firstHitNormal.x > 0 ? 1 : -1;
                    int z = move.z > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongY(x * z * -90, Mathf.RoundToInt(firstHitCenter.y + 1)));
                    Debug.Log("2nd: Second hit " + firstHitNormal + " | move: " + move);
                }
                else if (CheckSum(firstHitNormal, move, new Vector3(1, 0, 0), 'Y'))
                {
                    int z = firstHitNormal.z > 0 ? 1 : -1;
                    int y = move.y > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongX(z * y * -90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                    Debug.Log("3nd: Third hit " + firstHitNormal + " | move: " + move);
                }
                else if (CheckSum(firstHitNormal, move, new Vector3(0, 1, 0), 'X'))
                {
                    int z = firstHitNormal.z > 0 ? 1 : -1;
                    int x = move.x > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongY(z * x * 90, Mathf.RoundToInt(firstHitCenter.y + 1)));
                    Debug.Log("4nd: Four hit " + firstHitNormal + " | move: " + move);
                }
                else if (CheckSum(firstHitNormal, move, new Vector3(1, 0, 0), 'Z'))
                {
                    int y = firstHitNormal.y > 0 ? 1 : -1;
                    int z = move.z > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongX(y * z * 90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                    Debug.Log("5nd: Four hit " + firstHitNormal + " | move: " + move);
                }
                else if (CheckSum(firstHitNormal, move, new Vector3(0, 0, 1), 'X'))
                {
                    int y = firstHitNormal.y > 0 ? 1 : -1;
                    int x = move.x > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongZ(y * x * -90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                    Debug.Log("6nd: Four hit " + firstHitNormal + " | move: " + move);
                }
                #endregion
            }
            else
            {
                //char theo secondHitnormal
                if(CheckSum2(firstHitNormal , secondHitNormal , new Vector3(1,0,0) , 'Y'))
                {
                    int z = firstHitNormal.z > 0 ? 1 : -1;
                    int y = secondHitNormal.y > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongX( -z * y * 90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                }
                else if (CheckSum2(firstHitNormal, secondHitNormal, new Vector3(0, 0, 1), 'Y'))
                {
                    int x = firstHitNormal.x > 0 ? 1 : -1;
                    int y = secondHitNormal.y > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongZ( x * y * 90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                }
                else if (CheckSum2(firstHitNormal, secondHitNormal, new Vector3(0, 1, 0), 'X'))
                {
                    int z = firstHitNormal.z > 0 ? 1 : -1;
                    int x = secondHitNormal.x > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongY(x * z * 90, Mathf.RoundToInt(firstHitCenter.y + 1)));

                }
                else if (CheckSum2(firstHitNormal, secondHitNormal, new Vector3(0, 0, 1), 'X'))
                {
                    int y = firstHitNormal.y > 0 ? 1 : -1;
                    int x = secondHitNormal.x > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongZ(y * x * -90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                }
                else if (CheckSum2(firstHitNormal, secondHitNormal, new Vector3(0, 1 , 0), 'Z'))
                {
                    int x = firstHitNormal.x > 0 ? 1 : -1;
                    int z = secondHitNormal.z > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongY(x * z * -90, Mathf.RoundToInt(firstHitCenter.y + 1)));
                }
                else if (CheckSum2(firstHitNormal, secondHitNormal, new Vector3(1, 0, 0), 'Z'))
                {
                    int y = firstHitNormal.y > 0 ? 1 : -1;
                    int z = secondHitNormal.z > 0 ? 1 : -1;
                    StartCoroutine(_bigCube.RotateAlongX(y * z * 90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                }
            }
        }
    }
}



