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
		// 登記要跟著技能點變化並且手動刷新一次
		SaveManager.instance.playerData.renewSkillPoint += UpdateUI;
		UpdateUI();
	}
	private void OnDisable()
	{
		// 退出登記
		SaveManager.instance.playerData.renewSkillPoint -= UpdateUI;
	}
	
	private void Reset()
	{
		spText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>刷新技能點數</summary>
	void UpdateUI()
	{
		spText.text = SaveManager.instance.playerData.skillPoint.ToString("N0");
	}
}
