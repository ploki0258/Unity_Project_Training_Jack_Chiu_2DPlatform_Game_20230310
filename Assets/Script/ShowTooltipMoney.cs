using UnityEngine;
using UnityEngine.UI;

public class ShowTooltipMoney : MonoBehaviour
{
	[SerializeField, Header("金幣顯示動畫")]
	Animator showCoinAni = null;

	private void Start()
	{
		// 登記要跟著金錢變化並且手動刷新一次
		SaveManager.instance.playerData.renewCoin += RenewCoinAni;
		RenewCoinAni();
	}

	private void OnDisable()
	{
		Debug.Log("405");
		// 退出登記
		SaveManager.instance.playerData.renewCoin -= RenewCoinAni;
	}

	/// <summary>
	/// 更新金幣顯示動畫
	/// </summary>
	void RenewCoinAni()
	{
		// 播放動畫
		showCoinAni.SetTrigger("金幣顯示");
	}
}
