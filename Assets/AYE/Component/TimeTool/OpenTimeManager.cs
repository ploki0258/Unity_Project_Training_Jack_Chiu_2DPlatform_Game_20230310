using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڪ��u�@�O�t�d�T�{���p�ò���OpenTime�������a�W�A�]���L�O���������A���Ƹ��J���d�i��|�гy�X�@�j������C
/// </summary>
public class OpenTimeManager : MonoBehaviour
{
    [SerializeField] GameObject openTimeObj = null;
    void Start()
    {
        GameObject mainOpenTime = GameObject.Find("OpenTime");
        if (mainOpenTime == null)
        {
            // �ݨӳ��W�u�����s�bOpenTime�i�H��ߪ��إߤF�C
            GameObject temp = Instantiate(openTimeObj);
            temp.name = "OpenTime";
        }
    }
}
