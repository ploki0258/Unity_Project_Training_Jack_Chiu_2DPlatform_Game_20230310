using UnityEngine;

/// <summary>
/// 技能資料
/// </summary>
[CreateAssetMenu(fileName = "New Skill", menuName = "Add New Skill")]
public class Skill : ScriptableObject
{
	// 共同
	[Header("技能ID")]
	public int id;
	[Header("是否有前置技能")]
	public bool Pre_Skill;
	[Header("前置ID")]
	public int Pre_id;
	[Header("技能名稱")]
	public string skillName;
	[Header("技能圖示")]
	public Sprite skillIcon;
	[Header("技能特效")]
	public GameObject skillPrefab;
	[Header("技能屬性")]
	public Color skillCate;
	public TypeColor typeColor;
	[Header("特殊技")]
	public bool isSpecial;
	[Header("技能類別")]
	public TypeSkill typeSkill = TypeSkill.NormalSkill;
	[Header("攻擊形式")]
	public string attackDis;
	[Header("技能敘述"), TextArea(5, 5)]
	public string skillDis;
	[Header("魔力消耗")]
	public int skillCost;
	[Header("技能費用")]
	public int skillCoinCost;
	public int skillPointCost;
	[Header("技能傷害值")]
	public float skillDamage;
	[Header("施法速度")]
	public float skillSpeed;
	// 火
	[Header("燃燒位置")]
	public Transform burntPos = null;
	[Header("持續傷害值"), Range(0, 100)]
	public float holdDamage = 0f;
	// 風
	[Header("敵人擊退距離"), Range(0, 100)]
	public float enemyDis;
	// 冰
	[Header("降低敵人速度"), Range(0, 100)]
	public float enemySlowSpeed;
	[Header("敵人延遲時間"), Range(0, 100)]
	public float enemyDelayTime;
	// 土
	[Header("技能持續時間"), Range(0, 100)]
	public float skillHoldTime;
}

public enum TypeSkill
{
	NormalSkill, SpecialSkill
}

public enum TypeColor
{
	Red, Yellow, Green, Blue
}
