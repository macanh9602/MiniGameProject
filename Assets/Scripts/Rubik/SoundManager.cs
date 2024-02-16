using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Rubik{
    
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager instance { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        [Serializable]
        public class ClipArray
        {
            [SerializeField] AudioClip[] arrClip;
            public AudioClip getRandomAudioClip()
            {
                return arrClip[UnityEngine.Random.Range(0,arrClip.Length)];
            }
        }

        [SerializeField] AudioClip soundWin;
        [SerializeField] ClipArray soundRotate;

        public AudioClip SoundWin => soundWin;
        public ClipArray SoundRotate => soundRotate;

    }
    
}
