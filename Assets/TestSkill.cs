using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
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
	
    /// <summary>
    /// �ޯ�w�s���G�s��ޯ�S�Ī��C��
    /// </summary>
    public List<GameObject> skillEffPrefab = new List<GameObject>();

    /// <summary>
    /// �I��k�N
    /// </summary>
    /// <param name="index"></param>
    public void CastSkill(int index)
    {
        // �p�G index �p�� 0  �� �j�󵥩� �C��Ӽ� �N�������
        if (index < 0 || index >= skillEffPrefab.Count)
            return;
        GameObject skillPfb = skillEffPrefab[index];
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
