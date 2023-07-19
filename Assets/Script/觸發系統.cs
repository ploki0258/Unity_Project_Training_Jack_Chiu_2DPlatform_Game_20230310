using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class 觸發系統 : MonoBehaviour
{
	[SerializeField, Header("事件可否重複"), Tooltip("是否可重複")]
	bool isRepeatable = false;
	[Header("要執行的事件")]
	public UnityEvent thingsToDo = null;

	[Tooltip("該事件是否已被觸發過")]
	bool 觸發過 = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果 玩家 經過的話
		if (collision.gameObject.tag == "Player")
		{
			// 如果是可重複 或者 尚未觸發過
			if (isRepeatable || 觸發過 == false)
			{
				觸發過 = true;			// 該事件變成已觸發
				thingsToDo.Invoke();	// 執行要做的事情
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
