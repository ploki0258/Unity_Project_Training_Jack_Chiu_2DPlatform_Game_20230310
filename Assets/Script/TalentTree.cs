using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
	[Header("�ޯ�ID")]
	public int[] id;
	[Header("�ޯ�ϥ�")]
	public Image[] iconSkillArray = null;
	[SerializeField, Header("�|���֦����C��")]
	Color clolrNoSkill;

	bool alreSkill;     // �O�_�w�ǲ߸ӧޯ�
	Skill skillData;    // �ޯ���

	private void ShowSkillIcon(int id)
	{
		// �d����
		skillData = SkillManager.instance.FindSkillByID(id);

		for (int i = 0; i < iconSkillArray.Length; i++)
		{
			// �p�G�S���ӧޯ� �B�ӧޯ�L�e�m�ޯ� �h���
			if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) == false && skillData.Pre_Skill == false)
			{
				iconSkillArray[i].sprite = skillData.skillIcon;
				Debug.Log("���i�ǲߪ��ޯ�");
			}
			else
			{
				iconSkillArray[i].color = clolrNoSkill;
				Debug.Log("�|�L�i�ǲߪ��ޯ�");
			}
		}
	}
}
