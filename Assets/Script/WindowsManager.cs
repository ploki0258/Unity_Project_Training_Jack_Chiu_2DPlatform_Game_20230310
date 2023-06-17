using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindowsManager
{
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

	public void Start()
	{
		// ��l�Ʈɭ��m�����C�� �קK�W�@���������v�T�o�@��
		windowsList = new List<int>();
	}

	List<int> windowsList = new List<int>();

	public void AddWindows(int transformID)
	{
		for(int i = 0; i < windowsList.Count; i++)
		{
			if (transformID == windowsList[i])
				return;
		}
		windowsList.Add(transformID);
		// Debug.Log("�{�b�� " + windowsList.Count + " �ӵ���");
	}
	public void RemoveWindows(int transformID)
	{
		for (int i = windowsList.Count - 1; i >= 0 ; i--)
		{
			if (transformID == windowsList[i])
			{
				windowsList.RemoveAt(i);
			}
		}
		// Debug.Log("�{�b�� " + windowsList.Count + " �ӵ���");
	}

	/// <summary>�O�_�������Q�}��</summary>
	public bool IsWindowsOpen()
	{
		return windowsList.Count > 0;
	}
}
