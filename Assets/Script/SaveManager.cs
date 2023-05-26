using UnityEngine;

public class SaveManager
{
    #region 單例模式
    // 在整個專案全域宣告一個instance
    public static SaveManager instance
    {
        // 當被取得時
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

    // public PlayerData playerData = new PlayerData();
    
    /// <summary>
    /// 玩家資料
    /// </summary>
    /*[System.Serializable]
    public struct PlayerData
    {
        public int coinQuantity
        {
            get { return _coinQuantity; }
            set
            {
                _coinQuantity = value;
            }
        }
    }
    [SerializeField] public int _coinQuantity;
    public System.Action coinChage;

    public PlayerData(int coin)
    {
        _coinQuantity = coin;
    }

    public PlayerData(int value)
    {
        _coinQuantity = 0;
    }*/
}
