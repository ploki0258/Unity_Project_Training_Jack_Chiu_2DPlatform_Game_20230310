using Fungus;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	[Header("新增道具編號"), Tooltip("要新增的道具編號"), Range(0, 100)]
	public int idAdd;
	[Header("移除道具編號"), Tooltip("要移除的道具編號"), Range(0, 100)]
	public int idRemove;

	[Tooltip("計算碰撞的次數")]
	private int collisionsCount = 0;
	//private int itemAddID;                 // 新增道具ID
	//private int itemRemoveID;              // 移除道具ID

	public static PuzzleManager instance = null;

	private void Awake()
	{
		instance = this;
		//itemAddID = dataItem.id;
		//itemRemoveID = dataItem.id;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("bullet") == true)
		{
			if (PlayerCtrl.instance.atkObject != false && collision.gameObject.name != "土牆_12")
			{
				collisionsCount++;
			}
		}

		if (collision.gameObject.CompareTag("bullet") && collisionsCount >= 3)
		{
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// 檢查特定關卡道具：增減特定道具
	/// </summary>
	/// <param name="AddID">新增道具編號</param>
	/// <param name="RemoveID">移除道具編號</param>
	public void CheckItemForLevel(int AddID, int RemoveID)
	{
		if (SaveManager.instance.playerData.IsHaveItem(RemoveID))
		{
			Debug.Log("已取得該道具");
			if (idRemove != 0 && idAdd != 0)
			{
				SaveManager.instance.playerData.RemoveItem(RemoveID);
				SaveManager.instance.playerData.haveItem.Remove(RemoveID);
				SaveManager.instance.playerData.AddItem(AddID);
				SaveManager.instance.playerData.haveItem.Add(AddID);
			}
		}
		else
		{
			Debug.Log("目前尚未取得該道具");
		}
	}
}
