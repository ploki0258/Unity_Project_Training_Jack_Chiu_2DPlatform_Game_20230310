﻿using Fungus;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SaveManager
{
	#region 單例模式
	// 在整個專案全域宣告一個instance
	public static SaveManager instance
	{
		// 有人需要我
		get
		{
			// 如果我不存在
			if (_instance == null)
			{
				// 就自己創造自己
				_instance = new SaveManager();
			}

			// 回傳自己
			return _instance;
		}
	}
	// 記憶體實際的位置
	static SaveManager _instance = null;
	#endregion

	// 建立玩家的資料
	public PlayerData playerData = new PlayerData();

	/// <summary>
	/// 遊戲讀檔
	/// </summary>
	public void LoadData()
	{
		string json = PlayerPrefs.GetString("GameData", "0");
		// 如果 json為0時
		if (json == "0")
		{
			// 這是一個新玩家 請給他基本數值
			// 此為建構式的寫法
			// playerData = new PlayerData(0, 0, 100f, 100f, 10f, 7f, 500f, 100f, 100f, "遊戲場景", Vector3.zero);

			playerData.playerHpMax =
				PlayerCtrl.instance.maxHP;				// 血量最大值
			playerData.playerMpMax =
				PlayerCtrl.instance.maxMP;              // 魔力最大值
			playerData.playerHP =
				playerData.playerHpMax;					// 玩家血量
			playerData.playerMP =
				playerData.playerMpMax;					// 玩家魔力
			playerData.coinCount = 0;                   // 玩家金幣
			playerData.skillPoint = 0;                  // 玩家技能點數
			playerData.costMP = 10f;                    // 玩家的魔力消耗值
			playerData.playerSpeed = 10f;               // 玩家移動速度
			playerData.playerJump = 10f;                // 玩家跳躍力
			playerData.playerAttackSpeed = 500f;        // 玩家攻擊速度
			playerData.playerAttack = 20f;              // 玩家攻擊力
			playerData.playerDefense = 2f;              // 玩家防禦力
			playerData.levelName = "遊戲場景";			// 關卡名稱
			playerData.playerPos = Vector3.zero;        // 玩家位置
			playerData.goodsList = new List<Goods>();   // 持有物列表
			playerData.haveItem = new List<int>();      // 道具列表
			playerData.haveSkill = new List<int>();     // 技能列表
			playerData.itemNumberMax = 999;             // 最大持有道具數量
			playerData.messageTip = "";                 // 玩家提示訊息
			playerData.isSetSkill = false;              // 玩家是否設置技能
			playerData.skillSlotID = 0;
			playerData.skillZ = -1;
			playerData.skillX = -1;
			playerData.skillC = -1;
			//playerData.skillObjectID = -1;            // 設置的技能物件ID
		}
		else
		{
			// 從既有資料由json檔轉回來使用
			playerData = JsonUtility.FromJson<PlayerData>(json);
			// Debug.Log("撈資料給予玩家");
		}
	}

	/// <summary>
	/// 遊戲存檔
	/// </summary>
	public void SaveData()
	{
		// 玩家資料.玩家位置 = PlayerCtrl.transform.position
		playerData.playerPos = PlayerCtrl.instance.transform.position;

		// 轉換資料為Json格式
		string json = JsonUtility.ToJson(playerData, true);
		Debug.Log(json);
		// 儲存在硬碟中
		PlayerPrefs.SetString("GameData", json);
		// PlayerPrefs.Save();

		if (renewSave != null)
		{
			renewSave.Invoke();
		}
	}
	public System.Action renewSave;
}

/// <summary>
/// 玩家資料：定義資料內容
/// </summary>
[System.Serializable]
public struct PlayerData
{
	#region 玩家基本參數
	public string levelName;        // 關卡名稱
	public Vector3 playerPos;       // 玩家位置
	public bool isSetSkill;         // 用以判斷是否有放置技能
	public int skillObjectID;       // 用以儲存技能物件
	public int skillSlotID;         // 技能物件存放的座標位置

	#region 技能槽ZXC
	/// <summary>
	/// 技能槽Z儲存的技能ID
	/// </summary>
	[SerializeField]
	public int skillZ
	{
		get { return _skillZ; }
		set
		{
			_skillZ = value;

			// 呼叫技能槽刷新
			if (renewSkillSlotZXC != null)
			{
				renewSkillSlotZXC.Invoke();
			}
		}
	}
	[SerializeField] int _skillZ;

	/// <summary>
	/// 技能槽X儲存的技能ID
	/// </summary>
	[SerializeField]
	public int skillX
	{
		get { return _skillX; }
		set
		{
			_skillX = value;

			if (renewSkillSlotZXC != null)
			{
				renewSkillSlotZXC.Invoke();
			}
		}
	}
	[SerializeField] int _skillX;

