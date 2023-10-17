using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingGameManager : MonoBehaviour
{
	[Header("�C�����q�����")]
	public Slider gameVolumeSlider; // �Ω�վ�C�����q��Slider
	[Header("BGM�����")]
	public Slider bgmSlider; // �Ω�վ�BGM���q��Slider
	[Header("�C�����q�ϥ�"), Tooltip("���C�����q�����")]
	public Image gameVolumeIcon;
	[Header("�C�����q�ϥ�(�R��)"), Tooltip("�C�����q�R�������")]
	public Image gameVolumeIconNoSound;
	[Header("BGM�ϥ�"), Tooltip("��BGM���q�����")]
	public Image bgmIcon;
	[Header("BGM�ϥ�(�R��)"), Tooltip("BGM�R�������")]
	public Image bgmIconNoSound;
	
	private const string gameVolumeKey = "GameVolume";	// �s�x�C�����q����W
	private const string bgmVolumeKey = "BGMVolume";	// �s�xBGM���q����W
	[Tooltip("�C������")]
	public float gameVolume;
	[Tooltip("BGM����")]
	public float bgmVolume;

	private void Awake()
	{
		gameVolume = GameObject.Find("���ĺ޲z��").GetComponent<AudioSource>().volume;
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
	/// ��l�Ƴ]�w�G�]�mSlider����l��
	/// </summary>
	private void InitializeSettings()
	{
		gameVolumeIcon.enabled = true;
		bgmIcon.enabled = true;
		gameVolumeIconNoSound.enabled = false;
		bgmIconNoSound.enabled = false;

		float savedGameVolume = PlayerPrefs.GetFloat(gameVolumeKey, 0.5f); // Ū���s�x���C�����q�A�w�]��0.5f
		float savedBgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f); // Ū���s�x��BGM���q�A�w�]��0.5f
		gameVolumeSlider.value = savedGameVolume; // �]�wSlider���Ȭ�Ū�����C�����q
		bgmSlider.value = savedBgmVolume; // �]�wSlider���Ȭ�Ū����BGM���q
	}

	/// <summary>
	/// ��BGM���qSlider�ܰʮɩI�s
	/// </summary>
	public void OnVolumeChanged()
	{
		float newGameVolume = gameVolumeSlider.value; // ���Slider����
		float newBgmVolume = bgmSlider.value; // ���Slider����
		gameVolume = newGameVolume;
		bgmVolume = newBgmVolume;
		PlayerPrefs.SetFloat(gameVolumeKey, gameVolume); // �N�s���C�����q�Ȧs�x��PlayerPrefs
		PlayerPrefs.SetFloat(bgmVolumeKey, bgmVolume); // �N�s��BGM���q�Ȧs�x��PlayerPrefs

		// ���q���s���ܧ���ܹϥ�
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
