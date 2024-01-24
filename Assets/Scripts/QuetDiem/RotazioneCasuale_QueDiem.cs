using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem
{

    public class RotazioneCasuale_QueDiem : MonoBehaviour
    {
        [SerializeField] float ZScreen;
        [SerializeField] Vector3 offset;
        [SerializeField] Transform targetLook;
        [SerializeField] Vector3 posMouseStart;
        [SerializeField] Vector3 posMouseExcuted;
        Rigidbody rb;
        [SerializeField] float speed = 0.5f;
        private Vector3 posStart;
        [SerializeField] ParticleSystem effectUpdate;
        private float t;
        private Vector3 _hitPoint;
        public Vector3 _HitPoint => _hitPoint;

        [SerializeField] int hitLayerMask;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            posStart = transform.position;
            hitLayerMask = LayerMask.GetMask("Matchbox");
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(transform.position, targetLook.position - transform.position, out RaycastHit hit, 10, hitLayerMask))
            {
                _hitPoint = hit.point;
            }
            //rb.velocity = Vector3.forward * 0.5f;
            transform.LookAt(_hitPoint);

        }

        private void OnMouseDown()
        {
            posMouseStart = Extensions.getMouseInWorld(transform);
            offset = posMouseStart - transform.position;
            rb.isKinematic = false;
        }

        private void OnMouseDrag()
        {
            rb.isKinematic = false;
            posMouseExcuted = Extensions.getMouseInWorld(transform);
            Vector3 posEdit = posMouseExcuted - offset;
            posEdit.x = Mathf.Clamp(posEdit.x, -10f, 0f);
            posEdit.z = Mathf.Clamp(posEdit.z, 0f, 3.8f);
            //Vector3 v3 = (_hitPoint - transform.position).normalized;
            ////rb.AddForce(v3 * speed);
            rb.MovePosition(new Vector3(posEdit.x, posStart.y, posEdit.z));
            if (posMouseExcuted.z > posMouseStart.z)
            {
                float distance = posMouseExcuted.z - posMouseStart.z;
                rb.AddForce(Vector3.forward * 5f);
            }

            // khi keo trai phai +cham tuong thi add luc leen
        }

        private void OnMouseUp()
        {
            rb.isKinematic = true;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                //Debug.Log("halo");
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision != null)
            {
                //rb.constraints = RigidbodyConstraints.FreezePosition
                
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other != null)
            {
                rb.AddForce(Vector3.forward * 5f);
            }
        }
    }
}