	/// <summary>
	/// 技能槽C儲存的技能ID
	/// </summary>
	[SerializeField]
	public int skillC
	{
		get { return _skillC; }
		set
		{
			_skillC = value;

			// 呼叫刷新技能點數
			if (renewSkillSlotZXC != null)
			{
				renewSkillSlotZXC.Invoke();
			}
		}
	}
	[SerializeField] int _skillC;
	
	/// <summary>
	/// 技能槽刷新事件
	/// </summary>
	public System.Action renewSkillSlotZXC;
	#endregion

	/// <summary>
	/// 金幣數量
	/// </summary>
	[SerializeField]
	public int coinCount
	{
		get { return _coinCount; }
		set
		{
			_coinCount = value;

			// 呼叫刷新金幣
			if (renewCoin != null)
			{
				renewCoin.Invoke();
			}
		}
	}
	[SerializeField] int _coinCount;
	public System.Action renewCoin;

	/// <summary>
	/// 技能點數
	/// </summary>
	[SerializeField]
	public int skillPoint
	{
		get { return _skillPoint; }
		set
		{
			_skillPoint = value;

			// 呼叫刷新技能點數
			if (renewSkillPoint != null)
			{
				renewSkillPoint.Invoke();
			}
		}
	}
	[SerializeField] int _skillPoint;
	public System.Action renewSkillPoint;

	/// <summary>
	/// 提示訊息
	/// </summary>
	[SerializeField]
	public string messageTip
	{
		get { return _messageTip; }
		set
		{
			_messageTip = value;

			// 呼叫刷新技能點數
			if (renewMmessageTip != null)
			{
				renewMmessageTip.Invoke();
			}
		}
	}
	[SerializeField] string _messageTip;
	public System.Action renewMmessageTip;

	/// <summary>
	/// 玩家血量
	/// </summary>
	[SerializeField]
	public float playerHP
	{
		get { return _playerHP; }
		set
		{
			_playerHP = value;

			// 呼叫刷新技能點數
			if (renewPlayerHP != null)
			{
				renewPlayerHP.Invoke();
			}
		}
	}
	[SerializeField] float _playerHP;
	public System.Action renewPlayerHP;

	/// <summary>
	/// 玩家魔力
	/// </summary>
	[SerializeField]
	public float playerMP
	{
		get { return _playerMP; }
		set
		{
			_playerMP = value;

			// 呼叫刷新技能點數
			if (renewPlayerMP != null)
			{
				renewPlayerMP.Invoke();
			}
		}
	}
	[SerializeField] float _playerMP;
	public System.Action renewPlayerMP;

	/// <summary>
	/// 血量最大值
	/// </summary>
	[SerializeField]
	public float playerHpMax
	{
		get { return _playerHpMax; }
		set
		{
			_playerHpMax = value;

			// 呼叫刷新技能點數
			if (renewPlayerHpMax != null)
			{
				renewPlayerHpMax.Invoke();
			}
		}
	}
	[SerializeField] float _playerHpMax;
	public System.Action renewPlayerHpMax;

	/// <summary>
	/// 魔力最大值
	/// </summary>
	[SerializeField]
	public float playerMpMax
	{
		get { return _playerMpMax; }
		set
		{
			_playerMpMax = value;

			// 呼叫刷新技能點數
			if (renewPlayerMpMax != null)
			{
				renewPlayerMpMax.Invoke();
			}
		}
	}
	[SerializeField] float _playerMpMax;
	public System.Action renewPlayerMpMax;

	/// <summary>
	/// 玩家的魔力消耗值
	/// </summary>
	[SerializeField]
	public float costMP
	{
		get { return _costMP; }
		set
		{
			_costMP = value;

			if (renewCostMP != null)
			{
				renewCostMP.Invoke();
			}
		}
	}
	[SerializeField] float _costMP;
	public Action renewCostMP;

	/// <summary>
	/// 玩家移動速度
	/// </summary>
	public float playerSpeed
	{
		get { return _playerSpeed; }
		set
		{
			_playerSpeed = value;

			// 呼叫刷新玩家移動
			if (renewPlayerSpeed != null)
			{
				renewPlayerSpeed.Invoke();
			}
		}
	}
	[SerializeField] float _playerSpeed;
	public System.Action renewPlayerSpeed;

	/// <summary>
	/// 玩家跳躍力
	/// </summary>
	public float playerJump
	{
		get { return _playerJump; }
		set
		{
			_playerJump = value;

			// 呼叫刷新玩家跳躍力
			if (renewPlayerJump != null)
			{
				renewPlayerJump.Invoke();
			}
		}
	}
	[SerializeField] float _playerJump;
	public System.Action renewPlayerJump;

