using UnityEngine;

// ��ҳ]�p�Ҧ� ���i���Ʀs�b ����a��ҥi�I�s
public class GameManager : MonoBehaviour
{
	private GameObject dieWindos = null;

	static public GameManager instance
	{
		// ���H�ϥΧڪ��ɭ�
		get
		{
			// �p�G�ڤ��s�b
			if (_instance == null)
			{
				// �N�ۧھ̪ūإ�
				_instance = new GameManager();
			}
			// �^�ǧڥi�����ϥ�
			return _instance;
		}
	}
	static GameManager _instance = null;

	private void Awake()
	{
		dieWindos = GameObject.Find("���`��������");
	}

	public void PlayerDead()
	{
		dieWindos.SetActive(true);  // �}�Ҧ��`�e��
	}
}
