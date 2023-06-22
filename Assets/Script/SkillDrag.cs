using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrag : MonoBehaviour
{
    public GameObject clonePrefab; // 要克隆的物件預置體
    public Collider2D targetArea; // 目標區域的碰撞器

    private bool isDragging = false; // 標記是否正在拖曳
    private GameObject cloneObject; // 生成的克隆物件
    private Vector3 offset; // 拖曳的偏移量

    private GameObject reservedObject; // 保留的物件

    private void OnMouseDown()
    {
        // 生成克隆物件
        cloneObject = Instantiate(clonePrefab, transform.position, transform.rotation);

        // 計算拖曳的偏移量
        offset = cloneObject.transform.position - GetMouseWorldPosition();

        // 開始拖曳
        isDragging = true;
    }

    private void OnMouseUp()
    {
        // 停止拖曳
        isDragging = false;

        // 檢查是否在目標區域內
        if (targetArea.bounds.Contains(cloneObject.transform.position))
        {
            // 在目標區域內，保留物件
            reservedObject = cloneObject;
            // 可以在這裡執行其他操作或觸發事件
        }
        else
        {
            // 不在目標區域內，銷毀物件
            Destroy(cloneObject);
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            // 更新克隆物件位置為滑鼠位置加上偏移量
            cloneObject.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 將滑鼠位置轉換為世界座標
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}