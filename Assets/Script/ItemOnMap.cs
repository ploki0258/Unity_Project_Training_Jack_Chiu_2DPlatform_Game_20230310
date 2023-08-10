using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMap : MonoBehaviour
{
	public Item dataItem;	// 道具的腳本化物件
	
	private int itemID;		// 道具ID

	private void Awake()
	{
		itemID = dataItem.id;
		// 道具ID = 該道具的ID
		// Debug.Log(itemID);
		
		/*dataItem = ItemManager.instance.FindItemData(itemID);
		Debug.Log(dataItem);
		*/
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			bool successAdd = SaveManager.instance.playerData.AddItem(itemID);  // 是否成功添加道具
			// Debug.Log("是否成功添加：" + successAdd);
			if (successAdd)
			{
				Destroy(this.gameObject);
				// Debug.Log("道具+1");
			}
			else
			{
				Debug.Log("背包已滿，無法添加" + "\n請先消耗一些道具");
			}
		}
	}
}
