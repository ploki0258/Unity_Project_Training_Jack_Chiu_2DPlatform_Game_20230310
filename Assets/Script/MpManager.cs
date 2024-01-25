using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class MpManager : MonoBehaviour
{
	[Header("�]�O��")]
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
	/// ��s���a�]�O
	/// </summary>
	void RenewPlayerMP()
	{
		barMP.fillAmount = SaveManager.instance.playerData.playerMP / PlayerCtrl.instance.maxMP;
		//Debug.Log($"<color=blue>���a�]�O�G {SaveManager.instance.playerData.playerMP}</color>");
		//Debug.Log("<color=blue>�̤j�]�O�G</color>" + maxMP);
		//Debug.Log($"<color=blue>��e�]�O�G{barMP.fillAmount * 100}</color>");
	}
}
