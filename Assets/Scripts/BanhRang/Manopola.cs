using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.BanhRang
{

    public class Manopola : MonoBehaviour
    {
        [SerializeField] Handle _handle;
        [SerializeField] Transform[] _arrGears;
        [SerializeField] float angle;
        [SerializeField] float angularSpeed;
        [SerializeField] float tCooldown = 1.0f;
        [SerializeField] float zOffset;
        [SerializeField] Vector2 direction;
        private float angle2;
        private float currentZangle;
        [SerializeField] float speed;

        private void Start()
        {
            //_source = GetComponent<AudioSource>();
            currentZangle = getCurrentZAngle();

        }
        private void Update()
        {
            turnWrench(); //vặn cờ lê
            StartCoroutine(PlayOneShotRepeatedly());
        }
        private void turnWrench()
        {
            if (_handle.IsExcuting == true)
            {
                //lan dau chay raa 0
                direction = (_handle.PosMouseExcuted - transform.position).normalized;
                //goc xoay
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                RotateByPos();
            }
            else
            {
                //_source.Stop();
            }
        }

        private void RotateByPos()
        {
            //xoay dc but the problem is when mouse down , this rotation tele to this angle , need offset 
            Quaternion a = Quaternion.Euler(0f, 0f, angle);
            transform.localRotation = Quaternion.Slerp(transform.rotation, a, speed * Time.deltaTime);
            RotateGears(); //xoay bánh răng
            float previousAngle = transform.localRotation.eulerAngles.z;
            angularSpeed = (angle - previousAngle) / Time.deltaTime;
            //_handle.PosMouseDown = _handle.PosMouseExcuted;
        }

        private float getCurrentZAngle()
        {
            float z = transform.rotation.eulerAngles.z;
            return z;
        }

        private void RotateGears()
        {
            if (angle != 0f && angle != angle2 && _arrGears.Length > 0)
            {
                for (int i = 0; i < _arrGears.Length; i++)
                {                    
                    if (i % 2 == 0)
                    {
                        //_arrGears[i].rotation = Quaternion.Euler(0f, 0f, angle);
                        _arrGears[i].Find("Mesh").rotation =
                            Quaternion.Slerp(_arrGears[i].Find("Mesh").rotation,
                            Quaternion.Euler(180f, 0f, -angle), (speed) * Time.deltaTime);
                        _arrGears[i].Find("Ombra").rotation =
                            Quaternion.Slerp(_arrGears[i].Find("Ombra").rotation,
                            Quaternion.Euler(180f, 0f, -angle), (speed - 1) * Time.deltaTime);
                    }
                    else
                    {
                        //_arrGears[i].rotation = Quaternion.Euler(0f, 0f, -angle);
                        _arrGears[i].Find("Mesh").rotation =
                            Quaternion.Slerp(_arrGears[i].Find("Mesh").rotation,
                            Quaternion.Euler(180f, 0f, angle), (speed) * Time.deltaTime);
                        _arrGears[i].Find("Ombra").rotation =
                            Quaternion.Slerp(_arrGears[i].Find("Ombra").rotation,
                            Quaternion.Euler(180f, 0f, angle), (speed - 1) * Time.deltaTime);
                    }
                }
            }
        }

        IEnumerator PlayOneShotRepeatedly()
        {
            while (_handle.IsExcuting == true)
            {
                //_source.PlayOneShot(_clip);// Thay thế _source.clip bằng âm thanh bạn muốn phát

                // Chờ t giây trước khi phát âm thanh tiếp theo
                yield return new WaitForSeconds(tCooldown);
            }
        }

    }

}

//// Giảm giá trị của t theo thời gian nếu angularSpeed tăng
//float a = Mathf.Abs(angularSpeed);
//if (a > 0)
//{
//    tCooldown -= Time.deltaTime;
//    if (tCooldown <= 0)
//    {
//        tCooldown = 1.0f; // Reset tCooldown
//                          // Giảm giá trị của t (đảm bảo không nhỏ hơn 0)
//        float newT = Mathf.Max(0, tCooldown);
//        // Sử dụng newT trong âm thanh hoặc công việc khác
//    }
//}
