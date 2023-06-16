using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("�ˮ`�q")]
    [SerializeField] float hurt = 1f;

    Collider2D tempTarget = null;   // �Ȧs2D�I����
    bool killPlayer = false;        // �O�_�����a�ͩR��

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tempTarget = collision;
        killPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tempTarget = null;
        killPlayer = false;
    }

    private void Update()
    {
        if (killPlayer == true && tempTarget != null)
        {
            if (tempTarget.gameObject.tag == "Player")
            {
                PlayerCtrl.instance.TakeDamage(hurt * Time.deltaTime);
            }
        }
    }
}
