using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemField : Windows<ItemField>
{
	[Header("格子模板")]
	[SerializeField] GameObject tempGrid = null;
	[Header("道具欄背景")]
	[SerializeField] RectTransform itemFieldBG = null;

	protected override void Awake()
	{
		base.Awake();
		openSpeed = 15f;    // 視窗開啟速度 = 15
		ItemManager.instance.Initialization();
	}

	protected override void Start()
	{
		base.Start();
		// Test
		// SaveManager.instance.playerData.AddItem(21);
		// SaveManager.instance.playerData.AddItem(21);
		// SaveManager.instance.playerData.AddItem(21);
		// SaveManager.instance.playerData.AddItem(21);
		// SaveManager.instance.playerData.AddItem(21);

		刷新道具欄();

		SaveManager.instance.playerData.Act_goodsChange += 刷新道具欄;
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.Act_goodsChange -= 刷新道具欄;
	}

	protected override void Update()
	{
		base.Update();
		// 按 Q鍵 開啟道具欄介面
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

	// 打開視窗時 顯示滑鼠 時間暫停
	public override void Open()
	{
		base.Open();
		// Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0f;
	}

	// 關閉視窗時 隱藏滑鼠 時間繼續
	public override void Close()
	{
		base.Close();
		// Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1f;
	}

	[SerializeField]
	List<GameObject> 垃圾桶 = new List<GameObject>();

	void 刷新道具欄()
	{
		// 清除上次暫存的格子
		foreach (var g in 垃圾桶)
           Destroy(g);
       // 重製陣列
       垃圾桶.Clear();

		// 清除上次暫存的格子
		/*
       for (int i = 0; i < 垃圾桶.Count; i++)
		{
			Destroy(垃圾桶[i].gameObject);
		}
		// 重製陣列
		垃圾桶.Clear();
       */

		// 格子模板本身不顯示
		tempGrid.SetActive(false);
		// i小於格子數量 20
		for (int i = 0; i < 20; i++)
		{
			// 如果i小於玩家持有的道具數量 就顯示道具
			if (i < SaveManager.instance.playerData.goodsList.Count)
			{
				// 顯示持有道具
				// 複製一個格子模板 並放進道具欄背景中
				GameObject 剛創建的格子 = Instantiate(tempGrid, itemFieldBG);
				// 叫出格子時先把它啟動
				剛創建的格子.SetActive(true);
				// 將物品資料傳送給格子處理
				剛創建的格子.GetComponent<Grid>().InputData(SaveManager.instance.playerData.goodsList[i]);

				垃圾桶.Add(剛創建的格子);
			}
			// 否則顯示空格子
			else
			{
				// 複製一個格子模板出來 並且放進道具欄背景中
				GameObject 剛創建的格子 = Instantiate(tempGrid, itemFieldBG);
				// 叫出格子時先把它啟動
				剛創建的格子.SetActive(true);

				垃圾桶.Add(剛創建的格子);
			}
		}
	}

	/*
    void 刷新道具欄()
    {
        // 根據有幾個道具生成幾個商品
        for (int i = 0; i < ItemManager.instance.AllItem.Length; i++)
        {
            GameObject 剛剛生成的商品 = Instantiate(tempGrid, itemFieldBG);
            剛剛生成的商品.SetActive(true);
            RectTransform UI位置 = 剛剛生成的商品.GetComponent<RectTransform>();

            UI位置.anchoredPosition = new Vector2(UI位置.anchoredPosition.x, 20f + (i * 220f));

            剛剛生成的商品.GetComponent<Grid>().輸入資料(SaveManager.instance.goodsList[i]);
        }
        itemFieldBG.sizeDelta = new Vector2(itemFieldBG.sizeDelta.x, 20f + (ItemManager.instance.AllItem.Length * 22f));
    }
    */
}
