using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("生成間隔"), Range(0, 10)]
    [SerializeField] float interval = 0f;
    [Header("怪物預製物")]
    [SerializeField] GameObject prefabEnemy = null;
    //[Header("最大生成點範圍")]
    //[SerializeField] float rangeMax;
    //[Header("最小生成點範圍")]
    //[SerializeField] float rangeMin;
    [Header("怪物生成範圍")]
    [SerializeField] Vector2 monsterRange;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, interval);
    }

    void SpawnEnemy()
    {
        /*float rangeX = Random.Range(rangeMin, rangeMax);
        float rangeY = Random.Range(rangeMin, rangeMax);
        Vector2 spawnRange = new Vector2(rangeX, rangeY);
        */
        Instantiate(prefabEnemy, monsterRange, Quaternion.identity);
    }
}
