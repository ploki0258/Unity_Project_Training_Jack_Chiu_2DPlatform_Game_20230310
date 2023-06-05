using Fungus;
using UnityEngine;

public class SaveManager
{
    #region ��ҼҦ�
    // �b��ӱM�ץ���ŧi�@��instance
    public static SaveManager instance
    {
        // ���H�ݭn��
        get
        {
            // �p�G�ڤ��s�b
            if (_instance == null)
            {
                // �N�ۤv�гy�ۤv
                _instance = new SaveManager();
            }

            // �^�Ǧۤv
            return _instance;
        }
    }
    // �O�����ڪ���m
    static SaveManager _instance = null;
    #endregion

    // �إߪ��a�����
    public PlayerData playerData = new PlayerData();

    /// <summary>
    /// �C��Ū��
    /// </summary>
    public void LoadData()
    {
        string json = PlayerPrefs.GetString("GameData", "0");
        // �p�G json��0��
        if (json == "0")
        {
            // �o�O�@�ӷs���a �е��L�򥻼ƭ�
            playerData = new PlayerData(0);
        }
        else
        {
            // �q�J����ƥ�json����^�Өϥ�
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    /// <summary>
    /// �C���s��
    /// </summary>
    public void SaveData()
    {
        // �ഫ��Ƭ�Json�榡
        string json = JsonUtility.ToJson(playerData, true);
        Debug.Log(json);
        // �x�s�b�w�Ф�
        PlayerPrefs.SetString("GameData", json);
        // PlayerPrefs.Save();
    }

    public void SaveUser()
    {
        if(PlayerCtrl.instance.hp <= 0)
        {
            SaveData();
        }
    }
}

/// <summary>
/// ���a��ơG�w�q��Ƥ��e
/// </summary>
[System.Serializable]
public struct PlayerData
{
    // public int moneyCount;   // �����ƶq
    // public int skillPoint;   // �ޯ��I��

    /// <summary>
    /// �����ƶq
    /// </summary>
    [SerializeField] public int moneyCount
    {
        get { return _moneyCount; }
        set 
        {
            _moneyCount = value;

            // 
            if (��s���� != null)
            {
                ��s����.Invoke();
            }
        }
    }
    int _moneyCount;
    public System.Action ��s����;

    /// <summary>
    /// �ޯ��I��
    /// </summary>
    [SerializeField] public int skillPoint
    {
        get { return _skillPoint; }
        set 
        {
            _skillPoint = value;

            // 
            if (��s�ޯ��I != null)
            {
                ��s�ޯ��I.Invoke();
            }
        }
    }
    int _skillPoint;
    public System.Action ��s�ޯ��I;
    
    // �غc��
    public PlayerData(int coin, int skill)
    {
        // this.moneyCount = coin;
        // this.skillPoint = skill;

        _moneyCount = coin;
        _skillPoint = skill;
        ��s���� = null;
        ��s�ޯ��I = null;
    }

    public PlayerData(int v)
    {
        // this.moneyCount = 0;
        // this.skillPoint = 0;

        _moneyCount = 0;
        _skillPoint = 0;
        ��s���� = null;
        ��s�ޯ��I = null;
    }
}
