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

	private void Update()
	{
		TransferToLevel(indexLevel);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = false;
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

	/// <summary>
	/// 前往至關卡
	/// </summary>
	/// <param name="level">關卡編號</param>
	void TransferToLevel(int level)
	{
		if (Input.GetKeyDown(KeyCode.O) && inArea)
		{
			// 取得當前關卡編號
			indexLevel = SceneManager.GetActiveScene().buildIndex;
			// 如果 取得的關卡編號 不為 0 則關卡編號 加1
			if (indexLevel != 0)
			{
				indexLevel++;
			}
			// 如果 是要前往下一關 就載入下一關
			// 否則 就載入要前往的關卡
			if (isToNext)
			{
				SceneManager.LoadScene(indexLevel);
			}
			else
			{
				SceneManager.LoadScene(level);
			}
		}
	}
}
