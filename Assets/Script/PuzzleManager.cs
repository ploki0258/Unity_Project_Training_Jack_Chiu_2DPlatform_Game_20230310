using Fungus;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	[Header("新增道具編號"), Tooltip("要新增的道具編號")]
	public int idAdd;
	[Header("移除道具編號"), Tooltip("要移除的道具編號")]
	public int idRemove;

	[Tooltip("計算碰撞的次數")]
	private int collisionsCount = 0;

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
	/// <param name="id">道具編號</param>
	public void CheckItemForLevel(int id)
	{
		if (SaveManager.instance.playerData.IsHaveItem(id))
		{
			if (idRemove != 0 && idAdd != 0)
			{
				SaveManager.instance.playerData.RemoveItem(idRemove);
				SaveManager.instance.playerData.AddItem(idAdd);
			}
		}
	}
}
