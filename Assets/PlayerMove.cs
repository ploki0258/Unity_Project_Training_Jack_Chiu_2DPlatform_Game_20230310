using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �禡 �p���
    // �ѨM���D���F�� �j���D ���B�s�� �C��Ѳ����w�����J....
    // �j���D > �h�Ӥp���D
    // �j���D > �h�Ӥp���D
    // �j���D > �h�Ӥp���D
    // �j���D > �h�Ӥp���D
    // �j���D > �h�Ӥp���D
    // �X�A���j�p
    // ���D�Ӳ� => �զX���ƤӦh �u�@�q���|���
    // ���D�Ӥj => �i���ƨϥΪ��F��Ӥ�
    // A�Ǫ� (���m�����B��q�B�l���a�B���u�޿�....)
    // B�Ǫ� (�i�𵦲��B��q�B�l���a�B���u�޿�....)
    // A�Ǫ��{�� (�����޿�)
    // B�Ǫ��{�� (�����޿�)
    // ��q�޲z�{��
    // �ͪ��M���t��
    // ���u����

    // �禡�O�ѨM���D�̤p�����(�p�q��)
    // private �����{��(�i�ٲ�) public ���S���F��
    // float = �Ʀr���

    //���~�� �^�ǵ��G �W�� ��J���Ѽ�(,�I�j�}�h�ӭ�)
    private float �p��A�[B(float a, float b)
    {
        // �p�⧹���^�ǵ��G
        return a + b;
    }
    // void = ��� ���ݭn�^�ǭȮɨϥ�
    private void �p���`���Z()
    {
        float a = 10;
        float b = 20;
        float c = 30;
        float �`���Z = 0;
        �`���Z = �p��A�[B(a, b);
        �`���Z = �p��A�[B(�`���Z, c);
        // ��UI����`���Z
    }
    // ���`�p�⦨�Z �ӧQ�]�n�p�⦨�Z ����p�⦨�Z 9 * 3

    // ���O = ���
    // �H �� �� �ѹ�
    // ��float �Ʀr(�p���I, ���t��)
    // ulong �Ʀr(���, ����) (��)
    // long �Ʀr(���, ���t��) ()
    // ��int �Ʀr(���, ���t��) (21���h)
    // short �Ʀr(���, ���t��) (-32767~32767)
    // ushort �Ʀr(���, ����) (0~65535)
    // byte �Ʀr(���, ����) (0~255)
    // �Υ��Y�B�����j�B�֪�}�B�Ů�\���r��

    // 10/20 = 100/200
    // 20/100 = 1/5
    // �ƾǤW�ӻ� = ���k���w�O�۵�

    void �C�������n�����Ʊ�()
    {

    }

    // ����Q�R����
    private void OnDestroy()
    {
        // �h�q�C�������ƥ�
        GameManager.instance.�C�������ƥ� -= �C�������n�����Ʊ�;
    }

    // �{����l�ƪ��ɭԱN�|����@��
    private void Start()
    {
        // �q�\�C�������ƥ�
        GameManager.instance.�C�������ƥ� += �C�������n�����Ʊ�;

        /*GameObject �p�� = null;
        GameObject �p�� = null;
        GameObject �p�� = null;
        // ��p��������ʦ̰���
        �p��.transform.position = new Vector3(0f, 100f, 0f);
        �p��.transform.position = new Vector3(0f, 100f, 0f);
        �p��.transform.position = new Vector3(0f, 100f, 0f);

        // Array �}�C�n���w����
        GameObject[] �d��ޥ��쪺�F�� = new GameObject[3];
        �d��ޥ��쪺�F��[0] = �p��;
        �d��ޥ��쪺�F��[1] = �p��;
        �d��ޥ��쪺�F��[2] = �p��;

        // �j��̷ӱ���M�w���檺����
        for(int i = 0; i < �d��ޥ��쪺�F��.Length; i++)
        {
            �d��ޥ��쪺�F��[i].transform.position = new Vector3(0f, 100f, 0f);
        }*/

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                Debug.Log(i + " X " + j + " = " + (i*j));
            }
        }

        GameManager.instance.playerMoney = 1000;
    }
    // [SerializeField] �ǦC��
    [SerializeField] [Range(-1f, 1f)] float hp = 0f;// value Type ���a�O����
    [SerializeField] Rigidbody ���z���� = null;// reference type �O���{��

    [SerializeField] ���� �ڪ����q���� = new ����();

    public void ����(���� ��誺����)
    {
        float �ڥثe���� = 100f;
        �ڥثe���� = �ڥثe���� - ��誺����.�����O;
    }
}

[System.Serializable]
public struct ����
{
    public float �����O;
    public float ��z�O;
    public float �]�k�����O;
    public int �ݩ�;
    public Vector3 �����̪��y��;
}

