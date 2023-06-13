using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SkillInterface : Windows<SkillInterface>
{
	public override void Open()
	{
		base.Open();
	}

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Open();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}
}
