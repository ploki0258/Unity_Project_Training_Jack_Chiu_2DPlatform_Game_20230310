using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	#region 欄位
	[Header("怪物最大血量"), Range(0, 1000)]
	public float hpMonsterMax = 100f;
	[Header("怪物血條")]
	public Image barHP = null;
	[Header("怪物攻擊力"), Range(10, 1000)]
	public float atkMonster = 5f;
	[Header("生成點")]
	public Transform point = null;
	[Header("怪物移動速度"), Range(0, 30)]
	public float speedMonster = 5f;
	[Header("追蹤距離")]
	public float disChase = 8f;
	[Header("掉落(技能)道具")]
	public GameObject itemSkill = null;
	[Header("掉落機率"), Tooltip("怪物的掉落機率"), Range(1, 10)]
	public int probDrop = 5;
	[SerializeField, Header("尋找物件的名稱")]
	string objectName;
	
	[Tooltip("怪物所受到的傷害值")]
	private float damage;
	/*
    [Header("資訊欄顯示")]
    [SerializeField] Text coinInfo = null;
    [SerializeField] Text skillInfo = null;
    [Header("金幣數量")]
    [SerializeField] TextMeshProUGUI coinCount = null;
    [Header("技能點數")]
    [SerializeField] TextMeshProUGUI skillCount = null;
    [Header("金幣顯示動畫")]
    [SerializeField] Animator showCoinAni = null;
    [Header("技能點數顯示動畫")]
    [SerializeField] Animator showSkillPointAni = null;

    // [Header("最大血量")]
    // [SerializeField] float maxHP = 200f;
    */

	[Tooltip("獲得金幣數量")]
	int coinNumber;
	[Tooltip("獲得技能點數數量")]
	int skillNumber;
	// bool aniCoin = false;       // 播放金幣動畫
	// bool aniSkill = false;      // 播放技能點數動畫
	bool isDeath = false;       // 是否死亡
	Transform player = null;
	Rigidbody2D rig = null;
	Animator ani = null;
	SpawnSystem spawnSystem;
	#endregion

	// 在整個專案全域宣告一個instance
	public static Enemy instance = null;

	private void Awake()
	{
		instance = this;    // 讓單例等於自己
		player = GameObject.Find("女主角").transform;
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		spawnSystem = FindObjectOfType<SpawnSystem>();
		// spawnSystem = GameObject.Find(objectName).GetComponent<SpawnSystem>();
	}

	private void Start()
	{
		hpMonster = hpMonsterMax;
		
		// coinInfo.text = "";
		// skill = 0;
	}

	private void Update()
	{
		TrackingPlayer();
		GotEnemyDamage();
		//Debug.Log("怪物血量：" + hpMonster);
	}

	/// <summary>
	/// 怪物所受到的攻擊傷害：依據玩家的攻擊力受到傷害量
	/// </summary>
	void GotEnemyDamage()
	{
		if (PlayerCtrl.instance.atkObject == null || SaveManager.instance.playerData.playerMP == 0)
		{
			damage = SaveManager.instance.playerData.playerAttack;
			// Debug.Log("玩家傷害：" + SaveManager.instance.playerData.playerAttack);
		}
		else if (PlayerCtrl.instance.atkObject != null)
		{
			damage = GameObject.Find("女主角").GetComponent<PlayerCtrl>().atkObject.GetComponent<AttackObject>().skillData.skillDamage;
			// Debug.Log("技能傷害：" + GameObject.Find("女主角").GetComponent<PlayerCtrl>().atkObject.GetComponent<AttackObject>().skillData.skillDamage);
		}
	}

	/// <summary>
	/// 追蹤玩家
	/// </summary>
	private void TrackingPlayer()
	{
		// 面向玩家：如果玩家的 X 大於 敵人的 X 角度 0，否則 角度 180
		if (player.position.x > this.transform.position.x)
		{
			transform.eulerAngles = Vector3.zero;
		}
		else if (player.position.x < this.transform.position.x)
		{
			transform.eulerAngles = new Vector3(0f, 180f, 0f);
		}


		// 距離 = 二維向量的 距離(A點, B點)
		float dis = Vector3.Distance(transform.position, player.position);
		// 如果玩家進入追蹤範圍 就追蹤玩家
		if (dis <= disChase)
		{
			Vector3 newPos = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 0.1f);
			transform.position = newPos;

			// transform.position = Vector3.MoveTowards(transform.position, player.position, speedMonster);
			// rig.velocity = transform.right * speedMonster;
			// rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
		}
	}

	/// <summary>
	/// 怪物受到傷害功能：扣血量
	/// </summary>
	/// <param name="hurt">所受的傷害量</param>
	public void TakeDamageMonster(float hurt)
	{
		hpMonster -= hurt;
	}

	/// <summary>
	/// 死亡功能：死亡後掉落道具、關閉腳本、刪除自己
	/// </summary>
	void Dead()
	{
		DropItem();
		enabled = false;
		Destroy(gameObject);
	}

	/// <summary>
	/// 道具掉落功能：掉落金幣、技能道具
	/// </summary>
	void DropItem()
	{
		if (isDeath == true)
		{
			coinNumber = Random.Range(10, 100) * 10;    // 獲得隨機金幣數量
			skillNumber = Random.Range(1, 10) * 10;     // 獲得隨機技能點數

			// 顯示資訊欄文字
			PlayerCtrl.instance.coinInfo.text = "+" + coinNumber + "金幣";
			PlayerCtrl.instance.skillInfo.text = "獲得" + skillNumber + "點";
			//Debug.Log("金幣：" + coinNumber);
			//Debug.Log("技能點數：" + skillNumber);

			// 玩家的金幣 & 技能點數 增加獲得的金幣 & 技能點數
			SaveManager.instance.playerData.moneyCount += coinNumber;
			SaveManager.instance.playerData.skillPoint += skillNumber;
			// SaveManager.instance.playerData.skillPoint += itemSkillValue.獲得額外點數;
			// Debug.Log(coinNumber);

			/*if (aniCoin == true)
            {
                PlayerCtrl.instance.showCoinAni.SetTrigger("play");
                PlayerCtrl.instance.coinCount.text = "× " + SaveManager.instance.playerData.moneyCount;
                coinNumber += SaveManager.instance.playerData.moneyCount;
            }

            if (aniSkill == true)
            {
                PlayerCtrl.instance.showSkillPointAni.SetTrigger("play");
                PlayerCtrl.instance.skillCount.text = "× " + SaveManager.instance.playerData.skillPoint;
                skillNumber += SaveManager.instance.playerData.skillPoint;
            }
            */

			// 有一半的機率會生成道具
			int randomDrop = Random.Range(1, 10);
			// 如果 randomDrop 小於等於 掉落機率 就生成道具
			if (randomDrop <= probDrop)
			{
				Instantiate(itemSkill, point.position, point.rotation);
			}
			// Debug.Log("隨機號：" + randomDrop);
		}
	}

	// 觸發事件 進入事件
	// 怪物受到傷害功能：受傷、死亡
	private void OnTriggerEnter2D(Collider2D collision)
	{
		spawnSystem.SpawnEnemy();

		// 如果已死亡 就不執行
		if (isDeath)
			return;
		// 如果碰到的物件的Tag 等於 bullet
		if (collision.gameObject.tag == "bullet")
		{
			TakeDamageMonster(damage);
			// Debug.Log($"<color=#ff9669>受到的傷害：{damage}</color>");

			if (hpMonster < 1)
			{
				//if (MistManager.instance.inMist_cyan == false)
				//	// 每消失一隻怪物 計數器就-1
				//	spawnSystem.enemyCount--;
				
				ani.SetTrigger("damage");
				isDeath = true;
				Invoke("Dead", 1f);

				if (GameObject.FindObjectOfType<MistManager>() == true && MistManager.instance.inMist_cyan == false)
				{
					// 如果 目前生成的怪物數量 小於 最大生成數量的話 而且 生成的怪物數量為0 則於5秒後生成怪物
					if (spawnSystem.enemyCount == 0)
					{
						spawnSystem.interval = 10;
						InvokeRepeating("spawnSystem.SpawnEnemy", 5f, spawnSystem.interval);
					}
				}
			}
		}
	}

	// 碰撞事件
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 如果碰到玩家 就依據怪物攻擊力扣玩家血量
		if (collision.gameObject.tag == "Player")
		{
			ani.SetTrigger("attack");
			PlayerCtrl.instance.ani.SetTrigger("hurt");
			PlayerCtrl.instance.TakeDamage(atkMonster);
		}
	}

	// 怪物血量
	public float hpMonster
	{
		// 取得時
		get { return hpMonsterMax * barHP.fillAmount; }
		// 寫入時
		set
		{
			barHP.fillAmount = value / hpMonsterMax;
		}
	}
}
