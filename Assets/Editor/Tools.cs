using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/�R���Ҧ�����")]

    static public void ClearPlayerData()
	{
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
	}
}
