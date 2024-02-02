using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour
{
    [SerializeField] bool logMe = false;
    public static MessageWindow instance = null;
    private void Awake()
    {
        instance = this;
        windowObj.SetActive(false);
        oneOption.SetActive(false);
        twoOption.SetActive(false);
    }

    [SerializeField] GameObject windowObj = null;
    [SerializeField] Text messageText = null;
    [SerializeField] GameObject oneOption = null;
    [SerializeField] GameObject twoOption = null;

    System.Action Act_OK = null;
    System.Action Act_NO = null;
    System.Action<bool> Act_Confirm = null;

    bool confirmMod = false;
    bool autoClose = true;

    /// <summary>單選不回覆</summary>
    public void OpenWindow(string message)
    {
        if (logMe)
            Debug.LogWarning(message);
        confirmMod = false;
        messageText.text = message;
        oneOption.SetActive(true);
        twoOption.SetActive(false);
        this.Act_OK = null;
        this.Act_Confirm = null;
        this.autoClose = true;
        windowObj.SetActive(true);
    }

    /// <summary>單選</summary>
    public void OpenWindow (string message, System.Action Act_OK, bool autoClose = true)
    {
        if (logMe)
            Debug.LogWarning(message);
        confirmMod = false;
        messageText.text = message;
        oneOption.SetActive(true);
        twoOption.SetActive(false);
        this.Act_OK = Act_OK;
        this.Act_NO = null;
        this.Act_Confirm = null;
        this.autoClose = autoClose;
        windowObj.SetActive(true);
    }
    /// <summary>複選</summary>
    public void OpenWindow(string message, System.Action<bool> Act_Confirm, bool autoClose = true)
    {
        if (logMe)
            Debug.LogWarning(message);
        confirmMod = true;
        messageText.text = message;
        oneOption.SetActive(false);
        twoOption.SetActive(true);
        this.Act_OK = null;
        this.Act_NO = null;
        this.Act_Confirm = Act_Confirm;
        this.autoClose = autoClose;
        windowObj.SetActive(true);
    }

    /// <summary>複選二分</summary>
    public void OpenWindow(string message, System.Action Act_Ok, System.Action Act_NO, bool autoClose = true)
    {
        if (logMe)
            Debug.LogWarning(message);
        confirmMod = false;
        messageText.text = message;
        oneOption.SetActive(false);
        twoOption.SetActive(true);
        this.Act_OK = Act_Ok;
        this.Act_NO = Act_NO;
        this.Act_Confirm = null;
        this.autoClose = autoClose;
        windowObj.SetActive(true);
    }

    public void ButtonOK ()
    {
        if (confirmMod)
        {
            if (Act_Confirm != null)
                Act_Confirm.Invoke(true);
        }
        else
        {
            if (Act_OK != null)
                Act_OK.Invoke();
        }
        if (autoClose)
            Close();
    }

    public void ButtonNO ()
    {
        if (confirmMod)
        {
            if (Act_Confirm != null)
                Act_Confirm.Invoke(false);
        }
        else
        {
            if (Act_NO != null)
                Act_NO.Invoke();
        }
        if (autoClose)
            Close();
    }

    /// <summary>手動關閉視窗</summary>
    public void Close ()
    {
        windowObj.SetActive(false);
    }
}

// 2020 by 阿葉