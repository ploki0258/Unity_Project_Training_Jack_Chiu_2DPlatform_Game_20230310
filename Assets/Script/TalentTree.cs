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
    /// <param name="index"></param>
    public void CastSkill(int index)
    {
        Debug.Log("���U���s�G" + index + "���ޯ�");
    }
}
