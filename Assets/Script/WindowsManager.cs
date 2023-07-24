using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager
{
	#region 單例
	public static WindowsManager instance
	{
		get
		{
			if (_instance == null)
				_instance = new WindowsManager();
			return _instance;
		}
	}
	static WindowsManager _instance = null;
	#endregion

	public List<int> windowsList = new List<int>();
	
	public void Start()
	{
		// 初始化時重置視窗列表 避免上一關的視窗影響這一關
		windowsList = new List<int>();
	}

	public void Update()
	{
		Debug.Log(windowsList);
	}

	public void AddWindows(int transformID)
	{
		for (int i = 0; i < windowsList.Count; i++)
		{
			if (transformID == windowsList[i])
				return;
		}
		windowsList.Add(transformID);
		Debug.Log($"<color=yellow>現在有 {windowsList.Count} 個視窗</color>");
	}

	public void RemoveWindows(int transformID)
	{
		for (int i = windowsList.Count - 1; i >= 0; i--)
		{
			if (transformID == windowsList[i])
			{
				windowsList.RemoveAt(i);
			}
		}
		Debug.Log($"<color=#00ffff>現在有 {windowsList.Count} 個視窗</color>");
	}

	/// <summary>是否有視窗被開啟</summary>
	public bool IsWindowsOpen()
	{
		return windowsList.Count > 0;
	}
}
