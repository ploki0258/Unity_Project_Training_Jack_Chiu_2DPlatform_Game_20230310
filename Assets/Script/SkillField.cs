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
	Text[] infoSkill;
	[SerializeField, Header("技能按鈕")]
	GameObject[] btnSkill;

	bool alreSkill;     // 是否已學習該技能
	Skill skillData;    // 技能資料

	private void Awake()
	{
		iconSkill.transform.localScale = Vector3.zero;
		titleSkill.text = "";
		infoSkill[0].text = "";
		infoSkill[1].text = "";
		btnSkill[0].SetActive(false);
		btnSkill[1].SetActive(false);
	}

	public void 初始化技能(int id)
	{
		// 查找資料
		skillData = SkillManager.instance.FindSkillByID(id);

		iconSkill.sprite = skillData.skillIcon;
		iconSkill.transform.localScale = (iconSkill.sprite == null) ? Vector3.zero : Vector3.one;
		titleSkill.text = skillData.skillName;
		infoSkill[0].text = skillData.skillDis;
		infoSkill[1].text = "花費金幣：" + skillData.skillCoinCost.ToString() + "\n花費技能點數" + skillData.skillPointCost;
		// 如果玩家沒有該技能的話 才顯示按鈕
		alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
		btnSkill[0].SetActive(alreSkill == false);
		btnSkill[1].SetActive(alreSkill == true);
	}

	/// <summary>
	/// 學習技能
	/// </summary>
	public void LearnSkill()
	{
		// 判斷能否學習技能
		if (SaveManager.instance.playerData.moneyCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
		{
			Debug.Log("升級技能： " + skillData.id);
			// 扣錢
			SaveManager.instance.playerData.moneyCount -= skillData.skillCoinCost;
			// 扣技能
			SaveManager.instance.playerData.skillPoint -= skillData.skillPointCost;
			// 到玩家紀錄中 添加技能列表(ID)
			SaveManager.instance.playerData.haveSkill.Add(skillData.id);
			// 買完後進行存檔
			SaveManager.instance.SaveData();
			// 手動刷新一次技能列表
			// 商城介面.ins.刷新商店();
		}
		else
		{
			Debug.Log("金幣或點數不足");
		}
	}
}
