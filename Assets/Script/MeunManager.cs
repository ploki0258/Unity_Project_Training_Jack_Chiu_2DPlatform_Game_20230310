using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeunManager : MonoBehaviour
{
	[SerializeField, Header("尋找物件的名稱")]
	string objectName;
	[SerializeField, Header("遊戲設定介面")]
	GameObject settingWindows;

	TransferManager transferManager = null;

	public static MeunManager instance = null;

	private void Awake()
	{
		instance = this;
		transferManager = GameObject.Find(objectName).GetComponent<TransferManager>();
	}

	private void Update()
	{
		if (settingWindows == true)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				settingWindows.SetActive(false);
				Time.timeScale = 1f;
			}
		}
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
		PlayerPrefs.DeleteKey("GameData");  // 刪除玩家資料
		SceneManager.LoadScene("遊戲場景"); // 載入遊戲場景
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
	/// 返回主畫面(死亡介面)
	/// </summary>
	public void ReturnToHome()
	{
		// transferManager.ChangeScene();
		SceneManager.LoadScene("開始畫面");
	}

	/// <summary>
	/// 開啟遊戲設定視窗
	/// </summary>
	public void OpenSettingGame()
	{
		settingWindows.SetActive(true);
		Time.timeScale = 0f;
		if (PlayerCtrl.instance != null)
			PlayerCtrl.instance.enabled = false;
	}

	/// <summary>
	/// 關閉遊戲設定視窗
	/// </summary>
	public void CloseSettingGameWindows()
	{
		settingWindows.SetActive(false);
		Time.timeScale = 1f;
		if (PlayerCtrl.instance != null)
			PlayerCtrl.instance.enabled = true;
	}
}
