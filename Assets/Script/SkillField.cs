using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
    [SerializeField, Header("�ޯ�ϥ�")]
    Image iconSkill;
    [SerializeField, Header("�ޯ�W��")]
    Text titleSkill;
    [SerializeField, Header("�ޯ໡��")]
    Text[] infoSkill;
    [SerializeField, Header("�ޯ���s")]
    GameObject btnSkill;
    
    bool alreSkill;     // �O�_�w�ǲ߸ӧޯ�
    Skill skillData;    // �ޯ���

    public void ��l�Ƨޯ�(int id)
	{
        // �d����
        skillData = SkillManager.instance.FindSkillByID(id);

        iconSkill.sprite = skillData.skillIcon;
        titleSkill.text = skillData.skillName;
        infoSkill[0].text = skillData.skillDis;
        infoSkill[1].text = skillData.skillCoinCost.ToString() + "\n" + skillData.skillPointCost;
        // �p�G���a�S���ӧޯ઺�� �~��ܫ��s
        alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
        btnSkill.SetActive(alreSkill == false);
    }

    /// <summary>
    /// �ǲߧޯ�
    /// </summary>
    public void LearnSkill()
	{
        // �P�_��_�ǲߧޯ�
        if (SaveManager.instance.playerData.moneyCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
        {
            Debug.Log("�ɯŧޯ�G " + skillData.id);
            // ����
            SaveManager.instance.playerData.moneyCount -= skillData.skillCoinCost;
            // ���ޯ�
            SaveManager.instance.playerData.skillPoint -= skillData.skillPointCost;
            // �쪱�a������ �K�[�ӫ~���(ID)
            SaveManager.instance.playerData.haveSkill.Add(skillData.id);
            // �R����i��s��
            SaveManager.instance.SaveData();

            // ��ʨ�s�@���ӫ~
            // �ӫ�����.ins.��s�ө�();
        }
        else
        {
            Debug.Log("�������I�Ƥ���");
        }
    }
}
