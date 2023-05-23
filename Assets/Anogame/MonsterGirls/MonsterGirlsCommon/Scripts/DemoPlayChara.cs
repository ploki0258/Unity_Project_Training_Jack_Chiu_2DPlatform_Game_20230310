using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace anogamelib
{

    public class DemoPlayChara : MonoBehaviour
    {
        public Animator m_animator;
        public string TriggerName;
        public bool IsSleep = false;

        void Update()
        {
            if (TriggerName != "")
            {
                m_animator.SetTrigger(TriggerName);
            }
            m_animator.SetBool("sleep", IsSleep);
        }

    }
}