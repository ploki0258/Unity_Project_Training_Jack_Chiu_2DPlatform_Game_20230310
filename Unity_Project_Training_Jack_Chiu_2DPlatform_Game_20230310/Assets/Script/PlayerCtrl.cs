using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    [SerializeField] float speed = 10f;
    [Header("跳躍力量")]
    [SerializeField] float 跳躍力 = 0f;
    [Header("攻擊物件")]
    [SerializeField] GameObject atkObject = null;
    [Header("生成點")]
    [SerializeField] Transform 攻擊生成點 = null;
    [Header("攻擊速度")]
    [SerializeField] float 攻擊速度 = 0f;
    [Header("血量條")]
    [SerializeField] Image barHP = null;
    [Header("魔力條")]
    [SerializeField] Image barMP = null;
    [Header("最大血量")]
    [SerializeField] float maxHP = 100f;
    [Header("最大魔力")]
    [SerializeField] float maxMP = 100f;
    [Header("金幣數量")]
    [SerializeField] Text countCoin = null;
    [Header("魔力消耗")]
    [SerializeField] float costMP = 0f;
    /*[Header("血量值")]
    [SerializeField] Text valueHP = null;
    [Header("魔力值")]
    [SerializeField] Text valueMP = null;
    */

    [Tooltip("用來儲存玩家是否站在地板上")]
    private bool onFloor = false;
    bool 翻轉 = false;
    Rigidbody2D rig;
    Animator ani;
    #endregion

    // 在整個專案全域宣告一個instance
    public static PlayerCtrl instance = null;

    private void Awake()
    {
        instance = this;    // 讓單例等於自己
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        hp = maxHP;
        mp = maxMP;
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
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
            翻轉 = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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
    /// 攻擊功能：發射子彈
    /// </summary>
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("attack");

            if (atkObject == true)
            {
                if (mp <= 0)
                    return;

                //costMP = 1f;
                mp -= costMP;
            }

            if (翻轉 != true)
            {
                GameObject temp = Instantiate(atkObject, 攻擊生成點.position, Quaternion.Euler(0, 0, 90f));
                temp.GetComponent<Rigidbody2D>().AddForce(transform.right * 攻擊速度 + transform.up * 250);
            }
            else
            {
                GameObject temp = Instantiate(atkObject, 攻擊生成點.position, Quaternion.Euler(0, 0, 270f));
                temp.GetComponent<Rigidbody2D>().AddForce(transform.right * 攻擊速度 + transform.up * 250);
            }
        }
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

    float coin
    {
        get { return _coin; }
        set
        {
            _coin = value;
            countCoin.text = Mathf.Round(value).ToString();
        }
    }
    float _coin = 0;
}
