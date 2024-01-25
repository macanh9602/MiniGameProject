using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Scripts.QuetDiem
{

    public class HopDiem : MonoBehaviour
    {
        private Vector3 m_Position;
        private Quaternion m_Rotation;
        [SerializeField] GameObject _effectStart;
        [SerializeField] RotazioneCasuale_QueDiem _queDiem;
        [SerializeField] Vector3 _posStartEffect;
        [SerializeField] Vector3 _startHit;
        [SerializeField] Vector3 _endHit;
        private float t;
        private float dis;
        private bool isActiveFire = false;
        private bool isActiveFrictionEffect = false;
        public event EventHandler OnFire;

        public bool getActive()
        {
            return isActiveFire;
        }

        public void setActive()
        {
            isActiveFire = !isActiveFire;
        }

        public void setActiveFrictionEffect()
        {
            isActiveFrictionEffect = !isActiveFrictionEffect;
        }


        // Start is called before the first frame update
        void Start()
        {
            m_Position = transform.position;
            m_Rotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = m_Position;
            transform.rotation = m_Rotation;
            isActiveEqualTrue(isActiveFire);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                //Debug.Log("hh");
                //effect khi thoa man condition
                //StartCoroutine(ActiveEffect(_queDiem._HitPoint));
                _startHit = _queDiem._HitPoint;
                t = Time.time;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                _endHit = _queDiem._HitPoint;
                //quang duong
                dis = Vector3.Distance(_startHit, _endHit);
                if (Time.time - t > 1f && dis > 0.5f && !isActiveFire)
                {                  
                    isActiveFire = true;
                }
            }
        }

        private void isActiveEqualTrue(bool active)
        {
            if(active)
            {
                OnFire?.Invoke(this, EventArgs.Empty);
                if (!isActiveFrictionEffect)
                {
                    //StartCoroutine(ActiveEffect(_queDiem._HitPoint));
                    _effectStart.SetActive(true);
                    _effectStart.transform.position = _queDiem._HitPoint;
                    _effectStart.GetComponent<ParticleSystem>().Play();
                    isActiveFrictionEffect = true;
                }                              
            }
            else
            {
                return;
            }
        }

        IEnumerator ActiveEffect(Vector3 pos)
        {
            isActiveFrictionEffect = true;
            _effectStart.SetActive(true);
            _effectStart.transform.position = pos;
            _effectStart.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(.3f);
            isActiveFrictionEffect = false;
            _effectStart.SetActive(false);
        }

    }
}