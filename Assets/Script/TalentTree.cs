using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
    [Header("技能ID")]
    public int[] id;

    /// <summary>
    /// 施放法術
    /// </summary>
    /// <param name="index"></param>
    public void CastSkill(int index)
    {
        Debug.Log("按下按鈕：" + index + "號技能");
    }
}
