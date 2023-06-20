using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMap : MonoBehaviour
{
	// private List<Item> itemList = new List<Item>();
	public int itemID = 0;
	Item dataItem;

	private void Start()
	{
		itemID = dataItem.id;

		// 載入玩家目前擁有的道具
		/*ItemManager.instance.Initialization();

		dataItem = ItemManager.instance.FindItemData(itemID);
		Debug.Log(dataItem);
		*/
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			bool successAdd = SaveManager.instance.playerData.AddItem(itemID);
			if (successAdd)
			{
				Destroy(this.gameObject);
				Debug.Log("道具+1");
			}
			else
			{
				Debug.Log("背包已滿，無法添加" + "\n請先消耗一些道具");
			}

		}
	}
}
