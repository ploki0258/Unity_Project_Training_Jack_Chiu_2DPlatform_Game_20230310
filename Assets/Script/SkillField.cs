﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
	[SerializeField, Header("技能圖示")]
	Image iconSkill;
	[SerializeField, Header("技能名稱")]
	Text titleSkill;
	[SerializeField, Header("技能說明")]
	Text infoSkill;
	[SerializeField, Header("技能按鈕：學習按鈕、已習得、尚未解鎖")]
	GameObject skillBtn;

	[Tooltip("是否已學習該技能")]
	bool alreSkill;     // 是否已學習該技能
	[Tooltip("技能資料")]
	Skill skillData;    // 技能資料
	Text skillBtnText;
	
	private void Awake()
	{

		iconSkill.color = new Color(255, 255, 255, 50);
		iconSkill.transform.localScale = Vector3.zero;
		titleSkill.text = "";
		infoSkill.text = "";
		skillBtn.SetActive(false);
	}

	private void Start()
	{
		skillBtnText = skillBtn.GetComponentInChildren<Text>();
	}

	/// <summary>
	/// 點擊技能圖示初始化技能：顯示該技能的資訊
	/// </summary>
	/// <param name="id">技能ID</param>
	public void 初始化技能(int id)
	{
		skillBtn.SetActive(true);
		// 查找技能資料
		skillData = SkillManager.instance.FindSkillByID(id);

		iconSkill.sprite = skillData.skillIcon;
		iconSkill.transform.localScale = (iconSkill.sprite == null) ? Vector3.zero : Vector3.one;
		titleSkill.text = skillData.skillName;
		infoSkill.text = skillData.skillDis + "\n" + "\n花費金幣：" + skillData.skillCoinCost.ToString() + "\n花費技能點數" + skillData.skillPointCost;
		// infoSkill[1].text = "花費金幣：" + skillData.skillCoinCost.ToString() + "\n花費技能點數" + skillData.skillPointCost;
		// 如果玩家沒有該技能的話 才顯示按鈕
		alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
		// 如果沒有學習該技能 則顯示學習按鈕
		// 如果已經學習該技能 顯示已習得按鈕
		skillBtnText.text = alreSkill == false ? "學習" : "已習得";
		// 如果玩家沒有該技能的話
		if (alreSkill == false)
		{
			// Debug.Log("無此技能");
			Skill currentlySkill = SkillManager.instance.FindSkillByID(id);
			// 該技能有前置技能
			if (currentlySkill.Pre_Skill == true)
			{
				// Debug.Log("該技能有前置技能");
				// 尚未取得前置技能
				if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == false)
				{
					// Debug.Log("需先習得前一個技能");
					skillBtnText.text = "尚未解鎖";    // 顯示尚未解鎖按鈕
				}
			}
		}
		#region 測試(原)
		// 如果玩家沒有該技能 且 沒有取得該技能的前置技能 則顯示"尚未解鎖"字樣
		// 如果玩家沒有該技能 且 有取得該技能的前置技能 或 如果玩家沒有該技能 且 該技能的沒有前置技能 則顯示"學習"字樣
		// 如果玩家已經取得該技能 則顯示"已習得"字樣
		/*for (int i = 0; i < SkillManager.instance.AllSkillData.Length; i++)
		{
			// 如果玩家沒有該技能
			if (SaveManager.instance.playerData.IsHaveSkill(SkillManager.instance.AllSkillData[i].id) == false)
			{
				Skill currentlySkill = SkillManager.instance.FindSkillByID(SkillManager.instance.AllSkillData[i].id);
				// 該技能有前置技能
				if (currentlySkill.Pre_Skill == true)
				{
					// 尚未取得前置技能
					if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == false)
					{
						btnSkill[1].SetActive(true);
						btnSkill[0].SetActive(false);
						btn2Text.text = "尚未解鎖";
					}
					// 已取得前置技能
					else if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == true)
					{
						btnSkill[0].SetActive(true);
						btnSkill[1].SetActive(false);
						btn1Text.text = "學習";
					}
				}
				// 該技能沒有前置技能
				else if (currentlySkill.Pre_Skill == false)
				{
					btnSkill[0].SetActive(true);
					btnSkill[1].SetActive(false);
					btn1Text.text = "學習";
				}
			}
			// 如果玩家有該技能
			else if (SaveManager.instance.playerData.IsHaveSkill(SkillManager.instance.AllSkillData[i].id) == true)
			{
				btnSkill[1].SetActive(true);
				btnSkill[0].SetActive(false);
				btn2Text.text = "已習得";
			}
		}
		*/
		#endregion
	}

	/// <summary>
	/// 學習技能：按下學習按鈕，可學習該技能
	/// </summary>
	public void LearnSkill()
	{
		// 判斷能否學習技能
		if (SaveManager.instance.playerData.coinCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
		{
			Debug.Log($"<color=#90ff06>升級技能： { + skillData.id + "\n已習得：" + skillData.skillName}</color>");
			// 扣錢
			SaveManager.instance.playerData.coinCount -= skillData.skillCoinCost;
			// 扣技能點數
			SaveManager.instance.playerData.skillPoint -= skillData.skillPointCost;
			// 到玩家紀錄中 添加技能列表(ID)
			SaveManager.instance.playerData.haveSkill.Add(skillData.id);
			// 買完後進行存檔
			SaveManager.instance.SaveData();
			// 手動刷新一次技能欄資訊
			初始化技能(skillData.id);
		}
		else
		{
			Debug.Log($"<color=white>金幣或點數不足" + "\n無法學習此技能</color>");
		}
	}
}
