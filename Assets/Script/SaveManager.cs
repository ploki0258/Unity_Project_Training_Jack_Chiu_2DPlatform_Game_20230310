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
        PlayerPrefs.SetString("GameData", json);
    }
}

/// <summary>
/// 玩家資料
/// </summary>
[System.Serializable]
public struct PlayerData
{
    /// <summary>
    /// 金幣數量
    /// </summary>
    public int coinQuantity
    {
        get { return _coinQuantity; }
        set
        {
            _coinQuantity = value;
        }
    }
    [SerializeField] public int _coinQuantity;
    public System.Action coinChage;

    public PlayerData(int coin)
    {
        _coinQuantity = coin;
        coinChage = null;
    }

    /*public PlayerData(int v)
    {
        _coinQuantity = 0;
        coinChage = null;
    }
    */
}
