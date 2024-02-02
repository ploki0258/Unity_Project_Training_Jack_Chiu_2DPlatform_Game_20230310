using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    static public FoodCreater instance = null;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] int x = 10;
    [SerializeField] int y = 5;
    [SerializeField] int z = 10;
    [SerializeField] int maxNumber = 10;
    /// <summary>Á`¼Æ</summary>
    public int number = 0;
    [SerializeField] float cd = 5f;
    float nextCreateTime = 0f;

    private void Update()
    {
        if (number < maxNumber && Time.time > nextCreateTime)
        {
            nextCreateTime = Time.time + cd;
            Create();
        }
    }

    [SerializeField] LayerMask floor = default;
    [SerializeField] GameObject foodObj = null;
    void Create()
    {
        Vector3 rayPos = new Vector3(Random.Range(0, Aye.AbsAdd(x, 1)), y, Random.Range(0, Aye.AbsAdd(z, 1)));

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + rayPos, -Vector3.up, out hit, Mathf.Abs(y)+1f, floor))
        {
            Instantiate(foodObj, hit.point, this.transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 c = this.transform.position + new Vector3(x * 0.5f, y * 0.5f, z * 0.5f);
        Gizmos.DrawWireCube(c, new Vector3(x, y, z));
    }
}
