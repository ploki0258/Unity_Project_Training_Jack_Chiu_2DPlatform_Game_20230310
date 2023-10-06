using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
	[Header("技能欄按鈕陣列"), Tooltip("用於儲存技能施放的按鈕")]
	public Transform[] skillSlot; // 技能欄位，用於接受技能的拖放
								  //[Header("當前技能ID"), Tooltip("當前選擇的技能ID")]

	[Header("文字顯示顏色")]
	public Color textColor = new Color();

	public int keyCodeNumber;                   // 按鈕數字
	Skill skillData;                            // 技能資料
	[SerializeField] KeyCode currentKeyCode;    // 當前紀錄的鍵盤按鈕
												//SpriteRenderer iconSkill = null;

	private void Awake()
	{
		skillData = FindObjectOfType<Skill>();
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

	/// <summary>
	/// 設置當前技能
	/// </summary>
	/// <param name="skill">技能ID</param>

	/// <summary>
	/// 紀錄所按下的鍵盤按鈕
	/// </summary>
	private void KeepKeyCode()
	{
		foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
		{
			// 如果按下 keyCode 等於 Z / X / C按鍵
			if (Input.GetKeyDown(keyCode) == Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(keyCode) == Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(keyCode) == Input.GetKeyDown(KeyCode.C))
			{
				// 如果 keyCode 等於 當前的keyCode
				if (keyCode == currentKeyCode)
				{
					//Debug.Log("按下相同按鈕");

					if (PlayerCtrl.instance.atkObject != null)
					{

					}
					else if (PlayerCtrl.instance.atkObject == null)
					{

					}
				}
				else if (keyCode != currentKeyCode)
				{
					//Debug.Log("按下不同按鈕");
				}

				// 當前的鍵盤按鈕 = 按下的按鈕
				currentKeyCode = keyCode;
				//Debug.Log("按下：" + currentKeyCode);
			}
		}
	}

	private void Start()
	{
		當前選到的技能 = ZXCType.NONE;
		SaveManager.instance.playerData.renewSkillZXC += 技能發生變化;
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewSkillZXC -= 技能發生變化;
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
			if (_當前選到的技能 == ZXCType.NONE)
				PlayerCtrl.instance.atkObject = null;
			else
			{
				int id = -1;
				if (_當前選到的技能 == ZXCType.Z)
					id = SaveManager.instance.playerData.skillZ;
				else if (_當前選到的技能 == ZXCType.X)
					id = SaveManager.instance.playerData.skillX;
				else if (_當前選到的技能 == ZXCType.C)
					id = SaveManager.instance.playerData.skillC;

				if (id == -1)
					PlayerCtrl.instance.atkObject = null;
				else
					PlayerCtrl.instance.atkObject = SkillManager.instance.FindSkillByID(id).skillPrefab;
			}
			if (選到的技能變化了 != null)
				選到的技能變化了.Invoke();
		}
	}
	ZXCType _當前選到的技能 = ZXCType.NONE;

	public Action 選到的技能變化了 = null;
	
	/// <summary>
	/// 切換攻擊物件：相同按鈕按下2次切回物理攻擊
	/// </summary>
	public void SwitchAtkObject()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (當前選到的技能 == ZXCType.Z)
				當前選到的技能 = ZXCType.NONE;
			else
				當前選到的技能 = ZXCType.Z;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			if (當前選到的技能 == ZXCType.X)
				當前選到的技能 = ZXCType.NONE;
			else
				當前選到的技能 = ZXCType.X;
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			if (當前選到的技能 == ZXCType.C)
				當前選到的技能 = ZXCType.NONE;
			else
				當前選到的技能 = ZXCType.C;
		}
	}
}
