using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("�l�ܥؼ�")]
    [SerializeField] Transform target;
    [Header("�l�ܳt��"), Range(0, 100)]
    [SerializeField] float speed = 1.5f;
    [Header("��v������W�U�d��")]
    [SerializeField] Vector2 limitUD = new Vector2(-1.7f, 1.7f);
    [Header("��v������k�d��")]
    [SerializeField] Vector2 limitLR = new Vector2(-1.19f, 1.19f);

    private void LateUpdate()
    {
        Track();
    }

    /// <summary>
    /// ����l��
    /// </summary>
    void Track()
    {
        Vector3 posA = transform.position;                      // ���o��v���y��
        Vector3 posB = target.position;                         // ���o�ؼЮy��

        posB.z = -10;
        posB.y = Mathf.Clamp(posB.y, limitUD.x, limitUD.y);     // �N Y �b���b����d��
        posB.x = Mathf.Clamp(posB.x, limitLR.x, limitLR.y);     // �N X �b���b����d��

        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);

        transform.position = posA;
    }
}
