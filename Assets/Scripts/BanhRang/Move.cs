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
        private void Update()
        {
            turnWrench();
            RotateGears();
        }
        private void turnWrench()
        {
            if (test.IsStarting == true)
            {
                Vector2 direction = (test.PosMouseExcuted - transform.position).normalized;
                Vector3 centerToCMP = test.PosMouseExcuted - transform.position;
                Vector3 centerToSMP = test.PosMouseDown - transform.position;
                float offsetAngle = Vector3.Angle(centerToSMP, centerToCMP);
                //Debug.Log(offsetAngle);
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 120f;
                transform.localRotation = Quaternion.Euler(0f, 0f, angle);
                test.PosMouseDown = test.PosMouseExcuted;

                //Vector3 centerToCMP = test.PosMouseExcuted - transform.position;
                //Vector3 centerToSMP = test.PosMouseDown - transform.position;
                //float angle = Vector3.SignedAngle(centerToSMP, centerToCMP , Vector3.forward);
                //Debug.Log(angle);
                //Vector3 newRotation = transform.eulerAngles;
                //newRotation.z += angle * 1f;
                //transform.eulerAngles = newRotation;
            }
        }

        private void RotateGears()
        {
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
