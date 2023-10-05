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

	[Tooltip("格子資料")]
	Item dataGrid;
	[Tooltip("是否為空格子")]
	bool isNoneGrid = true; // 是否為空格子
	
	/*
	public float 恢復HP;
	public float 恢復MP;
	public float 提升防禦力;
	public float 提升跳躍力;
	public float 提升攻擊速度;
	public float 提升移動速度;
	public float 魔力消耗降低;
	public float 提升技能傷害;
	public int 獲得額外點數;
	public int 增加技能欄位;
	*/

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
		// 如果是空格子的話 就隱藏格子資訊
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
		// 否則就顯示該道具的描述
		// 目前選到的格子 等於 自己
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
			SaveManager.instance.playerData.playerHP =
				Mathf.Clamp(SaveManager.instance.playerData.playerHP, SaveManager.instance.playerData.playerHP, PlayerCtrl.instance.maxHP);
			SaveManager.instance.playerData.playerMP += dataGrid.恢復MP;
			SaveManager.instance.playerData.playerMP =
				Mathf.Clamp(SaveManager.instance.playerData.playerMP, SaveManager.instance.playerData.playerMP, PlayerCtrl.instance.maxMP);
			SaveManager.instance.playerData.playerAttack += dataGrid.提升攻擊力;
			SaveManager.instance.playerData.playerDefense += dataGrid.提升防禦力;
			SaveManager.instance.playerData.playerJump += dataGrid.提升跳躍力;
			SaveManager.instance.playerData.playerAttackSpeed += dataGrid.提升攻擊速度;
			SaveManager.instance.playerData.playerSpeed += dataGrid.提升移動速度;

			// 如果玩家有攻擊物件時 攻擊物件的魔力消耗 降低 道具資料的魔力消耗降低的值
			if (PlayerCtrl.instance.atkObject != null)
				PlayerCtrl.instance.atkObject.GetComponent<AttackObject>().skillData.skillCost += -dataGrid.魔力消耗降低;
			// 如果玩家有攻擊物件時 攻擊物件的技能傷害 增加 道具資料的提升技能傷害的值
			if (PlayerCtrl.instance.atkObject != null)
				PlayerCtrl.instance.atkObject.GetComponent<AttackObject>().skillData.skillDamage += dataGrid.提升技能傷害;

			// 如果此次獲得的技能點數為 0 則不呼叫事件更新
			if (dataGrid.獲得額外點數 == 0)
				SaveManager.instance.playerData.renewSkillPoint = null;
			SaveManager.instance.playerData.skillPoint += dataGrid.獲得額外點數;

			// 如果 "Mist Area" 存在的話
			if (GameObject.Find("Mist Area") == true)
			{
				bool inMistType_gree = MistManager.instance.inMist_gree;
				// 如果是綠色迷霧的話 道具的恢復效果變為扣除效果(負面效果)
				if (inMistType_gree == true)
				{
					SaveManager.instance.playerData.playerHP -= dataGrid.恢復HP;
					SaveManager.instance.playerData.playerHP =
						Mathf.Clamp(SaveManager.instance.playerData.playerHP, SaveManager.instance.playerData.playerHP, PlayerCtrl.instance.maxHP);
					SaveManager.instance.playerData.playerMP -= dataGrid.恢復MP;
					SaveManager.instance.playerData.playerMP =
						Mathf.Clamp(SaveManager.instance.playerData.playerMP, SaveManager.instance.playerData.playerMP, PlayerCtrl.instance.maxMP);
				}
			}

			// 不可被使用的東西 就不執行
			if (dataGrid.canUse == false)
			{
				return;
			}
			// 如果會消耗 就減少一個
			else if (dataGrid.Consumables == true)
			{
				SaveManager.instance.playerData.RemoveItem(dataGrid.id);
				// 到玩家紀錄中 移除道具列表(ID)
				SaveManager.instance.playerData.haveItem.Remove(dataGrid.id);
				// Debug.Log("消耗" + dataGrid.title);
			}
		}
	}
}
