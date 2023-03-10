using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    [SerializeField] float speed = 10f;

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
    }

    /// <summary>
    /// 角色移動
    /// </summary>
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))                         // 如果 按下右方向鍵
        {
            rig.AddForce(transform.right * speed, ForceMode2D.Force);  // 向右側施加一個推力
            print("向右移動");
        }

        if (Input.GetKey(KeyCode.LeftArrow))                          // 如果 按下左方向鍵
        {
            rig.AddForce(new Vector2(-speed, 0f), ForceMode2D.Force);  // 向左側施加一個推力
            print("向左移動");
        }
    }
}
