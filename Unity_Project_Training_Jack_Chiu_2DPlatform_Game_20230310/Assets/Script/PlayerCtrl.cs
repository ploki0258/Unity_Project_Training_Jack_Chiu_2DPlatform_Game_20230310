using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
	[Header("移動速度"), Range(0, 100)]
	[SerializeField] float speed = 10f;
	[Header("跳躍力量")]
	[SerializeField] float 跳躍力 = 0f;
	[SerializeField] GameObject atkObject = null;
	[SerializeField] Transform 攻擊生成點 = null;
	[SerializeField] float 攻擊速度 = 0f;

	[Tooltip("用來儲存玩家是否站在地板上")]
	private bool onFloor = false;
	bool 翻轉 = false;
	Rigidbody2D rig;
	Animator ani;

	private void Awake()
	{
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
	}

	private void Update()
	{
		PlayerMove();
		Jump();
		Attack();
	}

	private void FixedUpdate()
	{
		//偵測是否踩到地板
		onFloor = Physics2D.Raycast(this.transform.position, new Vector2(0f, -1f), 0.9f);
	}

	/// <summary>
	/// 角色移動
	/// </summary>
	void PlayerMove()
	{
		// 移動
		float ad = Input.GetAxisRaw("Horizontal");                        // 取得水平值
		rig.velocity = new Vector2(ad * speed, rig.velocity.y);

		//移動動畫
		ani.SetBool("isRun", ad != 0);

		// 翻轉
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
			翻轉 = false;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
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
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && onFloor != false)
		{
			// rig.velocity = new Vector2(rig.velocity.x, 跳躍力);
			rig.AddForce(transform.up * 跳躍力, ForceMode2D.Impulse);
		}

		//跳躍動畫
		ani.SetBool("isJump", onFloor == false);
		Debug.Log("踩到地板" + onFloor);
	}

	/// <summary>
	/// 攻擊功能
	/// </summary>
	void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (翻轉 != true)
			{
				GameObject temp = Instantiate(atkObject, 攻擊生成點.position, Quaternion.Euler(0, 0, 90f));
				temp.GetComponent<Rigidbody2D>().AddForce(transform.right * 攻擊速度 + transform.up * 100);
			}
			else
			{
				GameObject temp = Instantiate(atkObject, 攻擊生成點.position, Quaternion.Euler(0, 0, 270f));
				temp.GetComponent<Rigidbody2D>().AddForce(transform.right * 攻擊速度 + transform.up * 100);
			}
		}
	}
}
