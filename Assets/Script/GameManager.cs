using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ҳ]�p�Ҧ� ���i���Ʀs�b ����a��ҥi�I�s
public class GameManager
{
    static public GameManager instance
    {
        // ���H�ϥΧڪ��ɭ�
        get
        {
            // �p�G�ڤ��s�b
            if (_instance == null)
            {
                // �N�ۧھ̪ūإ�
                _instance = new GameManager();
            }
            // �^�ǧڥi�����ϥ�
            return _instance;
        }
    }
    static GameManager _instance = null;

    public float playerHp = 100f;

    public int playerMoney = 10;

    public System.Action �C�������ƥ� = null;
    public void �C������()
    {
        // ���H�b��ť�ƥ󪺸ܴN�I�s�ƥ�
        if (�C�������ƥ� != null)
            �C�������ƥ�.Invoke();
    }
}
