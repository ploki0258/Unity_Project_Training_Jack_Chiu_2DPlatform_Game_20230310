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

		// ���J���a�ثe�֦����D��
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
				Debug.Log("�D��+1");
			}
			else
			{
				Debug.Log("�I�]�w���A�L�k�K�[" + "\n�Х����Ӥ@�ǹD��");
			}

		}
	}
}
