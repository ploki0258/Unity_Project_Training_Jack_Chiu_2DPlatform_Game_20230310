using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
    private void Start()
    {
        FoodCreater.instance.number++;
    }
    private void OnDestroy()
    {
        FoodCreater.instance.number--;
    }
    [SerializeField] Animator anim = null;
    public void BeEat()
    {
        anim.SetTrigger("BeEat");
    }
}