	/// <summary>
	/// 玩家攻擊速度
	/// </summary>
	public float playerAttackSpeed
	{
		get { return _playerAttackSpeed; }
		set
		{
			_playerAttackSpeed = value;

			// 呼叫刷新玩家攻擊速度
			if (renewPlayerAttackSpeed != null)
			{
				renewPlayerAttackSpeed.Invoke();
			}
		}
	}
	[SerializeField] float _playerAttackSpeed;
	public System.Action renewPlayerAttackSpeed;

	/// <summary>
	/// 玩家攻擊力
	/// </summary>
	public float playerAttack
	{
		get { return _playerAttack; }
		set
		{
			_playerAttack = value;

			// 呼叫刷新玩家攻擊力
			if (renewPlayerAttack != null)
			{
				renewPlayerAttack.Invoke();
			}
		}
	}
	[SerializeField] float _playerAttack;
	public System.Action renewPlayerAttack;

	/// <summary>
	/// 玩家防禦力
	/// </summary>
	public float playerDefense
	{
		get { return _playerDefense; }
		set
		{
			_playerDefense = value;

			// 呼叫刷新玩家防禦力
			if (renewPlayerDefense != null)
			{
				renewPlayerDefense.Invoke();
			}
		}
	}
	[SerializeField] float _playerDefense;
	public System.Action renewPlayerDefense;
	#endregion

	/// <summary>
	/// 已擁有的技能列表
	/// </summary>
	public List<int> haveSkill; // 已擁有的技能

	/// <summary>
	/// 已擁有的道具列表
	/// </summary>
	public List<int> haveItem;  // 已擁有的道具

