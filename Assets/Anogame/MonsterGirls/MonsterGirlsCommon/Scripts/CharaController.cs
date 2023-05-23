using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace anogamelib
{
    [RequireComponent(typeof(Animator))]
    public class CharaController : MonoBehaviour
    {
        private Animator m_animator;

        private int m_iCurrentTexutureIndex;
        [SerializeField]
        private Texture2D[] m_texture2DArr;

        [SerializeField]
        private OverrideSprite m_overrideSprite;

        [SerializeField]
        private Button m_btnPrev;
        [SerializeField]
        private Button m_btnNext;
        private void Start()
        {
            if (m_btnPrev != null)
            {
                m_btnPrev.onClick.AddListener(() => { OnChangeSkin(-1); });
            }
            if (m_btnNext != null)
            {
                m_btnNext.onClick.AddListener(() => { OnChangeSkin(1); });
            }
            if (m_overrideSprite == null)
            {
                m_overrideSprite = GetComponent<OverrideSprite>();
            }
            m_iCurrentTexutureIndex = 0;
            m_animator = GetComponent<Animator>();
            m_animator.SetFloat("x", 0f);
            m_animator.SetFloat("y", -1f);
            OnChangeSkin(0);
        }

        private void Update()
        {
            float fX = Input.GetAxisRaw("Horizontal");
            float fY = Input.GetAxisRaw("Vertical");

            if (fX != 0f || fY != 0f)
            {
                m_animator.SetFloat("x", fX);
                m_animator.SetFloat("y", fY);
            }
        }
        public void OnChangeSleepToggle(bool _isSleep)
        {
            m_animator.SetBool("sleep", _isSleep);
        }

        public void OnChangeSkin(int _iAdd)
        {
            m_iCurrentTexutureIndex += _iAdd;
            if (m_iCurrentTexutureIndex < 0)
            {
                m_iCurrentTexutureIndex = m_texture2DArr.Length - 1;
            }
            m_iCurrentTexutureIndex %= m_texture2DArr.Length;
            m_overrideSprite.overrideTexture = m_texture2DArr[m_iCurrentTexutureIndex];
        }


    }
}
