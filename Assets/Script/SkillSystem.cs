using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
	public Transform skillSlot; // �ޯ����A�Ω󱵨��ޯ઺���
	public Skill currentSkill; // ��e��ܪ��ޯ�

	public void SetCurrentSkill(Skill skill)
	{
		// �b�o�̥i�H����������I��ޯ઺�欰
		currentSkill = skill;
	}
}
