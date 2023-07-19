using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MistManager : MonoBehaviour
{
	[SerializeField, Header("迷霧圖片")]
	SpriteRenderer mistImage = null;
	[SerializeField, Header("漸變時間"), Range(0, 50)]
	float timeGradient = 1f;
	[SerializeField, Header("傷害量"), Range(0, 100)]
	float damage = 0f;
	[SerializeField, Header("傷害間隔"), Range(0, 10)]
	float damageInterval = 1f;
	[SerializeField, Header("CD加重倍數"), Range(0, 10)]
	float cdWeight = 0f;
	[SerializeField, Header("降低移動速度倍數"), Range(0, 50)]
	float speedWeight = 1f;
	[SerializeField, Header("最低移動速度"), Range(0, 5)]
	float minSpeed = 1f;
	[SerializeField, Header("降低攻擊速度倍數"), Range(10, 100)]
	float attackSpeedWeight = 100f;
	[SerializeField, Header("最低攻擊速度"), Range(0, 100)]
	float minAttackSpeed = 10f;
	[SerializeField, Header("生成提升倍數"), Range(0, 20)]
	float spawnWeight = 0f;
	[Header("迷霧種類")]
	[SerializeField] bool mistType_cyan;    // 青色
	[SerializeField] bool mistType_blue;    // 藍色
	[SerializeField] bool mistType_purple;  // 紫色
	[SerializeField] bool mistType_red;     // 紅色
	[SerializeField] bool mistType_gree;    // 綠色
	[SerializeField, Header("改變的顏色")]
	Color colorChange;
	[SerializeField, Header("尋找物件名稱")]
	string objectName;

	public bool inMist_gree = false;
	public bool inMist_cyan = false;

	[Tooltip("變化後的顏色")]
	private Color tempColor;            // 漸變顏色
	[Tooltip("是否在進行漸變 ")]
	private bool transitioning = false; // 是否在漸變
	private bool startDamage = false;   // 是否開始傷害
	private float originalCostMP;       // 原來的MP消耗值
	private float originalSpeed;        // 原來的移動速度
	private float originalAttackSpeed;  // 原來的攻擊速度
	private float originalSpawn;        // 原來的生成數量
	SpawnSystem spawnSystem;

	public static MistManager instance; // 單例

	private void Awake()
	{
		instance = this;
		spawnSystem = GameObject.Find(objectName).GetComponent<SpawnSystem>();
	}

	private void Start()
	{
		if (mistType_blue == true)
			cdWeight = PlayerCtrl.instance.costMP * cdWeight;
		if (mistType_cyan == true)
			spawnWeight = spawnSystem.enemyCountMax * spawnWeight;

		originalCostMP = PlayerCtrl.instance.costMP;
		originalSpeed = SaveManager.instance.playerData.playerSpeed;
		originalAttackSpeed = SaveManager.instance.playerData.playerAttackSpeed;
		originalSpawn = (float)spawnSystem.enemyCountMax;
	}

	private void Update()
	{
		// Debug.Log(PlayerCtrl.instance.costMP);
		// Debug.Log(tempColor);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// 呼叫協程 ColorGradient()
		StartCoroutine(ColorGradient());
		StartCoroutine(TakeDamage());

		// 青色迷霧：提升怪物數量
		if (collision.gameObject.CompareTag("Player") && mistType_cyan == true)
		{
			// inMist = true;

			if (transitioning)
			{
				ColorTransition(Color.white, new Color(00, 10, 90));
			}
			// mistImage.color = Color.Lerp(Color.white, new Color(00, 10, 90), 100f * Time.deltaTime);
			tempColor = mistImage.color;

			// 提升怪物數量
			spawnSystem.enemyCountMax = (int)spawnWeight;
		}

		// 藍色迷霧：加重技能消耗費用
		if (collision.gameObject.CompareTag("Player") && mistType_blue == true)
		{
			// inMist = true;

			if (transitioning)
			{
				ColorTransition(Color.white, new Color(00, 00, 99));
			}
			// mistImage.color = Color.Lerp(Color.white, new Color(00, 00, 99), Mathf.PingPong(Time.unscaledTime, 2f));
			// tempColor = mistImage.color;

			// 加重技能消耗費用
			PlayerCtrl.instance.costMP = instance.cdWeight;
		}

		// 紫色迷霧：減速
		if (collision.gameObject.CompareTag("Player") && mistType_purple == true)
		{
			// inMist = true;

			if (transitioning)
			{
				ColorTransition(Color.white, new Color(60, 00, 90));
			}
			// mistImage.color = Color.Lerp(Color.white, new Color(60, 00, 90), 1000f * Time.deltaTime);
			tempColor = mistImage.color;

			// 降低玩家移動速度
			SaveManager.instance.playerData.playerSpeed -= speedWeight * Time.unscaledDeltaTime;
			SaveManager.instance.playerData.playerSpeed =
				Mathf.Clamp(SaveManager.instance.playerData.playerSpeed, minSpeed, SaveManager.instance.playerData.playerSpeed);

			// 降低玩家攻擊速度
			SaveManager.instance.playerData.playerAttackSpeed -= attackSpeedWeight * Time.unscaledDeltaTime;
			SaveManager.instance.playerData.playerAttackSpeed =
				Mathf.Clamp(SaveManager.instance.playerData.playerAttackSpeed, minAttackSpeed, SaveManager.instance.playerData.playerAttackSpeed);

			// 速度影響動畫
			PlayerCtrl.instance.ani.speed = 1f / SaveManager.instance.playerData.playerSpeed;
			// Debug.Log("移動速度：" + SaveManager.instance.playerData.playerSpeed);
			// Debug.Log("動畫速度：" + PlayerCtrl.instance.ani.speed);
		}

		// 紅色迷霧：減少HP
		if (collision.gameObject.CompareTag("Player") && mistType_red == true)
		{
			// inMist = true;

			// 如果進行漸變的話 則漸變迷霧的顏色
			if (transitioning)
			{
				ColorTransition(Color.white, new Color(99, 00, 00));
			}
			// mistImage.color = Color.Lerp(Color.white, new Color(99, 00, 00), 100f * Time.deltaTime);
			tempColor = mistImage.color;

			// 減少玩家HP
			if (startDamage)
			{
				Invoke("TakeDamage", 5f);
				SaveManager.instance.playerData.playerHP -= damage * Time.unscaledDeltaTime;
			}
			Debug.Log(SaveManager.instance.playerData.playerHP);
		}

		// 綠色迷霧：道具回復效果相反
		if (collision.gameObject.CompareTag("Player") && mistType_gree == true)
		{
			inMist_gree = true;

			if (transitioning)
			{
				ColorTransition(Color.white, new Color(00, 99, 00));
			}
			// mistImage.color = Color.Lerp(Color.white, new Color(00, 99, 00), 100f * Time.deltaTime);
			tempColor = mistImage.color;
		}

		// Test
		if (collision.gameObject.CompareTag("Player") && colorChange != Color.white && mistType_cyan == false && mistType_blue == false &&
			mistType_purple == false && mistType_red == false && mistType_gree == false)
		{
			mistImage.color = Color.Lerp(Color.white, colorChange, 100f * Time.deltaTime);
		}
	}

	/// <summary>
	/// 離開事件
	/// </summary>
	/// <param name="collision">碰到的物件</param>
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			inMist_gree = false;

			PlayerCtrl.instance.costMP = originalCostMP;								// 變回原本魔力消耗
			SaveManager.instance.playerData.playerSpeed = originalSpeed;                // 變回原本移動速度
			SaveManager.instance.playerData.playerAttackSpeed = originalAttackSpeed;    // 變回原本攻擊速度
			PlayerCtrl.instance.ani.speed = 1f;                                         // 變回原本動畫播放
			spawnSystem.enemyCountMax = (int)originalSpawn;                             // 變回原本最大敵人生成數量

			if (transitioning == true)
			{
				ColorTransition(tempColor, Color.white);
				// Debug.Log("從" + tempColor.ToString() + "漸變成" + Color.white.ToString());
			}
			// mistImage.color = Color.Lerp(tempColor, Color.white, 100f * Time.deltaTime);
		}
	}

	/// <summary>
	/// 顏色漸變：改變迷霧顏色
	/// </summary>
	/// <param name="originalColor">原始顏色</param>
	/// <param name="changeColor">變化顏色</param>
	private void ColorTransition(Color originalColor, Color changeColor)
	{
		float time = Mathf.Clamp01(Time.time / timeGradient);
		// SpriteRenderer tempImage = Instantiate(mistImage, transform.position, transform.rotation);
		mistImage.color = Color.Lerp(originalColor, changeColor, time);
		tempColor = mistImage.color;

		// 檢查漸變是否完成
		if (time >= 1f)
			transitioning = false;
	}

	/// <summary>
	/// 漸變顏色協程
	/// </summary>
	/// <returns></returns>
	IEnumerator ColorGradient()
	{
		yield return null;
		// 漸變開始
		transitioning = true;
		// 等待漸變時間
		yield return new WaitForSeconds(timeGradient);
		// 漸變結束
		transitioning = false;
	}

	/// <summary>
	/// 持續扣血協程
	/// </summary>
	/// <returns></returns>
	IEnumerator TakeDamage()
	{
		yield return null;
		startDamage = true;
		yield return new WaitForSeconds(damageInterval);
		startDamage = false;
	}
}
