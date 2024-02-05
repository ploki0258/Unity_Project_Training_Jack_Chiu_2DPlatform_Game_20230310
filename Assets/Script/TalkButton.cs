using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [SerializeField, Header("閃爍提示")] GameObject Button = null;
    [SerializeField, Header("提示動畫")] Animator archiveAni = null;
    [SerializeField, Header("存檔點")] bool isArchive = false;

	private void Start()
	{
        // 一開始先隱藏提示
        if (isArchive == true)
        {
			Button.SetActive(false);
		}
	}

	private void Update()
    {
        // HideUI();
        Archive();
    }

    // 當 玩家 靠近NPC時 就顯示對話提示按鈕
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);     //顯示提示按鈕
            archiveAni.SetBool("play", true);
        }
    }

    // 當 玩家 離開NPC時 就隱藏對話提示按鈕
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);    //隱藏提示按鈕
            archiveAni.SetBool("play", false);
        }
    }

    /// <summary>
    /// 記錄點存檔功能
    /// </summary>
    public void Archive()
    {
        if (Button.activeInHierarchy == true && isArchive == true && Input.GetKeyDown(KeyCode.E))
		{
            SaveManager.instance.SaveData();
		}
	}

    /// <summary>
    /// 顯示對話介面，並執行開始對話
    /// </summary>
    /*public void HideUI()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            // 如果已經開始對話
            if (DialogSystem.instance.對話中 == false)
            {
                // 執行對話系統中的 開始對話
                DialogSystem.instance.開始對話(lines);
            }
        }
    }
    */
}
