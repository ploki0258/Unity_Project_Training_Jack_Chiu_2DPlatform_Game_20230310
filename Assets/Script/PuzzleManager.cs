using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	private int collisionsCount = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("bullet") == true)
		{
			collisionsCount++;
		}

		if (collision.gameObject.CompareTag("bullet") && collisionsCount >= 3)
		{
			Destroy(this.gameObject);
		}
	}
}
