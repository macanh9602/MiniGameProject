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
                            if (secondHit != firstHit && !CheckOnce)
                            {
                                Vector3 move = secondHitCenter - firstHitCenter;
                                //Debug.Log(move.normalized);
                                //accept to rotate
                                if (!_bigCube.currentlyRotate)
                                {
                                    DoRotation(move);

                                }
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

        private bool CheckSum(Vector3 normal, Vector3 direction, Vector3 vectorBoSung, char index)
        {
            Vector3 sum = normal + vectorBoSung;
            switch (index)
            {
                case 'Y':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(direction.y), Mathf.Abs(sum.z));
                    Debug.LogWarning(sum);
                    break;
                case 'Z':
                    sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(sum.y), Mathf.Abs(direction.z));
                    Debug.LogWarning(sum);
                    break;
                case 'X':
                    sum = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(sum.y), Mathf.Abs(sum.z));
                    Debug.LogWarning(sum);
                    break;
                default:
                    return false;
            }
            Vector3 _faker = new Vector3(Mathf.RoundToInt(sum.x), Mathf.RoundToInt(sum.y), Mathf.RoundToInt(sum.z));
            return _faker == Vector3.one;
        }

        private void DoRotation(Vector3 move)
        {
            if (CheckSum(firstHitNormal, move, new Vector3(0, 0, 1), 'Y'))
            {
                StartCoroutine(_bigCube.RotateAlongZ(firstHitNormal.x * move.y * 90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                Debug.Log("1nd: First hit " + firstHitNormal + " | move: " + move);
            }
            else if (CheckSum(firstHitNormal, move, new Vector3(0, 1, 0), 'Z'))
            {
                StartCoroutine(_bigCube.RotateAlongY(firstHitNormal.x * move.z * -90, Mathf.RoundToInt(firstHitCenter.y + 1)));
                Debug.Log("2nd: Second hit " + firstHitNormal + " | move: " + move);
            }
            else if (CheckSum(firstHitNormal, move, new Vector3(1, 0, 0), 'Y'))
            {
                Debug.LogWarning("haha");
                StartCoroutine(_bigCube.RotateAlongX(firstHitNormal.z * move.y * -90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                Debug.Log("3nd: Third hit " + firstHitNormal + " | move: " + move);
            }
            else if (CheckSum(firstHitNormal, move, new Vector3(0, 1, 0), 'X'))
            {
                Debug.LogWarning("haha");
                StartCoroutine(_bigCube.RotateAlongY(firstHitNormal.z * move.x * 90, Mathf.RoundToInt(firstHitCenter.y + 1)));
                Debug.Log("4nd: Four hit " + firstHitNormal + " | move: " + move);
            }
            else if (CheckSum(firstHitNormal, move, new Vector3(1, 0, 0), 'Z'))
            {
                StartCoroutine(_bigCube.RotateAlongX(firstHitNormal.y * move.z * 90, Mathf.RoundToInt(firstHitCenter.x + 1)));
                Debug.Log("5nd: Four hit " + firstHitNormal + " | move: " + move);
            }
            else if (CheckSum(firstHitNormal, move, new Vector3(0, 0, 1), 'X'))
            {
                StartCoroutine(_bigCube.RotateAlongZ(firstHitNormal.y * move.x * -90, Mathf.RoundToInt(firstHitCenter.z + 1)));
                Debug.Log("6nd: Four hit " + firstHitNormal + " | move: " + move);
            }
            else
            {
                Debug.Log("sai");
                Debug.LogWarning("7nd: Four hit " + firstHitNormal + " | move: " + move);
            }
        }
    }
}



