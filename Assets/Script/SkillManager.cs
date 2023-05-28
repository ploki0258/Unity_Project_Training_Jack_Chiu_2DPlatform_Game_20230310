using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{
	[SerializeField] Transform pointSkill;
	[SerializeField] string skillName = null;
	public KeyCode activationKey;
	public GameObject skillPrefab = null;
	[SerializeField] Transform skillSlot;
	SkillManager currentSkill;

    // public List<GameObject> skillPrefab = new List<GameObject>();

    /*public void CastSkill(int index)
    {
        if (index < 0 || index >= skillPrefab.Count)
            return;
        GameObject skillPfb = skillPrefab[index];

        if (Input.GetKeyDown(KeyCode.Z))
            Instantiate(skillPfb, pointSkill.position, Quaternion.identity);
    }
    */

    private void Update()
    {
        CastSpell();
    }

    public void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(skillPrefab, pointSkill.position, Quaternion.identity);
        }
    }
}