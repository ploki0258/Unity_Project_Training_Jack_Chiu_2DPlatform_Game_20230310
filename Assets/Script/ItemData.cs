using UnityEngine;

/// <summary>
/// 道具系統：建立道具的資料格式
/// </summary>
[CreateAssetMenu(fileName = "新道具", menuName = "建立新道具")]
public class ItemData : ScriptableObject
{
    // ScriptableObject = 把資料物件化
    // public string userInput;
    // const string PlayerPrefsKey = "UserInputKey";
    // 共同
    [Header("道具ID")]
    public int id;                      // 道具ID
    [Header("道具名稱")]
    public string title;                // 道具標題
    [Header("道具圖示")]
    public Sprite icon;                 // 道具圖示
    [Header("道具說明"), TextArea(5, 5)]
    public string info;                 // 道具敘述
    [Header("道具可否使用")]
    public bool canUse;                 // 是否可使用
    [Header("道具是否會消耗")]
    public bool Consumables;            // 使用後是否會消耗
    [Header("道具顏色")]
    public Color category;              // 類別的顏色

    // 個別
    // public float regainHp;   // 回血
    // public float regainMp;   // 補魔
    [Header("恢復體力值")]
    public float regainStr;     // 回體力

    // 在 Awake 時設定預設值
    // 如果使用者有輸入值 則讀取使用者的值
    // 如果使用者沒有輸入值 則值為預設值
    private void Awake()
    {
        SaveUserInput();
        if (PlayerPrefs.HasKey("ItemID"))
        {
            // userInput = PlayerPrefs.GetString(PlayerPrefsKey);
            id = PlayerPrefs.GetInt("ItemID", id);
            title = PlayerPrefs.GetString("ItemTitle", title);
            info = PlayerPrefs.GetString("ItemInfo", info);
            int canUseBool = PlayerPrefs.GetInt("ItemCanUse", 0);
            canUse = canUseBool != 0;
            int ConsumablesBool = PlayerPrefs.GetInt("ItemConsumables", 0);
            Consumables = ConsumablesBool != 0;
            regainStr = PlayerPrefs.GetFloat("ItemRegainStr", regainStr);
        }
        else
        {
            title = "未命名";
            info = "無敘述";
            canUse = false;
            Consumables = false;
            regainStr = 0f;
        }
    }

    /// <summary>
    /// 儲存玩家所輸入的值
    /// </summary>
    /// <param name="iconBool">所輸入的圖示</param>
    /// <param name="canUseBool">所輸入可否被使用</param>
    /// <param name="ConsumablesBool">所輸入可否會消耗</param>
    public void SaveUserInput()
    {
        // userInput = value;
        // PlayerPrefs.SetString(PlayerPrefsKey, userInput);
        // iconBool = icon == null ? 0 : 1;                       // 如果 canUse 為 false 轉換為 0 否則就為1
        // PlayerPrefs.SetInt("ItemIcon", iconBool);              // 儲存所輸入的道具圖示
        PlayerPrefs.SetInt("ItemID", id);                         // 儲存所輸入的道具ID
        PlayerPrefs.SetString("ItemTitle", title);                // 儲存所輸入的道具標題
        PlayerPrefs.SetString("ItemInfo", info);                  // 儲存所輸入的道具敘述
        int canUseBool = canUse == false ? 0 : 1;                 // 如果 canUse 為 false 轉換為 0 否則就為1
        PlayerPrefs.SetInt("ItemCanUse", canUseBool);             // 儲存所輸入的道具可否使用
        int ConsumablesBool = Consumables == false ? 0 : 1;       // 如果 Consumables 為 false 轉換為 0 否則就為1
        PlayerPrefs.SetInt("ItemConsumables", ConsumablesBool);   // 儲存所輸入的道具是否會消耗
        PlayerPrefs.SetFloat("ItemRegainStr", regainStr);         // 儲存所輸入的體力恢復值

        PlayerPrefs.Save();
    }
}