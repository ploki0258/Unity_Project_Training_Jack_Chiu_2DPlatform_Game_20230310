using UnityEngine;

public class SkillInterface : Windows<SkillInterface>
{
	bool Opening = false;

	public override void Open()
	{
		base.Open();
		Opening = true;
		// TalentTree.instance.ShowSkillIcon(4);
		Time.timeScale = 0f;
	}

	public override void Close()
	{
		base.Close();
		Opening = false;
		Time.timeScale = 1f;
	}

	protected override void Awake()
	{
		base.Awake();
		SkillManager.instance.Initialization();
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
	}
}
