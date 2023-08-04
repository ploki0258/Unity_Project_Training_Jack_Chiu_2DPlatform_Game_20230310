using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
	[Header("技能ID")]
	public int[] id;
	[Header("技能圖示")]
	public SpriteRenderer[] iconSkillArray = null;
	[SerializeField, Header("尚未擁有之顏色")]
	Color clolrNoSkill;

	bool alreSkill;     // 是否已學習該技能
	Skill skillData;    // 技能資料

	public static TalentTree instance = null;

	private void Awake()
	{
		instance = this;
	}

	public void ShowSkillIcon(int id)
	{
		// 查找資料
		skillData = SkillManager.instance.FindSkillByID(id);

		for (int i = 0; i < iconSkillArray.Length; i++)
		{
			// 如果沒有該技能 且該技能無前置技能 則直接顯示圖示
			if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) == false && skillData.Pre_Skill == false)
			{
				iconSkillArray[i].sprite = skillData.skillIcon;
				Debug.Log("有可學習的技能");
			}
			// 如果沒有該技能 且尚未取得前置技能 顯示灰階
			else if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) == false && skillData.Pre_Skill == true)
			{
				if (SaveManager.instance.playerData.IsHaveSkill(skillData.Pre_id) == false)
				{
					iconSkillArray[i].color = clolrNoSkill;
					Debug.Log("尚無可學習的技能");
				}
			}
		}
	}
}
