using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
	[Header("技能欄按鈕陣列"), Tooltip("用於儲存技能施放的按鈕")]
	public Transform[] skillSlot; // 技能欄位，用於接受技能的拖放
	[Header("當前技能ID"), Tooltip("當前選擇的技能ID")]
	public int currentSkillIndex; // 當前選擇的技能ID
	[Header("文字顯示顏色")]
	public Color textColor = new Color();

	Skill skillData;	// 技能資料
	int keyboard;		// 按鈕數字
	//SpriteRenderer iconSkill = null;

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

		// 如果 技能ID >= 0 且 已擁有該技能時
		// 才設置攻擊物件
		if (currentSkillIndex >= 0 && SaveManager.instance.playerData.IsHaveSkill(currentSkillIndex))
		{
			PlayerCtrl.instance.atkObject = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillPrefab;
			//Debug.Log($"<color=yellow>{currentSkillIndex + "\n" + PlayerCtrl.instance.atkObject.name}</color>");
			//Debug.Log($"<color=green>{currentSkillIndex + "\n" + skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillPrefab.name}</color>");
			//Debug.Log($"<Color=blue>{keyboard}</color>");
		}
	}

	/// <summary>
	/// 切換攻擊物件
	/// </summary>
	public void SwitchAtkObject()
	{
		List<int> skillPrefabs = SaveManager.instance.playerData.haveSkill;

		SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
		Skill skill = FindObjectOfType<Skill>();

		if (Input.GetKeyDown(KeyCode.Z))
		{
			keyboard = 0;

			if (skillSlot[0].GetComponentInChildren<SkillDragDrop>())
			{
				// 當前選擇的技能ID = 技能槽位置.取得組件<SkillDragDrop>().技能資料.id
				currentSkillIndex = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.id;
				// 設置技能玩家的攻擊物件
				SetCurrentSkill(currentSkillIndex);
				//iconSkill.sprite = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillIcon;
				//iconSkill.color = new Color(1f, 1f, 1f, 1f);
			}
			Debug.Log("快捷鍵Z： " + currentSkillIndex);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			keyboard = 1;

			if (skillSlot[1].GetComponentInChildren<SkillDragDrop>())
			{
				currentSkillIndex = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.id;
				SetCurrentSkill(currentSkillIndex);
				//iconSkill.sprite = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillIcon;
				//iconSkill.color = new Color(1f, 1f, 1f, 1f);
			}
			Debug.Log("快捷鍵X： " + currentSkillIndex);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			keyboard = 2;

			if (skillSlot[2].GetComponentInChildren<SkillDragDrop>())
			{
				currentSkillIndex = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.id;
				SetCurrentSkill(currentSkillIndex);
				//iconSkill.sprite = skillSlot[keyboard].GetComponentInChildren<SkillDragDrop>().skillData.skillIcon;
				//iconSkill.color = new Color(1f, 1f, 1f, 1f);
			}
			Debug.Log("快捷鍵C： " + currentSkillIndex);

			// PlayerCtrl.instance.atkObject = skillSlot[2].GetComponent<Skill>().skillPrefab;
			// currentSkillIndex = 2;
		}

		#region 測試
		/*
		for (int i = 0; i < 3; i++)
		{
			if (i == keyboard)
			{
				skillSlot[i].GetComponentInChildren<Image>().color = new Color(255f, 255f, 255f, 1f);
			}
			else
			{
				skillSlot[i].GetComponentInChildren<Image>().color = new Color(255f, 255f, 255f, 0.8f);
			}
		}*/

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
		#endregion
	}
}
