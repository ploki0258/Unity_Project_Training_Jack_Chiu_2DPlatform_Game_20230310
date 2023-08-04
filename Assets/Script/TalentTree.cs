using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
	[Header("�ޯ�ID")]
	public int[] id;
	[Header("�ޯ�ϥ�")]
	public SpriteRenderer[] iconSkillArray = null;
	[SerializeField, Header("�|���֦����C��")]
	Color clolrNoSkill;

	bool alreSkill;     // �O�_�w�ǲ߸ӧޯ�
	Skill skillData;    // �ޯ���

	public static TalentTree instance = null;

	private void Awake()
	{
		instance = this;
	}

	public void ShowSkillIcon(int id)
	{
		// �d����
		skillData = SkillManager.instance.FindSkillByID(id);

		for (int i = 0; i < iconSkillArray.Length; i++)
		{
			// �p�G�S���ӧޯ� �B�ӧޯ�L�e�m�ޯ� �h������ܹϥ�
			if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) == false && skillData.Pre_Skill == false)
			{
				iconSkillArray[i].sprite = skillData.skillIcon;
				Debug.Log("���i�ǲߪ��ޯ�");
			}
			// �p�G�S���ӧޯ� �B�|�����o�e�m�ޯ� ��ܦǶ�
			else if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) == false && skillData.Pre_Skill == true)
			{
				if (SaveManager.instance.playerData.IsHaveSkill(skillData.Pre_id) == false)
				{
					iconSkillArray[i].color = clolrNoSkill;
					Debug.Log("�|�L�i�ǲߪ��ޯ�");
				}
			}
		}
	}
}
