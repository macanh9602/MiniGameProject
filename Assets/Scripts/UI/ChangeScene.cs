using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Scripts.UI{
    
    public class ChangeScene : MonoBehaviour
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void GoToGearScene()
        {
            SceneManager.LoadScene(1);

        }

        public void GoToFireScene()
        {
            SceneManager.LoadScene(2);
        }

        public void GoToRubikScene()
        {
            SceneManager.LoadScene(3);
        }
    }
    
}
