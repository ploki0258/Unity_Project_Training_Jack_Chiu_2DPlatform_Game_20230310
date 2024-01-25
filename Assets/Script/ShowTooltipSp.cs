using UnityEngine;

public class ShowTooltipSp : MonoBehaviour
{
	[SerializeField, Header("技能點數顯示動畫")]
	Animator showSkillPointAni = null;

	private void Start()
	{
		// 登記要跟著技能點變化並且手動刷新一次
		SaveManager.instance.playerData.renewSkillPoint += RenewSkillPointAni;
		RenewSkillPointAni();
	}

	private void OnDisable()
	{
		Debug.Log("506");
		// 退出登記
		SaveManager.instance.playerData.renewSkillPoint -= RenewSkillPointAni;
	}

	/// <summary>
	/// 更新金幣顯示動畫
	/// </summary>
	void RenewSkillPointAni()
	{
		// 播放動畫
		showSkillPointAni.SetTrigger("技能點數顯示");
	}
}
