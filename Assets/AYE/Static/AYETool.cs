using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>Aye系列為阿葉提供的綜合工具，最新版本請找阿葉拿。</summary>
public static class Aye
{
    #region MultipleRandom 多重抽籤器
    /// <summary>多重抽籤器，請按照float,float,data,data的方式輸入(權重與對應的物件)</summary>
    /// <param name="count">項目數量</param>
    /// <param name="list">float,float,data,data</param>
    public static object MultipleRandom(int count, params object[] list)
    {
        if (list.Length != count*2)
        {
            Debug.LogError("長度不對! count 如果是 " + count + " 則後面要剛好有 " + count * 2 + " 個物件，前 " + count + " 個是權重(float)後 " + count + " 個是項目。");
            return null;
        }

        List<float> allP = new List<float>();
        float total = 0f;
        for (int i = 0; i < count; i++)
        {
            float t = (float)list[i];
            allP.Add(t);
            total += t;
        }
        total = Random.Range(0f, total);
        for (int i = count; i < count*2; i++)
        {
            if (total > allP[i-count])
            {
                total -= allP[i-count];
            }
            else
            {
                return list[i];
            }
        }
        return null;
        // 0 1 2 3 4 5
    }
    /// <summary>多重抽籤器，請按照float,data,float,data的方式輸入(權重與對應的物件)</summary>
    public static object MultipleRandom(params object[] list)
    {
        if(list.Length %2!= 0)
        {
            Debug.LogError("長度不對! 應該是權重(float)+物件+權重(float)+物件，一對一對的，應該會是偶數。");
            return null;
        }

        List<float> allP = new List<float>();
        float total = 0f;
        for(int i = 0; i < list.Length; i++)
        {
            if (i%2==0)
            {
                float t = (float)list[i];
                allP.Add(t);
                total += t; 
            }
        }
        total = Random.Range(0f, total);
        for(int i = 0; i < allP.Count; i++)
        {
            if (total > allP[i])
            {
                total -= allP[i];
            }
            else
            {
                return list[(i * 2) + 1];
            }
        }
        return null;
        // 10 A 20 B 30 C 40 D
        // 0  1 2  3 4  5 6  7
        // 0    1    2    3
        // X = (X*2)+1
    }
    #endregion

    #region IsRandom 百分比機率
    /// <summary>百分比機率</summary>
    public static bool IsRandom(int v)
    {
        int r = Random.Range(0, 100);
        return r < v;
    }

    /// <summary>百分比機率</summary>
    public static bool IsRandom(float v)
    {
        float r = Random.Range(0f, 100f);
        return r < v;
    }
    #endregion

