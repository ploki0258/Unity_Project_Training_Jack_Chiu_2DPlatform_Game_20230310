using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	// 基本欄位
	[Header("生成間隔"), Range(0, 10)]
	public float interval = 0f;
	[Header("生成類別"), Tooltip("是否生成道具")]
	[SerializeField] bool isItem = false;
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
	[Header("最大敵人生成數量"), Tooltip("敵人生成最大數量"), Range(0, 10)]
	public int enemyCountMax;
	[Header("最大道具生成數量"), Tooltip("道具生成最大數量"), Range(0, 10)]
	public int itemCountMax;

	[Tooltip("計算敵人生成數量")]
	public int enemyCount = 0;      // 計算敵人個數
	[Tooltip("計算道具生成數量")]
	public int itemCount = 0;		// 計算道具個數
	private Vector2 monsterRange;	// 怪物生成範圍
	private Vector2 itemRange;      // 道具生成範圍
	Transform player;

	private void Awake()
	{
		// player = GameObject.Find("玩家").transform;
		enemyCount = 0;
	}

	private void Start()
	{
		if (isItem)
		{
			// 延遲重複呼叫-生成技能道具
			InvokeRepeating("SpawnItem", 0, interval);
		}
		else
		{
			// 延遲重複呼叫-生成怪物
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
		// 圖示.顏色 = new Color(00, 10, 99)
		Gizmos.color = new Color(00, 10, 99);
		// 繪製圓形
		Gizmos.DrawSphere((new Vector2(transform.position.x, transform.position.y)) + new Vector2(posX, posY), 0.3f);
	}

	/// <summary>
	/// 生成怪物
	/// </summary>
	public void SpawnEnemy()
	{
		// 隨機X軸 Y軸的值
		float random_X = Random.Range(this.transform.position.x + min_X, this.transform.position.x + max_X);
		float random_Y = Random.Range(this.transform.position.y + min_Y, this.transform.position.y + max_Y);
		monsterRange = new Vector3(random_X, random_Y);

		// 如果 目前生成的怪物數量 小於 最大生成數量的話 則生成怪物 
		if (enemyCount < enemyCountMax)
		{
			int randomEnemy = Random.Range(0, prefabItem.Length);
			// 於生成範圍內生成怪物
			Instantiate(prefabEnemy[randomEnemy], monsterRange, Quaternion.identity);
			// 每生成一隻怪物 計數器就+1
			enemyCount++;
		}
	}

	/// <summary>
	/// 生成技能道具
	/// </summary>
	void SpawnItem()
	{
		// 隨機X軸 Y軸的值
		float random_X = Random.Range(this.transform.position.x + min_X, this.transform.position.x + max_X);
		float random_Y = Random.Range(this.transform.position.y + min_Y, this.transform.position.y + max_Y);
		itemRange = new Vector3(random_X, random_Y);

		if (itemCount < itemCountMax)
		{
			int randomItem = Random.Range(0, prefabItem.Length);
			// 於生成範圍內隨機生成道具
			Instantiate(prefabItem[randomItem], itemRange, Quaternion.identity);
			// 每生成一個道具 計數器就+1
			itemCount++;
		}
		
	}
}
