using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("生成間隔"), Range(0, 10)]
    [SerializeField] float interval = 0f;
    [Header("怪物預製物")]
    [SerializeField] GameObject prefabEnemy = null;
    [Header("生成點範圍X")]
    [SerializeField] float range_X;
    [Header("生成點範圍Y")]
    [SerializeField] float range_Y;
    [Header("X軸位置")]
    [SerializeField] float posX;
    [Header("Y軸位置")]
    [SerializeField] float posY;
    [Header("檢查地面位移")]
    [SerializeField] Vector2 offset;
    [Header("X軸最小值")]
    [SerializeField] float minX;
    [Header("X軸最大值")]
    [SerializeField] float maxX;
    [Header("Y軸最小值")]
    [SerializeField] float minY;
    [Header("Y軸最大值")]
    [SerializeField] float maxY;
    
    [Header("怪物生成範圍")]
    private Vector2 monsterRange;

    private void Start()
    {
        // 延遲重複呼叫
        InvokeRepeating("SpawnEnemy", 0, interval);
    }

    // 繪製圖示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // 繪製矩形
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + offset, new Vector3(range_X, range_Y, 0f));
        
        Gizmos.color = new Color(00, 10, 99);
        // 繪製圓形
        Gizmos.DrawSphere((new Vector2(transform.position.x, transform.position.y)) + new Vector2(posX, posY), 0.3f);
    }

    /// <summary>
    /// 生成怪物
    /// </summary>
    void SpawnEnemy()
    {
        // 隨機X軸 Y軸的值
        float rangeX = Random.Range(this.transform.position.x + minX, this.transform.position.x + maxX);
        float rangeY = Random.Range(this.transform.position.y + minY, this.transform.position.y + maxY);
        monsterRange = new Vector3(rangeX, rangeY);
        // 於生成範圍內生成怪物
        Instantiate(prefabEnemy, monsterRange, Quaternion.identity);
    }
}
