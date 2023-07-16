using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
	#region 欄位
	[Header("攻擊物件"), Tooltip("用來儲存攻擊物件的預製物")]
	public GameObject atkObject = null;
	[SerializeField, Header("攻擊生成點"), Tooltip("用來儲存攻擊物件的生成位置")]
	Transform atkPoint = null;
	[Header("血量條")]
	public Image barHP = null;
	[Header("魔力條")]
	public Image barMP = null;
	[Header("金幣數量")]
	public TextMeshProUGUI coinCount = null;
	[Header("技能點數")]
	public TextMeshProUGUI skillCount = null;
	[Header("最大血量"), Range(100, 1000)]
	public float maxHP = 100f;
	[Header("最大魔力"), Range(100, 1000)]
	public float maxMP = 100f;
	[Header("魔力消耗")]
	public float costMP = 0f;

	[Header("移動速度"), Range(0, 100)]
	public float speed = 10f;
	[Header("跳躍力量"), Range(0, 100)]
	public float jumpForce = 7f;
	[Header("攻擊速度"), Range(0, 1000)]
	public float atkSpeed = 500f;
	[Header("攻擊力"), Range(10, 1000)]
	public float attack = 100f;
	[Header("防禦力"), Range(10, 1000)]
	public float defense = 100f;

	// 怪物使用
	[Header("資訊欄顯示")]
	public Text coinInfo = null;
	public Text skillInfo = null;
	[Header("金幣顯示動畫")]
	public Animator showCoinAni = null;
	[Header("技能點數顯示動畫")]
	public Animator showSkillPointAni = null;

	/*
	[Header("血量值文字")]
    [SerializeField] Text valueHP = null;
    [Header("魔力值文字")]
    [SerializeField] Text valueMP = null;
    */

	public Animator ani;
	Rigidbody2D rig;
	[Tooltip("用來儲存玩家是否站在地板上")]
	private bool onFloor = false;
	bool 翻轉 = false;
	bool isWindowsOpen = WindowsManager.instance.IsWindowsOpen();   // 視窗是否被開啟
	int skillID;
	#endregion

	// 在整個專案全域宣告一個instance
	public static PlayerCtrl instance = null;

	private void Awake()
	{
		instance = this;    // 讓單例等於自己
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		// 角色出生時 讀檔一次
		SaveManager.instance.LoadData();

		// 如果記錄中的位置不是000 才瞬間移動到記錄中的位置
		// 如果記錄中的位置是000表示可能沒有紀錄
		if (SaveManager.instance.playerData.playerPos != Vector3.zero)
			this.transform.position = SaveManager.instance.playerData.playerPos;    // 瞬間移動到記錄中的位置

		// SaveManager.instance.SaveData();
		// WindowsManager.instance.Start();
	}

	private void Start()
	{
		coinInfo.text = "";

		SaveManager.instance.playerData.renewCoin += RenewCoin;
		SaveManager.instance.playerData.renewSkillPoint += RenewSkillPoint;
		SaveManager.instance.playerData.renewPlayerHP += RenewPlayerHP;
		SaveManager.instance.playerData.renewPlayerMP += RenewPlayerMP;
		SaveManager.instance.playerData.renewPlayerSpeed += RenewPlayerMove;
		SaveManager.instance.playerData.renewPlayerJump += RenewPlayerJump;
		SaveManager.instance.playerData.renewPlayerAttackSpeed += RenewPlayerAttackSpeed;
		SaveManager.instance.playerData.renewPlayerAttack += RenewPlayerAttack;
		SaveManager.instance.playerData.renewPlayerDefense += RenewPlayerDefecse;
		RenewPlayerHP();
		RenewPlayerMP();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewCoin -= RenewCoin;
		SaveManager.instance.playerData.renewSkillPoint -= RenewSkillPoint;
		SaveManager.instance.playerData.renewPlayerHP -= RenewPlayerHP;
		SaveManager.instance.playerData.renewPlayerMP -= RenewPlayerMP;
		SaveManager.instance.playerData.renewPlayerSpeed -= RenewPlayerMove;
		SaveManager.instance.playerData.renewPlayerJump -= RenewPlayerJump;
		SaveManager.instance.playerData.renewPlayerAttackSpeed -= RenewPlayerAttackSpeed;
		SaveManager.instance.playerData.renewPlayerAttack -= RenewPlayerAttack;
		SaveManager.instance.playerData.renewPlayerDefense -= RenewPlayerDefecse;
	}

	private void Update()
	{
		PlayerMove();
		Jump();
		Attack();
		Panacea();
		Dead();

		// speedAtk += itemNormalValue.提升攻擊速度;
		// Debug.Log("以提升數值");
	}

	private void FixedUpdate()
	{
		//偵測是否踩到地板物件
		onFloor = Physics2D.Raycast(this.transform.position, new Vector2(0f, -1f), -1f, 1 << 6);
	}

	/// <summary>
	/// 角色移動
	/// </summary>
	void PlayerMove()
	{
		// 移動
		float ad = Input.GetAxisRaw("Horizontal");  // 取得水平值

		if (isWindowsOpen)                          // 如果有視窗被開啟 就不移動
			ad = 0;

		rig.velocity = new Vector2(ad * SaveManager.instance.playerData.playerSpeed, rig.velocity.y);

		if (ad != 0)
		{
			AudioClip sound = SoundManager.instance.run;
			SoundManager.instance.PlaySound(sound, 0.7f, 1f);
		}

		//移動動畫
		ani.SetBool("isRun", ad != 0);

		// 翻轉
		if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && isWindowsOpen == false)
		{
			this.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
			翻轉 = false;
		}

		if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && isWindowsOpen == false)
		{
			this.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
			翻轉 = true;
		}

		/*
        if (Input.GetKeyDown(KeyCode.RightArrow))                         // 如果 按下右方向鍵
        {
            // rig.AddForce(transform.right * speed, ForceMode2D.Force);  // 向右側施加一個推力
            // print("向右移動");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))                          // 如果 按下左方向鍵
        {
            // rig.AddForce(new Vector2(-speed, 0f), ForceMode2D.Force);  // 向左側施加一個推力
            // print("向左移動");
        }
        */
	}

	/// <summary>
	/// 跳躍功能
	/// </summary>
	void Jump()
	{
		// 如果 按下空白建(或上) 以及 onFloor = true 就跳躍
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && onFloor != false && isWindowsOpen == false)
		{
			// rig.velocity = new Vector2(rig.velocity.x, 跳躍力);
			rig.AddForce(transform.up * SaveManager.instance.playerData.playerJump, ForceMode2D.Impulse);
		}

		//跳躍動畫
		ani.SetBool("isJump", onFloor == false);
		// Debug.Log("踩到地板" + onFloor);
	}

	/// <summary>
	/// 攻擊功能：發射子彈
	/// </summary>
	void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && isWindowsOpen == false)
		{
			ani.SetTrigger("attack");

			if (atkObject)
			{
				if (SaveManager.instance.playerData.playerMP <= 0)
					return;

				SaveManager.instance.playerData.playerMP -= costMP;
			}
			else if (atkObject == null)
			{
				ani.SetTrigger("attack");
			}
			// 生成子彈並發射
			if (atkObject != null && 翻轉 != true)
			{
				GameObject temp = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 0f, 0f));
				temp.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

				AudioClip sound = SoundManager.instance.attack;
				SoundManager.instance.PlaySound(sound, 0.7f, 2f);
			}
			else if (atkObject != null && 翻轉 == true)
			{
				GameObject temp = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 0f, 180f));
				temp.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

				AudioClip sound = SoundManager.instance.attack;
				SoundManager.instance.PlaySound(sound, 0.7f, 2f);
			}
		}
	}

	/// <summary>
	/// 死亡功能
	/// </summary>
	void Dead()
	{
		if (SaveManager.instance.playerData.playerHP <= 0)
		{
			ani.SetTrigger("die");
			// SaveManager.instance.SaveUser();
			Destroy(this);  // 關閉此腳本
		}
	}

	/// <summary>
	/// 受傷功能
	/// </summary>
	/// <param name="hurt">傷害量</param>
	public void TakeDamage(float hurt)
	{
		SaveManager.instance.playerData.playerHP -= hurt;
	}

	/// <summary>
	/// 萬能藥：MP全回滿
	/// </summary>
	/// Test
	void Panacea()
	{
		if (Input.GetKeyDown(KeyCode.P) && SaveManager.instance.playerData.playerMP != maxMP)
		{
			SaveManager.instance.playerData.playerMP = maxMP;
		}
	}

	/// <summary>
	/// 儲存玩家資訊
	/// </summary>
	/*public void SaveBtn()
	{
		SaveManager.instance.SaveData();
	}
	*/

	/*public void Coin()
    {
        countCoin.text = "× " + Enemy.instance.coinNumber.ToString();
    }*/

	/*public float hp
    {
        get { return maxHP * barHP.fillAmount; }
        set
        {
            barHP.fillAmount = value / maxHP;
        }
    }

    public float mp
    {
        get { return maxMP * barMP.fillAmount; }
        set
        {
            barMP.fillAmount = value / maxMP;
        }
    }
    */

	/// <summary>
	/// 更新金幣顯示動畫
	/// </summary>
	void RenewCoin()
	{
		// 播放動畫
		PlayerCtrl.instance.showCoinAni.SetTrigger("play");
	}

	/// <summary>
	/// 技能點數顯示動畫
	/// </summary>
	void RenewSkillPoint()
	{
		// 播放動畫
		PlayerCtrl.instance.showSkillPointAni.SetTrigger("play");
	}

	/// <summary>
	/// 更新玩家血量
	/// </summary>
	void RenewPlayerHP()
	{
		barHP.fillAmount = SaveManager.instance.playerData.playerHP / maxHP;
	}

	/// <summary>
	/// 更新玩家魔力
	/// </summary>
	void RenewPlayerMP()
	{
		barMP.fillAmount = SaveManager.instance.playerData.playerMP / maxMP;
	}

	/// <summary>
	/// 更新玩家移動速度
	/// </summary>
	void RenewPlayerMove()
	{
		instance.speed += SaveManager.instance.playerData.playerSpeed;
	}

	/// <summary>
	/// 更新玩家跳躍力
	/// </summary>
	void RenewPlayerJump()
	{
		instance.jumpForce += SaveManager.instance.playerData.playerJump;
	}

	/// <summary>
	/// 更新玩家攻擊速度
	/// </summary>
	void RenewPlayerAttackSpeed()
	{
		instance.atkSpeed += SaveManager.instance.playerData.playerAttackSpeed;
	}

	/// <summary>
	/// 更新玩家攻擊力
	/// </summary>
	void RenewPlayerAttack()
	{
		instance.attack += SaveManager.instance.playerData.playerAttack;
	}

	/// <summary>
	/// 更新玩家防禦力
	/// </summary>
	void RenewPlayerDefecse()
	{
		instance.defense += SaveManager.instance.playerData.playerDefense;
	}
}
