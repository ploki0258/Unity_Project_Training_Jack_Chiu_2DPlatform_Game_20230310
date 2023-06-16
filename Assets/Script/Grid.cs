using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
	[SerializeField] Image 底色;
	[SerializeField] Image 圖示;
	[SerializeField] Text 數量;
	[SerializeField] Text 名稱;
	[SerializeField] Text 描述;
	[SerializeField] GameObject btnUse = null;

	Item dataGrid;
	bool isNoneGrid = true; // 是否為空格子

	public void InputData(SaveManager.Goods data)
	{
		isNoneGrid = false;
		dataGrid = ItemManager.instance.FindItemData(data.id);

		底色.color = dataGrid.colorBG;
		圖示.transform.localScale = Vector3.one;
		圖示.sprite = dataGrid.icon;
		名稱.text = dataGrid.title;
		描述.text = dataGrid.info;
		數量.text = "×" + data.number.ToString();
	}

	private void OnEnable()
	{
		if (isNoneGrid == true)
		{
			圖示.transform.localScale = Vector3.zero;
			底色.color = Color.black;
			名稱.text = "";
			描述.text = "";
			數量.text = "";
		}
	}

	/// <summary>
	/// 按下道具欄格子
	/// </summary>
	public void PressGrid()
	{
		// 如果是空格子 就不執行
		if (isNoneGrid)
		{
			return;
		}
		else
		{
			描述.text = dataGrid.info;
		}
		// Debug.Log("按下" + dataGrid.title);
	}

	/// <summary>
	/// 按下按鈕
	/// </summary>
	public void PressButton()
	{
		if (btnUse != null)
		{
			// 不可被使用的東西 就不執行
			if (dataGrid.canUse == false)
			{
				return;
			}
			// 如果會消耗 就減少一個
			else if (dataGrid.Consumables == true)
			{
				SaveManager.instance.removeItem(dataGrid.id);
				Debug.Log("消耗" + dataGrid.title);
			}
			// 根據道具效果做各種事情
		}
	}
}
