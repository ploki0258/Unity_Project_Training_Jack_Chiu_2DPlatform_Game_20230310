using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform[] skillSlot; // �ޯ����A�Ω󱵨��ޯ઺���
	public int currentSkillIndex; // ��e��ܪ��ޯ�
	Skill skillData;

	private void Update()
	{
		ChangeAtkObject();
	}

	/// <summary>
	/// �]�m��e�ޯ�
	/// </summary>
	/// <param name="skill">�ޯ�ID</param>
	public void SetCurrentSkill(int skill)
	{
		// �b�o�̥i�H����������I��ޯ઺�欰
		currentSkillIndex = skill;
	}

	/// <summary>
	/// ������������
	/// </summary>
	void ChangeAtkObject()
	{
		List<int> skillPrefabs = SaveManager.instance.playerData.haveSkill;

		SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
		Skill skill = FindObjectOfType<Skill>();
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			currentSkillIndex = 0;
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			currentSkillIndex = 1;
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			currentSkillIndex = 2;
		}

		// �ھڷ�e�ޯ���޳]�m��������
		if (currentSkillIndex >= 0 && currentSkillIndex < skillPrefabs.Count)
		{
			int skillID = skillData.id;
			skillSystem.SetCurrentSkill(skillID);

			PlayerCtrl.instance.atkObject = skill.skillPrefab;
			Debug.Log("�������G" + skill.skillPrefab.name);
		}
	}

}
