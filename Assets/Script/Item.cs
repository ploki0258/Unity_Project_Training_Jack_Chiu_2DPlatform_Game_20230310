using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具格式
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Add New Item")]
public class Item : ScriptableObject
{
    [Header("道具ID")]
    public int id;
    [Header("道具名稱")]
    public string title;
    [Header("道具圖示")]
    public Sprite icon;
    [TextArea(5, 5), Header("道具敘述")]
    public string info;
    [Header("道具類別")]
    public bool itemSkill;
    [Header("底圖顏色")]
    public Color colorBG;
    [Header("道具可否使用")]
    public bool canUse;                 // 是否可使用
    [Header("道具是否會消耗")]
    public bool Consumables;            // 使用後是否會消耗

    // 一般道具
    public float 恢復HP;
    public float 恢復MP;
    public float 提升攻擊力;
    public float 提升防禦力;
    public float 提升跳躍力;
    public float 提升攻擊速度;
    public float 提升移動速度;
    // 技能道具
    public float 提升額外點數;
    public float 魔力消耗降低;
    public float 提升技能傷害;
    public int 增加欄位;
}
