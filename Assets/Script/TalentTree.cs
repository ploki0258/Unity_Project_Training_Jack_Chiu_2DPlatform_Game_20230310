using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
    [Header("�ޯ�ID")]
    public int[] id;

    /// <summary>
    /// �I��k�N
    /// </summary>
    /// <param name="id"></param>
    public void PressSkill(int id)
    {
        Debug.Log("���U���s�G" + id + "���ޯ�");
        GetComponent<SkillField>().��l�Ƨޯ�(id);
    }
}
