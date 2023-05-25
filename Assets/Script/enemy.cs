using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("怪物血量")]
    [SerializeField] float hpMonster = 100f;
    [Header("怪物攻擊力")]
    [SerializeField] float atkMonster = 5f;
    [Header("生成點")]
    [SerializeField] Transform point = null;
    [Header("怪物移動速度")]
    [SerializeField] float speedMonster = 5f;
    [Header("基準距離")]
    [SerializeField] float disBase = 8f;
    [Header("掉落金幣")]
    [SerializeField] int coin = 0;
    [Header("掉落道具")]
    [SerializeField] GameObject itemNorm = null;

    [SerializeField] Image barHP = null;
    // [SerializeField] float maxHP = 100f;

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
        hp = hpMonster;
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
        hp -= hurt;
    }

    void Dead()
    {
        if(hp <= 1)
        {
            enabled = false;
            ani.SetTrigger("damage");
            Instantiate(itemNorm, point.position, point.rotation);
            // Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCtrl.instance.hp -= atkMonster;
        }
    }

    public float hp
    {
        get { return hpMonster * barHP.fillAmount; }
        set
        {
            barHP.fillAmount = value / hpMonster;
        }
    }
}
