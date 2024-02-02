using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 單例設計模式 不可重複存在 任何地方皆可呼叫
public class GameManager
{
    static public GameManager instance
    {
        // 當有人使用我的時候
        get
        {
            // 如果我不存在
            if (_instance == null)
            {
                // 就自我憑空建立
                _instance = new GameManager();
            }
            // 回傳我可給對方使用
            return _instance;
        }
    }
    static GameManager _instance = null;

    public float playerHp = 100f;

    public int playerMoney = 10;

    public System.Action 遊戲結束事件 = null;
    public void 遊戲結束()
    {
        // 有人在聆聽事件的話就呼叫事件
        if (遊戲結束事件 != null)
            遊戲結束事件.Invoke();
    }
}
