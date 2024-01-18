using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.QuetDiem
{

    public class MatchstickController : MonoBehaviour
    {
        [SerializeField] float Zscreen;
        [SerializeField] Vector3 offset;
        [SerializeField] Transform targetToLook;
        [SerializeField] Transform mouseInWorld;
        [SerializeField] bool IsTouchMatchBox;
        [SerializeField] private float currentTime;
        [SerializeField] float timeMax = 3f;

        [SerializeField] ParticleSystem _effectExcuted;
        [SerializeField] ParticleSystem _effectStart;

        [SerializeField] Transform currentFire;
        [SerializeField] Transform posTopMatchStick;

        [SerializeField] float maxDragDistance = 5f;

        private Vector3 newPos;

        [SerializeField] Transform test;

        private void Start()
        {
            IsTouchMatchBox = false;
            currentFire = posTopMatchStick;
            //Debug.Log(posTopMatchStick.position);
        }
        private void Update()
        {
            Zscreen = Camera.main.WorldToScreenPoint(transform.position).z;
            offset = transform.position - getMouseInWorld();
        }

        private Vector3 getMouseInWorld()
        {
            Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Zscreen);

            return Camera.main.ScreenToWorldPoint(mouse);
        }

        private void OnMouseDown()
        {
            //Debug.Log("halo");
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void OnMouseDrag()
        {
            newPos = getMouseInWorld() + offset;
            mouseInWorld.position = getMouseInWorld();
            transform.LookAt(targetToLook);
            Vector3 direction = (posTopMatchStick.position - transform.position).normalized;

            if (Physics.Raycast(posTopMatchStick.position, Vector3.forward, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawLine(transform.position, hit.point, Color.red);
                TouchScintille(transform.position, hit.point, newPos , hit);
                test.position = hit.point;
            }

        }
        private void TouchScintille(Vector3 start, Vector3 end, Vector3 newPos ,RaycastHit hit)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            float distanceAC = Vector3.Distance(start, end);
            float distanceAB = Vector3.Distance(start, posTopMatchStick.position);
            float distanceBC = distanceAC - distanceAB;
            //transform.position = newPos;
            //Debug.Log(distanceAB + " / " + distanceAC + " / " + (float)(distanceAC - distanceAB));
            if (distanceBC <= 0f)
            {
                //Debug.Log("Halo");
                //transform.position = newPos;
                Vector3 direction = (newPos - transform.position);
                transform.position = transform.position + direction * 1f;
                //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                transform.position = newPos;
            }

            //while (distanceBC <= 0f)
            //{
            //    Vector3 direction = (newPos - transform.position).normalized;
            //    transform.position = transform.position + direction * 0.3f;
            //    distanceBC += 0.2f;
            //}
            //transform.position = newPos;
        }

        private void OnMouseUp()
        {
            
        }

        private void SetPosEffectStart()
        {
            _effectStart.gameObject.transform.position = posTopMatchStick.position;
            _effectExcuted.gameObject.transform.position = posTopMatchStick.position;
            StartCoroutine(ExcutedEffect());
        }

        IEnumerator ExcutedEffect()
        {
            _effectStart.Play();
            _effectExcuted.Play();
            yield return new WaitForSeconds(1f);
            transform.GetComponentInChildren<Animation>().Play();
            _effectExcuted.gameObject.transform.DOMoveY(0.55f, 4f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "TriggerScintille")
            {
                Debug.Log("halo");
            }
        }
    }

}
