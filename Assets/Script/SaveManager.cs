using UnityEngine;

public class SaveManager
{
    #region ��ҼҦ�
    // �b��ӱM�ץ���ŧi�@��instance
    public static SaveManager instance
    {
        // ��Q���o��
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

    // public PlayerData playerData = new PlayerData();
    
    /// <summary>
    /// ���a���
    /// </summary>
    /*[System.Serializable]
    public struct PlayerData
    {
        public int coinQuantity
        {
            get { return _coinQuantity; }
            set
            {
                _coinQuantity = value;
            }
        }
    }
    [SerializeField] public int _coinQuantity;
    public System.Action coinChage;

    public PlayerData(int coin)
    {
        _coinQuantity = coin;
    }

    public PlayerData(int value)
    {
        _coinQuantity = 0;
    }*/
}
