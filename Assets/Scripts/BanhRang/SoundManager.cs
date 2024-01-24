using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BanhRang{
    
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance {  get; private set; }
        [SerializeField] AudioSource _source;
        [SerializeField] AudioClip _clip;

        public AudioSource Source => _source;
        public AudioClip Clip => _clip; 

        private void Start()
        {
            instance = this;
        }





    }
    
}