	/// <summary>
	/// 檢查該技能ID是否已擁有
	/// </summary>
	/// <param name="id">技能ID</param>
	/// <returns></returns>
	public bool IsHaveSkill(int id)
	{
		for (int i = 0; i < haveSkill.Count; i++)
		{
			if (haveSkill[i] == id)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 檢查該道具ID是否已獲得
	/// </summary>
	/// <param name="id">道具ID</param>
	/// <returns></returns>
	public bool IsHaveItem(int id)
	{
		for (int i = 0; i < haveItem.Count; i++)
		{
			if (haveItem[i] == id)
			{
				return true;
			}
		}
		return false;
	}

	// 持有物品列表
	public List<Goods> goodsList;
	// 最大持有道具數量
	public int itemNumberMax;

	#region 物品功能
	// 道具發生變化：新增、減少
	public System.Action Act_goodsChange;

	/// <summary>
	/// 添加道具(By ID)
	/// </summary>
	/// <param name="id">道具編號</param>
	public bool AddItem(int id)
	{
		// 如果已經有道具 就累計
		if (CheckItem(id) > 0)
		{
			// 掃描所有道具
			for (int i = 0; i < goodsList.Count; i++)
			{
				// 如果遇到相同ID的道具 且目前持有數量 <= 最大持有數量 就只單純增加數量
				if (goodsList[i].id == id && goodsList[i].number <= itemNumberMax)
				{
					Goods temp = goodsList[i];  // 複製一個陣列
					temp.number++;              // 將複製的陣列進行修改(數量加一)
					goodsList[i] = temp;        // 將修改後的陣列覆蓋回去
					break;                      // 已成功完成堆疊 結束迴圈
				}
			}
		}
		else
		{
			// 如果道具欄已滿 且無法堆疊
			if (goodsList.Count == 20)
			{
				// 回傳否定表示該道具並未添加 且添加失敗
				return false;
			}

			// 沒有東西可以堆疊
			// 增加一個道具欄位
			Goods newItem = new Goods();
			newItem.id = id;
			newItem.number = 1;
			// 為陣列新增一個元素
			goodsList.Add(newItem);
		}

		// 無論是堆疊道具還是添加新道具欄 都要通知更新
		if (Act_goodsChange != null)
		{
			Act_goodsChange.Invoke();
		}
		// 如果程式執行到最後並未停止 表示該道具成功添加
		return true;
	}

	/// <summary>
	/// 減少道具(By ID)
	/// </summary>
	/// <param name="id">道具編號</param>
	public void RemoveItem(int id)
	{
		int thisItem = CheckItem(id);

		// 如果道具數量有兩個或以上時 只需減少即可
		if (thisItem >= 2)
		{
			for (int i = 0; i < goodsList.Count; i++)
			{
				if (goodsList[i].id == id)
				{
					Goods temp = goodsList[i];  // 複製一個陣列
					temp.number--;              // 將複製的陣列進行修改(數量減一)
					goodsList[i] = temp;        // 將修改後的陣列覆蓋回去
					break;                      // 已成功完成堆疊 結束迴圈
				}
			}
		}
		// 道具數量只剩一個時 就要刪除整個項目
		else if (thisItem == 1)
		{
			for (int i = 0; i < goodsList.Count; i++)
			{
				if (goodsList[i].id == id)
				{
					// 移除第I個項目
					goodsList.RemoveAt(i);
					break;
				}
			}
		}
		else
		{
			Debug.LogError("嘗試移除不存在的道具");
		}

		// 通知道具欄發生變化
		if (Act_goodsChange != null)
		{
			Act_goodsChange.Invoke();
		}
	}

	/// <summary>
	/// 查詢道具數量
	/// </summary>
	/// <param name="id">道具編號</param>
	/// <returns></returns>
	public int CheckItem(int id)
	{
		for (int i = 0; i < goodsList.Count; i++)
		{
			// 如果遇到相同ID的道具
			if (goodsList[i].id == id)
			{
				// 就回傳該道具的數量
				return goodsList[i].number;
			}
		}
		return 0;
	}
	#endregion

	// 建構式
	public PlayerData(int coin, int skill, float hp, float mp, float maxHp, float maxMp, float cost, float moveSpeed, float jumpPower, float attackSpeed, float attack, float defense,
		string nameLV, Vector3 pos, string tip, bool isHave, int skillObjectID, int skillZ, int skillX, int skillC)
	{
		_coinCount = coin;
		_skillPoint = skill;
		renewCoin = null;
		renewSkillPoint = null;
		Act_goodsChange = null;
		renewSkillSlotZXC = null;
		itemNumberMax = 999;
		haveSkill = new List<int>();
		haveItem = new List<int>();
		goodsList = new List<Goods>();
		_playerHP = hp;
		renewPlayerHP = null;
		_playerMP = mp;
		renewPlayerMP = null;
		_playerHpMax = maxHp;
		renewPlayerHpMax = null;
		_playerMpMax = maxMp;
		renewPlayerMpMax = null;
		_costMP = cost;
		renewCostMP = null;
		_playerSpeed = moveSpeed;
		renewPlayerSpeed = null;
		_playerJump = jumpPower;
		renewPlayerJump = null;
		_playerAttackSpeed = attackSpeed;
		renewPlayerAttackSpeed = null;
		_playerAttack = attack;
		renewPlayerAttack = null;
		_playerDefense = defense;
		renewPlayerDefense = null;
		levelName = nameLV;
		playerPos = pos;
		_messageTip = tip;
		renewMmessageTip = null;
		isSetSkill = isHave;
		this.skillObjectID = skillObjectID;
		skillSlotID = 0;
		_skillZ = skillZ;
		_skillX = skillX;
		_skillC = skillC;
		// this.moneyCount = coin;
		// this.skillPoint = skill;
	}

	public PlayerData(int v)
	{
		_coinCount = 0;
		_skillPoint = 0;
		renewCoin = null;
		renewSkillPoint = null;
		Act_goodsChange = null;
		renewSkillSlotZXC = null;
		itemNumberMax = 999;
		haveSkill = new List<int>();
		haveItem = new List<int>();
		goodsList = new List<Goods>();
		_playerHP = 100f;
		renewPlayerHP = null;
		_costMP = 0;
		renewCostMP = null;
		_playerMP = 100f;
		renewPlayerMP = null;
		_playerHpMax = 100f;
		renewPlayerHpMax = null;
		_playerMpMax = 100f;
		renewPlayerMpMax = null;
		_playerSpeed = 10f;
		renewPlayerSpeed = null;
		_playerJump = 7f;
		renewPlayerJump = null;
		_playerAttackSpeed = 500f;
		renewPlayerAttackSpeed = null;
		_playerAttack = 100f;
		renewPlayerAttack = null;
		_playerDefense = 100f;
		renewPlayerDefense = null;
		levelName = "遊戲場景";
		playerPos = new Vector3(0, 0, 0);
		_messageTip = "";
		renewMmessageTip = null;
		isSetSkill = false;
		skillObjectID = -1;
		skillSlotID = 0;
		_skillZ = -1;
		_skillX = -1;
		_skillC = -1;

		// this.moneyCount = 0;
		// this.skillPoint = 0;
	}
}

/// <summary>
/// 持有物
/// </summary>
[System.Serializable]
public struct Goods
{
	[SerializeField] public int id;     // 有什麼樣的道具(ID)
	[SerializeField] public int number; // 有幾個這個道具(數量)
}
