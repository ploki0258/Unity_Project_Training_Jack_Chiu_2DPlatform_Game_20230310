using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class 觸發系統 : MonoBehaviour
{
	[SerializeField, Header("事件可否重複")] bool 是否可重複 = false;
	[Header("要執行的事件")]
	public UnityEvent 要做的事情 = null;

	bool 觸發過 = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果 玩家 經過的話
		if (collision.gameObject.tag == "Player")
		{
			// 如果是可重複 或者 尚未觸發過
			if (是否可重複 || 觸發過 == false)
			{
				觸發過 = true;
				要做的事情.Invoke(); // 執行要做的事情
			}
		}
	}

	// 繪製圖形
	private void OnDrawGizmos()
	{
		BoxCollider2D Cube2D = GetComponent<BoxCollider2D>();

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(new Vector3(Cube2D.transform.position.x, Cube2D.transform.position.y, Cube2D.transform.position.z),
			Cube2D.size);
	}
}
