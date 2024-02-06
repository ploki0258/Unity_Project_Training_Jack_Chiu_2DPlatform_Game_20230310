using UnityEngine;
using UnityEngine.UI;

public class StatusWindows : Windows<StatusWindows>
{
	[SerializeField, Header("顯示文字")] Text textHp, textMp, textAttack, textDefense, textJump, textSpeed, textAttackSpeed, textCostMp;
	[SerializeField, Header("開啟按鍵")] KeyCode keyCode = KeyCode.R;

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
		if (Input.GetKeyDown(keyCode))
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

	// 打開視窗時 顯示滑鼠 時間暫停
	public override void Open()
	{
		base.Open();
		Time.timeScale = 0f;
		PlayerCtrl.instance.enabled = false;
		//Enemy.instance.enabled = false;
		// Cursor.lockState = CursorLockMode.None;
	}

	// 關閉視窗時 隱藏滑鼠 時間繼續
	public override void Close()
	{
		base.Close();
		Time.timeScale = 1f;
		PlayerCtrl.instance.enabled = true;
		//Enemy.instance.enabled = true;
		// Cursor.lockState = CursorLockMode.Locked;
	}

	/// <summary>
	/// 更新各項狀態的數值
	/// </summary>
	void UpdateStatusValueUI()
	{
		// 現在 - 原本 = 此次變化量
		//float originalCostMp = SaveManager.instance.playerData.costMP;

		textHp.text = "血量：" + SaveManager.instance.playerData.playerHP.ToString("F0") + " / " + SaveManager.instance.playerData.playerHpMax.ToString("F0");
		textMp.text = "魔力：" + SaveManager.instance.playerData.playerMP.ToString("F0") + " / " + SaveManager.instance.playerData.playerMpMax.ToString("F0");
		textAttack.text = "攻擊力：" + SaveManager.instance.playerData.playerAttack.ToString("F0");
		textDefense.text = "防禦力：" + SaveManager.instance.playerData.playerDefense.ToString("F0");
		textJump.text = "跳躍力：" + SaveManager.instance.playerData.playerJump.ToString("F0");
		textSpeed.text = "移動速度：" + SaveManager.instance.playerData.playerSpeed.ToString("F0");
		textAttackSpeed.text = "攻擊速度：" + SaveManager.instance.playerData.playerAttackSpeed.ToString("F0");
		textCostMp.text = "魔力消耗減輕：" + SaveManager.instance.playerData.costMP.ToString("F0") + "%";
	}
}
