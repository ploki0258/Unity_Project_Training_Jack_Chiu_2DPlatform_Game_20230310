using Fungus;
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

    public void SaveUser()
    {
        if(PlayerCtrl.instance.hp <= 0)
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
    [SerializeField] public int moneyCount
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
    [SerializeField] public int skillPoint
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
    
    // 建構式
    public PlayerData(int coin, int skill)
    {
        // this.moneyCount = coin;
        // this.skillPoint = skill;

        _moneyCount = coin;
        _skillPoint = skill;
        更新金幣 = null;
        更新技能點 = null;
    }

    public PlayerData(int v)
    {
        // this.moneyCount = 0;
        // this.skillPoint = 0;

        _moneyCount = 0;
        _skillPoint = 0;
        更新金幣 = null;
        更新技能點 = null;
    }
}
