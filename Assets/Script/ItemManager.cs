using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具管理器
/// </summary>
public class ItemManager
{
	#region 單例
	public static ItemManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ItemManager();
			}
			return _instance;
		}
	}
	static ItemManager _instance = null;
	#endregion

	// 所有道具的資料
	public Item[] AllItemData = new Item[0];

	/// <summary>
	/// 初始化：載入所有道具資料
	/// </summary>
	public void Initialization()
	{
		AllItemData = Resources.LoadAll<Item>("");
	}

	/// <summary>
	/// 找道具資料
	/// </summary>
	/// <param name="id">道具ID</param>
	/// <returns></returns>
	public Item FindItemByID(int id)
	{
		for (int i = 0; i < AllItemData.Length; i++)
		{
			if (AllItemData[i].id == id)
			{
				return AllItemData[i];
			}
		}
		Debug.LogError("ID：" + id + "查無此ID");
		return new Item();
	}
}
