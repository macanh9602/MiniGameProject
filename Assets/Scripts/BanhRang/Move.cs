using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scripts.BanhRang{
    
    public class Move : MonoBehaviour
    {
        [SerializeField] test test;
        [SerializeField] Transform[] _arrTransform;
        [SerializeField] float angle;
        private void Start()
        {
            //_arrTransform = new Transform[_arrTransform.Length];
        }
        private void Update()
        {
            //Debug.Log(test.PosMouseDown + "/" + test.PosMouseExcuted);
            if (test.PosMouseExcuted != Vector3.zero && test.PosMouseDown != Vector3.zero)
            {
    
                Vector2 direction = test.PosMouseExcuted - transform.position;
    
                //Vector3 direction = Vector3.zero;
    
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 120f;
                transform.localRotation = Quaternion.Euler(0f, 0f, angle);      
                //transform.localRotation = Quaternion.LookRotation(test.PosMouseExcuted, new Vector3(0,0,1));
                //transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z);
                test.PosMouseDown = test.PosMouseExcuted;
    
            }
            //Debug.Log(angle);
            if (angle != 0f)
            {
                for (int i = 0; i < _arrTransform.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        _arrTransform[i].rotation = Quaternion.Euler(0f, 0f, angle);
                    }
                    else
                    {
                        _arrTransform[i].rotation = Quaternion.Euler(0f, 0f, -angle);
                    }
                }
            }
        }
    }
    
}
