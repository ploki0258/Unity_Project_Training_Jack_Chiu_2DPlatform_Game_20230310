using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
	[SerializeField, Header("是否前往下一關"), Tooltip("是否要前往下一關卡")]
	private bool isToNext = true;
	[SerializeField, Header("要前往的關卡編號")]
	int indexLevel = 0;


	[Tooltip("是否在區域內")]
	private bool inArea;
	Animator ani;
	Animator aniTransform;

	private void Awake()
	{
		ani = GameObject.Find("傳送區群組").GetComponentInChildren<Animator>();
		// aniTransform = GameObject.Find("場景切換").GetComponent<Animator>();
	}

	private void Update()
	{
		TransferToLevel(indexLevel);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = true;

		ani.SetBool("play", inArea);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = false;

		ani.SetBool("play", inArea);
	}

	/// <summary>
	/// 前往至關卡
	/// </summary>
	/// <param name="level">關卡編號</param>
	public void TransferToLevel(int level)
	{
		// 如果 按下O鍵 且 在區域內
		if (Input.GetKeyDown(KeyCode.O) && inArea)
		{
			// 設置下一關玩家的起始位置
			SaveManager.instance.playerData.playerPos = new Vector3(0f, 0f, 0f);
			PlayerCtrl.instance.transform.position = SaveManager.instance.playerData.playerPos;

			// 取得當前關卡編號
			indexLevel = SceneManager.GetActiveScene().buildIndex;
			// 如果 取得的關卡編號 不為 0 則關卡編號+1
			if (indexLevel != 0)
			{
				indexLevel++;
			}

			ChangeScene();

			// 如果 是要前往下一關 就載入下一關
			if (isToNext)
			{
				SceneManager.LoadScene(indexLevel);
			}
			// 否則 就載入要前往的關卡
			else
			{
				SceneManager.LoadScene(level);
			}

			SaveManager.instance.SaveData();
		}
	}

	/// <summary>
	/// 切換場景
	/// </summary>
	public void ChangeScene()
	{
		// StartCoroutine(ChangeSceneAni());
	}

	/// <summary>
	/// 切換場景動畫
	/// </summary>
	/// <returns></returns>
	IEnumerator ChangeSceneAni()
	{
		// 切換動畫播完後 等待5秒 才執行
		aniTransform.SetTrigger("Play");
		yield return new WaitForSeconds(5f);
	}

	/// <summary>
	/// 傳送關卡至下一關
	/// </summary>
	/*
	void TransferNextLevel()
	{
		if (Input.GetKeyDown(KeyCode.O) && inArea)
		{
			// 取得當前關卡編號
			int indexLevel = SceneManager.GetActiveScene().buildIndex;
			// 關卡編號 加一
			indexLevel++;
			// 載入下一關
			SceneManager.LoadScene(indexLevel);
		}
	}
	*/
}
