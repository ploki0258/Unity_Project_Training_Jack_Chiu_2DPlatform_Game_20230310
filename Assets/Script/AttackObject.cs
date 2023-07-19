using UnityEngine;

public class AttackObject : MonoBehaviour
{
	[SerializeField, Header("傷害值")]
	float damage = 0f;
	[SerializeField, Header("物理攻擊"), Tooltip("是否在物理攻擊")]
	bool isWandAttack = false;  // 是否在物理攻擊(MP <= 0)
	[SerializeField, Header("技能資料")]
	Skill skillData;

	private float timer;

	// 土牆技能
	private void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Destroy(this.gameObject);
		}
	}

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

	void SkillAttackEffect(int ID)
	{
		ID = skillData.id;
		if (ID == 0)
		{
			Debug.Log("這是" + skillData.skillName);
		}
	}
}
