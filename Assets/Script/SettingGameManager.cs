using UnityEngine;
using UnityEngine.UI;

public class SettingGameManager : MonoBehaviour
{
	[Header("BGM控制條"), Tooltip("用於調整BGM的音量")]
	public Slider bgmSlider; // 用於調整BGM音量的Slider
	[Header("遊戲音效控制條"), Tooltip("用於調整遊戲音效的音量")]
	public Slider gameSfxSlider; // 用於調整遊戲音量的Slider
	[Header("遊戲音量圖示"), Tooltip("有遊戲音量時顯示")]
	public Image gameVolumeIcon;
	[Header("遊戲音量圖示(靜音)"), Tooltip("遊戲音量靜音時顯示")]
	public Image gameVolumeIconNoSound;
	[Header("BGM圖示"), Tooltip("有BGM音量時顯示")]
	public Image bgmIcon;
	[Header("BGM圖示(靜音)"), Tooltip("BGM靜音時顯示")]
	public Image bgmIconNoSound;
	[SerializeField] AudioSource musicSource, sfxSource;

	private const string gameVolumeKey = "GameVolume";  // 存儲遊戲音效音量的鍵名
	private const string bgmVolumeKey = "BGMVolume";    // 存儲BGM音量的鍵名
	[Tooltip("BGM音效")]
	float bgmVolume;
	[Tooltip("遊戲音效")]
	float sfxVolume;

	private void Awake()
	{
		bgmVolume = musicSource.volume;
		sfxVolume = sfxSource.volume;
	}

	private void Start()
	{
		bgmSlider.value = bgmVolume;
		gameSfxSlider.value = sfxVolume;
		InitializeSettings();
	}

	/// <summary>
	/// 初始化設定：設置Slider的初始值
	/// </summary>
	private void InitializeSettings()
	{
		// 顯示一般圖示 關閉靜音圖示
		gameVolumeIcon.enabled = true;
		bgmIcon.enabled = true;
		gameVolumeIconNoSound.enabled = false;
		bgmIconNoSound.enabled = false;

		float savedGameVolume = PlayerPrefs.GetFloat(gameVolumeKey, 1f); // 讀取存儲的遊戲音量，預設為0.5f
		float savedBgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 1f); // 讀取存儲的BGM音量，預設為0.5f
		gameSfxSlider.value = savedGameVolume; // 設定Slider的值為讀取的遊戲音量
		bgmSlider.value = savedBgmVolume; // 設定Slider的值為讀取的BGM音量
	}

	/// <summary>
	/// 當音量變動時變更音量圖示
	/// </summary>
	public void OnVolumeChangedIcon()
	{
		float newBgmVolume = bgmSlider.value;       // 獲取Slider的值
		float newGameVolume = gameSfxSlider.value;  // 獲取Slider的值
		bgmVolume = newBgmVolume;
		sfxVolume = newGameVolume;
		PlayerPrefs.SetFloat(bgmVolumeKey, bgmVolume);  // 將新的BGM音量值存儲到PlayerPrefs
		PlayerPrefs.SetFloat(gameVolumeKey, sfxVolume); // 將新的遊戲音量值存儲到PlayerPrefs

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
	/*
	public void ToggleMusic()
	{
		SoundManager.instance.ToggleMusic();
	}

	public void ToggleSfx()
	{
		SoundManager.instance.ToggleSfx();
	}
	*/
	public void BGMVolume()
	{
		SoundManager.instance.MusicVolume(bgmSlider.value);
	}

	public void gameSfxVolume()
	{
		SoundManager.instance.SfxVolume(gameSfxSlider.value);
	}
}
