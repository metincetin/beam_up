using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Utils
{
    public class BlackholeTriggerListener : MonoBehaviour
    {
		private List<Lightbeam> _pulledBeams = new List<Lightbeam>();
		public List<Lightbeam> PulledBeams => _pulledBeams;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var beam = collision.GetComponentInParent<Lightbeam>();
			if (beam)
			{
				_pulledBeams.Add(beam);
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			var beam = collision.GetComponentInParent<Lightbeam>();
			if (beam)
			{
				_pulledBeams.Remove(beam);
			}
		}
	}
}