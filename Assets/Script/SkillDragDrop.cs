using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform originalParent; // 初始父物件
	private Vector3 startPosition; // 初始位置

	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent; // 保存初始父物件
		startPosition = transform.position; // 保存初始位置

		// 將技能拖放物件的父物件設置為技能欄位
		transform.SetParent(originalParent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false; // 關閉射線檢測，以便拖放期間不會阻擋其他事件
	}

	public void OnDrag(PointerEventData eventData)
	{
		// 更新技能拖放物件的位置
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(originalParent); // 將技能拖放物件的父物件設置回初始父物件
		transform.position = startPosition; // 將技能拖放物件的位置設置回初始位置
		GetComponent<CanvasGroup>().blocksRaycasts = true; // 打開射線檢測

		// 確認技能是否放置在技能欄位內
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot"))
		{
			// 獲取技能系統並設置當前選擇的技能
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				SkillManager skill = GetComponent<SkillManager>();
				skillSystem.SetCurrentSkill(skill);
			}
		}
	}
}
