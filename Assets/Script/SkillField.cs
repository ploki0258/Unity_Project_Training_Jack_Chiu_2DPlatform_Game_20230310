using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
    [SerializeField, Header("�ޯ�ϥ�")]
    Image icon;
    [SerializeField, Header("�ޯ�W��")]
    Text title;
    [SerializeField, Header("�ޯ໡��")]
    Text[] info;
    [SerializeField, Header("�ޯ���s")]
    Button skillBtn;
    [SerializeField, Header("�O�_�w�ǲ߸ӧޯ�")]
    bool alreSkill;
}
