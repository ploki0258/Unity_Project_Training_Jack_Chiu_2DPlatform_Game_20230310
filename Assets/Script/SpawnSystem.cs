using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[Header("生成間隔"), Range(0, 10)]
	public float interval = 0f;
	[Header("怪物預製物")]
	[SerializeField] GameObject[] prefabEnemy = null;
	[Header("道具預製物")]
	[SerializeField] GameObject[] prefabItem = null;
	// 繪製圖形
	[Header("生成點範圍X")]
	[SerializeField] float range_X;
	[Header("生成點範圍Y")]
	[SerializeField] float range_Y;
	[Header("圓形X軸位置")]
	[SerializeField] float posX;
	[Header("圓形Y軸位置")]
	[SerializeField] float posY;
	[Header("矩形中心點位移")]
	[SerializeField] Vector2 offset;
	// 生成範圍
	[Header("生成範圍X軸最小值")]
	[SerializeField] float min_X;
	[Header("生成範圍X軸最大值")]
	[SerializeField] float max_X;
	[Header("生成範圍Y軸最小值")]
	[SerializeField] float min_Y;
	[Header("生成範圍Y軸最大值")]
	[SerializeField] float max_Y;
	[Header("與玩家的距離")]
	[SerializeField] float playerDis;
	[Header("生成類別")]
	[SerializeField] bool isItem = false;

	private Vector2 monsterRange;   // 怪物生成範圍
	private Vector2 itemRange;      // 道具生成範圍
	int enemyCount = 0;             // 計算敵人個數
	Transform player;

	private void Awake()
	{
		player = GameObject.Find("玩家").transform;
		enemyCount = 0;
	}

	private void Start()
	{
		if (isItem)
		{
			InvokeRepeating("SpawnItem", 0, interval);
		}
		else
		{
			// 延遲重複呼叫
			InvokeRepeating("SpawnEnemy", 0, interval);
		}

		// float dis = Vector2.Distance(player.position, transform.position);

		/*if (dis < playerDis)
		{
			
		}
		else
		{
			return;
		}
		*/
	}

	// 繪製圖示
	private void OnDrawGizmos()
	{
		// 圖示.顏色 = 紅色
		Gizmos.color = Color.red;
		// 繪製(框線)矩形
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
		float random_X = Random.Range(this.transform.position.x + min_X, this.transform.position.x + max_X);
		float random_Y = Random.Range(this.transform.position.y + min_Y, this.transform.position.y + max_Y);
		monsterRange = new Vector3(random_X, random_Y);

		if (enemyCount < 3)
		{
			int randomEnemy = Random.Range(0, prefabItem.Length);
			// 於生成範圍內生成怪物
			Instantiate(prefabEnemy[randomEnemy], monsterRange, Quaternion.identity);
		}
		// 每生成一隻怪物 計數器就+1
		enemyCount++;
	}

	/// <summary>
	/// 生成怪物道具
	/// </summary>
	void SpawnItem()
	{
		// 隨機X軸 Y軸的值
		float random_X = Random.Range(this.transform.position.x + min_X, this.transform.position.x + max_X);
		float random_Y = Random.Range(this.transform.position.y + min_Y, this.transform.position.y + max_Y);
		itemRange = new Vector3(random_X, random_Y);

		int randomItem = Random.Range(0, prefabItem.Length);
		// 於生成範圍內隨機生成道具
		Instantiate(prefabItem[randomItem], itemRange, Quaternion.identity);
	}
}
