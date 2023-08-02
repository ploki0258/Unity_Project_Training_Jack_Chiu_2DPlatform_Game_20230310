using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	private int collisionsCount = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("bullet") == true)
		{
			if (PlayerCtrl.instance.atkObject != false && collision.gameObject.name != "¤gÀð_12")
			{
				collisionsCount++;
			}
		}

		if (collision.gameObject.CompareTag("bullet") && collisionsCount >= 3)
		{
			Destroy(this.gameObject);
		}
	}
}
