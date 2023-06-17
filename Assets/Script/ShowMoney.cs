using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowMoney : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI coinText = null;

	private void Start()
	{
		// 登記要跟著金錢變化並且手動刷新一次
		SaveManager.instance.playerData.renewCoin += UpdateUI;
		UpdateUI();
	}

	private void OnDisable()
	{
		// 退出登記
		SaveManager.instance.playerData.renewCoin -= UpdateUI;
	}

	private void Reset()
	{
		coinText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>刷新金錢數量</summary>
	void UpdateUI()
	{
		coinText.text = SaveManager.instance.playerData.moneyCount.ToString("N0");
	}
}
