using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	public int collisionsNumber = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("bullet") == true)
		{
			collisionsNumber++;
		}

		if (collision.gameObject.CompareTag("bullet") && collisionsNumber >= 3)
		{
			Destroy(this.gameObject);
		}
	}
}
