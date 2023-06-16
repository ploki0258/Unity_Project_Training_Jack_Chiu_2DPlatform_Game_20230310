using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text description;
    [SerializeField] Text itemName;
    [SerializeField] int itemNumber;

    public void 初始化(int id)
    {
        Item 商品資料 = ItemManager.instance.FindItemData(id);

        icon.sprite = 商品資料.icon;
        description.text = 商品資料.info;
        itemName.text = 商品資料.title;
    }
}
