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
        [SerializeField] AudioSource _audioSource;

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
            _audioSource.clip = SoundManager.instance.sfregatoClip;
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
            }
        }

        private bool soundFriction = false;
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "QueDiem")
            {
                // Nếu vận tốc theo trục y (lên xuống) lớn hơn một giá trị nhỏ, đó có thể là di chuyển
                //if (Mathf.Abs(_queDiem.getLocalVelocity().x) > 1.2f)
                //{
                //    // Nếu không phát âm thanh, thì phát âm thanh
                //    if (!_audioSource.isPlaying)
                //    {
                //        _audioSource.Play();
                //        Debug.Log("halo");
                //    }
                //    soundFriction = true;
                //}
                //else
                //{
                //    // Nếu không di chuyển nữa, ngừng phát âm thanh
                //    if (_audioSource.isPlaying && soundFriction)
                //    {
                //        _audioSource.Stop();
                //        soundFriction = false;
                //    }
                //}
                //logic bat anim lua , anime friction
                _endHit = _queDiem._HitPoint;
                dis = Vector3.Distance(_startHit, _endHit);
                if (Time.time - t > 1f && dis > 0.6f && !isActiveFire)
                {
                    isActiveFrictionEffect = true;
                    isActiveFire = true;
                    soundFriction = false;
                    _audioSource.Stop();
                }
                if( dis > 0.3f && dis < 0.6f)
                {
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                        Debug.Log("halo");
                    }
                    soundFriction = true;

                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.tag == "QueDiem")
            {
                //soundFriction = false;
                _audioSource.Stop();
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