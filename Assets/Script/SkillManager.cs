using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{
    [Header("�ޯ��m")]
    [SerializeField] Transform pointSkill;
    [Header("�ޯ�W��")]
    [SerializeField] string skillName = null;
    [Header("�I�k����")]
    public KeyCode activationKey;
    [Header("�ޯ�S��")]
    public GameObject skillPrefab = null;
    [Header("��e�ޯ�S��")]
    SkillManager currentSkill;

    public List<GameObject> skillPrefabs = new List<GameObject>();

    /// <summary>
    /// �I��k�N
    /// </summary>
    /// <param name="index"></param>
    public void CastSkill(int index)
    {
        // �p�G index �p�� 0  �� �j�󵥩� �C��Ӽ� �N�������
        if (index < 0 || index >= skillPrefabs.Count)
            return;
        GameObject skillPfb = skillPrefabs[index];
        Instantiate(skillPfb, pointSkill.position, Quaternion.identity);
    }

    private void Update()
    {
        CastSpell();
    }

    /// <summary>
    /// �I��k�N(����)
    /// </summary>
    public void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(skillPrefab, pointSkill.position, Quaternion.identity);
        }
    }
}