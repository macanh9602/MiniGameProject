using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem {

    public class HopDiem : MonoBehaviour
    {
        private Vector3 m_Position;
        private Quaternion m_Rotation;
        [SerializeField] GameObject _effectStart;
        [SerializeField] RotazioneCasuale_QueDiem _queDiem;
        [SerializeField] Vector3 _posStartEffect;
        [SerializeField] Vector3 _startHit;
        [SerializeField] Vector3 _endHit;
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                Debug.Log("hh");
                //effect khi thoa man condition
                //StartCoroutine(ActiveEffect(_queDiem._HitPoint));
                _startHit = _queDiem._HitPoint; 

            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.tag == "QueDiem")
            {
                //quang duong

            }
        }

        IEnumerator ActiveEffect(Vector3 pos)
        {
            _effectStart.SetActive(true);
            _effectStart.transform.position = pos;
            _effectStart.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1f);
            _effectStart.SetActive(false);
        }

    }
}