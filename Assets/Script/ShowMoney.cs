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
		// �n�O�n��۪����ܤƨåB��ʨ�s�@��
		SaveManager.instance.playerData.renewCoin += UpdateUI;
		UpdateUI();
	}

	private void OnDisable()
	{
		// �h�X�n�O
		SaveManager.instance.playerData.renewCoin -= UpdateUI;
	}

	private void Reset()
	{
		coinText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>��s�����ƶq</summary>
	void UpdateUI()
	{
		coinText.text = SaveManager.instance.playerData.moneyCount.ToString("N0");
	}
}
