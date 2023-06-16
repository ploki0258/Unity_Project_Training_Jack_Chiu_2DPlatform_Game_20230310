using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    // �N DialogSystem �]�w����ҼҦ�
    public static DialogSystem instance = null;

    [SerializeField] Image ����ϥ�_�� = null;
    [SerializeField] Image ����ϥ�_�k = null;
    [SerializeField] Text ��ܤH�W_�� = null;
    [SerializeField] Text ��ܤH�W_�k = null;
    [SerializeField] Text ��ܤ��e = null;
    [SerializeField] Transform �~�򴣥� = null;
    [SerializeField] ��ܤ奻 ���դ奻 = null;
    [SerializeField] GameObject Button;
    [SerializeField] CanvasGroup talkUI;

    ��ܤ奻 ��e�奻;
    public bool ��ܤ� = false;    // �O�_�b���
    bool pressEnter = false;      // �O�_���F�~��
    bool wait = false;            // �O�_�b����
    
    private void Awake()
    {
        instance = this;         // ����ҵ���ۤv
        talkUI.alpha = 0f;
    }

    /// <summary>
    /// �}�l���
    /// </summary>
    /// <param name="�奻">�奻���e</param>
    public void �}�l���(��ܤ奻 �奻)
    {
        // �p�G���b��ܴN�������R�O
        if (��ܤ� == true)
        {
            // Debug.Log("�w�g�b��ܤF");
            return;
        }
        // ���o�奻
        ��e�奻 = �奻;
        // �}�l���B������
        StartCoroutine(���());
    }

    IEnumerator ���()
    {
        ��ܤ� = true;
        talkUI.alpha = 1f;
        // ��ܥ��b��ܪ�����W��
        // ��ܤH�W.text = ��e�奻.��[0].����W��;
        // ��ܥ��b��ܪ�����ϥ�
        // �p�G�O���䪺���� �N��ܨ���ϥܥ�
        // �p�G�O�k�䪺���� �N��ܨ���ϥܥk
        // ���� ? ���߰����Ʊ� : �����߰����Ʊ�
        ����ϥ�_��.sprite = ��e�奻.��[0].���䨤�� ? ��e�奻.��[0].����ϥܥ� : null;
        ����ϥ�_�k.sprite = ��e�奻.��[0].���䨤�� ? null : ��e�奻.��[0].����ϥܥk;

        // ����ϥܥ�.transform.localScale = �p�G����ϥܥ��� null �N���� �_�h�N���
        // ����ϥܥk.transform.localScale = �p�G����ϥܥk�� null �N���� �_�h�N���
        ����ϥ�_��.transform.localScale = (����ϥ�_��.sprite == null) ? Vector3.zero : Vector3.one;
        ����ϥ�_�k.transform.localScale = (����ϥ�_�k.sprite == null) ? Vector3.zero : Vector3.one;
        /*
        if (��e�奻.��[0].���䨤�� == true)
        {
            ����ϥ�_�� = ��e�奻.��[0].����ϥܥ�;
        }
        else if(��e�奻.��[0].���䨤�� == false)
        {
            ����ϥ�_�k = ��e�奻.��[0].����ϥܥk;
        }
        */

        // �p�G�O���䪺�H�W �N��ܤH�W�b���䪺��r����� �p�G���O�N��J�ť�
        ��ܤH�W_��.text = ��e�奻.��[0].���䨤�� ? ��e�奻.��[0].����W�� : "";
        ��ܤH�W_�k.text = ��e�奻.��[0].���䨤�� ? "" : ��e�奻.��[0].����W��;
        ��ܤ��e.text = "";
        �~�򴣥�.localScale = Vector3.zero;
        //����0.5��
        yield return new WaitForSeconds(0.5f);

        // ����`��
        for (int j = 0; j < ��e�奻.��.Count; j++)
        {
            // �}�l�o�y�ܤ��e�]�w�n�H�W�åB��������

            ����ϥ�_��.sprite = ��e�奻.��[j].���䨤�� ? ��e�奻.��[j].����ϥܥ� : null;
            ����ϥ�_�k.sprite = ��e�奻.��[j].���䨤�� ? null : ��e�奻.��[j].����ϥܥk;

            ����ϥ�_��.transform.localScale = (����ϥ�_��.sprite == null) ? Vector3.zero : Vector3.one;
            ����ϥ�_�k.transform.localScale = (����ϥ�_�k.sprite == null) ? Vector3.zero : Vector3.one;

            ��ܤH�W_��.text = ��e�奻.��[j].���䨤�� ? ��e�奻.��[j].����W�� : "";
            ��ܤH�W_�k.text = ��e�奻.��[j].���䨤�� ? "" : ��e�奻.��[j].����W��;
            �~�򴣥�.localScale = Vector3.zero;
            //�v�B��ܨC�@�Ӧr��e���W
            string textFinal = "";
            for (int i = 0; i < ��e�奻.��[j].�奻���e.Length; i++)
            {
                // ���X�Ӧr�]�O�Ӱj��
                textFinal = textFinal + ��e�奻.��[j].�奻���e[i];
                // ��ܨ�e���W
                ��ܤ��e.text = textFinal;
                yield return new WaitForSeconds(0.05f);
            }
            // ����~�򴣥� �����a���F�~��
            �~�򴣥�.localScale = Vector3.one;
            // ���UEnter�~��}�l�U�q���
            wait = true;
            while (pressEnter == false)
            {
                yield return new WaitForSeconds(0.1f);
            }
            wait = false;
            pressEnter = false;
        }

        talkUI.alpha = 0f;
        yield return new WaitForSeconds(0.5f);
        ��ܤ� = false;
    }

    private void Update()
    {
        // �� Enter �~��U�q���
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && wait == true)
        {
            pressEnter = true;
        }
    }
}
