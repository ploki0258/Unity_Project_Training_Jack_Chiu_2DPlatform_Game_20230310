using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
	[SerializeField] Image 底色;
	[SerializeField] Image 圖示;
	[SerializeField] Text 數量;
	[SerializeField] Text 名稱;
	[SerializeField] Text 描述;
	[SerializeField] 道具欄使用按鈕 btnUse = null;

	Item dataGrid;
	bool isNoneGrid = true; // 是否為空格子
	public float 恢復HP;
	public float 恢復MP;
	public float 提升攻擊力;
	public float 提升防禦力;
	public float 提升跳躍力;
	public float 提升攻擊速度;
	public float 提升移動速度;
	public float 魔力消耗降低;
	public float 提升技能傷害;
	public int 獲得額外點數;
	public int 增加欄位;

	public void InputData(Goods data)
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
			btnUse.目前選到的格子 = null;
			return;
		}
		else
		{
			描述.text = dataGrid.info;
			btnUse.目前選到的格子 = this;
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
			// 根據道具效果做各種事情
			SaveManager.instance.playerData.playerHP += dataGrid.恢復HP;
			Mathf.Clamp(SaveManager.instance.playerData.playerHP, SaveManager.instance.playerData.playerHP,PlayerCtrl.instance.maxHP);
			SaveManager.instance.playerData.playerMP += dataGrid.恢復MP;
			Mathf.Clamp(SaveManager.instance.playerData.playerMP, SaveManager.instance.playerData.playerMP, PlayerCtrl.instance.maxMP);
			提升攻擊力 += dataGrid.提升攻擊力;
			提升防禦力 += dataGrid.提升防禦力;
			提升跳躍力 += dataGrid.提升跳躍力;
			提升攻擊速度 += dataGrid.提升攻擊速度;
			提升移動速度 += dataGrid.提升移動速度;
			獲得額外點數 += dataGrid.獲得額外點數;
			魔力消耗降低 += dataGrid.魔力消耗降低;
			提升技能傷害 += dataGrid.提升技能傷害;
			增加欄位 += dataGrid.增加欄位;

			// 不可被使用的東西 就不執行
			if (dataGrid.canUse == false)
			{
				return;
			}
			// 如果會消耗 就減少一個
			else if (dataGrid.Consumables == true)
			{
				SaveManager.instance.playerData.RemoveItem(dataGrid.id);
				Debug.Log("消耗" + dataGrid.title);
			}
		}
	}
}
