using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform[] skillSlot; // �ޯ����A�Ω󱵨��ޯ઺���
	public int currentSkillIndex; // ��e��ܪ��ޯ�
	Skill skillData;

	private void Awake()
	{
		skillData = FindObjectOfType<Skill>();
	}

	private void Update()
	{
		SwitchAtkObject();
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
	void SwitchAtkObject()
	{
		List<int> skillPrefabs = SaveManager.instance.playerData.haveSkill;

		SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
		Skill skill = FindObjectOfType<Skill>();
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			currentSkillIndex = skillSlot[0].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			// PlayerCtrl.instance.atkObject = skillSlot[0].GetComponent<Skill>().skillPrefab;
			Debug.Log("�ֱ���Z�G " + currentSkillIndex);
			// currentSkillIndex = 0;
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			currentSkillIndex = skillSlot[1].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			// PlayerCtrl.instance.atkObject = skillSlot[1].GetComponent<Skill>().skillPrefab;
			Debug.Log("�ֱ���X�G " + currentSkillIndex);
			// currentSkillIndex = 1;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			currentSkillIndex = skillSlot[2].GetComponentInChildren<SkillDragDrop>().skillData.id;
			SetCurrentSkill(currentSkillIndex);
			// PlayerCtrl.instance.atkObject = skillSlot[2].GetComponent<Skill>().skillPrefab;
			Debug.Log("�ֱ���C�G " + currentSkillIndex);
			// currentSkillIndex = 2;
		}

		// PlayerCtrl.instance.atkObject = skillSlot[currentSkillIndex].GetComponentInChildren<SkillDragDrop>().skillData.skillPrefab;
		Debug.Log(currentSkillIndex + PlayerCtrl.instance.atkObject.name);
		// �ھڷ�e�ޯ���޳]�m��������
		// if (currentSkillIndex >= 0)
		// {
		/*
		int skillID = skillData.id;
		skillSystem.SetCurrentSkill(skillID);
		PlayerCtrl.instance.atkObject = skillSlot[currentSkillIndex].GetComponent<SkillDragDrop>().skillData.skillPrefab;
		Debug.Log("�������G" + skill.skillPrefab.name);
		*/
		// }
	}

}
