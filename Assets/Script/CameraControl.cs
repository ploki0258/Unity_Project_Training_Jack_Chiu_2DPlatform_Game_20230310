using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("追蹤目標")]
    [SerializeField] Transform target;
    [Header("追蹤速度"), Range(0, 100)]
    [SerializeField] float speed = 1.5f;
    [Header("攝影機限制上下範圍")]
    [SerializeField] Vector2 limitUD = new Vector2(-1.7f, 1.7f);
    [Header("攝影機限制左右範圍")]
    [SerializeField] Vector2 limitLR = new Vector2(-1.19f, 1.19f);

    private void LateUpdate()
    {
        Track();
    }

    /// <summary>
    /// 物件追蹤
    /// </summary>
    void Track()
    {
        Vector3 posA = transform.position;                      // 取得攝影機座標
        Vector3 posB = target.position;                         // 取得目標座標

        posB.z = -10;
        posB.y = Mathf.Clamp(posB.y, limitUD.x, limitUD.y);     // 將 Y 軸夾在限制範圍內
        posB.x = Mathf.Clamp(posB.x, limitLR.x, limitLR.y);     // 將 X 軸夾在限制範圍內

        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);

        transform.position = posA;
    }
}
