using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{
    [Header("技能位置")]
    [SerializeField] Transform pointSkill;
    [Header("技能名稱")]
    [SerializeField] string skillName = null;
    [Header("施法按鍵")]
    public KeyCode activationKey;
    [Header("技能特效")]
    public GameObject skillPrefab = null;
    [Header("當前技能特效")]
    SkillManager currentSkill;

    public List<GameObject> skillPrefabs = new List<GameObject>();

    /// <summary>
    /// 施放法術
    /// </summary>
    /// <param name="index"></param>
    public void CastSkill(int index)
    {
        // 如果 index 小於 0  或 大於等於 列表個數 就停止執行
        if (index < 0 || index >= skillPrefabs.Count)
            return;
        GameObject skillPfb = skillPrefabs[index];
        Instantiate(skillPfb, pointSkill.position, Quaternion.identity);
    }

    private void Update()
    {
        CastSpell();
    }

    /// <summary>
    /// 施放法術(按鍵)
    /// </summary>
    public void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(skillPrefab, pointSkill.position, Quaternion.identity);
        }
    }
}