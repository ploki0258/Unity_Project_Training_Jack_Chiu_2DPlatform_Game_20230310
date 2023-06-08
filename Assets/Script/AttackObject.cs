using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [Header("傷害值")]
    [SerializeField] float damage = 5f;

    bool isWandAttack = false;  // 物理攻擊(MP <= 0)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果 子彈 碰到 地板 或 敵人 就消失
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "enemy")
        {
            // 如果 不在物理攻擊(MP <= 0)時 消失自己
            if (isWandAttack == false)
            {
                Destroy(this.gameObject);
            }
        }
        // 如果 子彈 碰到 敵人 就給予傷害
        if (collision.gameObject.tag == "enemy")
        {
            // Enemy.instance.TakeDamageMonster(damage);
        }
    }

    private void Update()
    {
        if (PlayerCtrl.instance.mp > 0)
        {
            isWandAttack = false;
        }
        if(PlayerCtrl.instance.mp <= 0)
        {
            isWandAttack = true;
        }
    }
}
