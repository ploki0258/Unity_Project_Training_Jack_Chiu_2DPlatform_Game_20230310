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
		SaveManager.instance.playerData.renewSkillPoint += UpdateSkillPointUI;
		UpdateSkillPointUI();
	}
	private void OnDisable()
	{
		Debug.Log("技能點數被關閉或被刪除");
		// 退出登記
		SaveManager.instance.playerData.renewSkillPoint -= UpdateSkillPointUI;
	}
	
	private void Reset()
	{
		spText = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>更新技能點數的顯示</summary>
	void UpdateSkillPointUI()
	{
		spText.text = "× " + SaveManager.instance.playerData.skillPoint.ToString("N0") + "點";
	}
}