    #region 複製到剪貼簿
    /// <summary>複製到剪貼簿</summary>
    public static void CopyToClipboard(this string str)
    {
        var textEditor = new TextEditor();
        textEditor.text = str;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    #endregion

    #region 從剪貼簿取得
    /// <summary>複製到剪貼簿</summary>
    public static string GetToClipboard()
    {
        var textEditor = new TextEditor();
        textEditor.Paste();
        return textEditor.text;
    }
    #endregion

    #region 轉換成金錢顯示方式
    /// <summary>轉換成金錢顯示方式</summary>
    public static string ToMoney(this int fr)
    {
        if (fr >= 0)
            return System.String.Format("{0:$#,##0;$#,##0;$0}", fr);
        else
            return "-" + System.String.Format("{0:$#,##0;$#,##0;$0}", fr);
    }
    /// <summary>轉換成金錢顯示方式</summary>
    public static string ToMoney(this float fr)
    {
        if (fr >= 0f)
            return System.String.Format("{0:$#,##0;$#,##0;$0}", fr);
        else
            return "-" + System.String.Format("{0:$#,##0;$#,##0;$0}", fr);
    }
    #endregion

    #region 取得迴避數字後的隨機值
    /// <summary>取得迴避數字後的隨機值</summary>
    /// <param name="min">最小</param>
    /// <param name="max">最大</param>
    /// <param name="original">迴避</param>
    /// <returns></returns>
    public static int NRandom(int min, int max, params int[] originalList)
    {
        if (min == max)
            return min;
        if (Mathf.Abs(max - min) <= 1)
        {
            return min;
        }
        int an = Random.Range(min, max);

        for (int i = 0; i < originalList.Length; i++)
        {
            if (an == originalList[i])
            {
                return NRandom(min, max, originalList);
            }
        }
        return an;
    }
    #endregion

    #region K轉換為KB
    /// <summary>K轉換為KB</summary>
    public static string ToKB (this ulong ul)
    {
        return Mathf.Round(ul / (ulong)1024) + "KB";
    }
    #endregion

    #region Json簡化轉換
    /// <summary>轉換為序列化的Json檔資料</summary>
    public static string ToJson(this List<string> list)
    {
        ListString listString = new ListString();
        listString.list = list;
        return JsonUtility.ToJson(listString);
    }

    /// <summary>轉換為序列化的Json檔資料</summary>
    public static string ToJson(this List<int> list)
    {
        ListInt listString = new ListInt();
        listString.list = list;
        return JsonUtility.ToJson(listString);
    }

    /// <summary>轉換為序列化的Json檔資料</summary>
    public static string ToJson(this List<float> list)
    {
        ListFloat listString = new ListFloat();
        listString.list = list;
        return JsonUtility.ToJson(listString);
    }

    /// <summary>將Json檔轉換為string List</summary>
    public static List<string> ToStringList(this string listData)
    {
        ListString ls = JsonUtility.FromJson<ListString>(listData);
        return ls.list;
    }

    /// <summary>將Json檔轉換為int List</summary>
    public static List<int> ToIntList(this string listData)
    {
        ListInt ls = JsonUtility.FromJson<ListInt>(listData);
        return ls.list;
    }

    /// <summary>將Json檔轉換為float List</summary>
    public static List<float> ToFloatList(this string listData)
    {
        ListFloat ls = JsonUtility.FromJson<ListFloat>(listData);
        return ls.list;
    }
    #endregion

    #region 數值彈簧
    /// <summary>
    /// 數值彈簧
    /// </summary>
    /// <param name="x">值(輸入/輸出)</param>
    /// <param name="v">值(輸入/輸出)</param>
    /// <param name="xt">目標(輸入)</param>
    /// <param name="zeta">阻尼(輸入)</param>
    /// <param name="omega">頻率(輸入)</param>
    /// <param name="h">時間(輸入)</param>
    public static void Spring(ref float x, ref float v, float xt, float zeta, float omega, float h)
    {
        float f = 1.0f + 2.0f * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1.0f / (f + hhoo);
        float detX = f * x + h * v + hhoo * xt;
        float detV = v + hoo * (xt - x);
        x = detX * detInv;
        v = detV * detInv;
    }
    /* 源自 : https://github.com/TheAllenChou/numeric-springing */
    #endregion

    #region 文字轉時間
    /// <summary>
    /// 文字轉時間 yyyy-MM-ddTHH:mm:ss.fff 格式
    /// </summary>
    public static System.DateTime GetTimeByString(string v)
    {
        return System.DateTime.ParseExact(v, "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
    }
    /// <summary>
    /// 時間轉文字 yyyy-MM-ddTHH:mm:ss.fff 格式
    /// </summary>
    public static string GetStringByDateTime(System.DateTime v)
    {
        return v.ToString("yyyy-MM-ddTHH:mm:ss.fff");
    }
    #endregion

    #region 正愈大、負愈小
    /// <summary>正愈大、負愈小</summary>
    public static int AbsAdd(int v, int add)
    {
        bool ab = v >= 0;
        if (ab)
            return v + add;
        else
            return v - add;
    }
    /// <summary>正愈大、負愈小</summary>
    public static float AbsAdd(float v, float add)
    {
        bool ab = v >= 0;
        if (ab)
            return v + add;
        else
            return v - add;
    }
    /// <summary>正愈大、負愈小</summary>
    public static double AbsAdd(double v, double add)
    {
        bool ab = v >= 0;
        if (ab)
            return v + add;
        else
            return v - add;
    }
    /// <summary>正愈大、負愈小</summary>
    public static decimal AbsAdd(decimal v, decimal add)
    {
        bool ab = v >= 0;
        if (ab)
            return v + add;
        else
            return v - add;
    }
    #endregion
}
#region Json簡化轉換
[System.Serializable]
public struct ListString
{
    public List<string> list;
}

[System.Serializable]
public struct ListInt
{
    public List<int> list;
}

[System.Serializable]
public struct ListFloat
{
    public List<float> list;
}
#endregion

// 2021 by 阿葉