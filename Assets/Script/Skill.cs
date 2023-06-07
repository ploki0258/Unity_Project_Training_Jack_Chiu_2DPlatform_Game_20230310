using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Add New Skill")]
public class Skill : ScriptableObject
{
    [Header("�ޯ�ID")]
    public int id;
    [Header("�ޯ�W��")]
    public string skillName;
    [Header("�ޯ�ϥ�")]
    public Sprite skillIcon;
    [Header("�ޯ�S��")]
    public GameObject skillPrefab;
    [Header("�ޯ����O")]
    public Color skillCate;
    [Header("�ޯ�ԭz")]
    [TextArea(5, 5)] public string skillDis;
    [Header("�ޯ�O��")]
    public int skillCoinCost;
    public int skillPointCost;
    [Header("�ˮ`��")]
    public float skillAttack;
    [Header("�I�k�t��")]
    public float skillSpeed;
    [Header("�ޯ����ɶ�")]
    public float skillHoldTime;
}
