using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.QuetDiem
{

    public class RotazioneCasuale_QueDiem : MonoBehaviour
    {

        [SerializeField] float ZScreen;
        [SerializeField] Vector3 offset;
        private Vector3 targetLook;
        [SerializeField] Vector3 posMouseStart;
        [SerializeField] Vector3 posMouseExcuted;
        Rigidbody rb;
        private Vector3 posStart;
        private float t;
        private Vector3 _hitPoint;
        public Vector3 _HitPoint => _hitPoint;

        [SerializeField] int hitLayerMask;

        [SerializeField] HopDiem _hopdiem;
        [SerializeField] Transform _effExcuted;
        [SerializeField] Transform _light;
        [SerializeField] Animation _QueDiemAnim;
        [SerializeField] float z;
        private float tEff = 17;
        private bool PlayAnim = false;
        private GameObject a;
        [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
        Vector3[] _arrBones;
        private Vector3 bonePosition;
        private Transform posEnd;
        private int countBones;
        private float tFire = 0f;
        Vector3 localVelocity;
        [SerializeField] AudioSource audioSource;

        void Start()
        {
            audioSource.clip = SoundManager.instance.bruciandoClip;
            posEnd = GameObject.Find("a").transform;
            a = GameObject.Find("a");
            _hopdiem = GameObject.Find("ScatolaFiammiferi").GetComponent<HopDiem>();

            targetLook = _hopdiem.transform.position + new Vector3(0,0.5f,0);
            rb = GetComponent<Rigidbody>();
            posStart = transform.position;
            hitLayerMask = LayerMask.GetMask("Matchbox");
            _hopdiem.OnFire += _hopdiem_OnFire;
            _arrBones = new Vector3[skinnedMeshRenderer.bones.Length];

            if (skinnedMeshRenderer != null)
            {
                // Lấy mesh từ SkinnedMeshRenderer
                Mesh mesh = skinnedMeshRenderer.sharedMesh;

                if (mesh != null)
                {
                    int boneCount = skinnedMeshRenderer.bones.Length;
                    Debug.Log(boneCount);
                    for (int i = 0; i < boneCount; i++)
                    {
                        bonePosition = skinnedMeshRenderer.bones[i].position;
                        GameObject go = Instantiate(a,
                            bonePosition,
                            Quaternion.identity);
                        go.name = "bone[" + i + "]";
                        go.transform.parent = skinnedMeshRenderer.gameObject.transform;
                        _arrBones[i] = go.transform.localPosition;
                    }
                    for (int i = 0; i < _arrBones.Length; i++)
                    {
                       // Debug.Log(_arrBones[i]);
                    }
                }
                else
                {
                    Debug.LogWarning("Mesh not found in SkinnedMeshRenderer.");
                }
            }
            else
            {
                Debug.LogWarning("SkinnedMeshRenderer component not found.");
            }
        }

        private void _hopdiem_OnFire(object sender, System.EventArgs e)
        {
            if (!PlayAnim)
            {
                audioSource.PlayOneShot(SoundManager.instance.accesoClip.GetRandomClip());
                _effExcuted.gameObject.SetActive(true);
                _light.gameObject.SetActive(true);
                _QueDiemAnim.Play();
                _effExcuted.transform.parent = skinnedMeshRenderer.gameObject.transform;
                StartCoroutine(PlayAnimFire());
                countBones = skinnedMeshRenderer.bones.Length-1;
                _effExcuted.transform.localPosition = new Vector3(0, _arrBones[countBones].y, 0);
                PlayAnim = true;
            }
            if (tEff >= 0 )
            {
                tFire += Time.deltaTime;
                if (tFire > 0.5f && countBones >= 0 && tEff > 0)
                {
                    //_effExcuted.transform.localPosition = new Vector3(0, _arrBones[countBones].y ,0);
                    _effExcuted.transform.DOLocalMove(new Vector3(0, _arrBones[countBones].y, 0),0.5f);
                    _light.transform.localPosition = _effExcuted.transform.localPosition + new Vector3(0,0,-1);
                    tFire = 0f;
                    countBones--;
                    tEff--;
                }
            }
            if (countBones == 0)
            {      
                if(audioSource.isPlaying)
                {
                    audioSource.Stop();
                }              
                StartCoroutine(EndGame());
            }
        }

        IEnumerator EndGame()
        {
            _effExcuted.gameObject.SetActive(false); //tat lua
            _light.gameObject.SetActive(false);
            transform.DOMove(posEnd.position, 1f).SetEase(Ease.Linear); //di chuyen
            _hopdiem.setActiveFire(false); //dat bool lua bang false
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);

        }

        IEnumerator PlayAnimFire()
        {
            yield return new WaitForSeconds(0.5f);
            _effExcuted.GetComponent<ParticleSystem>().Play();
            audioSource.loop = true;
            audioSource.Play();
            
        }

        private void OnDestroy()
        {
            _hopdiem.OnFire -= _hopdiem_OnFire;
        }

        // Update is called once per frame
        void Update()
        {
            if(Physics.Raycast(transform.position, targetLook - transform.position, out RaycastHit hit, 10, hitLayerMask))
            {
                _hitPoint = hit.point;
            }
            transform.LookAt(_hitPoint);
            
        }

        public Vector3 getLocalVelocity()
        {
            return localVelocity;
        }

        private void OnMouseDown()
        {
            posMouseStart = Extensions.getMouseInWorld(transform);
            offset = posMouseStart - transform.position;
            rb.isKinematic = false;
        }

        private void OnMouseDrag()
        {
            rb.isKinematic = false;
            posMouseExcuted = Extensions.getMouseInWorld(transform);
            Vector3 posEdit = posMouseExcuted - offset;
            posEdit.x = Mathf.Clamp(posEdit.x, -10f, 0f);
            posEdit.z = Mathf.Clamp(posEdit.z, 0f, 3.9f);
            rb.MovePosition(new Vector3(posEdit.x, posStart.y, posEdit.z));
            if (posMouseExcuted.z > posMouseStart.z)
            {
                float distance = posMouseExcuted.z - posMouseStart.z;
                rb.AddForce(Vector3.forward * 5f);
            }
            localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
            Debug.Log(getLocalVelocity().x);
        }


        private void OnMouseExit()
        {
            rb.isKinematic = true;
            localVelocity = Vector3.zero;
        }
    }
}
