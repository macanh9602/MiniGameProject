using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BanhRang{
    
    public class test : MonoBehaviour
    {
    
        [SerializeField] float zScreen;
        //[SerializeField] Vector3 offset;
        [SerializeField] Vector3 thisCurrentInWorld;
    
        [SerializeField] Vector3 posMouseDown;
        [SerializeField] Vector3 posMouseExcuted;
        [SerializeField] Transform testA;
        [SerializeField] Transform testB;
        private bool isExcuting = false;
        private bool isStarting = false;
        public bool IsExcuting => isExcuting;
        public bool IsStarting => isStarting;
    
        public Vector3 PosMouseDown { get => posMouseDown; set => posMouseDown = value; }
        public Vector3 PosMouseExcuted { get => posMouseExcuted; set => posMouseExcuted = value; }
    
        private void Start()
        {
            //Debug.Log(myDad.TransformPoint(gameObject.transform.localPosition));
        }
        private void Update()
        {
            zScreen = Camera.main.WorldToScreenPoint(transform.position).z;
            //Debug.Log(zScreen);
            //Debug.Log(offset);
            //offset = transform.position - getMouseInWorld();
        }
    
        private Vector3 getMouseInWorld()
        {
            Vector3 mouseInWorld = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zScreen);
            return Camera.main.ScreenToWorldPoint(mouseInWorld);
        }

        private void OnMouseDrag()
        {
            //transform.position = getMouseInWorld() + offset;
            
            posMouseExcuted = getMouseInWorld();
            testB.position = posMouseExcuted;
            isExcuting = true;

        }
    
        private void OnMouseDown()
        {
            posMouseDown = getMouseInWorld();
            testA.position = posMouseDown;
            isStarting = true;
        }
    
        private void OnMouseUp()
        {
            posMouseDown = posMouseExcuted;
            isExcuting = false;
            isStarting = false;
        }
    }
    
}
