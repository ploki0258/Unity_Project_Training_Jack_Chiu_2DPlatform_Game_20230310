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
			playerData = new PlayerData(0);
		}
		else
		{
			// 從既有資料由json檔轉回來使用
			playerData = JsonUtility.FromJson<PlayerData>(json);
		}
	}

	/// <summary>
	/// 遊戲存檔
	/// </summary>
	public void SaveData()
	{
		// 轉換資料為Json格式
		string json = JsonUtility.ToJson(playerData, true);
		Debug.Log(json);
		// 儲存在硬碟中
		PlayerPrefs.SetString("GameData", json);
		// PlayerPrefs.Save();
	}

	// 最大持有道具數量
	int itemNumberMax = 999;

	// 持有物品列表
	public List<Goods> goodsList = new List<Goods>();

	// 道具發生變化：新增、減少
	public System.Action Act_goodsChange;

	/// <summary>
	/// 添加道具(By ID)
	/// </summary>
	/// <param name="id">道具編號</param>
	public bool addItem(int id)
	{
		// 如果已經有道具 就累計
		if (checkItem(id) > 0)
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
	public void removeItem(int id)
	{
		int thisItem = checkItem(id);

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
	public int checkItem(int id)
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

	/// <summary>
	/// 持有物
	/// </summary>
	[System.Serializable]
	public struct Goods
	{
		[SerializeField] public int id;     // 有什麼樣的道具(ID)
		[SerializeField] public int number; // 有幾個這個道具(數量)
	}

	/// <summary>
	/// 儲存使用者資料
	/// </summary>
	public void SaveUser()
	{
		if (PlayerCtrl.instance.hp <= 0)
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

			// 
			if (更新金幣 != null)
			{
				更新金幣.Invoke();
			}
		}
	}
	int _moneyCount;
	public System.Action 更新金幣;

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

			// 
			if (更新技能點 != null)
			{
				更新技能點.Invoke();
			}
		}
	}
	int _skillPoint;
	public System.Action 更新技能點;

	public List<int> haveSkill; // 已擁有的技能
	public List<int> haveItem;  // 已擁有的道具

	/// <summary>
	/// 檢查該技能ID是否已購買
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
	/// 檢查該道具ID是否已購買
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

	// 建構式
	public PlayerData(int coin, int skill)
	{
		// this.moneyCount = coin;
		// this.skillPoint = skill;

		_moneyCount = coin;
		_skillPoint = skill;
		更新金幣 = null;
		更新技能點 = null;
		haveSkill = new List<int>();
		haveItem = new List<int>();
	}

	public PlayerData(int v)
	{
		// this.moneyCount = 0;
		// this.skillPoint = 0;

		_moneyCount = 0;
		_skillPoint = 0;
		更新金幣 = null;
		更新技能點 = null;
		haveSkill = new List<int>();
		haveItem = new List<int>();
	}
}
