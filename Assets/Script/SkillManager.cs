using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    [SerializeField] Transform pointSkill;

    public List<GameObject> skillPrefab = new List<GameObject>();

    public void CastSkill(int index)
    {
        if (index < 0 || index >= skillPrefab.Count)
            return;
        GameObject skillPfb = skillPrefab[index];

        if (Input.GetKeyDown(KeyCode.Z))
            Instantiate(skillPfb, pointSkill.position, Quaternion.identity);
    }
}
