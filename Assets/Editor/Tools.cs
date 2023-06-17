using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/§R°£©Ò¦³¬ö¿ý")]

    static public void ClearPlayerData()
	{
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
	}
}
