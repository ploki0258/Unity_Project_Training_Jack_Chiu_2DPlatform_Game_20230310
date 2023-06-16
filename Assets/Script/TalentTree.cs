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
    /// <param name="id"></param>
    public void PressSkill(int id)
    {
        Debug.Log("按下按鈕：" + id + "號技能");
        GetComponent<SkillField>().初始化技能(id);
    }
}
