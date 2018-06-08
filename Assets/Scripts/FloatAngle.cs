using System;
using UnityEngine;

[System.Serializable]
	public class FloatAngle
	{
		public float x = 0.0f;
		public float y = 0.0f;

		public FloatAngle (float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	public void log(string preFix = "") {
		Debug.Log (preFix + x + ", " + y );
	}


}
