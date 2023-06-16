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
}