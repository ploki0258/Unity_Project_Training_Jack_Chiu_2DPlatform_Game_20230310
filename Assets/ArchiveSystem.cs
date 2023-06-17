using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveSystem : MonoBehaviour
{
	[SerializeField] Animator archiveAni = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			archiveAni.SetBool("play",true);
		}
	}
}
