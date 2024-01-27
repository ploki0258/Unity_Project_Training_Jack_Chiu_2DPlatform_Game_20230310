using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class ShowSaveMessage : MonoBehaviour
{
	[SerializeField, Header("訊息文字")] Text textSave = null;
	[SerializeField, Header("顯示訊息文字")] string saveText = null;

	Animator aniSave;

	private void Awake()
	{
		aniSave = GetComponent<Animator>();
	}

	private void Start()
	{
		SaveManager.instance.renewSave += UpdateSaveMessageUI;
		SaveManager.instance.renewSave += RenewSaveMessageAni;

		textSave.text = "";
	}

	private void OnDisable()
	{
		SaveManager.instance.renewSave -= UpdateSaveMessageUI;
		SaveManager.instance.renewSave -= RenewSaveMessageAni;
	}

	/// <summary>更新存檔訊息的顯示</summary>
	public void UpdateSaveMessageUI()
	{
		textSave.text = saveText;
	}

	/// <summary>
	/// 更新存檔訊息顯示動畫
	/// </summary>
	void RenewSaveMessageAni()
	{
		// 播放動畫
		aniSave.SetTrigger("play");
	}
}
