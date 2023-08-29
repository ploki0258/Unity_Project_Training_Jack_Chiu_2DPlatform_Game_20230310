using UnityEngine;
using UnityEngine.UI;

public class SettingGameManager : MonoBehaviour
{
	[Header("遊戲音量控制條")]
	public Slider gameVolumeSlider; // 用於調整遊戲音量的Slider
	[Header("BGM控制條")]
	public Slider bgmSlider; // 用於調整BGM音量的Slider

	private const string gameVolumeKey = "GameVolume"; // 存儲遊戲音量的鍵名
	private const string bgmVolumeKey = "BGMVolume"; // 存儲BGM音量的鍵名

	private void Start()
	{
		InitializeSettings();
	}

	// 初始化設定，設置Slider的初始值
	private void InitializeSettings()
	{
		float savedGameVolume = PlayerPrefs.GetFloat(gameVolumeKey, 0.5f); // 讀取存儲的遊戲音量，預設為0.5f
		float savedBgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f); // 讀取存儲的BGM音量，預設為0.5f
		gameVolumeSlider.value = savedGameVolume; // 設定Slider的值為讀取的遊戲音量
		bgmSlider.value = savedBgmVolume; // 設定Slider的值為讀取的BGM音量
	}

	// 當BGM音量Slider變動時呼叫
	public void OnVolumeChanged()
	{
		float newGameVolume = gameVolumeSlider.value; // 獲取Slider的值
		float newBgmVolume = bgmSlider.value; // 獲取Slider的值
		PlayerPrefs.SetFloat(gameVolumeKey, newGameVolume); // 將新的遊戲音量值存儲到PlayerPrefs
		PlayerPrefs.SetFloat(bgmVolumeKey, newBgmVolume); // 將新的BGM音量值存儲到PlayerPrefs
	}
}
