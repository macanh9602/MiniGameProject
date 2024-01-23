using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BanhRang
{

    public class Handle2 : MonoBehaviour
    {
        [SerializeField] Vector3 thisCurrentInWorld;

        [SerializeField] Vector3 posMouseDown;
        [SerializeField] Vector3 posMouseExcuted;
        //[SerializeField] Transform testA;
        //[SerializeField] Transform testB;
        private bool isExcuting = false;
        private bool isInitializing = false;
        public bool IsExcuting => isExcuting;
        public bool IsInitializing => isInitializing;

        [SerializeField] HingeJoint joint;
        private Vector2 direction;
        float angle;

        public Vector3 PosMouseDown { get => posMouseDown; set => posMouseDown = value; }
        public Vector3 PosMouseExcuted { get => posMouseExcuted; set => posMouseExcuted = value; }

        private void Start()
        {
            //posMouseDown = transform.parent.localPosition;
            //posMouseExcuted = transform.parent.localPosition;
        }

        private void OnMouseDown()
        {
            posMouseDown = Extensions.getMouseInWorld(transform);
            //testA.position = posMouseDown;
            isInitializing = true;

        }

        private void OnMouseDrag()
        {
            joint.useLimits = false;
            joint.useSpring = true;
            posMouseExcuted = Extensions.getMouseInWorld(transform);
            //testB.position = posMouseExcuted;
            isExcuting = true;
            RotateByPos();
        }



        private void OnMouseUp()
        {
            //posMouseDown = posMouseExcuted;
            //posMouseDown = transform.parent.localPosition;
            //posMouseExcuted = transform.parent.localPosition;
            isExcuting = false;
            isInitializing = false;
            //StartCoroutine(QuanTinh());
        }

        IEnumerator QuanTinh()
        {
            yield return new WaitForSeconds(3f);
        }

        private void RotateByPos()
        {
            direction = (posMouseExcuted - transform.position).normalized;
            //goc xoay
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 120f;
            Quaternion a = Quaternion.Euler(0f, 0f, angle);
            //joint.spring.targetPosition = angle;
        }
    }
}



