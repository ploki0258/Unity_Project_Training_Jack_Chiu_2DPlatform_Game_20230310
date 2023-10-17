using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	[Header("音效清單")]
	public AudioClip run;
	public AudioClip attack;
	public AudioClip playerDead;
	public AudioClip BGM;

	public AudioSource aud;

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
}
