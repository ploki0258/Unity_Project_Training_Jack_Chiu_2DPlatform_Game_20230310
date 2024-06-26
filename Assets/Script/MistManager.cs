﻿using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistManager : MonoBehaviour
{
	#region 欄位
	[SerializeField, Header("迷霧圖片")]
	SpriteRenderer mistImage = null;
	[SerializeField, Header("漸變時間"), Range(0, 10)]
	float timeGradient = 1f;
	[Header("紅色迷霧")]
	[SerializeField, Header("傷害量"), Range(0, 100)]
	float damage = 0f;
	[SerializeField, Header("傷害間隔"), Range(0, 10)]
	float damageInterval = 1f;
	[Header("藍色迷霧")]
	[SerializeField, Header("CD加重倍數"), Range(0, 10)]
	float cdWeight = 0f;
	[Header("紫色迷霧")]
	[SerializeField, Header("降低移動速度倍數"), Range(0, 50)]
	float speedWeight = 1f;
	[SerializeField, Header("最低移動速度"), Range(0, 5)]
	float minSpeed = 1f;
	[SerializeField, Header("降低攻擊速度倍數"), Range(10, 100)]
	float attackSpeedWeight = 100f;
	[SerializeField, Header("最低攻擊速度"), Range(0, 100)]
	float minAttackSpeed = 10f;
	[Header("青色迷霧")]
	[SerializeField, Header("生成提升倍數"), Range(0, 20)]
	float spawnWeight = 0f;
	[SerializeField, Header("迷霧種類")] MistColorType mistType = MistColorType.None;
	//[SerializeField, Header("改變的顏色")] Color colorChange;
	[SerializeField, Header("尋找物件名稱")] string objectName;
	[SerializeField, Header("繪製矩形\n繪製中心點")] Vector2 center = Vector2.zero;
	[SerializeField, Header("繪製大小")] Vector2 size = Vector2.one;

	public bool inMist_gree = false;
	public bool inMist_cyan = false;

	[Tooltip("改變的顏色")]
	Color changeColor;
	[Tooltip("變化後的顏色")]
	public Color tempColor;            // 漸變顏色
	[Tooltip("是否在進行漸變 ")]
	private bool transitioning = false; // 是否在漸變
	private bool inMist = false;
	private float time = 0;
	private bool startDamage = false;   // 是否開始傷害
	private float originalCostMP;       // 原來的MP消耗值
	private float originalSpeed;        // 原來的移動速度
	private float originalAttackSpeed;  // 原來的攻擊速度
	private float originalSpawn;        // 原來的生成數量
	private BoxCollider2D boxCollider2D;
	#endregion

	public static MistManager instance; // 單例

	private void Awake()
	{
		instance = this;

		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		if (mistType == MistColorType.Blue)
			cdWeight *= SaveManager.instance.playerData.costMP;
		if (mistType == MistColorType.Cyan)
			spawnWeight *= SpawnSystem.instance.enemyCountMax;

		originalCostMP = SaveManager.instance.playerData.costMP;
		originalSpeed = SaveManager.instance.playerData.playerSpeed;
		originalAttackSpeed = SaveManager.instance.playerData.playerAttackSpeed;
		originalSpawn = (float)SpawnSystem.instance.enemyCountMax;
		tempColor = Color.white;
		center = (Vector2)boxCollider2D.transform.position;

		switch (mistType)
		{
			case MistColorType.None:
				break;
			case MistColorType.Cyan:
				changeColor = new Color(0f, 0.9f, 0.7f);
				break;
			case MistColorType.Blue:
				changeColor = new Color(0f, 0f, 0.99f);
				break;
			case MistColorType.Purple:
				changeColor = new Color(0.6f, 0f, 0.9f);
				break;
			case MistColorType.Red:
				changeColor = new Color(0.99f, 0f, 0f);
				break;
			case MistColorType.Green:
				changeColor = new Color(0f, 0.99f, 0f);
				break;
		}
	}

	private void Update()
	{
		boxCollider2D.transform.position = center;

		if (transitioning == true && inMist == true)
		{
			ColorTransition(Color.white, changeColor);
		}

		if (transitioning == false && inMist == true)
		{
			ColorTransition(tempColor, Color.white);
		}

		/*// Test
		if (colorChange != Color.white && mistType == MistColorType.None)
		{
			time += Time.deltaTime;
			float t = time * (1f / timeGradient);
			// t值愈小 漸變時間愈慢
			mistImage.color = Color.Lerp(Color.white, colorChange, t);
			Debug.Log("t值:" + t);
		}*/
	}

	/// <summary>
	/// 進入事件
	/// </summary>
	/// <param name="collision">碰到的物件</param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 停止協程
		StopAllCoroutines();
		time = 0;

		#region 各類迷霧效果
		#region 青色迷霧：提升怪物數量
		// 青色迷霧：提升怪物數量
		if (collision.gameObject.CompareTag("Player") && mistType == MistColorType.Cyan)
		{
			transitioning = true;
			inMist = true;
			inMist_cyan = true;
			changeColor = new Color(0f, 0.9f, 0.7f);
			//StartCoroutine(ColorGradient(Color.white, new Color(0f, 0.1f, 0.9f)));

			// 提升怪物數量
			if (GameObject.FindObjectOfType<Enemy>())
			{
				SpawnSystem.instance.enemyCountMax = (int)spawnWeight;
				SpawnSystem.instance.RestartSpawn();
			}
		}
		#endregion

		#region 藍色迷霧：加重技能消耗費用
		// 藍色迷霧：加重技能消耗費用
		if (collision.gameObject.CompareTag("Player") && mistType == MistColorType.Blue)
		{
			transitioning = true;
			inMist = true;
			changeColor = new Color(0f, 0f, 0.99f);
			//StartCoroutine(ColorGradient(Color.white, new Color(0f, 0f, 0.99f)));

			//if (transitioning)
			//{
			//	ColorTransition(Color.white, new Color(0f, 0f, 0.99f));
			//}
			// mistImage.color = Color.Lerp(Color.white, new Color(00, 00, 99), Mathf.PingPong(Time.unscaledTime, 2f));
			// tempColor = mistImage.color;

			// 加重技能消耗費用
			SaveManager.instance.playerData.costMP = instance.cdWeight;
		}
		#endregion

		#region 紫色迷霧：減速
		// 紫色迷霧：減速
		if (collision.gameObject.CompareTag("Player") && mistType == MistColorType.Purple)
		{
			transitioning = true;
			inMist = true;
			changeColor = new Color(0.6f, 0f, 0.9f);
			//StartCoroutine(ColorGradient(Color.white, new Color(0.6f, 0f, 0.9f)));

			//if (transitioning)
			//{
			//	ColorTransition(Color.white, new Color(0.6f, 0f, 0.9f));
			//}
			// mistImage.color = Color.Lerp(Color.white, new Color(60, 00, 90), 1000f * Time.deltaTime);
			//tempColor = mistImage.color;

			// 降低玩家移動速度
			SaveManager.instance.playerData.playerSpeed -= speedWeight * Time.unscaledDeltaTime;
			SaveManager.instance.playerData.playerSpeed =
				Mathf.Clamp(SaveManager.instance.playerData.playerSpeed, minSpeed, SaveManager.instance.playerData.playerSpeed);

			// 降低玩家攻擊速度
			SaveManager.instance.playerData.playerAttackSpeed -= attackSpeedWeight * Time.unscaledDeltaTime;
			SaveManager.instance.playerData.playerAttackSpeed =
				Mathf.Clamp(SaveManager.instance.playerData.playerAttackSpeed, minAttackSpeed, SaveManager.instance.playerData.playerAttackSpeed);

			// 影響動畫播放速度
			PlayerCtrl.instance.ani.speed = 1f / SaveManager.instance.playerData.playerSpeed;
			// Debug.Log("移動速度：" + SaveManager.instance.playerData.playerSpeed);
			// Debug.Log("動畫速度：" + PlayerCtrl.instance.ani.speed);
		}
		#endregion

		#region 紅色迷霧：減少HP
		// 紅色迷霧：減少HP
		if (collision.gameObject.CompareTag("Player") && mistType == MistColorType.Red)
		{
			transitioning = true;
			inMist = true;
			changeColor = new Color(0.99f, 0f, 0f);
			//StartCoroutine(ColorGradient(Color.white, new Color(0.99f, 0f, 0f)));

			// 如果進行漸變的話 則漸變迷霧的顏色
			//if (transitioning)
			//{
			//	ColorTransition(Color.white, new Color(0.99f, 0f, 0f));
			//}
			// mistImage.color = Color.Lerp(Color.white, new Color(99, 00, 00), 100f * Time.deltaTime);
			//tempColor = mistImage.color;

			// 呼叫協程 ColorGradient()
			StartCoroutine(TakeDamage());

			// 減少玩家HP
			if (startDamage)
			{
				Invoke("TakeDamage", 5f);
				SaveManager.instance.playerData.playerHP -= damage * Time.unscaledDeltaTime;
			}
			// Debug.Log(SaveManager.instance.playerData.playerHP);
		}
		#endregion

		#region 綠色迷霧
		// 綠色迷霧：道具回復效果相反 or 怪物的道具掉落率為0，地圖掉落率降低
		if (collision.gameObject.CompareTag("Player") && mistType == MistColorType.Green)
		{
			transitioning = true;
			inMist = true;
			changeColor = new Color(0f, 0.99f, 0f);
			//StartCoroutine(ColorGradient(Color.white, new Color(0f, 0.99f, 0f)));

			inMist_gree = true;

			//if (transitioning)
			//{
			//	ColorTransition(Color.white, new Color(0f, 0.99f, 0f));
			//}
			// mistImage.color = Color.Lerp(Color.white, new Color(00, 99, 00), 100f * Time.deltaTime);
			//tempColor = mistImage.color;
		}
		#endregion
		#endregion

		/*// Test
		if (collision.gameObject.CompareTag("Player") && colorChange != Color.white && mistType == MistColorType.None)
		{
			Debug.Log("進入區域");
			float t = 1 / timeGradient;
			float time = Mathf.PingPong(Time.time * t, 1);
			mistImage.color = Color.Lerp(Color.white, colorChange, time);
			Debug.Log("t值:" + time);
		}*/
	}

	/// <summary>
	/// 離開事件
	/// </summary>
	/// <param name="collision">碰到的物件</param>
	private void OnTriggerExit2D(Collider2D collision)
	{
		transitioning = false;
		inMist_gree = false;
		inMist_cyan = false;
		time = 0;

		//ColorTransition(tempColor, Color.black);

		if (collision.gameObject.CompareTag("Player"))
		{
			SaveManager.instance.playerData.costMP = originalCostMP;                    // 變回原本魔力消耗
			SaveManager.instance.playerData.playerSpeed = originalSpeed;                // 變回原本移動速度
			SaveManager.instance.playerData.playerAttackSpeed = originalAttackSpeed;    // 變回原本攻擊速度
			PlayerCtrl.instance.ani.speed = 1f;                                         // 變回原本動畫播放
			SpawnSystem.instance.enemyCountMax = (int)originalSpawn;                    // 變回原本最大敵人生成數量

			/* 原本
			if (transitioning == false)
			{
				ColorTransition(tempColor, Color.white);
				// Debug.Log("從" + tempColor.ToString() + "漸變成" + Color.white.ToString());
			}
			tempColor = mistImage.color;*/
		}
	}

	/// <summary>
	/// 顏色漸變：改變迷霧顏色
	/// </summary>
	/// <param name="originalColor">原始顏色</param>
	/// <param name="newColor">變化顏色</param>
	private void ColorTransition(Color originalColor, Color newColor)
	{
		//float time = Mathf.Clamp01(Time.time / timeGradient);
		// 迷霧.顏色 = 顏色.漸變(原始顏色, 變化後顏色)
		time += Time.deltaTime;
		float t = time * (1f / timeGradient);
		mistImage.color = Color.Lerp(originalColor, newColor, t);
		tempColor = mistImage.color;

		// 檢查漸變是否完成
		if (time > timeGradient)
			time = timeGradient;
	}

	/// <summary>
	/// 漸變顏色協程
	/// </summary>
	/// <returns></returns>
	IEnumerator ColorGradient(Color originalColor, Color newColor)
	{
		float time = Mathf.Clamp01(Time.time / timeGradient);

		if (transitioning)
		{
			for (int i = 0; i < timeGradient; i++)
			{
				mistImage.color = originalColor + (newColor);
				yield return new WaitForSeconds(timeGradient);
			}
		}

		// 檢查漸變是否完成
		if (time >= timeGradient)
			transitioning = false;
		
		#region 不同寫法
		/*
		while (this.time < timeGradient)
		{
			mistImage.color = Color.Lerp(originalColor, newColor, this.time / timeGradient);
			this.time += Time.deltaTime;
			yield return null;
		}*/
		#endregion

		#region 原本寫法
		/*yield return null;
		// 漸變開始
		transitioning = true;

		for (int i = 0; i < timeGradient; i++)
		{
			ColorTransition(originalColor, changeColor);
		}

		// 等待漸變時間
		yield return new WaitForSeconds(timeGradient);
		// 漸變結束
		transitioning = false;*/
		#endregion
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

	#region 繪製圖形
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 0.5f, 0.5f);
		Gizmos.DrawCube(center, size);

		//Gizmos.DrawIcon(center, "Light Gizmo.tiff", true);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.1f, 0.1f, 0.8f, 0.5f);
		Gizmos.DrawCube(center, size);
	}
	#endregion

	/// <summary>
	/// 迷霧種類
	/// </summary>
	public enum MistColorType
	{
		None = 0,
		Cyan = 1,   // 青色
		Blue = 2,   // 藍色
		Purple = 3, // 紫色
		Red = 4,    // 紅色
		Green = 5,  // 綠色
	}
}