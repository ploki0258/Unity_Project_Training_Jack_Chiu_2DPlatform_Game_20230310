using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrag : MonoBehaviour
{
    public GameObject clonePrefab; // �n�J��������w�m��
    public Collider2D targetArea; // �ؼаϰ쪺�I����

    private bool isDragging = false; // �аO�O�_���b�즲
    private GameObject cloneObject; // �ͦ����J������
    private Vector3 offset; // �즲�������q

    private GameObject reservedObject; // �O�d������

    private void OnMouseDown()
    {
        // �ͦ��J������
        cloneObject = Instantiate(clonePrefab, transform.position, transform.rotation);

        // �p��즲�������q
        offset = cloneObject.transform.position - GetMouseWorldPosition();

        // �}�l�즲
        isDragging = true;
    }

    private void OnMouseUp()
    {
        // ����즲
        isDragging = false;

        // �ˬd�O�_�b�ؼаϰ줺
        if (targetArea.bounds.Contains(cloneObject.transform.position))
        {
            // �b�ؼаϰ줺�A�O�d����
            reservedObject = cloneObject;
            // �i�H�b�o�̰����L�ާ@��Ĳ�o�ƥ�
        }
        else
        {
            // ���b�ؼаϰ줺�A�P������
            Destroy(cloneObject);
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            // ��s�J�������m���ƹ���m�[�W�����q
            cloneObject.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // �N�ƹ���m�ഫ���@�ɮy��
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}