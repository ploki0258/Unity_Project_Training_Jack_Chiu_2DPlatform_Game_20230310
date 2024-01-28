using UnityEngine;
using UnityEngine.UI;

public class StatusWindows : Windows<StatusWindows>
{
	[SerializeField, Header("��ܤ�r")] Text textHp, textMp, textAttack, textDefense, textJump, textSpeed, textAttackSpeed, textCostMp;

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
		// �� R�� �}�Ҫ��A����
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
	/// ��s�U�����A���ƭ�
	/// </summary>
	void UpdateStatusValueUI()
	{
		float originalCostMp = SaveManager.instance.playerData.costMP;

		textHp.text = "��q�G" + SaveManager.instance.playerData.playerHP + " / " + SaveManager.instance.playerData.playerHpMax;
		textMp.text = "�]�O�G" + SaveManager.instance.playerData.playerMP + " / " + SaveManager.instance.playerData.playerMpMax;
		textAttack.text = "�����O�G" + SaveManager.instance.playerData.playerAttack;
		textDefense.text = "���m�O�G" + SaveManager.instance.playerData.playerDefense;
		textJump.text = "���D�O�G" + SaveManager.instance.playerData.playerJump;
		textSpeed.text = "���ʳt�סG" + SaveManager.instance.playerData.playerSpeed;
		textAttackSpeed.text = "�����t�סG" + SaveManager.instance.playerData.playerAttackSpeed;
		textCostMp.text = "�]�O���ӡG" + SaveManager.instance.playerData.costMP;
	}
}
