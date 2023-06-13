using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SkillInterface : Windows<SkillInterface>
{
	new bool isOpen = false;

	public override void Open()
	{
		base.Open();
		isOpen = true;
	}

	public override void Close()
	{
		base.Close();
		isOpen = false;
	}

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!isOpen)
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
