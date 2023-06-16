using UnityEngine;

/// <summary>
/// �D��t�ΡG�إ߹D�㪺��Ʈ榡
/// </summary>
[CreateAssetMenu(fileName = "�s�D��", menuName = "�إ߷s�D��")]
public class ItemData : ScriptableObject
{
    // ScriptableObject = ���ƪ����
    // public string userInput;
    // const string PlayerPrefsKey = "UserInputKey";
    // �@�P
    [Header("�D��ID")]
    public int id;                      // �D��ID
    [Header("�D��W��")]
    public string title;                // �D����D
    [Header("�D��ϥ�")]
    public Sprite icon;                 // �D��ϥ�
    [Header("�D�㻡��"), TextArea(5, 5)]
    public string info;                 // �D��ԭz
    [Header("�D��i�_�ϥ�")]
    public bool canUse;                 // �O�_�i�ϥ�
    [Header("�D��O�_�|����")]
    public bool Consumables;            // �ϥΫ�O�_�|����
    [Header("�D���C��")]
    public Color category;              // ���O���C��

    // �ӧO
    // public float regainHp;   // �^��
    // public float regainMp;   // ���]
    [Header("��_��O��")]
    public float regainStr;     // �^��O

    // �b Awake �ɳ]�w�w�]��
    // �p�G�ϥΪ̦���J�� �hŪ���ϥΪ̪���
    // �p�G�ϥΪ̨S����J�� �h�Ȭ��w�]��
    private void Awake()
    {
        SaveUserInput();
        if (PlayerPrefs.HasKey("ItemID"))
        {
            // userInput = PlayerPrefs.GetString(PlayerPrefsKey);
            id = PlayerPrefs.GetInt("ItemID", id);
            title = PlayerPrefs.GetString("ItemTitle", title);
            info = PlayerPrefs.GetString("ItemInfo", info);
            int canUseBool = PlayerPrefs.GetInt("ItemCanUse", 0);
            canUse = canUseBool != 0;
            int ConsumablesBool = PlayerPrefs.GetInt("ItemConsumables", 0);
            Consumables = ConsumablesBool != 0;
            regainStr = PlayerPrefs.GetFloat("ItemRegainStr", regainStr);
        }
        else
        {
            title = "���R�W";
            info = "�L�ԭz";
            canUse = false;
            Consumables = false;
            regainStr = 0f;
        }
    }

    /// <summary>
    /// �x�s���a�ҿ�J����
    /// </summary>
    /// <param name="iconBool">�ҿ�J���ϥ�</param>
    /// <param name="canUseBool">�ҿ�J�i�_�Q�ϥ�</param>
    /// <param name="ConsumablesBool">�ҿ�J�i�_�|����</param>
    public void SaveUserInput()
    {
        // userInput = value;
        // PlayerPrefs.SetString(PlayerPrefsKey, userInput);
        // iconBool = icon == null ? 0 : 1;                       // �p�G canUse �� false �ഫ�� 0 �_�h�N��1
        // PlayerPrefs.SetInt("ItemIcon", iconBool);              // �x�s�ҿ�J���D��ϥ�
        PlayerPrefs.SetInt("ItemID", id);                         // �x�s�ҿ�J���D��ID
        PlayerPrefs.SetString("ItemTitle", title);                // �x�s�ҿ�J���D����D
        PlayerPrefs.SetString("ItemInfo", info);                  // �x�s�ҿ�J���D��ԭz
        int canUseBool = canUse == false ? 0 : 1;                 // �p�G canUse �� false �ഫ�� 0 �_�h�N��1
        PlayerPrefs.SetInt("ItemCanUse", canUseBool);             // �x�s�ҿ�J���D��i�_�ϥ�
        int ConsumablesBool = Consumables == false ? 0 : 1;       // �p�G Consumables �� false �ഫ�� 0 �_�h�N��1
        PlayerPrefs.SetInt("ItemConsumables", ConsumablesBool);   // �x�s�ҿ�J���D��O�_�|����
        PlayerPrefs.SetFloat("ItemRegainStr", regainStr);         // �x�s�ҿ�J����O��_��

        PlayerPrefs.Save();
    }
}