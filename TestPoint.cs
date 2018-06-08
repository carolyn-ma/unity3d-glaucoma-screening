using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TestPointState {
	Untested,
	NeedsMoreTesting,
	DoneTestingCorrect
}

[System.Serializable] // can save what's in the class into the editor

public class TestPoint {

	public FloatAngle angles;
    public float radius;
    public TestPointState pointState = TestPointState.Untested;
	public List<float> lightIntensities;   //holds tested light intensities, store test light intensities for future use, list length equals times tested

    public TestPoint(float xRotation, float yRotation, float stimuliRadius, List<float> lightInts) {
        
        angles = new FloatAngle(xRotation, yRotation);
        radius = stimuliRadius;
        lightIntensities = lightInts;
   }

}


