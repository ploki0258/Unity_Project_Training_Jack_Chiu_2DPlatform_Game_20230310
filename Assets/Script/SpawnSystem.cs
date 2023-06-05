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
    [Header("檢查地面位移")]
    [SerializeField] Vector2 offset;
    [Header("怪物生成範圍")]
    private Vector3 monsterRange;

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
    }

    /// <summary>
    /// 生成怪物
    /// </summary>
    void SpawnEnemy()
    {
        float rangeX = Random.Range(range_X, range_Y);
        float rangeY = Random.Range(range_X, range_Y);
        monsterRange = new Vector3(rangeX, rangeY);
        // 於生成範圍內生成怪物
        Instantiate(prefabEnemy, monsterRange, Quaternion.identity);
    }
}
