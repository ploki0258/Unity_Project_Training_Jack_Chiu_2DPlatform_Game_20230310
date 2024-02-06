using UnityEngine;

public class SkillInterface : Windows<SkillInterface>
{
	bool Opening = false;
	public bool isPaused;

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
		Time.timeScale = 0f;
		// TalentTree.instance.ShowSkillIcon(4);
		PlayerCtrl.instance.enabled = false;
		//Enemy.instance.enabled = false;
	}

	public override void Close()
	{
		base.Close();
		Opening = false;
		isPaused = false;
		Time.timeScale = 1f;
		PlayerCtrl.instance.enabled = true;
		//Enemy.instance.enabled = true;
	}
}
