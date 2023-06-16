using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text description;
    [SerializeField] Text itemName;
    [SerializeField] int itemNumber;

    public void ��l��(int id)
    {
        Item �ӫ~��� = ItemManager.instance.FindItemData(id);

        icon.sprite = �ӫ~���.icon;
        description.text = �ӫ~���.info;
        itemName.text = �ӫ~���.title;
    }
}
