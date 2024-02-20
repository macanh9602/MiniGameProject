using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.QuetDiem{
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform _posFall;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            AddNewQueDiem();

        }

        private void AddNewQueDiem()
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("QueDiem");
            if (go.Length == 0)
            {
                Transform prfQueDiem = Resources.Load<Transform>("RotazioneCasuale");
                Transform _queDiem = Instantiate(prfQueDiem, _posFall.position, Quaternion.identity);

                //Transform QueDiem = Instantiate(prfQueDiem, new Vector3(-6.91000032f, 1.75f, 1.68999505f), Quaternion.identity);
                _queDiem.eulerAngles = new Vector3(8.70358086f, 12.231967f, 0f);

                _queDiem.DOMove(new Vector3(-6.91000032f, 1.75f, 1.68999505f), 1f);


            }

        }
    }
    
}
