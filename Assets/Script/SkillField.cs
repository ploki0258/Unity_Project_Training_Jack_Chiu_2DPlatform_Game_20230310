using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
    [SerializeField, Header("技能圖示")]
    Image icon;
    [SerializeField, Header("技能名稱")]
    Text title;
    [SerializeField, Header("技能說明")]
    Text[] info;
    [SerializeField, Header("技能按鈕")]
    Button skillBtn;
    [SerializeField, Header("是否已學習該技能")]
    bool alreSkill;
}
