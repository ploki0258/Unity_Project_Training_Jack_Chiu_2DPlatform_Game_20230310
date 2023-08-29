using UnityEngine;
using UnityEngine.UI;

public class SettingGameManager : MonoBehaviour
{
	[Header("�C�����q�����")]
	public Slider gameVolumeSlider; // �Ω�վ�C�����q��Slider
	[Header("BGM�����")]
	public Slider bgmSlider; // �Ω�վ�BGM���q��Slider

	private const string gameVolumeKey = "GameVolume"; // �s�x�C�����q����W
	private const string bgmVolumeKey = "BGMVolume"; // �s�xBGM���q����W

	private void Start()
	{
		InitializeSettings();
	}

	// ��l�Ƴ]�w�A�]�mSlider����l��
	private void InitializeSettings()
	{
		float savedGameVolume = PlayerPrefs.GetFloat(gameVolumeKey, 0.5f); // Ū���s�x���C�����q�A�w�]��0.5f
		float savedBgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 0.5f); // Ū���s�x��BGM���q�A�w�]��0.5f
		gameVolumeSlider.value = savedGameVolume; // �]�wSlider���Ȭ�Ū�����C�����q
		bgmSlider.value = savedBgmVolume; // �]�wSlider���Ȭ�Ū����BGM���q
	}

	// ��BGM���qSlider�ܰʮɩI�s
	public void OnVolumeChanged()
	{
		float newGameVolume = gameVolumeSlider.value; // ���Slider����
		float newBgmVolume = bgmSlider.value; // ���Slider����
		PlayerPrefs.SetFloat(gameVolumeKey, newGameVolume); // �N�s���C�����q�Ȧs�x��PlayerPrefs
		PlayerPrefs.SetFloat(bgmVolumeKey, newBgmVolume); // �N�s��BGM���q�Ȧs�x��PlayerPrefs
	}
}
