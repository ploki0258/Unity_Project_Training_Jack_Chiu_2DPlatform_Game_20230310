using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    #region 欄位
    [Header("怪物最大血量")]
    [SerializeField] float hpMonsterMax = 100f;
    [Header("血條")]
    [SerializeField] Image barHP = null;
    [Header("怪物攻擊力")]
    [SerializeField] float atkMonster = 5f;
    [Header("生成點")]
    [SerializeField] Transform point = null;
    [Header("怪物移動速度")]
    [SerializeField] float speedMonster = 5f;
    [Header("基準距離")]
    [SerializeField] float disBase = 8f;
    [Header("掉落道具")]
    [SerializeField] GameObject itemNorm = null;
    [Header("掉落金幣數量")]
    private int coinCount = 0;
    /*[Header("資訊欄顯示")]
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
    */
    // [Header("掉落機率")]
    // public int randomDrop = Random.Range(1, 10);
    // [SerializeField] float maxHP = 100f;

    int coinNumber;
    int skillNumber;
    bool aniCoin = false;       // 播放金幣動畫
    bool aniSkill = false;      // 播放技能點數動畫
    bool isDeath = false;       // 是否死亡
    Transform player = null;
    Rigidbody2D rig = null;
    Animator ani = null;
    #endregion

    // 在整個專案全域宣告一個instance
    public static Enemy instance = null;

    private void Awake()
    {
        instance = this;    // 讓單例等於自己
        player = GameObject.Find("玩家").transform;
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        hpMonster = hpMonsterMax;
        SaveManager.instance.playerData.更新金幣 += 更新;
        // coinInfo.text = "";
        // skill = 0;
    }

    private void OnDisable()
    {
        SaveManager.instance.playerData.更新金幣 -= 更新;
    }

    private void Update()
    {
        // Move();
        DropItem();
    }

    void 更新()
    {
        if (aniCoin == true)
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
    }

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    void Move()
    {
        if (player.position.x > this.transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(player.position.x < this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        float dis = Vector3.Distance(player.position, transform.position);
        // 如果玩家進入追蹤範圍 就追蹤玩家
        if (dis < disBase)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speedMonster);
            // rig.velocity = transform.right * speedMonster;
            // rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y);
        }
    }

    /// <summary>
    /// 怪物受到傷害
    /// </summary>
    /// <param name="hurt">所受的傷害量</param>
    public void TakeDamageMonster(float hurt)
    {
        hpMonster -= hurt;
    }

    /// <summary>
    /// 死亡功能：刪除自己
    /// </summary>
    void Dead()
    {
        enabled = false;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 道具掉落功能：掉落金幣、技能道具
    /// </summary>
     void DropItem()
    {
        if (isDeath == true)
        {
            aniCoin = true;
            aniSkill = true;

            int coinNumber = Random.Range(10, 100) * 10;    // 獲得金幣數量
            int skillNumber = Random.Range(10, 100) * 10;   // 獲得技能點數
            SaveManager.instance.SaveData();

            PlayerCtrl.instance.coinInfo.text = "+" + coinNumber + "金幣";
            PlayerCtrl.instance.skillInfo.text = "獲得" + skillNumber + "點";

            SaveManager.instance.playerData.moneyCount = coinNumber;
            SaveManager.instance.playerData.skillPoint = skillNumber;
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
            if (randomDrop <= 5)
            {
                Instantiate(itemNorm, point.position, point.rotation);
            }
            // Debug.Log("隨機號：" + randomDrop);
        }
    }

    // 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (hpMonster <= 0)
            {
                ani.SetTrigger("damage");
                isDeath = true;
                Invoke("Dead", 1f);
            }
        }
    }

    // 碰撞事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCtrl.instance.hp -= atkMonster;
        }
    }

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

    /*public int skill
    {
        get { return _skill; }
        set
        {
            _skill = value;
            PlayerCtrl.instance.skillCount.text = "× " + value;
        }
    }
    int _skill = 0;
    */
}
