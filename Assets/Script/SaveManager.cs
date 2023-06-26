using Fungus;
using System.Collections.Generic;
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
			playerData = new PlayerData(0, 0, 100f, 100f, 10f, 7f, 500f, 100f, 100f, "遊戲場景", Vector3.zero);
			playerData.moneyCount = 0;
			playerData.playerHP = 100;
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
		playerData.playerPos = PlayerCtrl.instance.transform.position;
		// 轉換資料為Json格式
		string json = JsonUtility.ToJson(playerData, true);
		Debug.Log(json);
		// 儲存在硬碟中
		PlayerPrefs.SetString("GameData", json);
		// PlayerPrefs.Save();
	}

	/// <summary>
	/// 儲存使用者資料
	/// </summary>
	public void SaveUser()
	{
		if (SaveManager.instance.playerData.playerHP <= 0)
		{
			SaveData();
		}
	}
}

/// <summary>
/// 玩家資料：定義資料內容
/// </summary>
[System.Serializable]
public struct PlayerData
{
	// public int moneyCount;   // 金幣數量
	// public int skillPoint;   // 技能點數
	public string levelName;    // 關卡名稱
	public Vector3 playerPos;   // 玩家位置

	/// <summary>
	/// 金幣數量
	/// </summary>
	[SerializeField]
	public int moneyCount
	{
		get { return _moneyCount; }
		set
		{
			_moneyCount = value;

			// 呼叫刷新金幣
			if (renewCoin != null)
			{
				renewCoin.Invoke();
			}
		}
	}
	[SerializeField] int _moneyCount;
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
					temp.number++;              // 將複製的陣列進行修改
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
	/// 減少道具
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
					temp.number--;              // 將複製的陣列進行修改
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
	public PlayerData(int coin, int skill, float maxHP, float maxMP, float moveSpeed, float jumpPower, float attackSpeed, float attack, float defense, string nameLV, Vector3 pos)
	{
		_moneyCount = coin;
		_skillPoint = skill;
		renewCoin = null;
		renewSkillPoint = null;
		Act_goodsChange = null;
		itemNumberMax = 999;
		haveSkill = new List<int>();
		haveItem = new List<int>();
		goodsList = new List<Goods>();
		_playerHP = maxHP;
		renewPlayerHP = null;
		_playerMP = maxMP;
		renewPlayerMP = null;
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

		// this.moneyCount = coin;
		// this.skillPoint = skill;
	}

	public PlayerData(int v)
	{
		_moneyCount = 0;
		_skillPoint = 0;
		renewCoin = null;
		renewSkillPoint = null;
		Act_goodsChange = null;
		itemNumberMax = 999;
		haveSkill = new List<int>();
		haveItem = new List<int>();
		goodsList = new List<Goods>();
		_playerHP = 100f;
		renewPlayerHP = null;
		_playerMP = 100f;
		renewPlayerMP = null;
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
