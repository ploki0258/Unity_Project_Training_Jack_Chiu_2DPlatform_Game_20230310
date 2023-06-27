using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject clonePrefab;      // 要克隆的物件預置體
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

		// 更新技能拖放物件的位置
		cloneObject.transform.position = eventData.position;

		if (transform.parent.name == "技能樹")
		{
			// 將技能拖放物件的父物件設置為技能欄位
			cloneObject.transform.SetParent(originalParent.parent.parent.parent.parent);
		}

		if (transform.parent.name == "技能欄按紐")
		{
			cloneObject.transform.SetParent(targetArea.transform.parent);
		}

		GetComponent<CanvasGroup>().blocksRaycasts = false; // 關閉射線檢測，以便拖放期間不會阻擋其他事件

		// 開始拖曳
		isDragging = true;
		Debug.Log("開始拖曳");
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

			Debug.Log(eventData.pointerCurrentRaycast.gameObject.tag);
			Debug.Log(eventData.pointerCurrentRaycast.gameObject.gameObject);
			Debug.Log("拖曳中");
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

		transform.SetParent(originalParent);	// 將技能拖放物件的父物件設置回初始父物件
		transform.position = startPosition;		// 將技能拖放物件的位置設置回初始位置
		
		cloneObject.transform.SetParent(originalParent);    // 複製物件的父物件設置回初始父物件
		cloneObject.transform.position = startPosition;     // 複製物件的位置設置回初始位置
		GetComponent<CanvasGroup>().blocksRaycasts = true;	// 打開射線檢測

		// 確認技能是否放置在技能欄位內
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot"))
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;

			// 如果技能欄位內有名稱含有 "Skill" 的話
			// 調換位置
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill") == true)
			{
				cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);			// 設置父集
				cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;  // 設置位置
				eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
				eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				Debug.Log("對調");
				return;
			}
			// 如果技能欄位內沒有名稱含有 "Skill" 的話
			// 設置位置
			if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Skill") == false)
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
		Debug.Log("結束拖曳");
	}
}
