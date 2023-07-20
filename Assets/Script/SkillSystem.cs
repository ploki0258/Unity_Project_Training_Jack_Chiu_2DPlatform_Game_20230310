using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform[] skillSlot; // 技能欄位，用於接受技能的拖放
	public int currentSkillIndex; // 當前選擇的技能

	Skill skillData;
	int keyboard;

	private void Awake()
	{
		skillData = FindObjectOfType<Skill>();
	}

	private void Update()
	{
		SwitchAtkObject();
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
	void SwitchAtkObject()
	{
		List<int> skillPrefabs = SaveManager.instance.playerData.haveSkill;

		SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
		Skill skill = FindObjectOfType<Skill>();

		if (Input.GetKeyDown(KeyCode.Z))
		{
			keyboard = 0;
			currentSkillIndex = skillSlot[0].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			Debug.Log("快捷鍵Z： " + currentSkillIndex);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			keyboard = 1;
			currentSkillIndex = skillSlot[1].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			// PlayerCtrl.instance.atkObject = skillSlot[1].GetComponent<Skill>().skillPrefab;
			Debug.Log("快捷鍵X： " + currentSkillIndex);
			// currentSkillIndex = 1;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			keyboard = 2;
			currentSkillIndex = skillSlot[2].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			// PlayerCtrl.instance.atkObject = skillSlot[2].GetComponent<Skill>().skillPrefab;
			Debug.Log("快捷鍵C： " + currentSkillIndex);
			// currentSkillIndex = 2;
		}

		if (currentSkillIndex >= 0 && currentSkillIndex < SaveManager.instance.playerData.haveSkill.Count)
		{
			PlayerCtrl.instance.atkObject = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillPrefab;
			Debug.Log(currentSkillIndex + PlayerCtrl.instance.atkObject.name);
		}

		// 根據當前技能索引設置攻擊物件
		/*
		if (currentSkillIndex >= 0)
		   {
		int skillID = skillData.id;
		skillSystem.SetCurrentSkill(skillID);
		PlayerCtrl.instance.atkObject = skillSlot[currentSkillIndex].GetComponent<SkillDragDrop>().skillData.skillPrefab;
		Debug.Log("切換為：" + skill.skillPrefab.name);
		   }
		*/
	}

}
