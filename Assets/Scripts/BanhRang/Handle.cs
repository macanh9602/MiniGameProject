using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BanhRang
{

    public class Handle : MonoBehaviour
    {
        [SerializeField] Vector3 thisCurrentInWorld;

        [SerializeField] Vector3 posMouseDown;
        [SerializeField] Vector3 posMouseExcuted;
        [SerializeField] Transform testA;
        [SerializeField] Transform testB;
        private bool isExcuting = false;
        private bool isInitializing = false;
        public bool IsExcuting => isExcuting;
        public bool IsInitializing => isInitializing;

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
            testA.position = posMouseDown;
            isInitializing = true;

        }

        private void OnMouseDrag()
        {
            posMouseExcuted = Extensions.getMouseInWorld(transform);
            testB.position = posMouseExcuted;
            isExcuting = true;
        }



        private void OnMouseUp()
        {
            isExcuting = false;
            isInitializing = false;
        }
    }

}
