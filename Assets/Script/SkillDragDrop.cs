using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 技能拖曳設置系統
/// </summary>
public class SkillDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[Header("克隆物件")]
	public GameObject clonePrefab;      // 要克隆的物件預製物
	[Header("技能放置區")]
	public Collider2D targetArea;       // 目標區域的碰撞器
	[Header("技能資料")]
	public Skill skillData;             // 技能資料

	private GameObject cloneObject;     // 生成的克隆物件
	private GameObject reservedObject;  // 保留的物件
	private Transform originalParent;   // 初始父物件
	private Vector3 startPosition;      // 初始位置
	private bool isDragging = false;    // 是否正在拖曳
	private int skillID;                // 技能ID
	bool raycastTarget;

	private void Start()
	{
		// raycastTarget = GetComponent<Image>().raycastTarget;
	}

	/// <summary>
	/// 開始拖拽
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent;  // 保存初始父物件
		startPosition = transform.position; // 保存初始位置

		// 如果有該技能 才生成克隆物件
		if (SaveManager.instance.playerData.IsHaveSkill(skillData.id) && transform.parent.name == "技能樹")
		{
			cloneObject = Instantiate(clonePrefab, transform.position, transform.rotation);
			cloneObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			Debug.Log($"<color=#ff90ff>技能：{skillData.skillName + "未持有，無法設置"}</color>");
		}

		// 如果克隆物件為空 就不執行
		if (cloneObject == null)
			return;

		// 避免物件拖曳時被其他介面遮擋
		if (transform.parent.name == "技能樹") // 父物件的名稱是"技能樹"的話
		{
			// 將技能拖放物件的父物件設置為技能欄位
			cloneObject.transform.SetParent(originalParent.parent.parent.parent.parent.parent);

			// 初始父物件.尺寸 = Vector3(1, 1, 1)
			originalParent.localScale = Vector3.one;
		}
		else if (transform.parent.name.Contains("skillBtn"))  // 父物件的名稱包含"skillBtn"的話
		{
			// 初始父物件.尺寸 = Vector3(1.5, 1.5, 1.5)
			//originalParent.localScale = new Vector3(1.5f, 1.5f, 1.5f);

			return;
			// 設置父集
			//cloneObject.transform.SetParent(transform.parent);
		}

		// 關閉射線檢測，以便拖放期間不會阻擋其他事件
		cloneObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

		// 開始拖曳
		isDragging = true;
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

			// Debug.Log("偵測物件標籤：" + eventData.pointerCurrentRaycast.gameObject.tag);
			// Debug.Log("偵測物件名稱：" + eventData.pointerCurrentRaycast.gameObject.name);
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

		// 如果沒有克隆物件 就不執行
		if (cloneObject == null)
			return;

		cloneObject.transform.SetParent(originalParent);    // 複製物件的父物件設置回初始父物件
		cloneObject.transform.position = startPosition;     // 複製物件的位置設置回初始位置

		// 確認技能是否放置在技能欄位內
		// layer == 1 << 5 (圖層設置的方式)
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillSlot") == true)
		{
			// 設置複製物件的父集
			cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
			// 設置複製物件的位置
			cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

			// 打開射線檢測
			cloneObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
			// Debug.Log("設置");
			#region 測試保留
			// cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
			// cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
			// raycastTarget = false;
			#endregion
			// 獲取技能系統並設置當前選擇的技能
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				skillID = skillData.id;
				skillSystem.SetCurrentSkill(skillID);
				Debug.Log("<color=#f0f>設置技能：</color>" + skillData.name);
				skillSystem.SwitchAtkObject();
			}
			//SaveManager.instance.playerData.isSetSkill = true;
			//Debug.Log("<COLOR=#f0f>保存技能</color>");
		}

		// 如果技能欄位內有名稱有包含 "SkillIcon" 且技能圖示的父集 不是 原始父集 的話
		// 調換位置
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillIcon") == true && eventData.pointerCurrentRaycast.gameObject.transform.parent != originalParent)
		{
			cloneObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);   // 設置複製的物件的父集
			cloneObject.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position; // 設置複製的物件的位置
			eventData.pointerCurrentRaycast.gameObject.transform.SetParent(null);                           // 移除偵測的物件的父集

			cloneObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
			// Debug.Log("調換");

			// 獲取技能系統並調換當前選擇的技能
			SkillSystem skillSystem = FindObjectOfType<SkillSystem>();
			if (skillSystem != null)
			{
				skillID = skillData.id;
				skillSystem.SetCurrentSkill(skillID);
				Debug.Log("<color=#f69>調換技能：</color>" + skillData.name);
				skillSystem.SwitchAtkObject();
			}
			//SaveManager.instance.playerData.isSetSkill = true;
			//Debug.Log("<color=#f69>保存技能</color>");
		}

		cloneObject.GetComponent<CanvasGroup>().blocksRaycasts = true;  // 打開射線檢測
		#region 測試保留
		// 如果技能欄位內有名稱有包含 "SkillIcon" 的話
		/*if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SkillIcon"))
		{
			// Destroy(eventData.pointerCurrentRaycast.gameObject);

			// 設置碰觸物件的父集
			eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);
			// 設置碰觸物件的位置
			eventData.pointerCurrentRaycast.gameObject.transform.position = startPosition;
			// cloneObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

		}*/
		#endregion
	}
}
