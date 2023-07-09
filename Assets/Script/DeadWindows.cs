using UnityEngine;

public class DeadWindows : Windows<DeadWindows>
{
	protected override void Update()
	{
		base.Update();
		if (SaveManager.instance.playerData.playerHP <= 0f)
		{
			Open();
		}
	}
}
