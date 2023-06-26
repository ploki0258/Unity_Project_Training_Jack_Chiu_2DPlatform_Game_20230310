using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class 觸發系統 : MonoBehaviour
{
    public UnityEvent 要做的事情 = null;
	[SerializeField] bool 是否可重複 = false;
	bool 觸發過 = false;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			// 如果可重複或者尚未觸發過
			if (是否可重複 || 觸發過 == false)
			{
				觸發過 = true;
				要做的事情.Invoke();
			}
		}
	}
}
