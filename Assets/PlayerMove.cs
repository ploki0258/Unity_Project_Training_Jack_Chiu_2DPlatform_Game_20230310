using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 函式 計算機
    // 解決問題的東西 大問題 投資、存股 每支股票的預期收入....
    // 大問題 > 多個小問題
    // 大問題 > 多個小問題
    // 大問題 > 多個小問題
    // 大問題 > 多個小問題
    // 大問題 > 多個小問題
    // 合適的大小
    // 問題太細 => 組合次數太多 工作量不會減少
    // 問題太大 => 可重複使用的東西太少
    // A怪物 (防禦策略、血量、追玩家、視線邏輯....)
    // B怪物 (進攻策略、血量、追玩家、視線邏輯....)
    // A怪物程式 (策略邏輯)
    // B怪物程式 (策略邏輯)
    // 血量管理程式
    // 生物尋路系統
    // 視線模擬

    // 函式是解決問題最小的單位(小電腦)
    // private 內部程式(可省略) public 暴露的東西
    // float = 數字單位

    //內外部 回傳結果 名稱 輸入的參數(,點隔開多個值)
    private float 計算A加B(float a, float b)
    {
        // 計算完成回傳結果
        return a + b;
    }
    // void = 虛空 不需要回傳值時使用
    private void 計算總成績()
    {
        float a = 10;
        float b = 20;
        float c = 30;
        float 總成績 = 0;
        總成績 = 計算A加B(a, b);
        總成績 = 計算A加B(總成績, c);
        // 用UI顯示總成績
    }
    // 死亡計算成績 勝利也要計算成績 平手計算成績 9 * 3

    // 型別 = 單位
    // 人 狗 貓 老鼠
    // ★float 數字(小數點, 正負號)
    // ulong 數字(整數, 正號) (錢)
    // long 數字(整數, 正負號) ()
    // ★int 數字(整數, 正負號) (21億多)
    // short 數字(整數, 正負號) (-32767~32767)
    // ushort 數字(整數, 正號) (0~65535)
    // byte 數字(整數, 正號) (0~255)
    // 用石頭、黃金磚、棉花糖、空氣蓋金字塔

    // 10/20 = 100/200
    // 20/100 = 1/5
    // 數學上來說 = 左右必定是相等

    void 遊戲結束要做的事情()
    {

    }

    // 物件被刪除時
    private void OnDestroy()
    {
        // 退訂遊戲結束事件
        GameManager.instance.遊戲結束事件 -= 遊戲結束要做的事情;
    }

    // 程式初始化的時候將會執行一次
    private void Start()
    {
        // 訂閱遊戲結束事件
        GameManager.instance.遊戲結束事件 += 遊戲結束要做的事情;

        /*GameObject 小明 = null;
        GameObject 小華 = null;
        GameObject 小美 = null;
        // 把小明順移到百米高空
        小明.transform.position = new Vector3(0f, 100f, 0f);
        小華.transform.position = new Vector3(0f, 100f, 0f);
        小美.transform.position = new Vector3(0f, 100f, 0f);

        // Array 陣列要指定長度
        GameObject[] 範圍技打到的東西 = new GameObject[3];
        範圍技打到的東西[0] = 小明;
        範圍技打到的東西[1] = 小華;
        範圍技打到的東西[2] = 小美;

        // 迴圈依照條件決定執行的次數
        for(int i = 0; i < 範圍技打到的東西.Length; i++)
        {
            範圍技打到的東西[i].transform.position = new Vector3(0f, 100f, 0f);
        }*/

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                Debug.Log(i + " X " + j + " = " + (i*j));
            }
        }

        GameManager.instance.playerMoney = 1000;
    }
    // [SerializeField] 序列化
    [SerializeField] [Range(-1f, 1f)] float hp = 0f;// value Type 本地記憶體
    [SerializeField] Rigidbody 物理引擎 = null;// reference type 別的程式

    [SerializeField] 攻擊 我的普通攻擊 = new 攻擊();

    public void 受傷(攻擊 對方的攻擊)
    {
        float 我目前的血 = 100f;
        我目前的血 = 我目前的血 - 對方的攻擊.攻擊力;
    }
}

[System.Serializable]
public struct 攻擊
{
    public float 攻擊力;
    public float 穿透力;
    public float 魔法攻擊力;
    public int 屬性;
    public Vector3 攻擊者的座標;
}

