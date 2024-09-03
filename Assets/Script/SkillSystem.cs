using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
	[Header("技能欄按鈕陣列"), Tooltip("用於儲存技能施放的按鈕")]
	public Transform[] skillSlot; // 技能欄位，用於接受技能的拖放

	[Header("文字顯示顏色")]
	public Color textColor = new Color();

	public int keyCodeNumber;                   // 按鈕數字
	Skill skillData;                            // 技能資料
	[SerializeField] KeyCode currentKeyCode;    // 當前紀錄的鍵盤按鈕

	// 單例模式
	public static SkillSystem instance = null;

	private void Awake()
	{
		instance = this;
		skillData = FindObjectOfType<Skill>();
	}

	private void Start()
	{
		if (SaveManager.instance.playerData.skillZ != -1 || SaveManager.instance.playerData.skillX != -1 || SaveManager.instance.playerData.skillC != -1)
			當前選到的技能 = ZXCType.NONE;
		SaveManager.instance.playerData.renewSkillSlotZXC += 技能發生變化;
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewSkillSlotZXC -= 技能發生變化;
	}

	private void Update()
	{
		SwitchAtkObject();
		//KeepKeyCode();
	}

	/// <summary>將編號轉換為位置</summary>
	public Transform GetTransformBySlotID(int slotID)
	{
		if (slotID < 0)
		{
			Debug.Log("數值最小為0");
			return null;
		}
		if (slotID >= skillSlot.Length)
		{
			Debug.Log("數值應該小於" + skillSlot.Length);
			return null;
		}
		return skillSlot[slotID];
	}

	void 技能發生變化()
	{
		當前選到的技能 = 當前選到的技能;
	}

	public ZXCType 當前選到的技能
	{
		get { return _當前選到的技能; }
		set
		{
			_當前選到的技能 = value;

			// 如果 當前選到的技能 等於 狀態NONE
			if (_當前選到的技能 == ZXCType.NONE)
				// 玩家控制器.攻擊物件 = null
				PlayerCtrl.instance.atkObject = null;
			// 否則 依據選擇的狀態切換玩家的攻擊物件
			else
			{
				int id = -1;
				// 如果 當前選到的技能 等於 狀態Z
				if (_當前選到的技能 == ZXCType.Z)
					// id = 存檔管理器.玩家資料.技能槽Z儲存的技能ID
					id = SaveManager.instance.playerData.skillZ;
				// 否則如果 當前選到的技能 等於 狀態X
				else if (_當前選到的技能 == ZXCType.X)
					// id = 存檔管理器.玩家資料.技能槽X儲存的技能ID
					id = SaveManager.instance.playerData.skillX;
				// 否則如果 當前選到的技能 等於 狀態C
				else if (_當前選到的技能 == ZXCType.C)
					// id = 存檔管理器.玩家資料.技能槽C儲存的技能ID
					id = SaveManager.instance.playerData.skillC;

				if (id == -1)
					PlayerCtrl.instance.atkObject = null;
				else
					// 玩家控制器.攻擊物件 = 技能管理器.找技能資料(技能ID).技能預製物
					PlayerCtrl.instance.atkObject = SkillManager.instance.FindSkillByID(id).skillPrefab;
			}

			if (選到的技能變化了 != null)
				選到的技能變化了.Invoke();
		}
	}
	[SerializeField] ZXCType _當前選到的技能 = ZXCType.NONE;
	public Action 選到的技能變化了 = null;

	/// <summary>
	/// 切換攻擊物件：相同按鈕按下2次切回物理攻擊
	/// </summary>
	public void SwitchAtkObject()
	{
		// 如果 按下Z鍵
		if (Input.GetKeyDown(KeyCode.Z))
		{
			// 如果 當前選到的技能 等於 狀態Z
			if (當前選到的技能 == ZXCType.Z)
				// 當前選到的技能 變為 狀態NONE(取消選取)
				當前選到的技能 = ZXCType.NONE;
			else
				// 當前選到的技能 變為 狀態Z
				當前選到的技能 = ZXCType.Z;
		}
		// 如果 按下X鍵
		if (Input.GetKeyDown(KeyCode.X))
		{
			if (當前選到的技能 == ZXCType.X)
				當前選到的技能 = ZXCType.NONE;
			else
				當前選到的技能 = ZXCType.X;
		}
		// 如果 按下C鍵
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (當前選到的技能 == ZXCType.C)
				當前選到的技能 = ZXCType.NONE;
			else
				當前選到的技能 = ZXCType.C;
		}
	}
}
