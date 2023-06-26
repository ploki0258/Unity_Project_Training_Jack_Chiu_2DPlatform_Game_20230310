using UnityEngine;
using UnityEngine.SceneManagement;

public class MeunManager : MonoBehaviour
{
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

	private void Awake()
	{
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
        // 
        if (PlayerPrefs.GetString("GameData", "") == "")
            return;
        SaveManager.instance.LoadData();
        SceneManager.LoadScene(SaveManager.instance.playerData.levelName);
    }
}
