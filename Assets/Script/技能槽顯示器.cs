using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 技能槽顯示器 : MonoBehaviour
{
	[SerializeField, Header("技能槽選擇狀態"), Tooltip("技能槽選擇狀態")]
	ZXCType zXCType = ZXCType.NONE;
	[SerializeField, Header("技能圖示"), Tooltip("要顯示的技能圖示")]
	Image imageSkill = null;
	[SerializeField, Header("欄位標示"), Tooltip("要顯示的欄位標示")]
	Text textSlot = null;

	private void Start()
	{
		SaveManager.instance.playerData.renewSkillSlotZXC += 刷新內容;
		SaveManager.instance.playerData.renewSkillSlotZXC += HideSkill;
		SkillSystem.instance.選到的技能變化了 += HideSkill;
		// 強制刷新事件
		HideSkill();
		刷新內容();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewSkillSlotZXC -= 刷新內容;
		SaveManager.instance.playerData.renewSkillSlotZXC -= HideSkill;
		SkillSystem.instance.選到的技能變化了 -= HideSkill;
	}

	/// <summary>
	/// 隱藏技能圖示
	/// </summary>
	void HideSkill()
	{
		//image.color = skillSystem.當前選到的技能 == zXCType ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.5f);
		// 如果 技能系統.當前選到的技能 等於 自己時
		if (SkillSystem.instance.當前選到的技能 == zXCType)
		{
			// 技能圖示.顏色 為完全顯示
			imageSkill.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			// 技能圖示.顏色 為半透明顯示
			imageSkill.color = new Color(1f, 1f, 1f, 0.5f);
		}
	}

	/// <summary>
	/// 更新技能槽的內容並顯示
	/// </summary>
	void 刷新內容()
	{
		// 如果 技能槽選擇狀態 等於 狀態Z的話
		if (zXCType == ZXCType.Z)
		{
			// 欄位標示.文字 = Z
			textSlot.text = "Z";
			//  如果 存檔管理器.玩家資料.技能槽Z的技能ID 不等於 -1的話 表示技能槽Z有技能
			if (SaveManager.instance.playerData.skillZ != -1)
			{
				// 依據技能槽Z的技能ID回傳技能資料並儲存在skill
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillZ);
				// 技能圖示 = 技能資料.技能圖示
				imageSkill.sprite = skill.skillIcon;
			}
			// 依據技能槽是否有技能 來顯示技能圖示
			imageSkill.transform.localScale = SaveManager.instance.playerData.skillZ != -1 ? Vector3.one : Vector3.zero;
		}

		if (zXCType == ZXCType.X)
		{
			// 欄位標示.文字 = X
			textSlot.text = "X";
			//  如果 存檔管理器.玩家資料.技能槽X的技能ID 不等於 -1的話 表示技能槽X有技能
			if (SaveManager.instance.playerData.skillX != -1)
			{
				// 依據技能槽X的技能ID回傳技能資料並儲存在skill
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillX);
				// 技能圖示 = 技能資料.技能圖示
				imageSkill.sprite = skill.skillIcon;
			}
			// 顯示技能圖示
			imageSkill.transform.localScale = SaveManager.instance.playerData.skillZ != -1 ? Vector3.one : Vector3.zero;
		}

		if (zXCType == ZXCType.C)
		{
			// 欄位標示.文字 = C
			textSlot.text = "C";
			//  如果 存檔管理器.玩家資料.技能槽C的技能ID 不等於 -1的話 表示技能槽C有技能
			if (SaveManager.instance.playerData.skillC != -1)
			{
				// 依據技能槽C的技能ID回傳技能資料並儲存在skill
				Skill skill = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillC);
				// 技能圖示 = 技能資料.技能圖示
				imageSkill.sprite = skill.skillIcon;
			}
			// 顯示技能圖示
			imageSkill.transform.localScale = SaveManager.instance.playerData.skillZ != -1 ? Vector3.one : Vector3.zero;
		}
	}
}

/// <summary>
/// 列舉：選單類型：技能槽選擇類別
/// </summary>
public enum ZXCType
{
	// 選擇狀態：Z、X、C、NONE
	Z, X, C, NONE
}
