using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.QuetDiem{
    
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
        [SerializeField] Transform startFire;

        private void Start ()
        {
            IsTouchMatchBox = false;
            currentFire = startFire;
        }
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

            //Ray ray = Camera.main.ScreenPointToRay(transform.position);
            //RaycastHit hit;
            //Debug.DrawRay(ray.origin,ray.direction, Color.red);
            //if (Physics.Raycast(ray, out hit) )
            //{
            //    float distance = Vector3.Distance(transform.position, hit.point);
            //    Debug.Log(distance);
            //    Debug.Log("Hit: " + hit.collider.gameObject.name);
            //}
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                //can move
                Move();
                //cho di chuyen

                //float distance = Vector3.Distance()

                //
            }
        }

        private void Move()
        {
            transform.position = getMouseInWorld() + offset;
            mouseInWorld.position = getMouseInWorld();
            transform.LookAt(targetToLook);
        }

        //private void OnMouseUp()
        //{
        //    transform.position = getMouseInWorld() + offset;
        //}

        //private void OnTriggerStay(Collider other)
        //{
        //    if(other.tag == "MatchBox")
        //    {
        //        currentTime += Time.deltaTime;
        //        if(currentTime >= timeMax) {
        //            SetPosEffectStart();
        //        }
        //    }
        //}


        private void SetPosEffectStart()
        {
            _effectStart.gameObject.transform.position = startFire.position;
            _effectExcuted.gameObject.transform.position = startFire.position;
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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "MatchBox")
            {
                Debug.Log("cham nha");
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.tag == "MatchBox")
            {
                Debug.Log("toi out");
            }
        }


    }
    
}
