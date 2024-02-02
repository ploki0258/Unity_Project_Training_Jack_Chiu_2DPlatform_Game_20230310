using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// �o�O�ƥ�u��Ϊ��F��
using UnityEngine.Events;

/// <summary>
/// �o�O��m�C���Ϊ��p�ɾ��A�b���a�U�u�A�W�u����N���o�U�u��������ơC
/// </summary>
public class OpenTime : MonoBehaviour
{
    // Unity�ƥ�
    [SerializeField] UnityEvent<float> how_long_have_you_slept;
    [SerializeField] bool never_kill_me = false;

    private void Start()
    {
        if (never_kill_me)
        {
            // �T�O�o�Ӫ��󤣷|�b�������d�ɳQ�����A���O�ϥγo�ӥ\��n�ԷV�A�b���Ƹ��J�o�����d�ɷ|�y���������p����~�A����ĳ�ҰʡC
            DontDestroyOnLoad(this.gameObject);
        }

        // �ˬd�O�_������ �p�G�S����ܪ��a�O�Ĥ@���W�u�A���ݭn�B�z�U�u�b�W�u����m�ɶ��C
        if (PlayerPrefs.GetString("CLOSE_GAME_TIME", "") != "")
        {
            // ��e�ɶ�(�O�ѤF�o�O��l��) - �W�������ɶ� �N�i�H�D�X���Z�F
            TimeSpan timeSpan = DateTime.Now.Subtract(Aye.GetTimeByString(PlayerPrefs.GetString("CLOSE_GAME_TIME")));
            // �o�X�ƥ����j�a�p�⪱�a�@�ΤF�X�� �n���줰�򵥵�
            how_long_have_you_slept.Invoke((float)timeSpan.TotalSeconds);
        }
    }
    private void OnDestroy()
    {
        // ��o�Ӫ���Q�����ɬ����U�u�ɶ�
        PlayerPrefs.SetString("CLOSE_GAME_TIME", Aye.GetStringByDateTime(DateTime.Now));
    }
}
