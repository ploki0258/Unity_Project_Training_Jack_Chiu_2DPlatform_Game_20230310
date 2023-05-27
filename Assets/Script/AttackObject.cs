using UnityEngine;

public class AttackObject : MonoBehaviour
{
	[Header("傷害值")]
	[SerializeField] float damage = 5f;

	/*
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy")
		{
			Destroy(this.gameObject);
		}

        if (collision.gameObject.tag == "enemy")
        {
			Enemy.instance.TakeDamageMonster(damage);
		}
	}
	*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy")
		{
			Destroy(this.gameObject);
		}

		if (collision.gameObject.tag == "enemy")
		{
			Enemy.instance.TakeDamageMonster(damage);
		}
	}
}
