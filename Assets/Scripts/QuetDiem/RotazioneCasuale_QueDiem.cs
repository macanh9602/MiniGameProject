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
        [SerializeField] float speed = 0.5f;
        private Vector3 posStart;
        [SerializeField] ParticleSystem effectUpdate;
        private float t;
        private Vector3 _hitPoint;
        public Vector3 _HitPoint => _hitPoint;

        [SerializeField] int hitLayerMask;

        [SerializeField] HopDiem _hopdiem;
        [SerializeField] Transform _effExcuted;
        [SerializeField] Transform Fire;
        [SerializeField] Animation _QueDiemAnim;
        [SerializeField] Animator _QueDiemAnimator;
        [SerializeField] float z;
        private float tEff = 17;
        private bool PlayAnim = false;
        [SerializeField] GameObject a;
        [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
        Vector3[] _arrBones;
        private Vector3 bonePosition;
        [SerializeField] Transform posEnd;
        // Start is called before the first frame update
        void Start()
        {
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
                        _arrBones[i] = skinnedMeshRenderer.bones[i].localPosition;
                    }
                    for (int i = 0; i < _arrBones.Length; i++)
                    {
                        Debug.Log(_arrBones[i]);
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

        private int countBones;
        private float tFire = 0f;
        private void _hopdiem_OnFire(object sender, System.EventArgs e)
        {
            _effExcuted.gameObject.SetActive(true);
            if (!PlayAnim)
            {
                _effExcuted.gameObject.SetActive(true);
                //_QueDiemAnimator.speed = speedAnim;
                _QueDiemAnim.Play();
                //_effExcuted.transform.parent= skinnedMeshRenderer.gameObject.transform;
                _effExcuted.transform.position = _arrBones[skinnedMeshRenderer.bones.Length - 1];
                StartCoroutine(PlayAnimFire());
                countBones = skinnedMeshRenderer.bones.Length - 1;
                PlayAnim = true;
            }
            if (tEff >= 0 )
            {
                tFire += Time.deltaTime;
                if (tFire > 0.5f && countBones >= 0 && tEff > 0)
                {
                    _effExcuted.transform.localPosition = _arrBones[countBones];
                    tFire = 0f;
                    countBones--;
                    tEff--;
                }
            }
            if (countBones == 0)
            {
                _effExcuted.gameObject.SetActive(false);
                transform.DOMove(posEnd.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _hopdiem.setActive();
                    Destroy(gameObject);


                });

                //Invoke("EndGame", 3f);
            }
        }

        IEnumerator PlayAnimFire()
        {
            yield return new WaitForSeconds(0.3f);
            _effExcuted.GetComponent<ParticleSystem>().Play();
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
            posEdit.z = Mathf.Clamp(posEdit.z, 0f, 3.8f);
            //Vector3 v3 = (_hitPoint - transform.position).normalized;
            ////rb.AddForce(v3 * speed);
            rb.MovePosition(new Vector3(posEdit.x, posStart.y, posEdit.z));
            if (posMouseExcuted.z > posMouseStart.z)
            {
                float distance = posMouseExcuted.z - posMouseStart.z;
                rb.AddForce(Vector3.forward * 5f);
            }
        }

        private void OnMouseExit()
        {
            rb.isKinematic = true;
        }
    }
}
