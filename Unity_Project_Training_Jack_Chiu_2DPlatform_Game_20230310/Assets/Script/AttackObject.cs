using UnityEngine;

public class AttackObject : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			Destroy(this.gameObject);
		}
	}
}
