using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform skillSlot; // 技能欄位，用於接受技能的拖放
	public Skill currentSkill; // 當前選擇的技能

	public void SetCurrentSkill(Skill skill)
	{
		// 在這裡可以執行相應的施放技能的行為
		currentSkill = skill;
	}
}
