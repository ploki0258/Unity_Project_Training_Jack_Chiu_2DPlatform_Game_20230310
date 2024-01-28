using UnityEngine;
using UnityEngine.UI;

public class StatusWindows : Windows<StatusWindows>
{
	[SerializeField, Header("顯示文字")] Text textHp, textMp, textAttack, textDefense, textJump, textSpeed, textAttackSpeed, textCostMp;

	protected override void Start()
	{
		base.Start();

		SaveManager.instance.playerData.renewPlayerHP += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerMP += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerHpMax += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerMpMax += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerAttack += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerDefense += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerJump += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerSpeed += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerAttackSpeed += UpdateStatusValueUI;
		SaveManager.instance.playerData.renewCostMP += UpdateStatusValueUI;

		UpdateStatusValueUI();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewPlayerHP -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerMP -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerHpMax -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerMpMax -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerAttack -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerDefense -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerJump -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerSpeed -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewPlayerAttackSpeed -= UpdateStatusValueUI;
		SaveManager.instance.playerData.renewCostMP -= UpdateStatusValueUI;
	}

	protected override void Update()
	{
		base.Update();
		// 按 R鍵 開啟狀態介面
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (isOpen == false)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Close();
	}

	/// <summary>
	/// 更新各項狀態的數值
	/// </summary>
	void UpdateStatusValueUI()
	{
		float originalCostMp = SaveManager.instance.playerData.costMP;

		textHp.text = "血量：" + SaveManager.instance.playerData.playerHP + " / " + SaveManager.instance.playerData.playerHpMax;
		textMp.text = "魔力：" + SaveManager.instance.playerData.playerMP + " / " + SaveManager.instance.playerData.playerMpMax;
		textAttack.text = "攻擊力：" + SaveManager.instance.playerData.playerAttack;
		textDefense.text = "防禦力：" + SaveManager.instance.playerData.playerDefense;
		textJump.text = "跳躍力：" + SaveManager.instance.playerData.playerJump;
		textSpeed.text = "移動速度：" + SaveManager.instance.playerData.playerSpeed;
		textAttackSpeed.text = "攻擊速度：" + SaveManager.instance.playerData.playerAttackSpeed;
		textCostMp.text = "魔力消耗：" + SaveManager.instance.playerData.costMP;
	}
}
