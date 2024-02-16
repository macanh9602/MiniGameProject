using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Rubik{
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] BigCube bigCube;
        [SerializeField] int ScrambleTimes = 15;
        [SerializeField] float speedScramble = 0.1f;
        private void Start()
        {
            StartCoroutine(bigCube.ScrambleCube(ScrambleTimes, speedScramble));
            bigCube.OnGameEnd += OnGameEnd;
        }

        private void OnGameEnd()
        {
            StartCoroutine(bigCube.ScrambleCube(ScrambleTimes, speedScramble));
        }
    }
    
}
