using UnityEngine;
using UnityEngine.UI;

public class StatusWindows : Windows<StatusWindows>
{
	[SerializeField, Header("��ܤ�r")] Text textHp, textMp, textAttack, textDefense, textJump, textSpeed, textAttackSpeed, textCostMp;
	[SerializeField, Header("�}�ҫ���")] KeyCode keyCode = KeyCode.R;

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

	// ���}������ ��ܷƹ� �ɶ��Ȱ�
	public override void Open()
	{
		base.Open();
		Time.timeScale = 0f;
		PlayerCtrl.instance.enabled = false;
		//Enemy.instance.enabled = false;
		// Cursor.lockState = CursorLockMode.None;
	}

	// ���������� ���÷ƹ� �ɶ��~��
	public override void Close()
	{
		base.Close();
		Time.timeScale = 1f;
		PlayerCtrl.instance.enabled = true;
		//Enemy.instance.enabled = true;
		// Cursor.lockState = CursorLockMode.Locked;
	}

	/// <summary>
	/// ��s�U�����A���ƭ�
	/// </summary>
	void UpdateStatusValueUI()
	{
		// �{�b - �쥻 = �����ܤƶq
		//float originalCostMp = SaveManager.instance.playerData.costMP;

		textHp.text = "��q�G" + SaveManager.instance.playerData.playerHP.ToString("F0") + " / " + SaveManager.instance.playerData.playerHpMax.ToString("F0");
		textMp.text = "�]�O�G" + SaveManager.instance.playerData.playerMP.ToString("F0") + " / " + SaveManager.instance.playerData.playerMpMax.ToString("F0");
		textAttack.text = "�����O�G" + SaveManager.instance.playerData.playerAttack.ToString("F0");
		textDefense.text = "���m�O�G" + SaveManager.instance.playerData.playerDefense.ToString("F0");
		textJump.text = "���D�O�G" + SaveManager.instance.playerData.playerJump.ToString("F0");
		textSpeed.text = "���ʳt�סG" + SaveManager.instance.playerData.playerSpeed.ToString("F0");
		textAttackSpeed.text = "�����t�סG" + SaveManager.instance.playerData.playerAttackSpeed.ToString("F0");
		textCostMp.text = "�]�O���Ӵ�G" + SaveManager.instance.playerData.costMP.ToString("F0") + "%";
	}
}
