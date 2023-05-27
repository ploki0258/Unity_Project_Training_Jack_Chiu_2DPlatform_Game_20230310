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
        PlayerPrefs.SetString("GameData", json);
    }
}

/// <summary>
/// ���a���
/// </summary>
[System.Serializable]
public struct PlayerData
{
    /// <summary>
    /// �����ƶq
    /// </summary>
    public int coinQuantity
    {
        get { return _coinQuantity; }
        set
        {
            _coinQuantity = value;
        }
    }
    [SerializeField] public int _coinQuantity;
    public System.Action coinChage;

    public PlayerData(int coin)
    {
        _coinQuantity = coin;
        coinChage = null;
    }

    /*public PlayerData(int v)
    {
        _coinQuantity = 0;
        coinChage = null;
    }
    */
}
