using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

//kha nang reset sai nen small cube bi destroy khi goi
//loi khi cap nhat vi tri trong mang

//check phim a : doc mang va game object cuar nos
namespace Scripts.Rubik
{

    public class BigCube : MonoBehaviour
    {
        public GameObject[,,] smallCube;
        public GameObject[] _startCube;
        private float rotationTime = 0.5f;
        public bool currentlyRotate = false;
        // Start is called before the first frame update
        private void Awake()
        {
            int i = 0;
            smallCube = new GameObject[3, 3, 3];
            for (int z = 0; z < 3; z++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        smallCube[x, y, z] = _startCube[i];
                        //_startCube[i].gameObject.name = "cube[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "]";
                        //Debug.Log("smallCube" + "[" + x + "," + y + "," + z + "] =>" + smallCube[x, y, z].transform.position);
                        i++;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("halo");
                for (int z = 0; z < 3; z++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            Debug.Log("smallCube" + "[" + x + "," + y + "," + z + "] :" + smallCube[x, y, z].name);
                        }
                    }
                }
            }
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
        }

        public IEnumerator RotateAlongZ(float angle, int rotationIndex)
        {
            if (!currentlyRotate)
            {
                currentlyRotate = true;
                Debug.Log("haloZ");
                GameObject newRotation = new GameObject();
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
                Quaternion quaternion = Quaternion.Euler(0f, 0f,Mathf.RoundToInt(angle));
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
                Destroy(newRotation);
                currentlyRotate = false;

            }

        }

        public IEnumerator RotateAlongY(float angle, int rotationIndex)
        {
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
                Destroy(newRotation);
                currentlyRotate = false;
            }
        }

        public IEnumerator RotateAlongX(float angle, int rotationIndex)
        {
            if (!currentlyRotate)
            {
                currentlyRotate = true;
                Debug.Log("haloX");
                GameObject newRotation = new GameObject();
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
                Destroy(newRotation);
                currentlyRotate = false;
            }
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
            //for(int x = 0;x < 3; x++)
            //{
            //    for(int y = 0;y < 3; y++)
            //    {
            //        for( int z = 0;z < 3;z++)
            //        {
            //            if (newSmallCubes[x, y, z] == null)
            //            {
            //                Debug.LogWarning("missing object");
            //                newSmallCubes = ResetPositionAfterRotation();
            //            }
            //        }
            //    }
            //}
            return newSmallCubes;
        }
    }

}
