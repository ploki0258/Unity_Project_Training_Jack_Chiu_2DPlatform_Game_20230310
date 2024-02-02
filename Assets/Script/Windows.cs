using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Windows<T> : SingletonMonoBehaviour<T> where T : class
{
    [SerializeField] CanvasGroup mainUI = null;
    /// <summary>淡入淡出速度</summary>
    [HideInInspector] public float openSpeed = 5f;
    private void Reset()
    {
        mainUI = this.gameObject.GetComponent<CanvasGroup>();
    }
    string ogName = "";
    virtual protected void Start()
    {
        ogName = this.gameObject.name;
        Close();
        mainUI.alpha = 0f;
        mainUI.blocksRaycasts = false;
    }
    /// <summary>啟動介面</summary>
    virtual public void Open()
    {
        targetAlpha = 1f;
#if UNITY_EDITOR
        this.gameObject.name = ">>>>>>>" + ogName + "<<<<<<<";
#endif
    }
    /// <summary>關閉介面</summary>
    virtual public void Close()
    {
        targetAlpha = 0f;
        mainUI.blocksRaycasts = false;
#if UNITY_EDITOR
        this.gameObject.name = ogName;
#endif
    }
    float targetAlpha = 0f;
    public bool isOpen = false;
    virtual protected void Update()
    {
        mainUI.alpha = Mathf.Lerp(mainUI.alpha, targetAlpha, Time.deltaTime * openSpeed);
        if (mainUI.alpha > 0.9f && isOpen == false)
        {
            isOpen = true;
            mainUI.blocksRaycasts = true;
            OnOpen();
        }
        if (mainUI.alpha < 0.1f && isOpen == true)
        {
            isOpen = false;
            OnClose();
        }
    }
    /// <summary>充分完成開啟介面</summary>
    virtual public void OnOpen() { }
    /// <summary>充分完成關閉介面</summary>
    virtual public void OnClose() { }
}
