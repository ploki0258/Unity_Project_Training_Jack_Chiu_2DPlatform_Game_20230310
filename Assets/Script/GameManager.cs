using UnityEngine;

// 單例設計模式 不可重複存在 任何地方皆可呼叫
public class GameManager : MonoBehaviour
{
	private GameObject dieWindos = null;

	static public GameManager instance
	{
		// 當有人使用我的時候
		get
		{
			// 如果我不存在
			if (_instance == null)
			{
				// 就自我憑空建立
				_instance = new GameManager();
			}
			// 回傳我可給對方使用
			return _instance;
		}
	}
	static GameManager _instance = null;

	private void Awake()
	{
		dieWindos = GameObject.Find("死亡介面切換");
	}

	public void PlayerDead()
	{
		dieWindos.SetActive(true);  // 開啟死亡畫面
	}
}
