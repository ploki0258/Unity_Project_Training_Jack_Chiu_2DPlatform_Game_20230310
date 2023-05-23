using UnityEngine;

public class AttackObject : MonoBehaviour
{
	[Header("¶Ë®`­È")]
	[SerializeField] float damage = 0f;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			Destroy(this.gameObject);
		}

        if (collision.gameObject.tag == "enemy")
        {
			PlayerCtrl.instance.hp -= damage;
		}
	}
}
