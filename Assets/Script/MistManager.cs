using UnityEngine;
using UnityEngine.UI;

public class MistManager : MonoBehaviour
{
	[SerializeField, Header("迷霧圖片")]
	SpriteRenderer mist = null;
	[SerializeField, Header("傷害量")]
	float damage = 0f;
	[SerializeField, Header("CD加重倍數")]
	float cdWeight = 0f;
	[Header("迷霧種類")]
	[SerializeField] bool mistType_cyanogen;
	[SerializeField] bool mistType_blue;
	[SerializeField] bool mistType_purple;
	[SerializeField] bool mistType_red;
	[SerializeField] bool mistType_gree;

	public bool inMist = false;

	public static MistManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && mistType_red == true)
		{
			// inMist = true;
			mist.color = Color.Lerp(Color.white, new Color(99, 00, 00), 100f * Time.deltaTime);
			// 減少HP
			SaveManager.instance.playerData.playerHP -= damage * Time.unscaledDeltaTime;
		}

		if (collision.gameObject.CompareTag("Player") && mistType_blue == true)
		{
			// inMist = true;
			mist.color = Color.Lerp(Color.white, new Color(00, 00, 99), 100f * Time.deltaTime);
			// 加重技能消耗費用
			PlayerCtrl.instance.costMP *= instance.cdWeight;
		}

		if (collision.gameObject.CompareTag("Player") && mistType_gree == true)
		{
			inMist = true;
			mist.color = Color.Lerp(Color.white, new Color(00, 99, 00), 100f * Time.deltaTime);
		}

		if (collision.gameObject.CompareTag("Player") && mistType_purple == true)
		{
			// inMist = true;
			mist.color = Color.Lerp(Color.white, new Color(60, 00, 90), 100f * Time.deltaTime);
		}

		if (collision.gameObject.CompareTag("Player") && mistType_cyanogen == true)
		{
			// inMist = true;
			mist.color = Color.Lerp(Color.white, new Color(00, 10, 90), 100f * Time.deltaTime);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			inMist = false;
		}
	}
}
