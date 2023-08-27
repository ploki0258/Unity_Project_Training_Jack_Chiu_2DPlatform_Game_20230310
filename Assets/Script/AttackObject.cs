using UnityEngine;

public class AttackObject : MonoBehaviour
{
	//[SerializeField, Header("傷害值")]
	//float damage = 0f;
	[SerializeField, Header("物理攻擊"), Tooltip("是否在物理攻擊")]
	bool isWandAttack = false;  // 是否在物理攻擊(MP <= 0)
	[Header("技能資料")]
	public Skill skillData;

	[Tooltip("原本怪物移動速度")]
	private float originalSpeedMonster;
	private float timer;
	private bool isTerraSkill = false;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rg2D;

	private void Awake()
	{
		if (skillData)
		{
			originalSpeedMonster = Enemy.instance.speedMonster;
		}
	}

	private void Start()
	{
		if (isWandAttack == false && skillData.skillName == "土牆")
		{
			timer = skillData.skillHoldTime;
			boxCollider = FindObjectOfType<SkillDragDrop>().skillData.skillPrefab.GetComponent<BoxCollider2D>();
		}

		if (isWandAttack == false && skillData.skillName == "風刃")
		{
			rg2D = GetComponent<Rigidbody2D>();
		}
	}

	private void Update()
	{
		SkillEffectButton();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果 子彈 碰到 地板 或 敵人 就消失
		if ((collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy") && isTerraSkill == false)
		{
			// 如果 不在物理攻擊(MP <= 0)時 消失自己
			if (isWandAttack == false)
			{
				Destroy(this.gameObject);
			}
			// Debug.Log($"<color=#ff0>是否施放土技能： {isTerraSkill}</color>");
			// Debug.Log($"<color=#f1f>是否在物理攻擊： {isWandAttack}</color>");
		}
		//// 如果子彈碰到"敵人" 就給予傷害
		//if (collision.gameObject.tag == "enemy")
		//{
		//	Enemy.instance.TakeDamageMonster(damage);
		//}
	}

	/// <summary>
	/// 技能效果：執行各個技能效果
	/// </summary>
	private void SkillEffectButton()
	{
		if (skillData != null)
		{
			if (skillData.skillName == "火球術") FireSkill();
			if (skillData.skillName == "風刃") WindSkill();
			if (skillData.skillName == "冰椎刺") IceSkill();
			if (skillData.skillName == "土牆") TerraSkill();
		}
	}
	#region 各屬性技能效果
	// 火球術技能
	void FireSkill()
	{

	}

	// 風刃技能
	void WindSkill()
	{
		PlayerCtrl.instance.UpdateDirectionIconPos();
		rg2D.velocity = transform.right * SaveManager.instance.playerData.playerAttackSpeed * 3f * Time.unscaledDeltaTime;
		//Vector3 movent = PlayerCtrl.instance.traDirectionIcon.eulerAngles * SaveManager.instance.playerData.playerAttackSpeed;
		//transform.position += movent;
	}

	// 冰椎刺技能
	void IceSkill()
	{
		bool speed = Enemy.instance.speedMonster == originalSpeedMonster;

		// 如果降速秒數 大於 零
		if (skillData.enemyDelayTime > 0)
		{
			// (擊中敵人時)降低敵人的移動速度
			Enemy.instance.speedMonster -= skillData.enemySlowSpeed;
			// Debug.Log("降低敵人速度" + speed);
		}

		// 降速秒數隨時間(秒)減少
		skillData.enemyDelayTime -= Time.deltaTime;

		// 如果降速秒數 小於等於 零
		if (skillData.enemyDelayTime <= 0)
		{
			// 恢復敵人的移動速度
			Enemy.instance.speedMonster = originalSpeedMonster;
			// Debug.Log("恢復敵人速度" + speed);
		}
	}

	// 土牆技能
	void TerraSkill()
	{
		isTerraSkill = true;

		if (timer >= 0)
		{
			boxCollider.isTrigger = false;
		}

		timer -= Time.deltaTime;
		// Debug.Log($"<color=#01f>計時器：{timer}</color>");
		if (timer <= 0)
		{
			boxCollider.isTrigger = true;
			Destroy(this.gameObject);
		}
	}
	#endregion
}
