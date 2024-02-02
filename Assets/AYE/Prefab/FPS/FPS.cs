using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYE
{
    public class FPS : MonoBehaviour
    {
        [SerializeField] Text fpsText = null;
        void LateUpdate()
        {
            fpsText.text = (1 / Time.deltaTime).ToString ("F0");
        }
    }
}

// 2020 by 阿葉