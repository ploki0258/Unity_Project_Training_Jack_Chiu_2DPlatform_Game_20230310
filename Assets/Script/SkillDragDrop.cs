using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform originalParent; // ��l������
	private Vector3 startPosition; // ��l��m

	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent; // �O�s��l������
		startPosition = transform.position; // �O�s��l��m

		// �N�ޯ��񪫥󪺤�����]�m���ޯ����
		transform.SetParent(originalParent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false; // �����g�u�˴��A�H�K���������|���ר�L�ƥ�
	}

	public void OnDrag(PointerEventData eventData)
	{
		// ��s�ޯ��񪫥󪺦�m
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(originalParent); // �N�ޯ��񪫥󪺤�����]�m�^��l������
		transform.position = startPosition; // �N�ޯ��񪫥󪺦�m�]�m�^��l��m
		GetComponent<CanvasGroup>().blocksRaycasts = true; // ���}�g�u�˴�

		// �T�{�ޯ�O�_��m�b�ޯ���줺
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot"))
		{
			// ����ޯ�t�Ψó]�m��e��ܪ��ޯ�
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				SkillManager skill = GetComponent<SkillManager>();
				skillSystem.SetCurrentSkill(skill);
			}
		}
	}
}
