using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
	[SerializeField, Header("�ޯ�ϥ�")]
	Image iconSkill;
	[SerializeField, Header("�ޯ�W��")]
	Text titleSkill;
	[SerializeField, Header("�ޯ໡��")]
	Text infoSkill;
	[SerializeField, Header("�ޯ���s")]
	GameObject[] btnSkill;
	// [SerializeField, Header("���s��r")]
	// Text btnText;

	bool alreSkill;     // �O�_�w�ǲ߸ӧޯ�
	Skill skillData;    // �ޯ���
	Text btn1Text;
	Text btn2Text;

	private void Awake()
	{

		iconSkill.color = new Color(255, 255, 255, 50);
		iconSkill.transform.localScale = Vector3.zero;
		titleSkill.text = "";
		infoSkill.text = "";
		// infoSkill[1].text = "";
		btnSkill[0].SetActive(false);
		btnSkill[1].SetActive(false);
		btnSkill[2].SetActive(false);
	}

	private void Start()
	{
		// btn1Text = btnSkill[0].GetComponentInChildren<Text>();
		// btn2Text = btnSkill[1].GetComponentInChildren<Text>();
	}

	/// <summary>
	/// �I���ޯ�ϥܪ�l�Ƨޯ�G��ܸӧޯ઺��T
	/// </summary>
	/// <param name="id"></param>
	public void ��l�Ƨޯ�(int id)
	{
		// �d����
		skillData = SkillManager.instance.FindSkillByID(id);

		iconSkill.sprite = skillData.skillIcon;
		iconSkill.transform.localScale = (iconSkill.sprite == null) ? Vector3.zero : Vector3.one;
		titleSkill.text = skillData.skillName;
		infoSkill.text = skillData.skillDis + "\n" + "\n��O�����G" + skillData.skillCoinCost.ToString() + "\n��O�ޯ��I��" + skillData.skillPointCost;
		// infoSkill[1].text = "��O�����G" + skillData.skillCoinCost.ToString() + "\n��O�ޯ��I��" + skillData.skillPointCost;
		// �p�G���a�S���ӧޯ઺�� �~��ܫ��s
		alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
		btnSkill[0].SetActive(alreSkill == false);  // ��ܾǲ߫��s
		btnSkill[1].SetActive(alreSkill == true);   // ��ܤw�߱o���s
		// �p�G���a�S���ӧޯ઺��
		if (alreSkill == false)
		{
			// Debug.Log("�L���ޯ�");
			Skill currentlySkill = SkillManager.instance.FindSkillByID(id);
			// �ӧޯ঳�e�m�ޯ�
			if (currentlySkill.Pre_Skill == true)
			{
				// Debug.Log("�ӧޯ঳�e�m�ޯ�");
				// �|�����o�e�m�ޯ�
				if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == false)
				{
					// Debug.Log("�ݥ��߱o�e�@�ӧޯ�");
					btnSkill[0].SetActive(false);
					btnSkill[1].SetActive(false);
					btnSkill[2].SetActive(true);    // ��ܩ|��������s
				}
			}
		}

		// �p�G���a�S���ӧޯ� �B �S�����o�ӧޯ઺�e�m�ޯ� �h���"�|������"�r��
		// �p�G���a�S���ӧޯ� �B �����o�ӧޯ઺�e�m�ޯ� �� �p�G���a�S���ӧޯ� �B �ӧޯ઺�S���e�m�ޯ� �h���"�ǲ�"�r��
		// �p�G���a�w�g���o�ӧޯ� �h���"�w�߱o"�r��
		/*for (int i = 0; i < SkillManager.instance.AllSkillData.Length; i++)
		{
			// �p�G���a�S���ӧޯ�
			if (SaveManager.instance.playerData.IsHaveSkill(SkillManager.instance.AllSkillData[i].id) == false)
			{
				Skill currentlySkill = SkillManager.instance.FindSkillByID(SkillManager.instance.AllSkillData[i].id);
				// �ӧޯ঳�e�m�ޯ�
				if (currentlySkill.Pre_Skill == true)
				{
					// �|�����o�e�m�ޯ�
					if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == false)
					{
						btnSkill[1].SetActive(true);
						btnSkill[0].SetActive(false);
						btn2Text.text = "�|������";
					}
					// �w���o�e�m�ޯ�
					else if (SaveManager.instance.playerData.IsHaveSkill(currentlySkill.Pre_id) == true)
					{
						btnSkill[0].SetActive(true);
						btnSkill[1].SetActive(false);
						btn1Text.text = "�ǲ�";
					}
				}
				// �ӧޯ�S���e�m�ޯ�
				else if (currentlySkill.Pre_Skill == false)
				{
					btnSkill[0].SetActive(true);
					btnSkill[1].SetActive(false);
					btn1Text.text = "�ǲ�";
				}
			}
			// �p�G���a���ӧޯ�
			else if (SaveManager.instance.playerData.IsHaveSkill(SkillManager.instance.AllSkillData[i].id) == true)
			{
				btnSkill[1].SetActive(true);
				btnSkill[0].SetActive(false);
				btn2Text.text = "�w�߱o";
			}
		}
		*/
	}

	/// <summary>
	/// �ǲߧޯ�G���U�ǲ߫��s�A�i�ǲ߸ӧޯ�
	/// </summary>
	public void LearnSkill()
	{
		// �P�_��_�ǲߧޯ�
		if (SaveManager.instance.playerData.moneyCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
		{
			Debug.Log($"<color=#90ff06>�ɯŧޯ�G { + skillData.id + "\n�w�߱o�G" + skillData.skillName}</color>");
			// ����
			SaveManager.instance.playerData.moneyCount -= skillData.skillCoinCost;
			// ���ޯ�
			SaveManager.instance.playerData.skillPoint -= skillData.skillPointCost;
			// �쪱�a������ �K�[�ޯ�C��(ID)
			SaveManager.instance.playerData.haveSkill.Add(skillData.id);
			// �R����i��s��
			SaveManager.instance.SaveData();
			// ��ʨ�s�@���ޯ����T
			��l�Ƨޯ�(skillData.id);
		}
		else
		{
			Debug.Log("�������I�Ƥ���" + "\n�L�k�ǲߦ��ޯ�");
		}
	}
}
