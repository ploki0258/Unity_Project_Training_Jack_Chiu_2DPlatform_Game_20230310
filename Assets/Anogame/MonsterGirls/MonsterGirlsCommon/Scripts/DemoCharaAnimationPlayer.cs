using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace anogamelib
{
	public class DemoCharaAnimationPlayer : MonoBehaviour
	{
		private int m_iAnimationIndex = 0;
		private readonly string[] AnimationName = new string[]
		{
			"attack",
			"orb",
			"win",
			"eat",
			"drink",
			"book",
			"magic",
			"damage",
		};

		[SerializeField]
		private Animator m_animatorChara;

		public void OnAnimationIndex(int _iIndex)
		{
			m_iAnimationIndex = _iIndex;
			//Debug.Log(AnimationName[_iIndex]);
		}

		public void PlayAnimation()
		{
			m_animatorChara.SetTrigger(AnimationName[m_iAnimationIndex]);
		}

	}
}

