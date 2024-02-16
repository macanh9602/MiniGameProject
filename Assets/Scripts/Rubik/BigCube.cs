using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;

//xoay da gap van de
namespace Scripts.Rubik
{

    public class BigCube : MonoBehaviour
    {
        [SerializeField] GameObject[,,] smallCube;
        private GameObject[,,] perfectCube;
        [SerializeField] GameObject[] _startCube;
        [SerializeField] bool currentlyRotate = false;
        [SerializeField] SoundManager soundManager;
        private float rotationTime = 0.1f;
        private AudioSource audioSource;
        [SerializeField] bool IsScrambling = false;
        [SerializeField] bool isWin = true;
        public bool IsWin => isWin;
        public bool CurrentlyRotate => currentlyRotate;
        public Action OnGameEnd;
        // Start is called before the first frame update
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            smallCube = new GameObject[3, 3, 3];
            perfectCube = new GameObject[3, 3, 3];
            int i = 0;
            for (int z = 0; z < 3; z++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        smallCube[x, y, z] = _startCube[i];
                        //perfectCube[x,y,z] = smallCube[x,y,z];
                        i++;
                    }
                }
            }
            perfectCube = smallCube;
        }
        // Update is called once per frame
        void Update()
        {
            #region check missing object
            //for (int z = 0; z < 3; z++)
            //{
            //    for (int y = 0; y < 3; y++)
            //    {
            //        for (int x = 0; x < 3; x++)
            //        {
            //            if (smallCube[x, y, z] == null)
            //            {
            //                Debug.LogWarning("missing object");
            //            }
            //        }
            //    }
            //}
            #endregion
        }

        public IEnumerator RotateAlongZ(float angle, int rotationIndex)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = soundManager.SoundRotate.getRandomAudioClip();
                audioSource.Play();
            }
            Debug.Log(angle + ", " + rotationIndex);    
            if (!currentlyRotate)
            {
                currentlyRotate = true;
                Debug.Log("haloZ");
                GameObject newRotation = new GameObject();
                //newRotation.transform.parent = transform;
                newRotation.transform.position = new Vector3(0f, 0f, 0f);
                float elapsedTime = 0;

                // nhet vao cha moi
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        smallCube[x, y, rotationIndex].transform.parent = newRotation.transform;
                        //Debug.Log(smallCubes[x, y, rotationIndex].transform.position + " | " + smallCubes[x, y, rotationIndex].transform.localPosition);
                    }
                }

                // xoay
                Quaternion quaternion = Quaternion.Euler(0f, 0f, Mathf.RoundToInt(angle));
                while (elapsedTime < rotationTime)
                {
                    newRotation.transform.rotation = Quaternion.Lerp(newRotation.transform.rotation, quaternion, (elapsedTime / rotationTime));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                newRotation.transform.rotation = quaternion;
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        smallCube[x, y, rotationIndex].transform.parent = transform;
                        //Debug.Log(smallCube[x, y, rotationIndex].transform.position);
                    }
                }

                smallCube = ResetPositionAfterRotation();
                audioSource.Stop();
                CheckWin();
                Destroy(newRotation);
                currentlyRotate = false;
                
            }

        }

        public IEnumerator RotateAlongY(float angle, int rotationIndex)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundManager.SoundRotate.getRandomAudioClip();
                audioSource.Play();
            }
            Debug.Log(angle + ", " + rotationIndex);
            if (!currentlyRotate)
            {
                currentlyRotate = true;
                Debug.Log("haloY");
                GameObject newRotation = new GameObject();
                newRotation.transform.position = new Vector3(0f, 0f, 0f);
                float elapsedTime = 0;

                // nhet vao cha moi
                for (int x = 0; x < 3; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        //Debug.Log(rotationIndex + "|" + smallCube[x, rotationIndex, z].name);
                        if (newRotation != null)
                        {
                            smallCube[x, rotationIndex, z].transform.parent = newRotation.transform;

                        }
                        else
                        {
                            Debug.LogWarning("haloZ");
                        }
                        //Debug.Log(smallCubes[x, y, rotationIndex].transform.position + " | " + smallCubes[x, y, rotationIndex].transform.localPosition);
                    }
                }

                // xoay
                Quaternion quaternion = Quaternion.Euler(0f, Mathf.RoundToInt(angle), 0f);
                //Debug.Log(angle);
                while (elapsedTime < rotationTime)
                {
                    newRotation.transform.rotation = Quaternion.Lerp(newRotation.transform.rotation, quaternion, (elapsedTime / rotationTime));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                newRotation.transform.rotation = quaternion;
                for (int x = 0; x < 3; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        smallCube[x, rotationIndex, z].transform.parent = transform;
                        //Debug.Log(smallCube[x, y, rotationIndex].transform.position);
                    }
                }
                smallCube = ResetPositionAfterRotation();
                audioSource.Stop();
                CheckWin();
                Destroy(newRotation);
                currentlyRotate = false;
            }
        }

        public IEnumerator RotateAlongX(float angle, int rotationIndex)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundManager.SoundRotate.getRandomAudioClip();
                audioSource.Play();
            }
            Debug.Log(angle + ", " + rotationIndex);
            if (!currentlyRotate)
            {
                currentlyRotate = true;
                Debug.Log("haloX");
                GameObject newRotation = new GameObject();
                //newRotation.transform.parent = transform;
                newRotation.transform.position = new Vector3(0f, 0f, 0f);
                float elapsedTime = 0;

                // nhet vao cha moi
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        //Debug.Log(rotationIndex + "|" + smallCube[x, rotationIndex, z].name);
                        if (newRotation != null)
                        {
                            smallCube[rotationIndex, y, z].transform.parent = newRotation.transform;

                        }
                        else
                        {
                            Debug.LogWarning("haloZ");
                        }
                        //Debug.Log(smallCubes[x, y, rotationIndex].transform.position + " | " + smallCubes[x, y, rotationIndex].transform.localPosition);
                    }
                }

                // xoay
                Quaternion quaternion = Quaternion.Euler(Mathf.RoundToInt(angle), 0f , 0f);
                while (elapsedTime < rotationTime)
                {
                    newRotation.transform.rotation = Quaternion.Lerp(newRotation.transform.rotation, quaternion, (elapsedTime / rotationTime));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                newRotation.transform.rotation = quaternion;
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        smallCube[rotationIndex, y, z].transform.parent = transform;
                        //Debug.Log(smallCube[x, y, rotationIndex].transform.position);
                    }
                }
                smallCube = ResetPositionAfterRotation();
                audioSource.Stop();
                CheckWin();
                Destroy(newRotation);
                currentlyRotate = false;
            }
        }
        private void CheckWin()
        {
            isWin = true;
            //check win
            if (!IsScrambling )
            {
                for (int z = 0; z < 3; z++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            if (smallCube[x,y,z] != perfectCube[x,y,z])
                            {
                                isWin = false;
                                return;
                            }
                        }
                    }
                }
                if (isWin)
                {
                    StartCoroutine(EndGame());            
                }

            }
        }
        IEnumerator EndGame()
        {
            audioSource.clip = soundManager.SoundWin;
            audioSource.loop = true;
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            audioSource.loop = false;
            audioSource.Stop();
            OnGameEnd?.Invoke();
        }
        private GameObject[,,] ResetPositionAfterRotation() // bug 1 số phần tử mảng bị missing
        {
            GameObject[,,] newSmallCubes = new GameObject[3, 3, 3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {

                        for (int x2 = 0; x2 < 3; x2++)
                        {
                            for (int y2 = 0; y2 < 3; y2++)
                            {
                                for (int z2 = 0; z2 < 3; z2++)
                                {
                                    if (Mathf.RoundToInt(smallCube[x2, y2, z2].transform.position.x) == x -1 
                                        && Mathf.RoundToInt(smallCube[x2, y2, z2].transform.position.y) == y - 1
                                        && Mathf.RoundToInt(smallCube[x2, y2, z2].transform.position.z) == z - 1)
                                    {
                                        //Debug.Log(new Vector3(x2, y2, z2) + " | " + new Vector3(-multi + x, -multi + y, -multi + z));
                                        newSmallCubes[x, y, z] = smallCube[x2, y2, z2];
                                    }

                                }
                            }
                        }

                    }
                }
            }
            return newSmallCubes;
        }

        public IEnumerator ScrambleCube(int scrambleTimes, float scrambleRotationTime)
        {
            IsScrambling = true;
            float oldRotationTime = rotationTime;
            rotationTime = scrambleRotationTime;

            for (int i = 0; i < scrambleTimes; i++)
            {
                int rotationType = UnityEngine.Random.Range(0, 3);
                int rotationIndex = UnityEngine.Random.Range(0, 3);
                int rotationAngle = UnityEngine.Random.Range(-1, 1) < 0 ? -90 : 90;
                switch (rotationType)
                {
                    case 0:
                        yield return StartCoroutine(RotateAlongX(rotationAngle, rotationIndex));
                        break;
                    case 1:
                        yield return StartCoroutine(RotateAlongY(rotationAngle, rotationIndex));
                        break;
                    case 2:
                        yield return StartCoroutine(RotateAlongZ(rotationAngle, rotationIndex));
                        break;
                    default:
                        break;
                }
            }

            rotationTime = oldRotationTime;
            IsScrambling = false;
        }
    }

}
