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
    [Header("X�b��m")]
    [SerializeField] float posX;
    [Header("Y�b��m")]
    [SerializeField] float posY;
    [Header("�ˬd�a���첾")]
    [SerializeField] Vector2 offset;
    [Header("X�b�̤p��")]
    [SerializeField] float minX;
    [Header("X�b�̤j��")]
    [SerializeField] float maxX;
    [Header("Y�b�̤p��")]
    [SerializeField] float minY;
    [Header("Y�b�̤j��")]
    [SerializeField] float maxY;
    
    [Header("�Ǫ��ͦ��d��")]
    private Vector2 monsterRange;

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
        
        Gizmos.color = new Color(00, 10, 99);
        // ø�s���
        Gizmos.DrawSphere((new Vector2(transform.position.x, transform.position.y)) + new Vector2(posX, posY), 0.3f);
    }

    /// <summary>
    /// �ͦ��Ǫ�
    /// </summary>
    void SpawnEnemy()
    {
        // �H��X�b Y�b����
        float rangeX = Random.Range(this.transform.position.x + minX, this.transform.position.x + maxX);
        float rangeY = Random.Range(this.transform.position.y + minY, this.transform.position.y + maxY);
        monsterRange = new Vector3(rangeX, rangeY);
        // ��ͦ��d�򤺥ͦ��Ǫ�
        Instantiate(prefabEnemy, monsterRange, Quaternion.identity);
    }
}
