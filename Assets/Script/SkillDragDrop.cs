using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject clonePrefab;      // �n�J��������w�m��
	public Collider2D targetArea;       // �ؼаϰ쪺�I����
	public Skill skillData;

	private GameObject cloneObject;     // �ͦ����J������
	private GameObject reservedObject;  // �O�d������
	private bool isDragging = false;    // �O�_���b�즲
	private Transform originalParent;   // ��l������
	private Vector3 startPosition;      // ��l��m
	private int skillID;

	/// <summary>
	/// �}�l���
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent;  // �O�s��l������
		startPosition = transform.position; // �O�s��l��m

		// �ͦ��J������
		cloneObject = Instantiate(clonePrefab, transform.position, transform.rotation);

		// ��s�ޯ��񪫥󪺦�m
		cloneObject.transform.position = eventData.position;

		if (transform.parent.name == "�ޯ��")
		{
			// �N�ޯ��񪫥󪺤�����]�m���ޯ����
			cloneObject.transform.SetParent(originalParent.parent.parent.parent.parent);
		}

		if (transform.parent.name == "�ޯ������")
		{
			cloneObject.transform.SetParent(targetArea.transform.parent);
		}

		GetComponent<CanvasGroup>().blocksRaycasts = false; // �����g�u�˴��A�H�K���������|���ר�L�ƥ�

		// �}�l�즲
		isDragging = true;
		Debug.Log("�}�l�즲");
	}

	/// <summary>
	/// �i����
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		if (isDragging)
		{
			// ��s�ޯ��񪫥󪺦�m
			cloneObject.transform.position = eventData.position;

			Debug.Log(eventData.pointerCurrentRaycast.gameObject.tag);
			Debug.Log(eventData.pointerCurrentRaycast.gameObject.gameObject);
			Debug.Log("�즲��");
		}
	}

	/// <summary>
	/// �������
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		// ����즲
		isDragging = false;

		transform.SetParent(originalParent);	// �N�ޯ��񪫥󪺤�����]�m�^��l������
		transform.position = startPosition;		// �N�ޯ��񪫥󪺦�m�]�m�^��l��m
		
		cloneObject.transform.SetParent(originalParent);    // �ƻs���󪺤�����]�m�^��l������
		cloneObject.transform.position = startPosition;     // �ƻs���󪺦�m�]�m�^��l��m
		GetComponent<CanvasGroup>().blocksRaycasts = true;	// ���}�g�u�˴�

		// �T�{�ޯ�O�_��m�b�ޯ���줺
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot"))
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;

			// �p�G�ޯ���줺���W�٧t�� "Skill" ����
			// �մ���m
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill") == true)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);			// �]�m����
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;  // �]�m��m
				eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
				eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				Debug.Log("���");
				return;
			}
			// �p�G�ޯ���줺�S���W�٧t�� "Skill" ����
			// �]�m��m
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill") == false)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				Debug.Log("�]�m");
				
				// cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
				// cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
			}

			// ����ޯ�t�Ψó]�m��e��ܪ��ޯ�
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				skillID = skillData.id;
				skillSystem.SetCurrentSkill(skillID);
				Debug.Log("�]�m�ޯ�G" + skillData.name);
			}
		}
		Debug.Log("�����즲");
	}
}
