using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �n�ݪ��v�r��ܾ�
/// </summary>
public class VeryGoodText : MonoBehaviour
{
    private void Reset()
    {
        accountSayText = this.gameObject.GetComponent<Text>();
    }
    public string text
    {
        get { return accountSayText.text; }
    }
    [SerializeField] Text accountSayText = null;
    string tempAccountSayText = "";
    [SerializeField] int maxCanAccountSayTextLine = 4;
    int nowCanAccountSayTextLine = 0;
    [SerializeField] float speed = 0.05f;
    [SerializeField] bool isOneLine = false;
    [SerializeField] string autoAttMe = "";
    private void Start()
    {
        if (autoAttMe != "")
            Add(autoAttMe);
    }
    /// <summary>�R���F�褧��K�[�@��</summary>
    public void CleanAdd(string v)
    {
        Clean();
        Add(v);
    }
    /// <summary>���R�F</summary>
    public void Clean()
    {
        nowCanAccountSayTextLine = 0;
        tempAccountSayText = "";
        accountSayText.text = "";
    }
    /// <summary>�K�[���e �@���@��</summary>
    public void Add(string v)
    {
        if (v.Contains("\n"))
        {
            // �峹�B�z
            string[] alls = v.Split('\n');
            for (int i = 0; i < alls.Length; i++)
            {
                if (alls[i] == "")
                    continue;
                if (isOneLine)
                    tempAccountSayText = tempAccountSayText + alls[i];
                else
                    tempAccountSayText = tempAccountSayText + alls[i] + "\n";
            }
        }
        else
        {
            // ���B�z
            if (isOneLine)
                tempAccountSayText = tempAccountSayText + v;
            else
                tempAccountSayText = tempAccountSayText + v + "\n";
        }
    }
    float nextUpdateAccountSayTextTime = 0f;
    bool jumpTempAccountSayText = false;
    [SerializeField] bool isRun = true;
    public void Stop()
    {
        isRun = false;
    }
    public void Play()
    {
        isRun = true;
    }
    /// <summary>�O�_�b�B�@</summary>
    public bool isMove
    {
        get { return tempAccountSayText.Length > 0; }
    }
    void Update()
    {
        if (!isRun)
            return;

        if (Time.time > nextUpdateAccountSayTextTime || jumpTempAccountSayText)
        {
            nextUpdateAccountSayTextTime = Time.time + speed;
            if (tempAccountSayText.Length > 0)
            {
                // �����h�����
                if (tempAccountSayText[0] == '\n')
                {
                    nowCanAccountSayTextLine++;
                    if (nowCanAccountSayTextLine > maxCanAccountSayTextLine)
                    {
                        string all = accountSayText.text;
                        string[] alls = all.Split('\n');
                        string done = "";
                        for (int i = 1; i < alls.Length; i++)
                        {
                            if (i < alls.Length - 1)
                                done = done + alls[i] + "\n";
                            else
                                done = done + alls[i];
                        }
                        accountSayText.text = done;
                        nowCanAccountSayTextLine--;
                    }
                }
                // �J��A���[�t
                if (tempAccountSayText[0] == '<')
                    jumpTempAccountSayText = true;
                if (tempAccountSayText[0] == '>')
                    jumpTempAccountSayText = false;
                // �K�[�r
                accountSayText.text = accountSayText.text + tempAccountSayText[0].ToString();
                tempAccountSayText = tempAccountSayText.Remove(0, 1);
            }
        }
        accountSayText.supportRichText = !isMove;
    }
}
