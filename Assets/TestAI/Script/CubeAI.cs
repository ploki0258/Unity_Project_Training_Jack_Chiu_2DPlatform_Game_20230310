using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYENavAI;
public class CubeAI : AYENavAI<AIStatus>
{
    private void Awake()
    {
        AddStatus(AIStatus.IDLE, OnIDLE, UpdateIDLE, ExitIDLE);
        AddStatus(AIStatus.MOVE, OnMOVE, UpdateMOVE, ExitMOVE);
        AddStatus(AIStatus.LOOK, OnLOOK, UpdateLOOK, ExitLOOK);
        AddStatus(AIStatus.CHECK, OnCHECK, UpdateCHECK, ExitCHECK, null, null, UpdateCHECK50);
        AddStatus(AIStatus.FIND_FOOD, OnFINDFOOD, UpdateFINDFOOD, ExitFINDFOOD);
        AddStatus(AIStatus.EAT_FOOD, OnEAT, UpdateEAT, ExitEAT);
        // ���j���H���_�l
        energy = Random.Range(maxEnergy * 0.5f, maxEnergy);
    }
    #region �ݩR
    void OnIDLE()
    {
        //needNavCtrl = false;
    }
    void UpdateIDLE()
    {
        if (energy < 50f)
        {
            if (foodPos.Count > 0)
                status = AIStatus.FIND_FOOD;
            else
                status = AIStatus.MOVE;
        }
        if (statusTime > 2f)
            status = AIStatus.MOVE;
    }
    void ExitIDLE()
    {

    }
    #endregion
    #region ���}
    void OnMOVE()
    {
        Goto(GetNAVRandomXZPos(10f, 2f), MoveDone);
    }
    void UpdateMOVE()
    {
        if (statusTime > 5f)
            status = AIStatus.LOOK;
    }
    void ExitMOVE()
    {
        StopGoto();
    }
    void MoveDone()
    {
        status = AIStatus.LOOK;
    }
    #endregion
    #region ��B�ݬ�
    void OnLOOK()
    {
        speedP = 0.01f;
        Goto(GetNAVRandomXZPos(5, 2f));
    }
    void UpdateLOOK()
    {
        if (statusTime > 3f)
            status = AIStatus.IDLE;
    }
    void ExitLOOK()
    {
        StopGoto();
        speedP = 1f;
    }
    #endregion
    #region �ˬd���
    [SerializeField] Animator checkAnim = null;
    [SerializeField] Transform checkObj = null;
    void OnCHECK()
    {
        checkTime = 0f;
        speedP = 0.01f;
        rSpeedP = 5f;
    }
    bool canCheck = false;
    Vector3 follorPos = Vector3.zero;
    void UpdateCHECK50()
    {
        if (lookTarget == null)
        {
            status = AIStatus.IDLE;
            return;
        }

        bool isClose = Vector3.Distance(lookTarget.position, this.transform.position) < 5f && IsLook(lookTarget);
        canCheck = isClose && IsFace(lookTarget.position, 30f);

        // �a��N���U ���}�N�[�t
        if (isClose)
            speedP = 0.01f;
        else
            speedP = 1.5f;

        // �]�m�ʵe
        checkAnim.SetBool("IsCheck", canCheck);

        // �ݨ�~��l
        if (IsLook(lookTarget))
            follorPos = lookTarget.position;
        Goto(follorPos);

        if (statusTime > 10f || checkTime > 3f)
        {
            // �p�G�O�ۤv�H�N�[�J�B�ͦC��
            if (checkTime > 3f)
            {
                if (lookTarget.gameObject.GetComponent<CubeAI>() != null)
                {
                    friend.Add(lookTarget.GetInstanceID());
                }
                if (lookTarget.gameObject.GetComponent<Food>() != null)
                {
                    friend.Add(lookTarget.GetInstanceID());
                    foodPos.Add(lookTarget.transform.position);
                }
            }
            status = AIStatus.IDLE;
        }
    }
    float checkTime = 0f;
    void UpdateCHECK()
    {
        if (canCheck)
            checkTime += Time.deltaTime;
    }
    void ExitCHECK()
    {
        checkAnim.SetBool("IsCheck", false);
        StopGoto();
        speedP = 1f;
        rSpeedP = 1f;
    }
    #endregion
    #region �䭹��
    void OnFINDFOOD()
    {
        if (foodPos.Count <= 0)
        {
            status = AIStatus.IDLE;
            return;
        }
        Goto(foodPos[foodPos.Count - 1], ToFoodPos);
    }
    void ToFoodPos()
    {
        // ��w�I�S��쭹��
        foodPos.RemoveAt(foodPos.Count - 1);
        status = AIStatus.IDLE;
    }
    void UpdateFINDFOOD()
    {
        if (statusTime > 15f)
            status = AIStatus.IDLE;
    }
    void ExitFINDFOOD()
    {
        StopGoto();
    }
    #endregion
    #region �Y����
    Transform foodTarget = null;
    void OnEAT()
    {
        Goto(foodTarget.position, EatDone);
    }
    void EatDone()
    {
        StopGoto();
    }
    void UpdateEAT()
    {
        if (foodTarget == null)
        {
            status = AIStatus.IDLE;
            return;
        }
        float dir = Vector3.Distance(foodTarget.position, this.transform.position);
        if (dir < 1f)
        {
            foodTarget.GetComponent<Food>().BeEat();
            energy = maxEnergy;
            status = AIStatus.IDLE;
        }
        if (statusTime > 5f)
        {
            status = AIStatus.IDLE;
        }
    }
    void ExitEAT()
    {
        StopGoto();
    }
    #endregion
    #region ���u����
    [SerializeField] LayerMask funs;
    [SerializeField] LayerMask food;
    Transform lookTarget = null;
    protected override void FixedUpdate30()
    {
        base.FixedUpdate30();
        float r = 10;
        float a = 90;
        switch(status)
        {
            case AIStatus.IDLE:
                r = 20f;
                a = 90f;
                break;
            case AIStatus.MOVE:
                r = 20f;
                a = 30f;
                break;
            case AIStatus.LOOK:
                r = 20f;
                a = 120f;
                break;
        }
        // �j�d�i�ù�H (�����B��)
        if (status == AIStatus.IDLE || status == AIStatus.MOVE || status == AIStatus.LOOK)
        {
            Transform target = EyeLook(funs, r, a, null, friend);
            if (target != null)
            {
                lookTarget = target;
                status = AIStatus.CHECK;
            }
        }
        if (status == AIStatus.FIND_FOOD)
        {
            Transform target = EyeLook(food, r, a, null);
            if (target != null)
            {
                foodTarget = target;
                status = AIStatus.EAT_FOOD;
            }
        }
    }
    #endregion
    #region �֥[���j��
    protected override void Update()
    {
        base.Update();
        energy -= Time.deltaTime;
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }
    #endregion
    /// <summary>��q</summary>
    float energy = 100f;
    /// <summary>��q�W��</summary>
    float maxEnergy = 100f;
    /// <summary>�{�Ѫ��͵���H</summary>
    List<int> friend = new List<int>();
    /// <summary>�O�Ф���������m</summary>
    List<Vector3> foodPos = new List<Vector3>();
}
public enum AIStatus
{
    /// <summary>�ݩR</summary>
    IDLE,
    /// <summary>���}</summary>
    MOVE,
    /// <summary>�ݬݪ���</summary>
    LOOK,
    /// <summary>�T�{����</summary>
    CHECK,
    /// <summary>�䭹��</summary>
    FIND_FOOD,
    /// <summary>�Y����</summary>
    EAT_FOOD,
}
