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
        // 飢餓度隨機起始
        energy = Random.Range(maxEnergy * 0.5f, maxEnergy);
    }
    #region 待命
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
    #region 閒逛
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
    #region 到處看看
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
    #region 檢查對方
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

        // 靠近就停下 離開就加速
        if (isClose)
            speedP = 0.01f;
        else
            speedP = 1.5f;

        // 設置動畫
        checkAnim.SetBool("IsCheck", canCheck);

        // 看到才能追
        if (IsLook(lookTarget))
            follorPos = lookTarget.position;
        Goto(follorPos);

        if (statusTime > 10f || checkTime > 3f)
        {
            // 如果是自己人就加入朋友列表
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
    #region 找食物
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
        // 到定點沒找到食物
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
    #region 吃食物
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
    #region 視線模擬
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
        // 搜查可疑對象 (忽略朋友)
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
    #region 累加飢餓度
    protected override void Update()
    {
        base.Update();
        energy -= Time.deltaTime;
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }
    #endregion
    /// <summary>能量</summary>
    float energy = 100f;
    /// <summary>能量上限</summary>
    float maxEnergy = 100f;
    /// <summary>認識的友善對象</summary>
    List<int> friend = new List<int>();
    /// <summary>記憶中的食物位置</summary>
    List<Vector3> foodPos = new List<Vector3>();
}
public enum AIStatus
{
    /// <summary>待命</summary>
    IDLE,
    /// <summary>閒逛</summary>
    MOVE,
    /// <summary>看看附近</summary>
    LOOK,
    /// <summary>確認身分</summary>
    CHECK,
    /// <summary>找食物</summary>
    FIND_FOOD,
    /// <summary>吃食物</summary>
    EAT_FOOD,
}
