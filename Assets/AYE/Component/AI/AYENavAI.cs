using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
namespace AYENavAI
{
    /// <summary>
    /// <para>���Τ��شM���t�Ϊ�����AI���h�A���AYEStatusBehaviour�B�@�C</para>
    /// </summary>
    public class AYENavAI<StatusEnum> : AYEStatusBehaviour<StatusEnum> where StatusEnum : Enum
    {
        #region ��l��
        /// <summary>�����t��</summary>
        [HideInInspector]
        public NavMeshAgent nav = null;
        virtual protected void Reset()
        {
            nav = GetComponent<NavMeshAgent>();
            if (nav == null)
                nav = this.gameObject.AddComponent<NavMeshAgent>();
        }
        void NavAIStart()
        {
            if (nav == null)
            {
                nav = GetComponent<NavMeshAgent>();
                Debug.LogWarning("�а���AI���W��Reset�T�O���NavMeshAgent");
            }
        }
        #endregion
        #region �ƾڽվ�
        void NavDataStart()
        {
            speed = nav.speed;
            rSpeed = nav.angularSpeed;
            radius = nav.radius;
            height = nav.height;
        }
        /// <summary>���ʳt��</summary>
        public float speed
        {
            get { return _speed; }
            set { _speed = value; UpdateNav(); }
        }
        float _speed = 3.5f;
        /// <summary>��V�t��</summary>
        public float rSpeed
        {
            get { return _rSpeed; }
            set { _rSpeed = value; UpdateNav(); }
        }
        float _rSpeed = 360f;
        /// <summary>���ʳt�׹B�઺�ʤ���</summary>
        public float speedP
        {
            get { return _speedP; }
            set { _speedP = value; UpdateNav(); }
        }
        float _speedP = 1f;
        /// <summary>����t�׹B�઺�ʤ���</summary>
        public float rSpeedP
        {
            get { return _rSpeedP; }
            set { _rSpeedP = value; UpdateNav(); }
        }
        float _rSpeedP = 1f;
        /// <summary>�e��(���|)</summary>
        public float radius
        {
            get { return _radios; }
            set { _radios = value; UpdateNav(); }
        }
        float _radios = 0.4f;
        /// <summary>����</summary>
        public float height
        {
            get { return _height; }
            set { _height = value; UpdateNav(); }
        }
        float _height = 1f;
        /// <summary>�e�צʤ���</summary>
        public float radiusP
        {
            get { return _radiosP; }
            set { _radiosP = value; UpdateNav(); }
        }
        float _radiosP = 1f;
        /// <summary>�����ʤ���</summary>
        public float heightP
        {
            get { return _heightP; }
            set { _heightP = value; UpdateNav(); }
        }
        float _heightP = 1f;
        /// <summary>�O�_�ݭnNAV��������</summary>
        public bool needNavCtrl
        {
            get { return _needNavCtrl; }
            set { _needNavCtrl = value; UpdateNav(); }
        }
        bool _needNavCtrl = true;
        void UpdateNav()
        {
            nav.speed = speed * speedP * (needNavCtrl? 1f : 0f);
            nav.angularSpeed = rSpeed * rSpeedP;
            nav.radius = radius * radiusP;
            nav.height = height * heightP;

            nav.updatePosition = needNavCtrl;
            nav.updateRotation = needNavCtrl;
            nav.updateUpAxis = needNavCtrl;
        }
        #endregion
        #region ����̡B���U
        /// <summary>��F</summary>
        Action Act_GotoDone = null;
        void UpdateGotoDone()
        {
            if (Act_GotoDone != null && !nav.pathPending)
            {
                if (nav.remainingDistance < 0.01f)
                {
                    Act_GotoDone.Invoke();
                    Act_GotoDone = null;
                }
            }
        }
        /// <summary>�h�o��</summary>
        public void Goto(Vector3 pos, Action done = null)
        {
            nav.SetDestination(pos);
            Act_GotoDone = done;
        }
        /// <summary>���U����</summary>
        public void StopGoto()
        {
            nav.SetDestination(this.transform.position);
            Act_GotoDone = null;
        }
        #endregion
        #region ���o�y��
        /// <summary>���j���o�b�|���i�H�`�����I r�����b�| h���ץb�|(�y�Ф����w�N�Φۨ�)</summary>
        public Vector3 GetNAVRandomXZPos(float r, float h, Vector3 pos = default)
        {
            Vector3 rpos = GetRandomXZPos(r, h, pos);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(rpos, out hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                // ���j�ۤv
                return GetNAVRandomXZPos(r, h, pos);
            }
        }
        /// <summary>���o�b�|���H��XZ�@�I(�y�Ф����w�N�Φۨ�)</summary>
        public Vector3 GetRandomXZPos(float r, float h, Vector3 pos = default)
        {
            Vector3 add = new Vector3(UnityEngine.Random.Range(-r, r), UnityEngine.Random.Range(-h, h), UnityEngine.Random.Range(-r, r));
            add = Vector3.ClampMagnitude(add, r);
            if (pos == Vector3.zero)
                return this.transform.position + add;
            else
                return pos + add;
        }
        #endregion
        #region ��ı�P�w
        float lestLookTime = 0f;
        Vector3 lookPos = Vector3.zero;
        Vector3 startLookPos = Vector3.zero;
        /// <summary>���o����ݨ쪺�F��(�����w������m�ɥΨ������@�b��m)</summary>
        public Transform EyeLook(LayerMask find, float range, float angle, Transform eyePos = null ,List<int> ignoreID = null, int target = -1)
        {
            Vector3 pos = this.transform.position + (Vector3.up * height * 0.5f);
            Transform eyeTransform = this.transform;
            if (eyePos != null)
            {
                pos = eyePos.position;
                eyeTransform = eyePos;
            }
            Collider[] all = Physics.OverlapSphere(pos, range, find);
            for (int i = 0; i < all.Length; i++)
            {
                // �����ۤv
                if (all[i].transform.GetInstanceID() == this.transform.GetInstanceID())
                    continue;
                // �����B��
                bool isFriend = false;
                if (ignoreID != null)
                {
                    for (int j = 0; j < ignoreID.Count; j++)
                    {
                        if (all[i].transform.GetInstanceID() == ignoreID[j])
                        {
                            isFriend = true;
                            break;
                        }
                    }
                }
                if (isFriend)
                    continue;
                // �����D�ؼ�
                if (target != -1)
                {
                    if (all[i].transform.GetInstanceID() != target)
                        continue;
                }
                // �ˬd����
                Vector3 ab = eyeTransform.forward;
                Vector3 ac = all[i].transform.position - eyeTransform.position;
                float a = Vector3.Angle(ab, ac);
                if (a < angle * 0.5f)
                {
                    // �O�_�ઽ��
                    RaycastHit hit;
                    bool isHit = Physics.Linecast(pos, all[i].transform.position, out hit);
                    if (isHit)
                    {
                        if (hit.transform.GetInstanceID() == all[i].transform.GetInstanceID())
                        {
                            lestLookTime = Time.time;
                            lookPos = all[i].transform.position;
                            startLookPos = pos;
                            return all[i].transform;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>�O�_�i�H�����ӥؼ�</summary>
        public bool IsLook(Transform target, Transform eyePos = null)
        {
            Vector3 pos = this.transform.position + (Vector3.up * height * 0.5f);
            if (eyePos != null)
                pos = eyePos.position;
            RaycastHit hit;
            bool isHit = Physics.Linecast(pos, target.position, out hit);
            if (!isHit)
                return false;
            if (hit.transform.GetInstanceID() == target.transform.GetInstanceID())
                return true;
            else
                return false;
        }
        /// <summary>�O�_����o�Ӧ�m</summary>
        public bool IsFace(Vector3 targetPos, float angle, Transform eyePos = null)
        {
            Vector3 pos = this.transform.position + (Vector3.up * height * 0.5f);
            Transform eyeTransform = this.transform;
            if (eyePos != null)
            {
                pos = eyePos.position;
                eyeTransform = eyePos;
            }
            Vector3 ab = eyeTransform.forward;
            Vector3 ac = targetPos - eyeTransform.position;
            float a = Vector3.Angle(ab, ac);
            return (a < angle * 0.5f);
        }
        #endregion
        #region Unity
        protected override void Update50()
        {
            base.Update50();
            UpdateGotoDone();
        }
        protected override void Start()
        {
            base.Start();
            NavAIStart();
            NavDataStart();
        }
        #endregion
        #region Gizmos
        private void OnDrawGizmosSelected()
        {
            if (!nav.pathPending)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(nav.destination, 0.2f);
            }
            if ((Time.time - lestLookTime) < 2f)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(startLookPos, lookPos);
            }

        }
        #endregion
    }
}

