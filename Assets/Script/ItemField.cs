using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemField : Windows<ItemField>
{
    [Header("��l�ҪO")]
    [SerializeField] GameObject tempGrid = null;
    [Header("�D����I��")]
    [SerializeField] RectTransform itemFieldBG = null;

    protected override void Awake()
    {
        base.Awake();
        openSpeed = 15f;    // �����}�ҳt�� = 15
        ItemManager.instance.Initialization();
    }

    protected override void Start()
    {
        base.Start();
        // Test
        // SaveManager.instance.playerData.AddItem(20);

        ��s�D����();

        SaveManager.instance.playerData.Act_goodsChange += ��s�D����;
    }

    private void OnDisable()
    {
        SaveManager.instance.playerData.Act_goodsChange -= ��s�D����;
    }

    protected override void Update()
    {
        base.Update();
        // �� Q�� �}�ҹD���椶��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isOpen == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }

    // ���}������ ��ܷƹ� �ɶ��Ȱ�
    public override void Open()
    {
        base.Open();
        // Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    // ���������� ���÷ƹ� �ɶ��~��
    public override void Close()
    {
        base.Close();
        // Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    List<GameObject> �U���� = new List<GameObject>();

    void ��s�D����()
    {
        foreach (var g in �U����)
            DestroyImmediate(g);
        �U����.Clear();

        // ��l�ҪO���������
        tempGrid.SetActive(false);
        // i�p���l�ƶq 20
        for (int i = 0; i < 20; i++)
        {
            // �p�Gi�p�󪱮a�������D��ƶq �N��ܹD��
            if (i < SaveManager.instance.playerData.goodsList.Count)
            {
                // ��ܫ����D��
                // �ƻs�@�Ӯ�l�ҪO �é�i�D����I����
                GameObject ��Ыت���l = Instantiate(tempGrid, itemFieldBG);
                // �s�X��l�ɥ��⥦�Ұ�
                ��Ыت���l.SetActive(true);
                // �N���~��ƶǰe����l�B�z
                ��Ыت���l.GetComponent<Grid>().InputData(SaveManager.instance.playerData.goodsList[i]);

                �U����.Add(��Ыت���l);
            }
            // �_�h��ܪŮ�l
            else
            {
                // �ƻs�@�Ӯ�l�ҪO�X�� �åB��i�D����I����
                GameObject ��Ыت���l = Instantiate(tempGrid, itemFieldBG);
                // �s�X��l�ɥ��⥦�Ұ�
                ��Ыت���l.SetActive(true);

                �U����.Add(��Ыت���l);
            }
        }
    }

    /*
    void ��s�D����()
    {
        // �ھڦ��X�ӹD��ͦ��X�Ӱӫ~
        for (int i = 0; i < ItemManager.instance.AllItem.Length; i++)
        {
            GameObject ���ͦ����ӫ~ = Instantiate(tempGrid, itemFieldBG);
            ���ͦ����ӫ~.SetActive(true);
            RectTransform UI��m = ���ͦ����ӫ~.GetComponent<RectTransform>();

            UI��m.anchoredPosition = new Vector2(UI��m.anchoredPosition.x, 20f + (i * 220f));

            ���ͦ����ӫ~.GetComponent<Grid>().��J���(SaveManager.instance.goodsList[i]);
        }
        itemFieldBG.sizeDelta = new Vector2(itemFieldBG.sizeDelta.x, 20f + (ItemManager.instance.AllItem.Length * 22f));
    }
    */
}
