using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    [SerializeField] ��ܤ奻 lines;

    // �� ���a �a��NPC�� �N��ܹ�ܴ��ܫ��s
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);     //��ܹ�ܴ��ܫ��s
        }
    }

    // �� ���a ���}NPC�� �N���ù�ܴ��ܫ��s
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);    //���ù�ܴ��ܫ��s
        }
    }

    /// <summary>
    /// ��ܹ�ܤ����A�ð���}�l���
    /// </summary>
    public void HideUI()
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

    private void Update()
    {
        HideUI();
    }
}
