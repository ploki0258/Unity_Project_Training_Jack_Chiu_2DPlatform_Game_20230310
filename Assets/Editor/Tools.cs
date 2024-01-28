using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/刪除所有紀錄")]

    static public void ClearPlayerData()
	{
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
	}
}
