using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �D��޲z��
/// </summary>
public class ItemManager
{
	#region ���
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

	// �Ҧ��D�㪺���
	public Item[] AllItemData = new Item[0];

	/// <summary>
	/// ��l�ơG���J�Ҧ��D����
	/// </summary>
	public void Initialization()
	{
		AllItemData = Resources.LoadAll<Item>("");
	}

	/// <summary>
	/// ��D����
	/// </summary>
	/// <param name="id">�D��ID</param>
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
		Debug.LogError("ID�G" + id + "�d�L��ID");
		return new Item();
	}
}
