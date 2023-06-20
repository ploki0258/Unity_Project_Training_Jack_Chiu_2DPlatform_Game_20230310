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
    [Header("怪物移動速度"), Range(0, 30)]
    [SerializeField] float speedMonster = 5f;
    [Header("追蹤距離")]
    [SerializeField] float disChase = 8f;
    [Header("掉落(技能)道具")]
    [SerializeField] GameObject itemSkill = null;
    [Header("掉落機率")]
    public float probDrop = 5f;
    [Header("被傷害值")]
    [SerializeField] float damage = 5f;
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
    // [Header("最大血量")]
    // [SerializeField] float maxHP = 200f;

    int coinNumber;
    int skillNumber;
    bool aniCoin = false;       // 播放金幣動畫
    bool aniSkill = false;      // 播放技能點數動畫
    bool isDeath = false;       // 是否死亡
    Transform player = null;
    Rigidbody2D rig = null;
    Animator ani = null;
    Grid itemSkillValue;
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
        
        // coinInfo.text = "";
        // skill = 0;
    }

    private void OnDisable()
    {
       
    }

    private void Update()
    {
        Move();
    }

    // 
    

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    private void Move()
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
    /// 怪物受到傷害
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
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 道具掉落功能：掉落金幣、技能道具
    /// </summary>
    void DropItem()
    {
        if (isDeath == true)
        {
            int coinNumber = Random.Range(10, 100) * 10;    // 獲得隨機金幣數量
            int skillNumber = Random.Range(1, 10) * 10;     // 獲得隨機技能點數

            // 顯示資訊欄文字
            PlayerCtrl.instance.coinInfo.text = "+" + coinNumber + "金幣";
            PlayerCtrl.instance.skillInfo.text = "獲得" + skillNumber + "點";

            SaveManager.instance.playerData.moneyCount += coinNumber;
            SaveManager.instance.playerData.skillPoint += skillNumber;
            SaveManager.instance.playerData.skillPoint += itemSkillValue.獲得額外點數;
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

    // 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeath)
            return;
        if (collision.gameObject.tag == "bullet")
        {
            TakeDamageMonster(damage);

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
        // 如果碰到玩家 就依據怪物攻擊力扣玩家血量
        if (collision.gameObject.tag == "Player")
        {
            PlayerCtrl.instance.TakeDamage(atkMonster);
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
