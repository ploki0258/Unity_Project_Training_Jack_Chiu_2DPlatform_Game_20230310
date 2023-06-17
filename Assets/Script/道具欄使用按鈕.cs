using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 道具欄使用按鈕 : MonoBehaviour
{
	public Grid 目前選到的格子 = null;

	public void 按了()
	{
		if (目前選到的格子 == null)
			return;
		目前選到的格子.PressButton();
	}
}
