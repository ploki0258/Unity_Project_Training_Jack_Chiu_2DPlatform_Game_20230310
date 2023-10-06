using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 技能曹顯示器 : MonoBehaviour
{
	SkillSystem skillSystem;
	[SerializeField] ZXCType zXCType = ZXCType.Z;

	private void Awake()
	{
		skillSystem = FindObjectOfType<SkillSystem>();
	}

	private void Start()
	{
		SaveManager.instance.playerData.renewSkillZXC += 刷新內容;
		SaveManager.instance.playerData.renewSkillZXC += HideSkill;
		skillSystem.選到的技能變化了 += HideSkill;
		HideSkill();
		刷新內容();
	}
	private void OnDisable()
	{
		SaveManager.instance.playerData.renewSkillZXC -= 刷新內容;
		SaveManager.instance.playerData.renewSkillZXC -= HideSkill;
		skillSystem.選到的技能變化了 -= HideSkill;
	}

	/// <summary>
	/// 隱藏技能圖示
	/// </summary>
	void HideSkill()
	{
		//image.color = skillSystem.當前選到的技能 == zXCType ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.5f);
		if (skillSystem.當前選到的技能 == zXCType)
		{
			image.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			image.color = new Color(1f, 1f, 1f, 0.5f);
		}
	}

	[SerializeField] Image image = null;
	[SerializeField] Text text = null;
	void 刷新內容()
	{
		if (zXCType == ZXCType.Z)
		{
			text.text = "Z";
			if (SaveManager.instance.playerData.skillZ != -1)
			{
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillZ);
				image.sprite = skill.skillIcon;
				image.transform.localScale = Vector3.one;
			}
			else
			{
				image.transform.localScale = Vector3.zero;
			}
		}
		if (zXCType == ZXCType.X)
		{
			text.text = "X";
			if (SaveManager.instance.playerData.skillX != -1)
			{
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillX);
				image.sprite = skill.skillIcon;
				image.transform.localScale = Vector3.one;
			}
			else
			{
				image.transform.localScale = Vector3.zero;
			}
		}
		if (zXCType == ZXCType.C)
		{
			text.text = "C";
			if (SaveManager.instance.playerData.skillC != -1)
			{
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillC);
				image.sprite = skill.skillIcon;
				image.transform.localScale = Vector3.one;
			}
			else
			{
				image.transform.localScale = Vector3.zero;
			}
		}
	}
}

public enum ZXCType
{
	Z, X, C, NONE
}
