using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("傷害量")]
    [SerializeField] float hurt = 1f;

    Collider2D tempTarget = null;   // 暫存2D碰撞器
    bool killPlayer = false;        // 是否扣玩家生命值

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tempTarget = collision;
        killPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tempTarget = null;
        killPlayer = false;
    }

    private void Update()
    {
        if (killPlayer == true && tempTarget != null)
        {
            if (tempTarget.gameObject.tag == "Player")
            {
                PlayerCtrl.instance.TakeDamage(hurt * Time.deltaTime);
            }
        }
    }
}
