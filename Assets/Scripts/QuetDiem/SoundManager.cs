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

        public AudioClip bruciandoClip => _bruciando; //tieng diem chay
        public AudioClip sfregatoClip => _sfregato; // quet chua chay
        public ClipArray accesoClip => _acceso; // tieng quet thanh cong

        private void Awake()
        {
            instance = this;
        }
    }
    
}
