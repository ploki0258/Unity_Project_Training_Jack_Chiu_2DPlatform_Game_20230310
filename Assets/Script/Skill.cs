using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Add New Skill")]
public class Skill : ScriptableObject
{
    [Header("技能ID")]
    public int id;
    [Header("技能名稱")]
    public string skillName;
    [Header("技能圖示")]
    public Sprite skillIcon;
    [Header("技能特效")]
    public GameObject skillPrefab;
    [Header("技能類別")]
    public Color skillCate;
    [Header("技能敘述")]
    [TextArea(5, 5)] public string skillDis;
    [Header("技能費用")]
    public int skillCoinCost;
    public int skillPointCost;
    [Header("傷害值")]
    public float skillAttack;
    [Header("施法速度")]
    public float skillSpeed;
    // [Header("技能持續時間")]
    // public float skillHoldTime;
}
