using UnityEngine;

public class DeadWindows : Windows<DeadWindows>
{
	protected override void Update()
	{
		base.Update();
		if (SaveManager.instance.playerData.playerHP <= 0f)
		{
			Open();

			AudioClip sound = SoundManager.instance.playerDead;
			SoundManager.instance.PlaySound(sound, 0.7f, 1f);
		}
	}
}
