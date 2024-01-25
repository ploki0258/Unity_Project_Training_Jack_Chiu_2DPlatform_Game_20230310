using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class MpManager : MonoBehaviour
{
	[Header("魔力條")]
	public Image barMP = null;

	private void Start()
	{
		SaveManager.instance.playerData.renewPlayerMP += RenewPlayerMP;
		RenewPlayerMP();
	}

	private void Reset()
	{
		barMP = GetComponent<Image>();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewPlayerMP -= RenewPlayerMP;
	}

	/// <summary>
	/// 更新玩家魔力
	/// </summary>
	void RenewPlayerMP()
	{
		barMP.fillAmount = SaveManager.instance.playerData.playerMP / PlayerCtrl.instance.maxMP;
		//Debug.Log($"<color=blue>玩家魔力： {SaveManager.instance.playerData.playerMP}</color>");
		//Debug.Log("<color=blue>最大魔力：</color>" + maxMP);
		//Debug.Log($"<color=blue>當前魔力：{barMP.fillAmount * 100}</color>");
	}
}
