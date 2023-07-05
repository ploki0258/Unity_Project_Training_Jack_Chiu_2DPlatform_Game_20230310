using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject clonePrefab;      // 要克隆的物件預製物
	public Collider2D targetArea;       // 目標區域的碰撞器
	public Skill skillData;

	private GameObject cloneObject;     // 生成的克隆物件
	private GameObject reservedObject;  // 保留的物件
	private bool isDragging = false;    // 是否正在拖曳
	private Transform originalParent;   // 初始父物件
	private Vector3 startPosition;      // 初始位置
	private int skillID;
	
	/// <summary>
	/// 開始拖拽
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent;  // 保存初始父物件
		startPosition = transform.position; // 保存初始位置
											
		// 生成克隆物件
		cloneObject = Instantiate(clonePrefab, transform.position, transform.rotation);

		if (transform.parent.name == "技能樹")
		{	
			// 將技能拖放物件的父物件設置為技能欄位
			cloneObject.transform.SetParent(originalParent.parent.parent.parent.parent.parent);
		}

		if (transform.parent.name == "技能欄按紐")
		{
			// 設置父集
			cloneObject.transform.SetParent(transform.parent);
			
			// 自己位置跟隨滑鼠的位置
			transform.position = eventData.position;
		}

		// 關閉射線檢測，以便拖放期間不會阻擋其他事件
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		// 開始拖曳
		isDragging = true;
		// Debug.Log("開始拖曳");
	}

	/// <summary>
	/// 進行拖拽
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		if (isDragging)
		{
			// 更新技能拖放物件的位置
			cloneObject.transform.position = eventData.position;

			Debug.Log("偵測物件標籤：" + eventData.pointerCurrentRaycast.gameObject.tag);
			Debug.Log("偵測物件名稱：" + eventData.pointerCurrentRaycast.gameObject.name);
			// Debug.Log("拖曳中");
		}
	}

	/// <summary>
	/// 結束拖拽
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		// 停止拖曳
		isDragging = false;

		// transform.SetParent(originalParent);    // 將技能拖放物件的父物件設置回初始父物件
		// transform.position = startPosition;     // 將技能拖放物件的位置設置回初始位置

		cloneObject.transform.SetParent(originalParent);    // 複製物件的父物件設置回初始父物件
		cloneObject.transform.position = startPosition;     // 複製物件的位置設置回初始位置
		

		// 確認技能是否放置在技能欄位內
		// layer == 1 << 5
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot") == true)
		{
			// 如果技能欄位內有名稱有包含 "Skill_" 的話
			// 調換位置
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill_") == true)
			{
				/*
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);           // 設置複製的物件的父集
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;  // 設置複製的物件的位置
				eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);							// 調換複製的物件的父集
				eventData.pointerCurrentRaycast.gameObject.transform.parent.position = startPosition;					// 調換複製的物件的位置
				*/
				// GetComponent<CanvasGroup>().blocksRaycasts = true;
				Debug.Log("對調");
				Debug.Log("父物件：" + originalParent.gameObject.name);
			}
			// 如果技能欄位內有名稱含有 "Btn_" 的話
			// 設置位置
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Btn_") == true)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				
				Debug.Log("設置");

				// cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
				// cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
			}

			// 獲取技能系統並設置當前選擇的技能
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				skillID = skillData.id;
				skillSystem.SetCurrentSkill(skillID);
				Debug.Log("設置技能：" + skillData.name);
			}
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;  // 打開射線檢測
															// Debug.Log("結束拖曳");
	}
}
