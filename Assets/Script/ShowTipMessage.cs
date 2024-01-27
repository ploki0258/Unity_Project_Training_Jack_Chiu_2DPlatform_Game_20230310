using UnityEngine;
using UnityEngine.UI;

public class ShowTipMessage : MonoBehaviour
{
	[SerializeField, Header("數值類型")] public valueType type = valueType.None;
	[SerializeField, Header("訊息文字")] Text textTip = null;
	public string valueName;
	Animator aniTip;

	public float 增加數值;
	//public float 恢復HP;
	//public float 恢復MP;
	//public float 提升攻擊力;
	//public float 提升防禦力;
	//public float 提升跳躍力;
	//public float 提升攻擊速度;
	//public float 提升移動速度;
	//public float 魔力消耗降低;

	private void Awake()
	{
		aniTip = GetComponent<Animator>();
	}

	private void Start()
	{
		SaveManager.instance.playerData.renewPlayerHpMax += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerMpMax += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerAttack += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerDefense += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerJump += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerSpeed += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerAttackSpeed += UpdateTipMessageUI;
		SaveManager.instance.playerData.renewCostMP += UpdateTipMessageUI;

		SaveManager.instance.playerData.renewPlayerHpMax += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerMpMax += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerAttack += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerDefense += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerJump += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerSpeed += RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerAttackSpeed += RenewTipMessageAni;
		SaveManager.instance.playerData.renewCostMP += RenewTipMessageAni;

		textTip.text = "";
		UpdateTipMessageUI();
	}

	private void OnDisable()
	{
		SaveManager.instance.playerData.renewPlayerHpMax -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerMpMax -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerAttack -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerDefense -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerJump -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerSpeed -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewPlayerAttackSpeed -= UpdateTipMessageUI;
		SaveManager.instance.playerData.renewCostMP -= UpdateTipMessageUI;

		SaveManager.instance.playerData.renewPlayerHpMax -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerMpMax -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerAttack -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerDefense -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerJump -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerSpeed -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewPlayerAttackSpeed -= RenewTipMessageAni;
		SaveManager.instance.playerData.renewCostMP -= RenewTipMessageAni;
	}

	/// <summary>更新提示訊息的顯示</summary>
	public void UpdateTipMessageUI()
	{
		SwitchValueText();
		textTip.text = valueName + " +" + 增加數值;
	}

	/// <summary>
	/// 更新提示訊息顯示動畫
	/// </summary>
	void RenewTipMessageAni()
	{
		// 播放動畫
		aniTip.SetTrigger("play");
	}

	/// <summary>
	/// 依據數值類型變更文字內容
	/// </summary>
	void SwitchValueText()
	{
		switch (type)
		{
			case valueType.None:
				valueName = "";
				break;
			case valueType.HpMax:
				valueName = "生命上限";
				break;
			case valueType.MpMax:
				valueName = "魔力上限";
				break;
			case valueType.Attack:
				valueName = "攻擊力";
				break;
			case valueType.Defense:
				valueName = "防禦力";
				break;
			case valueType.Jump:
				valueName = "跳躍力";
				break;
			case valueType.Speed:
				valueName = "移動速度";
				break;
			case valueType.AttackSpeed:
				valueName = "施法速度";
				break;
			case valueType.Cost:
				valueName = "魔力消耗";
				break;
			default:
				Debug.Log("查無此類型");
				break;
		}
	}

	/// <summary>
	/// 數值類型
	/// </summary>
	public enum valueType
	{
		None,
		HpMax,
		MpMax,
		Attack,
		Defense,
		Speed,
		Jump,
		AttackSpeed,
		Cost,
	}
}
