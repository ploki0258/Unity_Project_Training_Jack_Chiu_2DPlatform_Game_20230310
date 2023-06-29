using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform[] skillSlot; // 技能欄位，用於接受技能的拖放
	public int currentSkillIndex; // 當前選擇的技能
	Skill skillData;

	private void Update()
	{
		ChangeAtkObject();
	}

	/// <summary>
	/// 設置當前技能
	/// </summary>
	/// <param name="skill">技能ID</param>
	public void SetCurrentSkill(int skill)
	{
		// 在這裡可以執行相應的施放技能的行為
		currentSkillIndex = skill;
	}

	/// <summary>
	/// 切換攻擊物件
	/// </summary>
	void ChangeAtkObject()
	{
		List<int> skillPrefabs = SaveManager.instance.playerData.haveSkill;

		SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
		Skill skill = FindObjectOfType<Skill>();
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			currentSkillIndex = 0;
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			currentSkillIndex = 1;
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			currentSkillIndex = 2;
		}

		// 根據當前技能索引設置攻擊物件
		if (currentSkillIndex >= 0 && currentSkillIndex < skillPrefabs.Count)
		{
			int skillID = skillData.id;
			skillSystem.SetCurrentSkill(skillID);

			PlayerCtrl.instance.atkObject = skill.skillPrefab;
			Debug.Log("切換為：" + skill.skillPrefab.name);
		}
	}

}
