using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SatpamBehavior {

	[System.Serializable]
	public class SatpamMovement {
	
		public string act;		// up, down, left, right, rotate
		public float distance;	// move distance
		public float angle;		// rotation angle
		public float delay;		// wait after act

		public SatpamMovement () {
		}
	
	}
}
