using UnityEngine;

public class SkillInterface : Windows<SkillInterface>
{
	bool Opening = false;
	public bool isPaused;

	public float timeSkillInterface;

	protected override void Awake()
	{
		base.Awake();
		SkillManager.instance.Initialization();
		isPaused = FindObjectOfType<PlayerCtrl>().isPausedGame;
	}

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!Opening)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}

		// Debug.Log("是否暫停(技能)：" + isPaused);
	}

	public override void Open()
	{
		base.Open();
		Opening = true;
		isPaused = true;
		timeSkillInterface = 0f;
		PlayerCtrl.instance.enabled = false;
	}

	public override void Close()
	{
		base.Close();
		Opening = false;
		isPaused = false;
		timeSkillInterface = 1f;
		PlayerCtrl.instance.enabled = true;
	}
}
