using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem{
    
    public class TestTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                Debug.Log(other.gameObject.name);
            }
        }
    }
    
}
