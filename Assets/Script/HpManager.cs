using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
	[Header("血量條")]
	public Image barHP = null;

	private void Start()
	{
		SaveManager.instance.playerData.renewPlayerHP += RenewPlayerHP;
		RenewPlayerHP();
	}

	private void Reset()
	{
		barHP = GetComponent<Image>();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewPlayerHP -= RenewPlayerHP;
	}

	/// <summary>
	/// 更新玩家血量
	/// </summary>
	void RenewPlayerHP()
	{
		barHP.fillAmount = SaveManager.instance.playerData.playerHP / PlayerCtrl.instance.maxHP;
	}
}
