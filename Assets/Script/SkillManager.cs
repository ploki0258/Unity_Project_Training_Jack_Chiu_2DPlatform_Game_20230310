using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// 技能管理器
/// </summary>
public class SkillManager
{
	#region 單例
	public static SkillManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SkillManager();
			}
			return _instance;
		}
	}
	static SkillManager _instance = null;
	#endregion

	// 所有技能的資料
	public Skill[] AllSkillData = new Skill[0];

	/// <summary>
	/// 初始化：載入所有技能資料
	/// </summary>
	public void Initialization()
	{
		AllSkillData = Resources.LoadAll<Skill>("");
	}

	/// <summary>
	/// 找技能資料
	/// </summary>
	/// <param name="id">技能ID</param>
	/// <returns></returns>
	public Skill FindSkillByID(int id)
	{
		for (int i = 0; i < AllSkillData.Length; i++)
		{
			if (AllSkillData[i].id == id)
			{
				return AllSkillData[i];
			}
		}
		Debug.LogError("ID：" + id + "查無此ID");
		return new Skill();
	}

	/*
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
	*/	
}