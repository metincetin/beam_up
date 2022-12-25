using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
	public class RenderingStateInformer : MonoBehaviour
	{
		private void OnBecameVisible()
		{
			var target = GetComponentInParent<IRenderingStateListener>();
			if ((Object)target)
				target.OnBeginRendering();
		}

		private void OnBecameInvisible()
		{

			var target = GetComponentInParent<IRenderingStateListener>();
			if ((Object)target)
				target.OnEndRendering();
		}
	}
}
