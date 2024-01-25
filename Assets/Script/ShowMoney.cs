using UnityEngine;
using TMPro;

public class ShowMoney : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI coinText = null;

	private void Start()
	{
		// 登記要跟著金錢變化並且手動刷新一次
		SaveManager.instance.playerData.renewCoin += UpdateCoinUI;
		UpdateCoinUI();
	}

	private void OnDisable()
	{
		Debug.Log("404");
		// 退出登記
		SaveManager.instance.playerData.renewCoin -= UpdateCoinUI;
	}

	private void Reset()
	{
		coinText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>更新金錢數量的顯示</summary>
	void UpdateCoinUI()
	{
		coinText.text = "× " + SaveManager.instance.playerData.coinCount.ToString("N0");
	}
}
