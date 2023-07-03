using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject clonePrefab;      // �n�J��������w�s��
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

		if (transform.parent.name == "�ޯ��")
		{	
			// �N�ޯ��񪫥󪺤�����]�m���ޯ����
			cloneObject.transform.SetParent(originalParent.parent.parent.parent.parent);
		}

		if (transform.parent.name == "�ޯ������")
		{
			// �]�m����
			cloneObject.transform.SetParent(transform.parent);
			
			// �ۤv��m���H�ƹ�����m
			transform.position = eventData.position;
		}

		// �����g�u�˴��A�H�K���������|���ר�L�ƥ�
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		// �}�l�즲
		isDragging = true;
		// Debug.Log("�}�l�즲");
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

			Debug.Log("����������ҡG" + eventData.pointerCurrentRaycast.gameObject.tag);
			Debug.Log("��������W�١G" + eventData.pointerCurrentRaycast.gameObject.name);
			// Debug.Log("�즲��");
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

		// transform.SetParent(originalParent);    // �N�ޯ��񪫥󪺤�����]�m�^��l������
		// transform.position = startPosition;     // �N�ޯ��񪫥󪺦�m�]�m�^��l��m

		cloneObject.transform.SetParent(originalParent);    // �ƻs���󪺤�����]�m�^��l������
		cloneObject.transform.position = startPosition;     // �ƻs���󪺦�m�]�m�^��l��m
		GetComponent<CanvasGroup>().blocksRaycasts = true;  // ���}�g�u�˴�

		// �T�{�ޯ�O�_��m�b�ޯ���줺
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot") == true)
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;

			// �p�G�ޯ���줺�����Ҭ� "Untagged" ����
			// �մ���m
			if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Untagged") == true)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);           // �]�m�ƻs�����󪺤���
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;  // �]�m�ƻs�����󪺦�m
				eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);							// �մ��ƻs�����󪺤���
				eventData.pointerCurrentRaycast.gameObject.transform.parent.position = startPosition;					// �մ��ƻs�����󪺦�m
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				// Debug.Log("���");
				Debug.Log("������G" + originalParent.gameObject.name);
			}
			// �p�G�ޯ���줺�S���W�٧t�� "Skill" ����
			// �]�m��m
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill") == false)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				// Debug.Log("�]�m");

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
		// Debug.Log("�����즲");
	}
}
