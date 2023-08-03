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

		Debug.Log("¬O§_¼È°±(§Þ¯à)¡G" + isPaused);
	}

	public override void Open()
	{
		base.Open();
		Opening = true;
		isPaused = true;
		Time.timeScale = 0f;
		// TalentTree.instance.ShowSkillIcon(4);
	}

	public override void Close()
	{
		base.Close();
		Opening = false;
		isPaused = false;
		Time.timeScale = 1f;
	}
}
