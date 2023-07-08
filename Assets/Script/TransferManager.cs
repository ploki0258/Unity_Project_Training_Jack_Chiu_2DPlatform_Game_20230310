using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
	[SerializeField, Header("�O�_�e���U�@��"), Tooltip("�O�_�n�e���U�@���d")]
	private bool isToNext = true;
	[SerializeField, Header("�n�e�������d�s��")]
	int indexLevel = 0;

	[Tooltip("�O�_�b�ϰ줺")]
	private bool inArea;

	private void Update()
	{
		TransferToLevel(indexLevel);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			inArea = false;
	}

	/// <summary>
	/// �ǰe���d�ܤU�@��
	/// </summary>
	/*
	void TransferNextLevel()
	{
		if (Input.GetKeyDown(KeyCode.O) && inArea)
		{
			// ���o��e���d�s��
			int indexLevel = SceneManager.GetActiveScene().buildIndex;
			// ���d�s�� �[�@
			indexLevel++;
			// ���J�U�@��
			SceneManager.LoadScene(indexLevel);
		}
	}
	*/

	/// <summary>
	/// �e�������d
	/// </summary>
	/// <param name="level">���d�s��</param>
	void TransferToLevel(int level)
	{
		if (Input.GetKeyDown(KeyCode.O) && inArea)
		{
			// ���o��e���d�s��
			indexLevel = SceneManager.GetActiveScene().buildIndex;
			// �p�G ���o�����d�s�� ���� 0 �h���d�s�� �[1
			if (indexLevel != 0)
			{
				indexLevel++;
			}
			// �p�G �O�n�e���U�@�� �N���J�U�@��
			// �_�h �N���J�n�e�������d
			if (isToNext)
			{
				SceneManager.LoadScene(indexLevel);
			}
			else
			{
				SceneManager.LoadScene(level);
			}
		}
	}
}
