using UnityEngine;

public class DeadWindows : Windows<DeadWindows>
{
	protected override void Update()
	{
		base.Update();
		// 如果 玩家死亡 就開啟介面
		// 否則 就關閉介面
		if (SaveManager.instance.playerData.playerHP <= 0f)
		{
			Open();
			// 播放音效
			AudioClip sound = SoundManager.instance.playerDead;
			SoundManager.instance.PlayGameSfx(sound);
		}
		else
		{
			Close();
		}
	}
}
