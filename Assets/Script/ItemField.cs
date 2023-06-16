using UnityEngine;

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
        SaveManager.instance.addItem(20);
        SaveManager.instance.addItem(20);
        SaveManager.instance.addItem(21);
        SaveManager.instance.addItem(22);
        SaveManager.instance.addItem(23);

        ��s�D����();

        SaveManager.instance.Act_goodsChange += ��s�D����;
    }

    private void OnDisable()
    {
        SaveManager.instance.Act_goodsChange -= ��s�D����;
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


    void ��s�D����()
    {
        // ��l�ҪO���������
        tempGrid.SetActive(false);
        // i�p���l�ƶq 20
        for (int i = 0; i < 20; i++)
        {
            // �p�Gi�p�󪱮a�������D��ƶq �N��ܹD��
            if (i < SaveManager.instance.goodsList.Count)
            {
                // ��ܫ����D��
                // �ƻs�@�Ӯ�l�ҪO �é�i�D����I����
                GameObject ��Ыت���l = Instantiate(tempGrid, itemFieldBG);
                ��Ыت���l.SetActive(true);
                RectTransform UIPos = ��Ыت���l.GetComponent<RectTransform>();
                UIPos.anchoredPosition = new Vector2(200f * i, 200f * i);
                ��Ыت���l.GetComponent<Grid>().InputData(SaveManager.instance.goodsList[i]);
            }
            // �_�h��ܪŮ�l
            else
            {
                GameObject ��Ыت���l = Instantiate(tempGrid, itemFieldBG);
                ��Ыت���l.SetActive(true);
            }
        }
        itemFieldBG.sizeDelta = new Vector2(itemFieldBG.sizeDelta.x, 200f + (ItemManager.instance.AllItemData.Length * 205f));
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
