using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Rubik{
    
    public class Movement : MonoBehaviour
    {
        [SerializeField] Vector3 _posMouse;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            MoveAllCube();
        }

        private void MoveAllCube()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _posMouse = Extensions.getMouseInWorld(transform);
            }
            if(Input.GetMouseButton(0)) 
            {
                _posMouse = Extensions.getMouseInWorld(transform);

                //transform.rotation = Quaternion.Euler();
            }
        }
    }
    
}
