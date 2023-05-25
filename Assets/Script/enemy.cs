using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("怪物血量")]
    [SerializeField] float hpMonsterMax = 100f;
    [Header("怪物攻擊力")]
    [SerializeField] float atkMonster = 5f;
    [Header("生成點")]
    [SerializeField] Transform point = null;
    [Header("怪物移動速度")]
    [SerializeField] float speedMonster = 5f;
    [Header("基準距離")]
    [SerializeField] float disBase = 8f;
    [Header("掉落金幣數量")]
    public int coinNumber = 0;
    [Header("資訊欄")]
    [SerializeField] Text coinInfo = null;
    [Header("金幣數量")]
    [SerializeField] TextMeshProUGUI coinCount = null;
    [Header("掉落道具")]
    [SerializeField] GameObject itemNorm = null;
    [SerializeField] Animator 金幣顯示動畫 = null;
    [SerializeField] Image barHP = null;
    // [SerializeField] float maxHP = 100f;

    bool aniCoin = false;
    Transform player = null;
    Rigidbody2D rig = null;
    Animator ani = null;

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
        coinInfo.text = "";
    }

    private void Update()
    {
        // Move();
        Dead();
    }

    void Move()
    {
        if (player.position.x > this.transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        float dis = Vector3.Distance(player.position, transform.position);

        if (dis < disBase)
        {
            rig.velocity = transform.right * speedMonster;
            rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y);
        }
    }

    public void TakeDamageMonster(float hurt)
    {
        hpMonster -= hurt;
    }

    void Dead()
    {
        if (hpMonster < 1)
        {
            enabled = false;
            ani.SetTrigger("damage");
            Instantiate(itemNorm, point.position, point.rotation);
            aniCoin = true;
            int coinNumber = Random.Range(100, 1000);
            coinInfo.text = "+" + coinNumber + "金幣";
            
            if (aniCoin == true)
                金幣顯示動畫.SetTrigger("play");
            coinCount.text = "× " + coinNumber;
            Destroy(this.gameObject);
            // Debug.Log(coinNumber);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCtrl.instance.hp -= atkMonster;
        }
    }

    public float hpMonster
    {
        get { return hpMonsterMax * barHP.fillAmount; }
        set
        {
            barHP.fillAmount = value / hpMonsterMax;
        }
    }
}
