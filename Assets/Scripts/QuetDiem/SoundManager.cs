using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem{
    
    public class SoundManager : MonoBehaviour
    {
        [Serializable]
        public class ClipArray
        {
            public AudioClip[] clips;

            public AudioClip GetRandomClip()
            {
                return clips[UnityEngine.Random.Range(0, clips.Length)];
            }
        }
        public static SoundManager instance {  get; private set; }
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _bruciando; // tieng diem chay
        [SerializeField] AudioClip _sfregato; //tieng quet diem vao hop ma chua chay
        [SerializeField] ClipArray _acceso; //[] tieng quet diem thanh cong

        public AudioClip bruciandoClip => _bruciando;
        public AudioClip sfregatoClip => _sfregato;
        public ClipArray accesoClip => _acceso;

        private void Awake()
        {
            instance = this;
        }
    }
    
}
