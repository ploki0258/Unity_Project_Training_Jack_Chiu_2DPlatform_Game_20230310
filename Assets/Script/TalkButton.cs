using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [SerializeField] GameObject Button = null;
    [SerializeField] Animator archiveAni = null;

    private void Update()
    {
        // HideUI();
        Archive();
    }

    // �� ���a �a��NPC�� �N��ܹ�ܴ��ܫ��s
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);     //��ܴ��ܫ��s
            archiveAni.SetBool("play", true);
        }
    }

    // �� ���a ���}NPC�� �N���ù�ܴ��ܫ��s
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);    //���ô��ܫ��s
            archiveAni.SetBool("play", false);
        }
    }

    /// <summary>
    /// �O���I�\��
    /// </summary>
    public void Archive()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
		{
            SaveManager.instance.SaveData();
		}
    }

    /// <summary>
    /// ��ܹ�ܤ����A�ð���}�l���
    /// </summary>
    /*public void HideUI()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            // �p�G�w�g�}�l���
            if (DialogSystem.instance.��ܤ� == false)
            {
                // �����ܨt�Τ��� �}�l���
                DialogSystem.instance.�}�l���(lines);
            }
        }
    }
    */
}
