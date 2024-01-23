using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

namespace Scripts.QuetDiem.Test
{

    public class QueDiem : MonoBehaviour
    {
        [SerializeField] float ZScreen;
        [SerializeField] Transform myParent;
        [SerializeField] Vector3 offset;
        [SerializeField] Transform targetLook;
        [SerializeField] bool isTouchTrigger = false;
        [SerializeField] bool isTouchBox = false;
        [SerializeField] Transform dauQueDiem;
        [SerializeField] Vector3 posMouseStart;
        [SerializeField] bool isTouch = false;
        private float rotationX;
        private float rotationY;
        private float rotationZ;
        Rigidbody rb;
        [SerializeField] float rotateFirstTouch;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            //ZScreen = Extensions.getZScreen(transform);
            Move();
            //Debug.Log(GetComponent<Rigidbody>().velocity);
            if(!isTouch)
            {
                //rb.freezeRotation = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                transform.rotation = Quaternion.Euler(rotationX,rotationY , rotationZ);
                transform.position = new Vector3(transform.position.x , 0.62f , transform.position.z);
            }
        }

        private void Move()
        {
            if(Input.GetMouseButtonDown(0))
            {
                offset = myParent.position - Extensions.getMouseInWorld(transform);
                posMouseStart = Extensions.getMouseInWorld(transform);
            }
            if (Input.GetMouseButton(0))
            {              
                Vector3 mouse = Extensions.getMouseInWorld(transform);
                Vector3 direction = mouse - posMouseStart;
                transform.position = Vector3.MoveTowards(transform.position, mouse + offset, 2f);
                myParent.LookAt(targetLook);
                if (Physics.Raycast(dauQueDiem.position , targetLook.position - dauQueDiem.position , out RaycastHit hit))
                {
                    Debug.DrawRay(dauQueDiem.position, Vector3.forward, Color.red);
                    Debug.Log(hit.collider.name);
                    
                }

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                isTouch = true;
                rotationX = transform.rotation.eulerAngles.x;
                rotationY = transform.rotation.eulerAngles.y;
                rotationZ = transform.rotation.eulerAngles.z;
                Debug.Log(rotateFirstTouch);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if(collision != null)
            {
                isTouch = false;
            }
        }







    }

}
