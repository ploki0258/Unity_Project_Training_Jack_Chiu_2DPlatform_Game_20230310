using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeunManager : MonoBehaviour
{
	[SerializeField, Header("尋找物件的名稱")]
	string objectName;

	TransferManager transferManager = null;

	private void Awake()
	{
		transferManager = GameObject.Find(objectName).GetComponent<TransferManager>();
	}

	/// <summary>
	/// 離開遊戲
	/// </summary>
	public void QuitGame()
	{
		Application.Quit();
	}

	/// <summary>
	/// 開始遊戲
	/// </summary>
	public void StartGame()
	{
		PlayerPrefs.DeleteKey("GameData");
		SceneManager.LoadScene("遊戲場景");
	}

	/// <summary>
	/// 繼續遊戲
	/// </summary>
	public void ContinueGame()
	{
		// 如果 GameData 是空白 表示是新玩家
		// 是新玩家的話 就不執行
		if (PlayerPrefs.GetString("GameData", "") == "")
			return;

		SaveManager.instance.LoadData();    // 讀取玩家資料
		SceneManager.LoadScene(SaveManager.instance.playerData.levelName);  // 載入玩家上次儲存的關卡
	}

	/// <summary>
	/// 返回主畫面
	/// </summary>
	public void ReturnToHome()
	{
		transferManager.ChangeScene();
		SceneManager.LoadScene("開始畫面");
	}
}
