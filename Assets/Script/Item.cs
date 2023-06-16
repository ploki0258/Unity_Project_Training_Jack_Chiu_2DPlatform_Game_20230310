using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �D��榡
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Add New Item")]
public class Item : ScriptableObject
{
    [Header("�D��ID")]
    public int id;
    [Header("�D��W��")]
    public string title;
    [Header("�D��ϥ�")]
    public Sprite icon;
    [TextArea(5, 5), Header("�D��ԭz")]
    public string info;
    [Header("�D�����O")]
    public bool itemSkill;
    [Header("�����C��")]
    public Color colorBG;
    [Header("�D��i�_�ϥ�")]
    public bool canUse;                 // �O�_�i�ϥ�
    [Header("�D��O�_�|����")]
    public bool Consumables;            // �ϥΫ�O�_�|����

    // �@��D��
    public float ��_HP;
    public float ��_MP;
    public float ���ɧ����O;
    public float ���ɨ��m�O;
    public float ���ɸ��D�O;
    public float ���ɧ����t��;
    public float ���ɲ��ʳt��;
    // �ޯ�D��
    public float �����B�~�I��;
    public float �]�O���ӭ��C;
    public float ���ɧޯ�ˮ`;
    public int �W�[���;
}
