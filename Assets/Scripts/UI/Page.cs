using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp.UI
{
	public abstract class Page: MonoBehaviour
	{
		public virtual void Open()
		{
			gameObject.SetActive(true);
			OnOpened();
		}

		protected abstract void OnOpened();

		public virtual void Close()
		{
			gameObject.SetActive(false);
			OnClosed();
		}

		protected abstract void OnClosed();

		private void Awake()
		{
			if (gameObject.activeSelf)
			{
				OnOpened();
			}
		}
	}
}
