using UnityEngine;

public class AttackObject : MonoBehaviour
{
	[SerializeField, Header("傷害值")]
	float damage = 0f;
	[SerializeField, Header("物理攻擊")]
	bool isWandAttack = false;  // 是否在物理攻擊(MP <= 0)

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果 子彈 碰到 地板 或 敵人 就消失
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy")
		{
			// 如果 不在物理攻擊(MP <= 0)時 消失自己
			if (isWandAttack == false)
			{
				Destroy(this.gameObject);
			}
		}
		// 如果 子彈 碰到 敵人 就給予傷害
		/*if (collision.gameObject.tag == "enemy")
		{
			Enemy.instance.TakeDamageMonster(damage);
		}
		*/
	}
}
