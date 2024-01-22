using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	[Header("音效清單")]
	[Header("跑步音效")]
	public AudioClip run;
	[Header("攻擊音效")]
	public AudioClip attack;
	[Header("玩家死亡音效")]
	public AudioClip playerDead;
	[Header("背景音樂")]
	public AudioClip BGM;

	[SerializeField] AudioSource aud, musicSource, sfxSource;

	public static SoundManager instance;

	private void Awake()
	{
		instance = this;
		aud = GetComponent<AudioSource>();
	}

	/// <summary>
	/// 音效播放功能
	/// </summary>
	/// <param name="sound">播放的音效</param>
	/// <param name="min">音量最小值</param>
	/// <param name="max">音量最大值</param>
	public void PlaySound(AudioClip sound, float min, float max)
	{
		// 隨機範圍
		float volume = Random.Range(min, max);
		// 音效元件 的 撥放一次音效(音效, 音量)
		aud.PlayOneShot(sound, volume);
	}

	public void PlayMusic(AudioClip music, float value = 1f)
	{
		musicSource.PlayOneShot(music, value);
	}

	public void PlayGameSfx(AudioClip sfx, float value = 1f)
	{
		sfxSource.PlayOneShot(sfx, value);
	}
	/*
	// 切換靜音
	public void ToggleMusic()
	{
		musicSource.mute = !musicSource.mute;
	}
	// 切換靜音
	public void ToggleSfx()
	{
		sfxSource.mute = !sfxSource.mute;
	}
	*/
	public void MusicVolume(float volume)
	{
		musicSource.volume = volume;
	}

	public void SfxVolume(float volume)
	{
		sfxSource.volume = volume;
	}
}
