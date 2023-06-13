using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Add New Skill")]
public class Skill : ScriptableObject
{
    // 共同
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
    [Header("特殊技")]
    public bool isSpecial;
    [Header("攻擊形式")]
    public string attackDis;
    [Header("技能敘述")]
    [TextArea(5, 5)] public string skillDis;
    [Header("技能費用")]
    public int skillCoinCost;
    public int skillPointCost;
    [Header("傷害值")]
    public float skillAttack;
    [Header("施法速度")]
    public float skillSpeed;
    // 火
    [Header("燃燒位置")]
    public Transform burntPos = null;
    // 風
    [Header("敵人擊退距離")]
    public float enemyDis;
    // 冰
    [Header("降低敵人速度")]
    public float enemySlowSpeed;
    [Header("敵人延遲時間")]
    public float enemyDelayTime;
    // 土
    [Header("技能持續時間")]
    public float skillHoldTime;
}
