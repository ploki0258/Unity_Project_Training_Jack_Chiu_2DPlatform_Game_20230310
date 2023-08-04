using System.Collections;
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
	[SerializeField, Header("技能按鈕")]
	GameObject[] btnSkill;
	// [SerializeField, Header("按鈕文字")]
	// Text btnText;

	bool alreSkill;     // 是否已學習該技能
	Skill skillData;    // 技能資料
	Text btn1Text;
	Text btn2Text;

	private void Awake()
	{

		iconSkill.color = new Color(255, 255, 255, 50);
		iconSkill.transform.localScale = Vector3.zero;
		titleSkill.text = "";
		infoSkill.text = "";
		// infoSkill[1].text = "";
		btnSkill[0].SetActive(false);
		btnSkill[1].SetActive(false);
		btnSkill[2].SetActive(false);
	}

	private void Start()
	{
		// btn1Text = btnSkill[0].GetComponentInChildren<Text>();
		// btn2Text = btnSkill[1].GetComponentInChildren<Text>();
	}

	/// <summary>
	/// 點擊技能圖示初始化技能：顯示該技能的資訊
	/// </summary>
	/// <param name="id"></param>
	public void 初始化技能(int id)
	{
		// 查找資料
		skillData = SkillManager.instance.FindSkillByID(id);

		iconSkill.sprite = skillData.skillIcon;
		iconSkill.transform.localScale = (iconSkill.sprite == null) ? Vector3.zero : Vector3.one;
		titleSkill.text = skillData.skillName;
		infoSkill.text = skillData.skillDis + "\n" + "\n花費金幣：" + skillData.skillCoinCost.ToString() + "\n花費技能點數" + skillData.skillPointCost;
		// infoSkill[1].text = "花費金幣：" + skillData.skillCoinCost.ToString() + "\n花費技能點數" + skillData.skillPointCost;
		// 如果玩家沒有該技能的話 才顯示按鈕
		alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
		btnSkill[0].SetActive(alreSkill == false);  // 顯示學習按鈕
		btnSkill[1].SetActive(alreSkill == true);   // 顯示已習得按鈕
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
					btnSkill[0].SetActive(false);
					btnSkill[1].SetActive(false);
					btnSkill[2].SetActive(true);    // 顯示尚未解鎖按鈕
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
		if (SaveManager.instance.playerData.moneyCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
		{
			Debug.Log($"<color=#90ff06>升級技能： { + skillData.id + "\n已習得：" + skillData.skillName}</color>");
			// 扣錢
			SaveManager.instance.playerData.moneyCount -= skillData.skillCoinCost;
			// 扣技能
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
			Debug.Log("金幣或點數不足" + "\n無法學習此技能");
		}
	}
}
