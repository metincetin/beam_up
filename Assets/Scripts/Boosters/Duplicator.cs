using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeamUp.Boosters
{
	[CreateAssetMenu(menuName = "Boosters/Duplicator")]
	public class Duplicator : Booster
	{
		public int DuplicationNumber;
		public float Angle;

		protected override void OnApplied(Lightbeam lightBeam)
		{
			for (int i = 0; i < DuplicationNumber; i++)
			{				
				var angleBetween = Angle / DuplicationNumber * 2;

				var curAngle = angleBetween * (i * 2) - (angleBetween * DuplicationNumber * 0.5f);
				Debug.Log(curAngle);

				var dir = lightBeam.Rigidbody.velocity.normalized;
				var newDir = Quaternion.Euler(0, 0, curAngle) * dir;

				lightBeam.Duplicate(newDir);
			}
		}

		protected override void OnReverted(Lightbeam lightBeam)
		{
		
		}
	}
}
