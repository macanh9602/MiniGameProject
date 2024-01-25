using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem{
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform _queDiem;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("QueDiem");
            if(go.Length == 0)
            {
                Transform prfQueDiem = Resources.Load<Transform>("RotazioneCasuale");
                Transform QueDiem = Instantiate(prfQueDiem, new Vector3(-4.8499999f, 1.63999999f, 2.3499999f), Quaternion.identity);
            }
            
        }
    }
    
}
