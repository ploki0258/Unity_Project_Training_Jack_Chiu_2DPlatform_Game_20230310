using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowSp : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI spText = null;

	private void Start()
	{
		// �n�O�n��ۧޯ��I�ܤƨåB��ʨ�s�@��
		SaveManager.instance.playerData.renewSkillPoint += UpdateUI;
		UpdateUI();
	}
	private void OnDisable()
	{
		// �h�X�n�O
		SaveManager.instance.playerData.renewSkillPoint -= UpdateUI;
	}
	
	private void Reset()
	{
		spText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>��s�ޯ��I��</summary>
	void UpdateUI()
	{
		spText.text = SaveManager.instance.playerData.skillPoint.ToString("N0");
	}
}
