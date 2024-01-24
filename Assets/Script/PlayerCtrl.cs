using System;
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
	[Header("攻擊方向圖示")]
	public Transform traDirectionIcon = null;
	[SerializeField, Header("攻擊方向圖示位移")]
	Vector3 traDirectionIconOffset;
	[SerializeField, Header("地板偵測器")] Sensor floorSensor = null;
	public Camera cam;

	// 怪物使用
	[Header("資訊欄顯示")]
	public Text coinInfo = null;
	public Text skillInfo = null;
	[Header("訊息提示文字")]
	public Text textMessageTip;                // 提醒訊息
											   //[Header("金幣顯示動畫")]
											   //public Animator showCoinAni = null;
											   //[Header("技能點數顯示動畫")]
											   //public Animator showSkillPointAni = null;
	[Header("提示訊息顯示動畫")]
	public Animator showMessageTipAni = null;

	public Animator ani;
	public bool isPausedGame;
	Rigidbody2D rig;
	public SkillSystem skillSystem;
	bool 翻轉 = false;
	Vector3 mousePos;
	Vector2 iconDirection;
	Skill skillData;
	//[Tooltip("用來儲存玩家是否站在地板上")]
	//private bool onFloor = false;
	//bool isWindowsOpen = WindowsManager.instance.IsWindowsOpen();   // 視窗是否被開啟
	#endregion

	// 在整個專案全域宣告一個instance
	public static PlayerCtrl instance = null;

	private void Awake()
	{
		// 讓單例等於自己
		instance = this;

		// 角色出生時 讀檔一次
		SaveManager.instance.LoadData();
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		skillSystem = FindObjectOfType<SkillSystem>();

		#region 程式抓取組件
		//barHP = GameObject.Find("血條").GetComponent<Image>();
		//barMP = GameObject.Find("魔力條").GetComponent<Image>();
		//coinCount = GameObject.Find("金幣數量").GetComponent<TextMeshProUGUI>();
		//skillCount = GameObject.Find("技能點數").GetComponent<TextMeshProUGUI>();
		//cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		//coinInfo = GameObject.Find("金幣顯示").GetComponent<Text>();
		//skillInfo = GameObject.Find("技能點數顯示").GetComponent<Text>();
		//showCoinAni = GameObject.Find("資訊顯示動畫").GetComponent<Animator>();
		//showSkillPointAni = GameObject.Find("資訊顯示動畫").GetComponent<Animator>();
		#endregion
	}

	private void Start()
	{
		coinInfo.text = "";
		skillInfo.text = "";
		textMessageTip.text = "";
		//skillData = SkillManager.instance.FindSkillByID(SaveManager.instance.playerData.skillObjectID);

		// 如果 有放置技能 的話
		// 就生成技能圖示在技能欄中
		if (SaveManager.instance.playerData.isSetSkill == true)
		{
			Transform temp = skillSystem.GetTransformBySlotID(SaveManager.instance.playerData.skillSlotID);
			//Instantiate(, temp.position, temp.rotation);
			Debug.Log($"<color=#690>技能物件：{skillData.skillIcon}</color>");
		}

		// 如果記錄中的位置不是000 才瞬間移動到記錄中的位置
		// 如果記錄中的位置是000表示可能沒有紀錄
		if (SaveManager.instance.playerData.playerPos != Vector3.zero)
			this.transform.position = SaveManager.instance.playerData.playerPos;    // 瞬間移動到記錄中的位置

		// 登記事件變化
		SaveManager.instance.playerData.renewMmessageTip += RenewMessageTip;
		SaveManager.instance.playerData.renewPlayerHP += RenewPlayerHP;
		SaveManager.instance.playerData.renewPlayerMP += RenewPlayerMP;
		SaveManager.instance.playerData.renewPlayerSpeed += RenewPlayerMove;
		SaveManager.instance.playerData.renewPlayerJump += RenewPlayerJump;
		SaveManager.instance.playerData.renewPlayerAttackSpeed += RenewPlayerAttackSpeed;
		SaveManager.instance.playerData.renewPlayerAttack += RenewPlayerAttack;
		SaveManager.instance.playerData.renewPlayerDefense += RenewPlayerDefecse;
		// 強制刷新一次HP & MP
		RenewPlayerHP();
		RenewPlayerMP();

		// SaveManager.instance.playerData.renewPlayerDefense += () => { Debug.Log("renewPlayerDefense"); };
	}

	private void OnDisable()
	{
		// 退出登記
		SaveManager.instance.playerData.renewMmessageTip -= RenewMessageTip;
		//SaveManager.instance.playerData.renewPlayerHP -= RenewPlayerHP;
		//SaveManager.instance.playerData.renewPlayerMP -= RenewPlayerMP;
		//SaveManager.instance.playerData.renewPlayerSpeed -= RenewPlayerMove;
		//SaveManager.instance.playerData.renewPlayerJump -= RenewPlayerJump;
		//SaveManager.instance.playerData.renewPlayerAttackSpeed -= RenewPlayerAttackSpeed;
		//SaveManager.instance.playerData.renewPlayerAttack -= RenewPlayerAttack;
		//SaveManager.instance.playerData.renewPlayerDefense -= RenewPlayerDefecse;
	}

	private void Update()
	{
		PlayerMove();
		Jump();
		Attack();
		Dead();

#if UNITY_EDITOR
		Panacea();
#endif
		// speedAtk += itemNormalValue.提升攻擊速度;
		// Debug.Log("以提升數值");
		// Debug.Log("是否暫停：" + isPausedGame);

		//Debug.Log("玩家血量：" + SaveManager.instance.playerData.playerHP);
		//Debug.Log("最大血量：" + maxHP);
	}

	private void FixedUpdate()
	{
		//偵測是否踩到地板物件
		//onFloor = Physics2D.Raycast(this.transform.position, new Vector2(0f, -1f), -1f, 1 << 6);
	}

	/// <summary>
	/// 角色移動
	/// </summary>
	void PlayerMove()
	{
		// 移動
		float ad = Input.GetAxisRaw("Horizontal");  // 取得水平值

		/*if (isWindowsOpen)                          // 如果有視窗被開啟 就不移動
			ad = 0;
		*/

		rig.velocity = new Vector2(ad * SaveManager.instance.playerData.playerSpeed, rig.velocity.y);

		//移動動畫
		ani.SetBool("isRun", ad != 0);

		// 翻轉
		if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
		{
			this.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
			翻轉 = false;
		}

		if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
		{
			this.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
			翻轉 = true;
		}

		// 播放跑步音效
		if (ad != 0)
		{
			//AudioClip sound = SoundManager.instance.run;
			//SoundManager.instance.PlayGameSfx(sound);
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
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && floorSensor.isOn == true)
		{
			// rig.velocity = new Vector2(rig.velocity.x, 跳躍力);
			rig.AddForce(transform.up * SaveManager.instance.playerData.playerJump, ForceMode2D.Impulse);
		}

		//跳躍動畫
		ani.SetBool("isJump", floorSensor.isOn == false);
	}

	/// <summary>
	/// 攻擊功能：施放法術
	/// </summary>
	void Attack()
	{
		if (isPausedGame == false)
		{
			// 如果按下左鍵
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				ani.SetTrigger("attack");

				// 如果有攻擊物件
				if (atkObject)
				{
					// 如果魔力值 小於等於 零 就不執行
					if (SaveManager.instance.playerData.playerMP <= 0)
						return;
					// 魔力消耗 等於 攻擊物件的魔力消耗
					SaveManager.instance.playerData.costMP = atkObject.GetComponent<AttackObject>().skillData.skillCost;
					// 扣魔力消耗
					SaveManager.instance.playerData.playerMP -= SaveManager.instance.playerData.costMP;

					#region// 依據攻擊物件的種類 將攻擊物件生成點生成在特定座標
					if (atkObject.name == "火球_0")
					{
						// 生成子彈並發射
						if (atkObject != null && 翻轉 != true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 0f, 0f));
							tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
						else if (atkObject != null && 翻轉 == true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 180f, 0f));
							tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
					}

					if (atkObject.name == "風刃_4")
					{
						traDirectionIcon.gameObject.SetActive(true);
						// UpdateDirectionIconPos();

						// 生成子彈並發射
						if (atkObject != null && 翻轉 != true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(攻擊方向圖示.角度))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(traDirectionIcon.eulerAngles));
							// tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
						else if (atkObject != null && 翻轉 == true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(traDirectionIcon.eulerAngles));
							// tempAtkObject.GetComponent<Rigidbody2D>().AddForce(traDirectionIcon.eulerAngles * SaveManager.instance.playerData.playerAttackSpeed * 100f, ForceMode2D.Impulse);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
					}
					else
					{
						traDirectionIcon.gameObject.SetActive(false);
					}

					if (atkObject.name == "冰椎刺_8")
					{
						// 生成子彈並發射
						if (atkObject != null && 翻轉 != true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 0f, 0f));
							tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
						else if (atkObject != null && 翻轉 == true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 180f, 0f));
							tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
					}

					if (atkObject.name == "土牆_12")
					{
						// 取得滑鼠座標轉換為世界座標
						mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
						// 計算差值並標準化(只取其方向，但長度為1)
						// iconDirection = (mousePos - transform.position).normalized;

						// 生成子彈並發射
						if (atkObject != null && 翻轉 != true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, new Vector3(mousePos.x, -2.7f, 0f), Quaternion.Euler(0f, 0f, 0f));
							// tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
						else if (atkObject != null && 翻轉 == true)
						{
							// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
							GameObject tempAtkObject = Instantiate(atkObject, new Vector3(mousePos.x, -2.7f, 0f), Quaternion.Euler(0f, 180f, 0f));
							// Attack(atkObject, new Vector3(atkPoint.position.x, 0f, atkPoint.position.z) + new Vector3(1.5f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
							// tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

							AudioClip sound = SoundManager.instance.attack;
							SoundManager.instance.PlayGameSfx(sound);
						}
					}
					#endregion

					// SpellAttack();
				}
				else if (atkObject == null)
				{
					ani.SetTrigger("attack");
				}

				#region // 生成子彈並發射(原)
				/*
				if (atkObject != null && 翻轉 != true)
				{
					// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
					GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 0f, 0f));
					tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

					AudioClip sound = SoundManager.instance.attack;
					SoundManager.instance.PlaySound(sound, 0.7f, 2f);
				}
				else if (atkObject != null && 翻轉 == true)
				{
					// 暫存攻擊物件 = 實例化(攻擊物件, 攻擊生成點.位置, Quaternion.尤拉角(0f, 0f, 0f))
					GameObject tempAtkObject = Instantiate(atkObject, atkPoint.position, Quaternion.Euler(0f, 180f, 0f));
					tempAtkObject.GetComponent<Rigidbody2D>().AddForce(transform.right * SaveManager.instance.playerData.playerAttackSpeed + transform.up * 10);

					AudioClip sound = SoundManager.instance.attack;
					SoundManager.instance.PlaySound(sound, 0.7f, 2f);
				}*/
				#endregion
			}
		}
	}

	/// <summary>
	/// 法術功能：各個法術的效果
	/// </summary>
	//private void SpellAttack()
	//{

	//}

	/// <summary>
	/// 死亡功能
	/// </summary>
	void Dead()
	{
		if (SaveManager.instance.playerData.playerHP <= 0)
		{
			ani.SetTrigger("die");
			Destroy(this);  // 關閉此腳本
		}
	}

	/// <summary>
	/// 受傷功能
	/// </summary>
	/// <param name="hurt">傷害量</param>
	public void TakeDamage(float hurt)
	{
		// 生命值 - 傷害(扣除防禦力的百分比)           ( 100 - 100 * (                   10                         /  100))
		SaveManager.instance.playerData.playerHP -= (hurt - hurt * (SaveManager.instance.playerData.playerDefense / 100));
	}

	/// <summary>
	/// 更新攻擊方向圖示的座標
	/// </summary>
	public void UpdateDirectionIconPos()
	{
		Vector3 pos = transform.position + traDirectionIconOffset;
		traDirectionIcon.position = pos;

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);                         // 取得滑鼠座標轉換為世界座標
		iconDirection = (mousePos - transform.position).normalized;                     // 計算差值並標準化(只取其方向，但長度為1)
		float angle = Mathf.Atan2(iconDirection.y, iconDirection.x) * Mathf.Rad2Deg;    // 計算旋轉角度
		traDirectionIcon.eulerAngles = new Vector3(0, 0, angle);
		#region 測試
		// 取得當前物件的角度
		// float degree = traDirectionIcon.rotation.eulerAngles.z;
		// float radian = Mathf.PI / 180 * degree;

		// traDirectionIcon.rotation = degree;

		// float x = (float)Math.Cos(radian);
		// float x = Mathf.Cos(radian);
		// float y = Mathf.Sin(radian);
		// Vector3 movent = new Vector3(x, y, 0f) * SaveManager.instance.playerData.playerAttackSpeed * Time.deltaTime;
		// traDirectionIcon.position += movent;
		#endregion
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
	/// 提示訊息顯示動畫
	/// </summary>
	public void RenewMessageTip()
	{
		// 播放動畫
		PlayerCtrl.instance.showMessageTipAni.SetTrigger("play");
	}

	/// <summary>
	/// 更新玩家血量
	/// </summary>
	void RenewPlayerHP()
	{
		barHP.fillAmount = SaveManager.instance.playerData.playerHP / maxHP;
		//Debug.Log($"<color=red>玩家血量： {SaveManager.instance.playerData.playerHP}</color>");
		//Debug.Log("<color=red>最大血量：</color>" + maxHP);
		//Debug.Log($"<color=red>當前血量：{barHP.fillAmount * 100}</color>");
	}

	/// <summary>
	/// 更新玩家魔力
	/// </summary>
	void RenewPlayerMP()
	{
		barMP.fillAmount = SaveManager.instance.playerData.playerMP / maxMP;
		//Debug.Log($"<color=blue>玩家魔力： {SaveManager.instance.playerData.playerMP}</color>");
		//Debug.Log("<color=blue>最大魔力：</color>" + maxMP);
		//Debug.Log($"<color=blue>當前魔力：{barMP.fillAmount * 100}</color>");
	}

	/// <summary>
	/// 更新玩家移動速度
	/// </summary>
	void RenewPlayerMove()
	{
		//SaveManager.instance.playerData.playerSpeed += speed;
		//speed += SaveManager.instance.playerData.playerSpeed;
	}

	/// <summary>
	/// 更新玩家跳躍力
	/// </summary>
	void RenewPlayerJump()
	{
		//instance.jumpForce += SaveManager.instance.playerData.playerJump;
	}

	/// <summary>
	/// 更新玩家攻擊速度
	/// </summary>
	void RenewPlayerAttackSpeed()
	{
		//instance.atkSpeed += SaveManager.instance.playerData.playerAttackSpeed;
	}

	/// <summary>
	/// 更新玩家攻擊力
	/// </summary>
	void RenewPlayerAttack()
	{
		//instance.attack += SaveManager.instance.playerData.playerAttack;
	}

	/// <summary>
	/// 更新玩家防禦力
	/// </summary>
	void RenewPlayerDefecse()
	{
		//instance.defense += SaveManager.instance.playerData.playerDefense;
	}

	/// <summary>
	/// 更新金幣顯示動畫
	/// </summary>
	/*void RenewCoin()
	{
		// 播放動畫
		PlayerCtrl.instance.showCoinAni.SetTrigger("play");
	}
	/// <summary>
	/// 更新技能點數顯示動畫
	/// </summary>
	void RenewSkillPoint()
	{
		// 播放動畫
		PlayerCtrl.instance.showSkillPointAni.SetTrigger("play");
	}
	*/

	/// <summary>
	/// 儲存玩家資訊
	/// </summary>
	/*public void SaveBtn()
	{
		SaveManager.instance.SaveData();
	}

	/*public void Coin()
	{
		countCoin.text = "× " + Enemy.instance.coinNumber.ToString();
	}

	public float hp
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
}
