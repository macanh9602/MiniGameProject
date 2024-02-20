using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.BanhRang
{

    public class GearsController : MonoBehaviour
    {
        [SerializeField] Handle handle;
        [SerializeField] Transform[] arrBigGear;
        [SerializeField] Transform[] arrMediumGear;
        [SerializeField] Transform[] arrSmallGear;
        [SerializeField] Transform[] arrBigGearShadow;
        [SerializeField] Transform[] arrMediumGearShadow;
        [SerializeField] Transform[] arrSmallGearShadow;

        [SerializeField] AudioSource audioSource;

        private float sizeBig = 24; //so rang 
        private float sizeMedium = 18;
        private float sizeSmall = 12;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (handle.IsMoving)
            {
                for (int i = 0; i < arrBigGear.Length; i++)
                {
                    arrBigGear[i].Rotate(Vector3.forward, -handle.RotateSpeed);
                    arrBigGearShadow[i].Rotate(Vector3.forward, -handle.RotateSpeed);
                }
                for (int i = 0; i < arrMediumGear.Length; i++)
                {
                    float ratio1 = sizeBig / sizeMedium;
                    arrMediumGear[i].Rotate(Vector3.forward, handle.RotateSpeed * ratio1);
                    arrMediumGearShadow[i].Rotate(Vector3.forward, handle.RotateSpeed * ratio1);
                }
                for (int i = 0; i < arrSmallGear.Length; i++)
                {
                    float ratio2 = sizeBig / sizeSmall;
                    arrSmallGear[i].Rotate(Vector3.forward, -handle.RotateSpeed * ratio2);
                    arrSmallGearShadow[i].Rotate(Vector3.forward, -handle.RotateSpeed * ratio2);
                }

                PlaySoundEffect(handle.RotateSpeed);

            }


        }
        private float lastPlayTime;

        private void PlaySoundEffect(float rotationSpeed)
        {
            float speed = Mathf.Abs(rotationSpeed);
            if (speed != 0)
            {
                float speed1 = Mathf.Max(speed, 0.5f);
                float playInterval = (1.0f / speed1 )* 0.3f;
                
                if (Time.time >= lastPlayTime + playInterval)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                    lastPlayTime = Time.time; 
                }
            }

            
        }


    }

}
