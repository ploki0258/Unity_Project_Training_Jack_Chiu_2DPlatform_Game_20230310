using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillField : MonoBehaviour
{
    [SerializeField, Header("技能圖示")]
    Image iconSkill;
    [SerializeField, Header("技能名稱")]
    Text titleSkill;
    [SerializeField, Header("技能說明")]
    Text[] infoSkill;
    [SerializeField, Header("技能按鈕")]
    GameObject btnSkill;
    
    bool alreSkill;     // 是否已學習該技能
    Skill skillData;    // 技能資料

    public void 初始化技能(int id)
	{
        // 查找資料
        skillData = SkillManager.instance.FindSkillByID(id);

        iconSkill.sprite = skillData.skillIcon;
        titleSkill.text = skillData.skillName;
        infoSkill[0].text = skillData.skillDis;
        infoSkill[1].text = skillData.skillCoinCost.ToString() + "\n" + skillData.skillPointCost;
        // 如果玩家沒有該技能的話 才顯示按鈕
        alreSkill = SaveManager.instance.playerData.IsHaveSkill(id);
        btnSkill.SetActive(alreSkill == false);
    }

    /// <summary>
    /// 學習技能
    /// </summary>
    public void LearnSkill()
	{
        // 判斷能否學習技能
        if (SaveManager.instance.playerData.moneyCount >= skillData.skillCoinCost && SaveManager.instance.playerData.skillPoint >= skillData.skillPointCost)
        {
            Debug.Log("升級技能： " + skillData.id);
            // 扣錢
            SaveManager.instance.playerData.moneyCount -= skillData.skillCoinCost;
            // 扣技能
            SaveManager.instance.playerData.skillPoint -= skillData.skillPointCost;
            // 到玩家紀錄中 添加商品資料(ID)
            SaveManager.instance.playerData.haveSkill.Add(skillData.id);
            // 買完後進行存檔
            SaveManager.instance.SaveData();

            // 手動刷新一次商品
            // 商城介面.ins.刷新商店();
        }
        else
        {
            Debug.Log("金幣或點數不足");
        }
    }
}
