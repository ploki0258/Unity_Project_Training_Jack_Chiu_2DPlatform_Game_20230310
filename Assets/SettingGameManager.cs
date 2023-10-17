using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingGameManager : MonoBehaviour
{
	[Header("遊戲音量控制條")]
	public Slider gameVolumeSlider; // 用於調整遊戲音量的Slider
	[Header("BGM控制條")]
	public Slider bgmSlider; // 用於調整BGM音量的Slider
	[Header("遊戲音量圖示"), Tooltip("有遊戲音量時顯示")]
	public Image gameVolumeIcon;
	[Header("遊戲音量圖示(靜音)"), Tooltip("遊戲音量靜音時顯示")]
	public Image gameVolumeIconNoSound;
	[Header("BGM圖示"), Tooltip("有BGM音量時顯示")]
	public Image bgmIcon;
	[Header("BGM圖示(靜音)"), Tooltip("BGM靜音時顯示")]
	public Image bgmIconNoSound;
	
	private const string gameVolumeKey = "GameVolume";	// 存儲遊戲音量的鍵名
	private const string bgmVolumeKey = "BGMVolume";	// 存儲BGM音量的鍵名
	[Tooltip("遊戲音效")]
	public float gameVolume;
	[Tooltip("BGM音效")]
	public float bgmVolume;

	private void Awake()
	{
		gameVolume = GameObject.Find("音效管理器").GetComponent<AudioSource>().volume;
		bgmVolume = GameObject.Find("Spires(BGM)").GetComponent<AudioSource>().volume;
	}

	private void Start()
	{
		gameVolumeSlider.value = gameVolume;
		bgmSlider.value = bgmVolume;
		InitializeSettings();
		OnVolumeChanged();
	}

	/// <summary>
	/// 初始化設定：設置Slider的初始值
	/// </summary>
	private void InitializeSettings()
	{
		gameVolumeIcon.enabled = true;
		bgmIcon.enabled = true;
		gameVolumeIconNoSound.enabled = false;
		bgmIconNoSound.enabled = false;

		float savedGameVolume = PlayerPrefs.GetFloat(gameVolumeKey, 0.5f); // 讀取存儲的遊戲音量，預設為0.5f
		float savedBgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f); // 讀取存儲的BGM音量，預設為0.5f
		gameVolumeSlider.value = savedGameVolume; // 設定Slider的值為讀取的遊戲音量
		bgmSlider.value = savedBgmVolume; // 設定Slider的值為讀取的BGM音量
	}

	/// <summary>
	/// 當BGM音量Slider變動時呼叫
	/// </summary>
	public void OnVolumeChanged()
	{
		float newGameVolume = gameVolumeSlider.value; // 獲取Slider的值
		float newBgmVolume = bgmSlider.value; // 獲取Slider的值
		gameVolume = newGameVolume;
		bgmVolume = newBgmVolume;
		PlayerPrefs.SetFloat(gameVolumeKey, gameVolume); // 將新的遊戲音量值存儲到PlayerPrefs
		PlayerPrefs.SetFloat(bgmVolumeKey, bgmVolume); // 將新的BGM音量值存儲到PlayerPrefs

		// 音量為零時變更顯示圖示
		if (newGameVolume == 0)
		{
			gameVolumeIcon.enabled = false;
			gameVolumeIconNoSound.enabled = true;
		}
		else
		{
			gameVolumeIcon.enabled = true;
			gameVolumeIconNoSound.enabled = false;
		}
		if (newBgmVolume == 0)
		{
			bgmIcon.enabled = false;
			bgmIconNoSound.enabled = true;
		}
		else
		{
			bgmIcon.enabled = true;
			bgmIconNoSound.enabled = false;
		}
	}
}
