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
        private RotazioneCasuale_QueDiem _queDiem;
        [SerializeField] Vector3 _posStartEffect;
        [SerializeField] Vector3 _startHit;
        [SerializeField] Vector3 _endHit;
        private float t;
        private float dis;
        [SerializeField] bool isActiveFire = false;
        [SerializeField] bool isActiveFrictionEffect = false;
        public event EventHandler OnFire;
        public bool getActiveFire()
        {
            return isActiveFire;
        }

        public void setActiveFire(bool isActiveFire)
        {
            this.isActiveFire = isActiveFire;
        }

        public void setActiveFrictionEffect()
        {
            isActiveFrictionEffect = !isActiveFrictionEffect;
        }


        // Start is called before the first frame update
        void Start()
        {
            _queDiem = FindObjectOfType<RotazioneCasuale_QueDiem>();
            m_Position = transform.position;
            m_Rotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (_queDiem == null)
            {
                _queDiem = FindObjectOfType<RotazioneCasuale_QueDiem>();
            }
            transform.position = m_Position;
            transform.rotation = m_Rotation;
            isActiveEqualTrue(isActiveFire);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                _startHit = _queDiem._HitPoint;
                t = Time.time;
                Debug.Log("dd");
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                _endHit = _queDiem._HitPoint;
                //quang duong
                dis = Vector3.Distance(_startHit, _endHit);
                if (Time.time - t > 1f && dis > 0.5f && isActiveFire == false)
                {
                    isActiveFrictionEffect = true;
                    isActiveFire = true;
                }
            }
        }

        private void isActiveEqualTrue(bool activeFire)
        {
            if (activeFire)
            {
                OnFire?.Invoke(this, EventArgs.Empty);

            }
            if(isActiveFrictionEffect)
            {
                _effectStart.SetActive(true);
                _effectStart.transform.position = _queDiem._HitPoint;
                _effectStart.GetComponent<ParticleSystem>().Play();
                isActiveFrictionEffect = false;
            }
        }


    }
}