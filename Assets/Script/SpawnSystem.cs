using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("�ͦ����j"), Range(0, 10)]
    [SerializeField] float interval = 0f;
    [Header("�Ǫ��w�s��")]
    [SerializeField] GameObject prefabEnemy = null;
    [Header("�ͦ��I�d��X")]
    [SerializeField] float range_X;
    [Header("�ͦ��I�d��Y")]
    [SerializeField] float range_Y;
    [Header("�ˬd�a���첾")]
    [SerializeField] Vector2 offset;
    [Header("�Ǫ��ͦ��d��")]
    private Vector3 monsterRange;

    private void Start()
    {
        // ���𭫽ƩI�s
        InvokeRepeating("SpawnEnemy", 0, interval);
    }

    // ø�s�ϥ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // ø�s�x��
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + offset, new Vector3(range_X, range_Y, 0f));
    }

    /// <summary>
    /// �ͦ��Ǫ�
    /// </summary>
    void SpawnEnemy()
    {
        float rangeX = Random.Range(range_X, range_Y);
        float rangeY = Random.Range(range_X, range_Y);
        monsterRange = new Vector3(rangeX, rangeY);
        // ��ͦ��d�򤺥ͦ��Ǫ�
        Instantiate(prefabEnemy, monsterRange, Quaternion.identity);
    }
}
