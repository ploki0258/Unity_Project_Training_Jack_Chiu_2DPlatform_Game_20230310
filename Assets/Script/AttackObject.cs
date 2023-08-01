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
	private bool isTerraSkill = false;
	private float originalSpeedMonster;
	BoxCollider2D boxCollider;

	private void Awake()
	{
		if (skillData)
		{
			originalSpeedMonster = Enemy.instance.speedMonster;
		}
	}

	private void Start()
	{
		if (isWandAttack == false)
			timer = skillData.skillHoldTime;
	}

	private void Update()
	{
		SkillEffectButton();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果 子彈 碰到 地板 或 敵人 就消失
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy" || isTerraSkill == false)
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

	/// <summary>
	/// 技能效果：執行各個技能效果
	/// </summary>
	void SkillEffectButton()
	{
		if (skillData != null)
		{
			if (skillData.skillName == "火球術") FireSkill();
			if (skillData.skillName == "風刃") WindSkill();
			if (skillData.skillName == "冰椎刺") IceSkill();
			if (skillData.skillName == "土牆") TerraSkill();
		}
	}

	// 火球術技能
	void FireSkill()
	{

	}

	// 風刃技能
	void WindSkill()
	{
		Vector3 movent = PlayerCtrl.instance.traDirectionIcon.eulerAngles * SaveManager.instance.playerData.playerAttackSpeed * Time.deltaTime;
		transform.position += movent;
	}

	// 冰椎刺技能
	void IceSkill()
	{
		bool speed = Enemy.instance.speedMonster == originalSpeedMonster;

		if (skillData.enemyDelayTime > 0)
		{
			Enemy.instance.speedMonster -= skillData.enemySlowSpeed;
			Debug.Log("降低敵人速度" + speed);
		}

		skillData.enemyDelayTime -= Time.deltaTime;

		if (skillData.enemyDelayTime <= 0)
		{
			Enemy.instance.speedMonster = originalSpeedMonster;
			Debug.Log("恢復敵人速度" + speed);
		}
	}

	// 土牆技能
	void TerraSkill()
	{
		if (isWandAttack == false && skillData.skillName == "土牆")
			boxCollider = FindObjectOfType<SkillDragDrop>().skillData.skillPrefab.GetComponent<BoxCollider2D>();
		Debug.Log(boxCollider);

		isTerraSkill = true;
		if (timer > 0)
		{
			boxCollider.isTrigger = false;
		}

		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			boxCollider.isTrigger = true;
			Destroy(this.gameObject);
		}
	}
}
