using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("�ͦ����j"), Range(0, 10)]
    [SerializeField] float interval = 0f;
    [Header("�Ǫ��w�s��")]
    [SerializeField] GameObject prefabEnemy = null;
    //[Header("�̤j�ͦ��I�d��")]
    //[SerializeField] float rangeMax;
    //[Header("�̤p�ͦ��I�d��")]
    //[SerializeField] float rangeMin;
    [Header("�Ǫ��ͦ��d��")]
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
